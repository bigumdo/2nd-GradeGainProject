using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{

    public SoundSO sound;
    public Dictionary<string, AudioClip> getAudio;

    private void Awake()
    {
        getAudio = new Dictionary<string, AudioClip>();
        for (int i = 0; i < sound.soundArr.Length; ++i)
        {
            getAudio.Add(sound.soundArr[i].clipName, sound.soundArr[i].soundClip);
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(getAudio["HammerClik"]);
        }
    }

}
