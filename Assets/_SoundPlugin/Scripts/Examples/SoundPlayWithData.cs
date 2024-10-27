using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class SoundPlayWithData : SoundPlay
    {

        [SerializeField] private AudioDataSO audioData;

        public override void Play()
        {
            if (SoundManager.Instance != null)
            {
                if (audioData)
                {
                    SoundManager.Instance.Play(audioData, out inUseSoundSource);
                }
            }
        }

        public override void Play(AudioClip clip)
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.Play(audioData, out inUseSoundSource);
        }

    }
}