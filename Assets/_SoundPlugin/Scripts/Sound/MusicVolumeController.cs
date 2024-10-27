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

        private Coroutine transitionCoroutine;

        void Start()
        {
            // Start with volume at 0 and then fade in
            audioSource.volume = 0;
        }

        public void StartMusic(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            StartCoroutine(FadeVolume(audioSource, startTargetVolume, fadeDuration));
        }

        public void SwitchMusic(AudioClip audioClip)
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
                transitionCoroutine = null;
            }

            transitionCoroutine = StartCoroutine(TransitionAudio(audioSource, audioClip, fadeDuration));
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

        IEnumerator TransitionAudio(AudioSource audioSource, AudioClip targetClip, float duration)
        {
            //Old Fade out
            float startVolume = startTargetVolume;
            float targetVolume = 0;
            float elapsed = 0f;
            float time = duration * 0.5f;

            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / time);

                yield return null;
            }

            //New fade In
            audioSource.clip = targetClip;
            audioSource.Play();
            targetVolume = startTargetVolume;
            startVolume = 0;
            elapsed = 0f;

            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / time);

                yield return null;
            }

            audioSource.volume = targetVolume;
        }
    }
}