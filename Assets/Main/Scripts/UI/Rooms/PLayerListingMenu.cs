using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlayerListing _playerListing;

    [SerializeField]
    private TMP_Text _readyUpText;

    private List<PlayerListing> _listings = new List<PlayerListing>();
    private RoomsCanvases _roomsCanvases;
    private bool _ready = false;

    public Button startGameButton;
    public Button readyUpButton;

    private void Awake()
    {
        // GetCurrentRoomPlayers();
        startGameButton.onClick.AddListener(OnClick_StartGame);
        readyUpButton.onClick.AddListener(OnClick_ReadyUp);
    }
    
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }
    
    public override void OnEnable()
    {
        base.OnEnable();
        
        GetCurrentRoomPlayers();

        SetReadyUp(false);

        SetClientInteractivity();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < _listings.Count; i++)
            Destroy(_listings[i].gameObject);

        _listings.Clear();
    }

    /*
    public override void OnLeftRoom()
    {
        // Destroy player listings when player leaves the room
        _content.DestroyChildren();
    }
    */

    private void SetReadyUp(bool state)
    {
        _ready = state;

        if(_ready)
        {
            _readyUpText.text = "Ready";
        }
        else
        {
            _readyUpText.text = "Not Ready";
        }
    }


    /// <summary>
    /// creating a list of all the players currently in room
    /// </summary>
    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        
        // iterating through the Dictionary<int, Player> to add them to the _playerListing list
        foreach (KeyValuePair<int, Player> playerinfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerinfo.Value);
        }
    }

    /// <summary>
    /// Adding palyer to the local player list(_playerListting)
    /// </summary>
    /// <param name="player"></param>
    private void AddPlayerListing(Player player)
    {
        int index = _listings.FindIndex(x => x.Player == player);

        if(index != -1)
        {
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            // instantiating list item in scroll view as a child of content
            PlayerListing listing = Instantiate(_playerListing, _content);

            if (listing != null)
            {
                // setting the values of list item after instantiating it
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }

    }

    // PUN callback method for player entering room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined room!");

        // adding player to the list
        AddPlayerListing(newPlayer);
    }

    // PUN callback method
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " left room!");

        // locate the index for the room in the list through name which has been deleted
        int index = _listings.FindIndex(x => x.Player == otherPlayer);

        // if we find the room item being removed in the local list then destroy its gameObject in the UI
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // everyone leaves when master client leaves the room
        _roomsCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    // On click listener for Start-game button
    public void OnClick_StartGame()
    {
        // client who creates the room is Master client and this snippet is specific to master client only
        if(PhotonNetwork.IsMasterClient)
        {
            // traverse the entire player list
            for(int i=0; i<_listings.Count; i++)
            {
                // check if current player is not equal to local player(master client)
                if(_listings[i].Player != PhotonNetwork.LocalPlayer)
                {
                    // check if current player is ready or not
                    if (!_listings[i].Ready)
                    {
                        Debug.Log(_listings[i].Player.NickName.ToString() + " is not ready!");
                        return;
                    }
                }
            }

            // prevents the users from seeing and joining the room so the next scene can be loaded 
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            // loading scene through scene index
            PhotonNetwork.LoadLevel(1);
        }
    }

    // On Click listener for Ready-Up button 
    public void OnClick_ReadyUp()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            // opposite of ready on clicking it
            SetReadyUp(!_ready);

            // RPC function call with LocalPlayer and _ready as parameters,
            // this RPC method targets the MasterClient from all the remote clients
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);

        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        // locate the index for the room in the list through name which has been deleted
        int index = _listings.FindIndex(x => x.Player == player);

        // if we find the room item being removed in the local list then destroy its gameObject in the UI
        if (index != -1)
        {
            _listings[index].Ready = ready;
        }
    }

    // changing the UI based on if player client is master client or not
    private void SetClientInteractivity()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            // action specific to MasterClient only
            
            // disabling interactivity of ready up button for local player(Master Client)
            readyUpButton.interactable = false;
        }
        else
        {
            // action specific to remote clients only

            // disabling interactivity of start game button for local player(non Master Client)
            startGameButton.interactable = false;
        }
        
        // changing the Host player(Master Client) listing background color to distinguish
        int index = _listings.FindIndex(x => x.Player.UserId == PhotonNetwork.MasterClient.UserId);
        if (index != -1)
        {
            _listings[index].gameObject.GetComponent<RawImage>().color = Color.cyan;
        }
    }
}
