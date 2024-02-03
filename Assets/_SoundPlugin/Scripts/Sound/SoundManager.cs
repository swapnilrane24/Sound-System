using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    [System.Serializable]
    public class Sound
    {
        [SerializeField] private AudioClip clip;
        [Range(0f, 1f)][SerializeField] private float volume = 0.5f;
        [Range(0.1f, 3.0f)][SerializeField] private float pitch = 1f;
        [Range(0,100)]
        [SerializeField] private float randomPitchPercent = 10f;

        public AudioClip Clip { get => clip; }
        public float Volume { get => volume; }
        public float Pitch { get => pitch; }
        public float RandomPitchPercent => randomPitchPercent;
    }

    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;

        [SerializeField] private AudioClip musicClip;
        [SerializeField] private SoundSource soundSourcePrefab;
        [SerializeField] private MusicVolumeController musicVolumeController;
        private List<SoundSource> deactiveSoundSourceList;

        public static SoundManager Instance { get => instance; }

        private bool soundOn;
        public bool IsSoundOn => soundOn;

        private void Awake()
        {
            AudioListener.volume = 0;
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            soundOn = PlayerPrefs.GetInt("GameSound", 1) == 1 ? true : false;//ES3.Load<bool>("GameSound", true);
        }

        private void Start()
        {
            
            deactiveSoundSourceList = new List<SoundSource>();

            for (int i = 0; i < 5; i++)
            {
                SoundSource soundSource = SpawnSoundSource();
                deactiveSoundSourceList.Add(soundSource);
            }



            AudioListener.volume = soundOn == true ? 1 : 0;

            //SoundOn(true, true);

            musicVolumeController.StartMusic(musicClip);
        }

        public void RecheckSoundAfterAd()
        {
            AudioListener.volume = soundOn == true ? 1 : 0;
        }

        //public void StartSoundIfPossible()
        //{
        //    if (soundOn)
        //        AudioListener.volume = 1;
        //}

        public void ActivateAudio()
        {
            soundOn = true;
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("GameSound", 1);
            //ES3.Save<bool>("GameSound", soundOn);
        }

        public void DeactivateAudio()
        {
            soundOn = false;
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("GameSound", 0);

            //ES3.Save<bool>("GameSound", soundOn);
        }

        public void Play(AudioClip clip, float volumn, float RandomPitchPercent, out SoundSource soundSource)
        {
            SoundSource selectedAudioSource = GetSoundSource();
            soundSource = selectedAudioSource;
            float pitch = 1f;

            if (RandomPitchPercent > 2)
            {
                pitch *= 1 + Random.Range(-RandomPitchPercent / 100,
                    RandomPitchPercent / 100);
            }

            selectedAudioSource.PlayAudio(clip, volumn, pitch);
        }

        public void Play(AudioClip clip, float volumn, float spatialBlend)
        {
            SoundSource selectedAudioSource = GetSoundSource();
            float pitch = 1f;

            selectedAudioSource.PlayAudio(clip, volumn, pitch, spatialBlend);
        }

        private SoundSource SpawnSoundSource()
        {
            SoundSource soundSource = Instantiate(soundSourcePrefab, transform);
            soundSource.InitializeSoundSource(this);
            soundSource.gameObject.SetActive(false);

            return soundSource;
        }

        private SoundSource GetSoundSource()
        {
            SoundSource soundSource = null;

            if (deactiveSoundSourceList.Count > 0)
            {
                soundSource = deactiveSoundSourceList[0];
                deactiveSoundSourceList.RemoveAt(0);
            }
            else
            {
                soundSource = SpawnSoundSource();
            }

            return soundSource;
        }

        public void ReturnSoundSource(SoundSource soundSource)
        {
            if (!deactiveSoundSourceList.Contains(soundSource))
                deactiveSoundSourceList.Add(soundSource);
        }

        public void StartMusic(AudioClip audioClip)
        {
            musicVolumeController.StartMusic(audioClip);
        }

        public void EndMusic()
        {
            musicVolumeController.EndMusic();
        }


    }
}