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

        var flame = PoolingManager.Instance.Pop(ObjectPooling.PoolingType.Flame) as FlameEffect;
        flame.transform.position = _flameTrm.position;
        flame.Disable();
    }

}
