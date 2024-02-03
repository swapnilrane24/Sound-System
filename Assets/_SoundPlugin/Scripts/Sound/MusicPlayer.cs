using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] musicClipList;

        public void Start()
        {
            SoundManager.Instance.StartMusic(musicClipList[Random.Range(0, musicClipList.Length)]);
        }


        public void StopMusic()
        {
            SoundManager.Instance.EndMusic();
        }

    }
}