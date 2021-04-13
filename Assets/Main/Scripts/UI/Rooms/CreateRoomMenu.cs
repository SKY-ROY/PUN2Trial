using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text _roomName;

    public Button createRoom_button;

    private void Start()
    {
        // binding listener to createRoom button
        createRoom_button.onClick.AddListener(onClick_CreateRoom);
    }

    public void onClick_CreateRoom()
    {
        // connection check
        if (!PhotonNetwork.IsConnected)
            return;

        /* Create or join Room */

        // defining options for the room 
        RoomOptions options = new RoomOptions();
        
        // player limit
        options.MaxPlayers = 4;

        string name;
        if(_roomName.text.Equals(null))
        {
            name = "basic";
        }
        else
        {
            name = _roomName.text;
        }

        PhotonNetwork.JoinOrCreateRoom(name, options, TypedLobby.Default);    
    }

    // callback method for successful room creation
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully");
    }

    // callback method for failed room creation
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room creation failed: " + message);
    }
}
