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
        PhotonNetwork.GameVersion = gameVersion; 
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("Disconnected from server for reason: " + cause.ToString());
    }
}
