using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnershipTransfer : MonoBehaviourPun, IPunOwnershipCallbacks
{
    private void OnEnable()
    {
        // registers IPunOwnershipCallbacks internally
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    // these methods will be called on any object which utilizes the IPunOwnershipCallbacks
    // this method is called the Ownership transfer request is received from requesting player
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        // making sure that the request received is for this object only
        if (targetView != base.photonView)
            return;

        // Add ownership condition checks here

        base.photonView.TransferOwnership(requestingPlayer);
    }

    // this method is called when ownership gets transferred
    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        // making sure that the request received is for this object only
        if (targetView != base.photonView)
            return;

        Debug.Log("Ownership transferred from " + previousOwner + " to " + base.photonView.Owner);
    }

    private void OnMouseDown()
    {
        base.photonView.RequestOwnership();
    }
}
