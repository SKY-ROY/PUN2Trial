using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    /// <summary>
    /// for reference of sub-canvas "CreateOrJoinRoomCanvas"
    /// </summary>
    [SerializeField]
    private CreateOrJoinRoomCanvas _createOrJoinRoomCanvas;
    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas { get { return _createOrJoinRoomCanvas; } }

    /// <summary>
    /// for reference of sub-canvas "CurrentRoomCanvas"
    /// </summary>
    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return _currentRoomCanvas; } }

    private void Awake()
    {
        FirstInitialize();        
    }

    /// <summary>
    /// Initializing the RoomCanvases(parent) canvases in all the sub-canvases for reference
    /// </summary>
    private void FirstInitialize()
    {
        CurrentRoomCanvas.FirstInitialize(this);
        CreateOrJoinRoomCanvas.FirstInitialize(this);
    }
}
