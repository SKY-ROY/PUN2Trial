using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectMover : MonoBehaviourPun
{
    [SerializeField]
    private float _moveSpeed = 1f;

    private Animator _animator;

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
