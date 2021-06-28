using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    private Button joinRoomButton;
    
    [SerializeField]
    private TMP_Text _text;

    public RoomInfo RoomInfo { get; private set; }

    private void Awake()
    {
        joinRoomButton = GetComponent<Button>();
        joinRoomButton.onClick.AddListener(OnClick_Button);
    }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name + " (" + roomInfo.MaxPlayers + ")";
    }

    public void OnClick_Button()
    {
        // when we join an existing remote room
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
