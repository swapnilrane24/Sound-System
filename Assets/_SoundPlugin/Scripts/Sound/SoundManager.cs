using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioClip musicClip;
        [SerializeField] private SoundSource soundSourcePrefab;
        [SerializeField] private MusicVolumeController musicVolumeController;
        [SerializeField] private int maxSoundInstances = 30;

        private List<SoundSource> deactiveSoundSourceList;
        private Dictionary<AudioDataSO, int> audioCounts = new Dictionary<AudioDataSO, int>();

        public static SoundManager Instance { get => instance; }

        private bool isMusicOn = true;
        private bool isSfxOn = true;

        private const string MusicMixer = "Music";
        private const string SfxMixer = "SFX";

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1 ? true : false;//ES3.Load<bool>("GameSound", true);
            isSfxOn = PlayerPrefs.GetInt("SfxOn", 1) == 1 ? true : false;//ES3.Load<bool>("GameSound", true);

            mixer.SetFloat(MusicMixer, isMusicOn ? 0f : -80f);
            mixer.SetFloat(SfxMixer, isSfxOn ? 0f : -80f);
        }

        private void Start()
        {
            deactiveSoundSourceList = new List<SoundSource>();

            for (int i = 0; i < 5; i++)
            {
                SoundSource soundSource = SpawnSoundSource();
                deactiveSoundSourceList.Add(soundSource);
            }
        }

        public void RecheckSoundAfterAd()
        {
            AudioListener.volume = 1;
        }

        public void ToggleMusic()
        {
            isMusicOn = !isMusicOn;
            mixer.SetFloat(MusicMixer, isMusicOn ? 0f : -80f);
            PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);//ES3.Load<bool>("GameSound", true);
        }

        public void ToggleSFX()
        {
            isSfxOn = !isSfxOn;
            mixer.SetFloat(SfxMixer, isSfxOn ? 0f : -80f);
            PlayerPrefs.SetInt("SfxOn", isSfxOn ? 1 : 0);//ES3.Load<bool>("GameSound", true);
        }

        public bool CanPlaySound(AudioDataSO audioData)
        {
            if (audioCounts.TryGetValue(audioData, out int count))
            {
                if(count >= maxSoundInstances)
                    return false;
            }

            return true;
        }

        public void PlayMusic()
        {
            musicVolumeController.StartMusic(musicClip);
        }

        public void PlayMusic(AudioClip audioClip)
        {
            musicVolumeController.StartMusic(audioClip);
        }

        public void SwitchMusic(AudioClip audioClip)
        {
            musicVolumeController.SwitchMusic(audioClip);
        }

        public void Play(AudioDataSO audioData, out SoundSource soundSource)
        {
            SoundSource selectedAudioSource = GetSoundSource();
            soundSource = selectedAudioSource;
            float pitch = audioData.pitch;

            if (audioData.randomPitchPercent != 0)
            {
                pitch *= 1 + Random.Range(-audioData.randomPitchPercent / 100,
                    audioData.randomPitchPercent / 100);
            }

            selectedAudioSource.PlayAudio(audioData, pitch);
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



    }
}