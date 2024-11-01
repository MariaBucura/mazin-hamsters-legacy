using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    public Animator playerAnimator;

    [SerializeField]
    private float playerSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private Camera followCamera;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private bool isEmoting;
    private bool isMoving;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        isEmoting = false;
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        controller.Move(movementDirection * playerSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        playerAnimator.SetBool("isGrounded", groundedPlayer);
        playerAnimator.SetBool("isMoving", isMoving);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            if (isEmoting)
            {
                isEmoting = false;
            }
            isMoving = true;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            playerAnimator.SetTrigger("jump");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        if (movementDirection.magnitude > 0.1f)
        {
            if (isEmoting)
            {
                isEmoting = false;
            }
            isMoving = true;
            playerAnimator.SetTrigger("run");
            playerAnimator.ResetTrigger("idle");
        }
        else
        {
            playerAnimator.SetTrigger("idle");
            playerAnimator.ResetTrigger("run");
        }

        if (!groundedPlayer && playerVelocity.y < 0f)
        {
            playerAnimator.SetTrigger("fall");
        }
        else
        {
            playerAnimator.ResetTrigger("fall");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isMoving = false;
            isEmoting = !isEmoting;
            playerAnimator.SetBool("isEmoting", isEmoting);
        }

    }
}
