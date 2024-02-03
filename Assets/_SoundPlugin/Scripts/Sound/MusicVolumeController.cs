using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class MusicVolumeController : MonoBehaviour
    {
        public AudioSource audioSource;
        public float fadeDuration = 2f; // Duration of fade in/out
        public float startTargetVolume = 1f;

        void Start()
        {
            // Start with volume at 0 and then fade in
            audioSource.volume = 0;
        }

        //public void StartMusic()
        //{
        //    StartCoroutine(FadeVolume(audioSource, targetVolume: 1f, fadeDuration));
        //}

        public void StartMusic(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            StartCoroutine(FadeVolume(audioSource, startTargetVolume, fadeDuration));
        }

        public void EndMusic()
        {
            StartCoroutine(FadeVolume(audioSource, 0f, fadeDuration));
        }

        IEnumerator FadeVolume(AudioSource audioSource, float targetVolume, float duration)
        {
            float startVolume = audioSource.volume;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
                yield return null;
            }

            audioSource.volume = targetVolume;
        }
    }
}