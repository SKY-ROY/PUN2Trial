using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlayerListing _playerListing;

    private List<PlayerListing> _listings = new List<PlayerListing>();
    private RoomsCanvases _roomsCanvases;

    public Button startGameButton;
    
    private void Awake()
    {
        // GetCurrentRoomPlayers();
        startGameButton.onClick.AddListener(OnClick_StartGame);
    }
    
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }
    
    public override void OnEnable()
    {
        base.OnEnable();
        
        GetCurrentRoomPlayers();
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

    public void OnClick_StartGame()
    {
        // client who creates the room is Master client
        if(PhotonNetwork.IsMasterClient)
        {
            // prevents the users from seeing and joining the room so the next scene can be loaded 
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            // loading scene through scene index
            PhotonNetwork.LoadLevel(1);
        }

    }
}
