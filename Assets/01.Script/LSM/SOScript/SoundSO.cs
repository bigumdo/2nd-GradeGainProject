using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Sound")]
public class SoundSO : ScriptableObject
{
    [Serializable]
    public struct sound
    {
        public string clipName;
        public AudioClip soundClip;
    }

    public sound [] soundArr;

}
