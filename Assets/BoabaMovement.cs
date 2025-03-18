using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoabaMovement : MonoBehaviour
{
    [SerializeField] private CharacterController playerCharacterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private KeyCode jumpKey;

    private bool isGrounded;
    private float xMovement;
    private float yMovement;

    private float gravityScale = -9.81f;

    private Camera mainCamera;
    private Vector3 velocity;

    private void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        HandleGrounding();
        HandleMovement();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            HandleJump();
        }
    }

    private void HandleMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 moveDir = cameraRight * xMovement + cameraForward * yMovement;
        moveDir.Normalize();

        playerCharacterController.Move(moveSpeed * Time.deltaTime * moveDir);
    }

    private void ApplyGravity()
    {
        velocity.y += gravityScale * Time.deltaTime;
        playerCharacterController.Move(velocity * Time.deltaTime);
    }

    private void HandleGrounding()
    {
        isGrounded = playerCharacterController.isGrounded;
        if (!isGrounded || velocity.y >= 0f) return;

        velocity.y = -2f;
    }
  
    private void HandleJump()
    {
        velocity.y += Mathf.Sqrt(jumpForce * -2f * gravityScale);
    }
}
