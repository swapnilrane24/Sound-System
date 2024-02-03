using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem.Demo
{
    public class CustomPlay : MonoBehaviour
    {
        [SerializeField] private SoundPlay soundPlay;
        [SerializeField] private AudioClip audioClip;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                soundPlay.Play(audioClip);
            }
        }



    }
}