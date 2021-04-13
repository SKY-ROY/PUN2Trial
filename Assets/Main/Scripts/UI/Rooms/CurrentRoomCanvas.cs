using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private PlayerListingMenu _playerListingMenu;
    [SerializeField]
    private LeaveRoomMenu _leaveRoomMenu;

    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
        _playerListingMenu.FirstInitialize(canvases);
        _leaveRoomMenu.Firstinitialize(canvases);
    }

    public void Show()
    {
        // enabling the current room Canvas
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        // disabling the current room Canvas
        gameObject.SetActive(false);
    }
}
