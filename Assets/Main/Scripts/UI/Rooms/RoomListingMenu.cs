using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private RoomListing _roomListing;

    private List<RoomListing> _listings = new List<RoomListing>();      // to store all the existing rooms
    private RoomsCanvases _roomsCanvases;                               // for reference of RoomsCanvases

    /// <summary>
    /// for reference of the RoomCanvases(grandparent) 
    /// </summary>
    /// <param name="canvases"></param>
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    // PUN callback method called when player joins the room
    public override void OnJoinedRoom()
    {
        _roomsCanvases.CurrentRoomCanvas.Show();   
    }

    // PUN callback method called on room list update(addition/removal of rooms) 
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if(info.RemovedFromList)
            {
                // Removed from Rooms List

                // locate the index for the room in the list through name which has been deleted
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);

                // if we find the room item being removed in the local list then destroy its gameObject in the UI
                if(index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                // Added to Rooms List

                // instantiating list item in scroll view as a child of content
                RoomListing listing = Instantiate(_roomListing, _content);
                
                if(listing != null)
                {
                    // setting the values of list item after instantiating it
                    listing.SetRoomInfo(info);
                    _listings.Add(listing);
                }
            }
        }
    }
}
