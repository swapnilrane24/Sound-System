using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class SoundPlay : MonoBehaviour
    {
        [SerializeField] private bool onlyPlayInGamePlayMode;
        [SerializeField] private bool playOnActive;
        [SerializeField] private bool playOnDeactive;
        [Range(0f, 1f)]
        [SerializeField] private float volumn = 1;
        [Range(0,100)]
        [SerializeField] private float randomPitchPercent = 0;
        [SerializeField] private AudioClip _clip;

        private SoundSource inUseSoundSource;

        private void OnEnable()
        {
            if (playOnActive)
                Play();
        }

        private void OnDisable()
        {
            if (playOnDeactive)
                Play();
        }

        public void Play()
        {
            //if (onlyPlayInGamePlayMode && GameManager.Instance.GameState != GameState.PLAYING) return;
            if (SoundManager.Instance != null)
            {
                if (_clip)
                {
                    SoundManager.Instance.Play(_clip, volumn, randomPitchPercent, out inUseSoundSource);
                }
            }
        }

        public void Play(AudioClip clip)
        {
            //if (onlyPlayInGamePlayMode && GameManager.Instance.GameState != GameState.PLAYING) return;

            //_clip = clip;
            if (SoundManager.Instance != null)
                SoundManager.Instance.Play(clip, volumn, randomPitchPercent, out inUseSoundSource);
        }

        public void Stop()
        {
            if (inUseSoundSource) inUseSoundSource.StopAudio();
        }
    }
}