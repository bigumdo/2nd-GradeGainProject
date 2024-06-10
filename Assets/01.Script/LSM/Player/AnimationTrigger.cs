using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private AudioSource _playerAudio;
    public Transform _flameTrm;


    private void Awake()
    {
        _playerAudio = GetComponentInParent<AudioSource>();
        _flameTrm = transform.Find("FlameParticlePos");

    }

    public void HammerSound()
    {
        _playerAudio.Play();
        GreatEffect _gEffect;
        FailEffect _fEffect;
        switch (GameManager.Instance.currentSuccessEnum)
        {

            case SuccessEnum.GreatSuccess:
                _gEffect = PoolingManager.Instance.Pop(ObjectPooling.PoolingType.GreatEffect) as GreatEffect;
                _gEffect.transform.position = _flameTrm.position;
                _gEffect.Disable();
                break;
            case SuccessEnum.NormalSuccess:
                _gEffect = PoolingManager.Instance.Pop(ObjectPooling.PoolingType.GreatEffect) as GreatEffect;
                _gEffect.transform.position = _flameTrm.position;
                _gEffect.Disable();
                break;
            case SuccessEnum.Fail:
                _fEffect = PoolingManager.Instance.Pop(ObjectPooling.PoolingType.FailEffect) as FailEffect;
                _fEffect.transform.position = _flameTrm.position;
                _fEffect.Disable();
                break;
        }
        //GameManager.Instance.player.animator.SetTrigger("Reset");
    }

}
