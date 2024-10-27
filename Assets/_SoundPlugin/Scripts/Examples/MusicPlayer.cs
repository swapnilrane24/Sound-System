using System.Collections;
using System.Collections.Generic;
using SoundSystem.Demo;
using UnityEngine;

namespace SoundSystem
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] musicClipList;

        int index = 0;

        public void Start()
        {
            SoundManager.Instance.PlayMusic(musicClipList[index % musicClipList.Length]);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                index++;
                SoundManager.Instance.SwitchMusic(musicClipList[index % musicClipList.Length]);
            }
        }

        public void ToggleMusic()
        {
            SoundManager.Instance.ToggleMusic();
        }

    }
}