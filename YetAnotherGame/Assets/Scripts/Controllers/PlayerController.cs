using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    
    [SerializeField]
    [Header("First point view camera position")]
    private Transform firstPointView = null;

    [SerializeField]
    [Header("Third point view camera position")]
    private Transform thirdPointView = null;

    private bool firstPointViewEnabled;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(Input.GetButtonDown(Constants.ChangeView))
        {
            firstPointViewEnabled = !firstPointViewEnabled;
        }

        var newCameraViewTransform = firstPointViewEnabled ? thirdPointView : firstPointView;
        Camera.main.transform.position = newCameraViewTransform.position;
        Camera.main.transform.rotation = newCameraViewTransform.rotation;

        if (controller.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
