using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float turnSmoothTime = 0.1f;

    public float turnSmoothVelocity = 0.1f;
    public Animator animator;
    public Transform cam;
    public float walkSpeed = 0.3f;
    public float runSpeed = 1f;
    public float currentSpeed;
    bool isRunning;

    public float speed = 0.5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float velocity = 0f;

    // Переменные для прыжка и гравитации
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    private Vector3 playerVelocity;

    // Переменные для проверки касания с землей
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    public float jumpHorizontalMultiplier = 1.5f;
    private bool isJumping = false;
    public bool isDialogueActive = false;
    public float rollSpeed = 2f; // Скорость переката
    public float rollDuration = 0.5f; // Длительность переката
    public bool isRolling = false; // Флаг переката
    void Update()
    {
        if (isRolling)
        {
            return; 
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
            isJumping = false;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Q) && direction.magnitude >= 0.1f && isGrounded && !isRolling)
        {
            StartCoroutine(Roll(direction));
            return;
        }

        if (direction.magnitude >= 0.1f)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float currentMoveSpeed = isJumping ? speed * jumpHorizontalMultiplier : speed;
            controller.Move(moveDir.normalized * currentMoveSpeed * Time.deltaTime);

            currentSpeed = isRunning ? runSpeed : walkSpeed;
            velocity = Mathf.Lerp(velocity, isRunning ? 1.0f : 0.5f, acceleration * Time.deltaTime);
        }
        else
        {
            velocity = Mathf.Lerp(velocity, 0f, deceleration * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        animator.SetFloat("Velocity", velocity);
    }

    private IEnumerator Roll(Vector3 inputDirection)
    {
        isRolling = true;

        animator.SetTrigger("Roll");

        Vector3 rollDirection;

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
            rollDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            rollDirection = cam.forward;
            rollDirection.y = 0; 
            rollDirection.Normalize();
        }

        float elapsedTime = 0f;
        while (elapsedTime < rollDuration)
        {
            controller.Move(rollDirection * rollSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
    }

}
