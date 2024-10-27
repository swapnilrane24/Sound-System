using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Devshifu/Audio Data")]
    public class AudioDataSO : ScriptableObject
    {
        public AudioClip audioClip;
        public float volumn = 1;
        public float pitch = 1;
        [Range(-3f, 3f)]
        public float randomPitchPercent = 0;
        public AudioMixerGroup audioMixerGroup;



    }
}