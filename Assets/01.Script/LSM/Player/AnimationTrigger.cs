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
        GreatEffect _gEffect = PoolingManager.Instance.Pop(ObjectPooling.PoolingType.GreatEffect) as GreatEffect;
        FailEffect _fEffect = PoolingManager.Instance.Pop(ObjectPooling.PoolingType.FailEffect) as FailEffect;
        switch (GameManager.Instance.currentSuccessEnum)
        {

            case SuccessEnum.GreatSuccess:
                _gEffect.transform.position = _flameTrm.position;
                _gEffect.Disable();
                Debug.Log(1);
                break;
            case SuccessEnum.NormalSuccess:
                _gEffect.transform.position = _flameTrm.position;
                _gEffect.Disable();
                break;
            case SuccessEnum.Fail:
                _fEffect.transform.position = _flameTrm.position;
                _fEffect.Disable();
                break;
        }
    }

}
