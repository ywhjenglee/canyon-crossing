using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform playerCamera = null;
    [SerializeField] CharacterController playerBody = null;
    [SerializeField] float mouseSensitivity = 4f;
    [SerializeField] float movementSpeed = 8f;
    private float cameraPitch = 0f;
    private float gravity = -9.81f;
    private float velocityY;
    private float jumpHeight = 2f;

    // Lock cursor and make it invisible
    void Start()
    {
        playerBody = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update First Person Player
    void Update()
    {
        PlayerLook();
        PlayerMovement();
    }
    
    // Update Player's camera
    private void PlayerLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
    }

    // Update Player's position
    private void PlayerMovement()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        inputDirection.Normalize();

        if (playerBody.isGrounded)
        {
            velocityY = 0f;
        }
        velocityY += gravity * Time.deltaTime;

        // Jumping
        if (Input.GetButton("Jump") && playerBody.isGrounded)
        {
            velocityY = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        Vector3 playerVelocity = (transform.right * inputDirection.x + transform.forward * inputDirection.z) * movementSpeed + Vector3.up * velocityY;
        playerBody.Move(playerVelocity * Time.deltaTime);
    }
}
