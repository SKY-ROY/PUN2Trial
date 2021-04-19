using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDataType : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private MyCustomSerialization _customSerialization = new MyCustomSerialization();
    [SerializeField]
    private bool _sendAsType = true;

    void Start()
    {
        // Registering the custom type for exchange over network
        PhotonPeer.RegisterType(typeof(MyCustomSerialization), (byte)'M', MyCustomSerialization.Serialize, MyCustomSerialization.DeSerialize);        
    }

    void Update()
    {
        if(_customSerialization.MyNumber != -1)
        {
            // the custom type will be sent only when "Enter" is pressed
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SendCustomSerialization(_customSerialization, _sendAsType);
                _customSerialization.MyNumber = -1;
                _customSerialization.MyString = string.Empty;
            }
        }
    }

    private void SendCustomSerialization(MyCustomSerialization data, bool typed)
    {
        if (!typed)
            base.photonView.RPC("RPC_ReceiveMyCustomSerialization", RpcTarget.AllViaServer, MyCustomSerialization.Serialize(_customSerialization));
        else
            base.photonView.RPC("RPC_TypeReceiveMyCustomSerialization", RpcTarget.AllViaServer, _customSerialization);
    }

    [PunRPC]
    private void RPC_ReceiveMyCustomSerialization(byte[] datas)
    {
        MyCustomSerialization result = (MyCustomSerialization)MyCustomSerialization.DeSerialize(datas);
        print("Recieved byte array: " + result.MyNumber + ", " + result.MyString);
    }

    [PunRPC]
    private void RPC_TypeReceiveMyCustomSerialization(MyCustomSerialization datas)
    {
        print("Recieved type: " + datas.MyNumber + ", " + datas.MyString);
    }
}
