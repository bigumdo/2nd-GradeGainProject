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
        _player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
        _player.reader.OnJumpEvent += HandleJump;
    }

    private void HandleJump()
    {
        seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transform.position + Vector3.up, 1));
    }

    private void FixedUpdate()
    {

        Vector3 vecY = transform.right * _player.reader.Movement.x;
        Vector3 vecX = transform.forward * _player.reader.Movement.z;

        Vector3 _velocity = vecX + vecY;

        rigid.velocity = _velocity * speed;

    }

    private void OnDestroy()
    {
        _player.reader.OnJumpEvent -= HandleJump;
    }

}
