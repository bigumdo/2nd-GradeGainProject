using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{

    private AudioSource _playerAudio;

    private void Awake()
    {
        _playerAudio = GetComponentInParent<AudioSource>();
    }

    public void HammerSound()
    {
        _playerAudio.Play();
    }

}
