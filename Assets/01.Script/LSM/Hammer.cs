using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SuccessEnum
{
    GreatSuccess,
    NormalSuccess,
    Fail
}

public class Hammer : MonoBehaviour
{

    public Player _player;

    private void Start()
    {
        _player.audioSource.clip = SoundManager.Instance.getAudio["HammerClik"];
    }

    public void HammerStarCatch()
    {
        _player.animator.SetTrigger("Reset");
        _player.animator.SetTrigger("Click");
    }

}
