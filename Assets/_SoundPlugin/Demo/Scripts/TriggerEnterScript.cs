using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SoundSystem.Demo
{
    public class TriggerEnterScript : MonoBehaviour
    {
        public UnityEvent onTriggerEnterEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                onTriggerEnterEvent?.Invoke();
            }
        }
    }
}