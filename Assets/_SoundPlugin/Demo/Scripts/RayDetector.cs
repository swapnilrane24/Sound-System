using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem.Demo
{
    public class RayDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float rayLength;
        [SerializeField] private Color color = Color.red;

        private RaycastHit hit;
        private bool detected;

        public bool Detected { get => detected; }
        public RaycastHit Hit { get => hit; }

        void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                detected = true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.blue);
                detected = false;
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = color;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayLength);
        }
#endif
    }
}