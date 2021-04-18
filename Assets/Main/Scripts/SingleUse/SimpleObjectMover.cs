using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectMover : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private float _moveSpeed = 1f;

    private Animator _animator;

    // IPunObservable interface implementation enables PhotonView to obeserve a component
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
        // normally we would use PhotonTransformView or PhotonTransformViewClassic which will synchronize automatically
        // but for undestanding the OnPhotonSerializeView we would do it manuallly here

        if(stream.IsWriting)
        {
            // we are the owner and sending the data

            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if(stream.IsReading)
        {
            // remote client player(not owner) receiving the data

            // casting is required as tyoe is not specified when parsing the bytes from stream
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
        */
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
    }

    private void Start()
    {

    }

    void Update()
    {
        // to move the player's gameobject (using photonView.IsMine)
        if(base.photonView.IsMine)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            transform.position += (new Vector3(x, y, 0f) * _moveSpeed);
            
            // toggling the boolean for animation
            UpdateMovingBoolean((x != 0f || y != 0f));
        }
    }

    private void UpdateMovingBoolean(bool moving)
    {
        _animator.SetBool("moving", moving);
    }
}
