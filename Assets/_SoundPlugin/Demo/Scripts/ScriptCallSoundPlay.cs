using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem.Demo
{
    public class ScriptCallSoundPlay : MonoBehaviour
    {
        [SerializeField] private SoundPlay soundPlay;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                soundPlay.Play();
            }
        }


    }
}