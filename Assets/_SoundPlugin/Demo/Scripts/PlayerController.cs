using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem.Demo
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private RayDetector groundDetector;

        private float horizontalInput;
        private float verticalInput;
        private Vector3 direction;


        private void Update()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            direction = new Vector3(horizontalInput, 0, verticalInput);

            if (groundDetector.Detected)
            {
                characterController.Move(direction.normalized * moveSpeed * Time.deltaTime);
            }

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }


        }



    }

}