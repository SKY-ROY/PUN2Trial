using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnect : MonoBehaviourPunCallbacks
{
    public string gameVersion = "0.0.1";

    // Start is called before the first frame update
    void Start()
    {
        print("Connecting to server!");

        PhotonNetwork.SendRate = 20; // default: 20
        PhotonNetwork.SerializationRate = 10; // default: 10

        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion; 
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server!");
        print(PhotonNetwork.LocalPlayer.NickName);

        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("Disconnected from server for reason: " + cause.ToString());
    }
}
