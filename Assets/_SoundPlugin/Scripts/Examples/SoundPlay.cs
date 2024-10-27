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

        protected SoundSource inUseSoundSource;

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

        public virtual void Play()
        {
        }

        public virtual void Play(AudioClip clip)
        {
           
        }

        public void Stop()
        {
            if (inUseSoundSource) inUseSoundSource.StopAudio();
        }
    }
}