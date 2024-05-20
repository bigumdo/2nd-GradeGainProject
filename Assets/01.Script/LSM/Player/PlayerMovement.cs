using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Rigidbody rigid;

    [SerializeField] float jumpPower;
    [SerializeField] float speed;
    [SerializeField] Transform groundCheckerTrm;
    [SerializeField] Vector3 groundCheckerSize;
    [SerializeField] LayerMask goroundMask;

    private bool isDoubleJump;
    private bool isGorund;

    //Sequence seq;
    private void Awake()
    {
        _player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
        _player.reader.OnJumpEvent += HandleJump;
    }

    private void FixedUpdate()
    {
        Movement();
        GroundCheck();
    }

    private void Movement()
    {
        Vector3 vecZ = transform.right * _player.reader.Movement.x;
        Vector3 vecX = transform.forward * _player.reader.Movement.z;

        Vector3 _velocity = (vecX + vecZ) * speed;
        rigid.velocity = new Vector3(_velocity.x, rigid.velocity.y, _velocity.z);
    }


    public void GroundCheck()
    {
        Collider[] collider;
        collider =Physics.OverlapBox(groundCheckerTrm.position, groundCheckerSize, Quaternion.identity, goroundMask);
        if (collider.Length > 0)
        {
            isDoubleJump = true;
            isGorund = true;
        }
        else
        {
            isGorund = false;
        }
    }

    private void HandleJump()
    {
        if(isGorund)
        {
            StopImmediately(true);
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
        else if(isDoubleJump)
        {
            isDoubleJump = false;
            StopImmediately(true);
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
        
    }

    public void StopImmediately(bool check)
    {
        if (check)
            rigid.velocity = Vector3.zero;
        else
            rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
    }

    private void OnDestroy()
    {
        _player.reader.OnJumpEvent -= HandleJump;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheckerTrm.position, groundCheckerSize);
    }

}
