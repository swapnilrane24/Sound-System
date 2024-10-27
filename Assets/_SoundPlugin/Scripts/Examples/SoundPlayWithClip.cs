using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class SoundPlayWithClip : SoundPlay
    {
        [Range(0f, 1f)]
        [SerializeField] private float volumn = 1;
        [Range(0, 100)]
        [SerializeField] private float randomPitchPercent = 0;
        [SerializeField] private AudioClip _clip;

        public override void Play()
        {
            if (SoundManager.Instance != null)
            {
                if (_clip)
                {
                    SoundManager.Instance.Play(_clip, volumn, randomPitchPercent, out inUseSoundSource);
                }
            }
        }

        public override void Play(AudioClip clip)
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.Play(clip, volumn, randomPitchPercent, out inUseSoundSource);
        }
    }
}