using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem.Demo
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private float smoothSpeed = 0.15f;
        [SerializeField] private PlayerController player;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, smoothSpeed);
        }

    }
}