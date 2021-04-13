using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    public void Show()
    {
        // enabling the current room Canvas
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        // disabling the current room Canvas
        gameObject.SetActive(false);
    }
}
