using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    /// <summary>
    /// for reference of CreateRoomMenu so we can assign it during FirstInitialize()
    /// </summary>
    [SerializeField]
    private CreateRoomMenu _createRoomMenu;

    private RoomsCanvases _roomsCanvases;

    /// <summary>
    /// This function acts like a contructor to initialize all the member variables
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
        _createRoomMenu.FirstInitialize(canvases);
    }
}
