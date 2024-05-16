using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Rigidbody rigid;
    [SerializeField] float jumpPower;
    [SerializeField] float speed;

    private float time;
    Sequence seq;
    private void Awake()
    {
        seq  = DOTween.Sequence();
        _player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
        _player.reader.OnJumpEvent += HandleJump;

    }

    private void HandleJump()
    {
        //seq.Append(transform.DOMove(Vector3.up, 1))
        //    .Insert(2, transform.DOMove(Vector3.down, 1));
        //rigid.AddForce(Vector3.up * jumpPower, ForceMode.Acceleration);
    }

    private void FixedUpdate()
    {

        Vector3 vecY = transform.right * _player.reader.Movement.x;
        Vector3 vecX = transform.forward * _player.reader.Movement.z;

        Vector3 _velocity = vecX + vecY;

        rigid.velocity = _velocity;

    }

    private void OnDestroy()
    {
        _player.reader.OnJumpEvent -= HandleJump;
    }

}
