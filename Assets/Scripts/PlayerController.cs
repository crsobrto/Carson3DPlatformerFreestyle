using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 movementDirection;

    public float playerSpeed;
    public float playerRotationSpeed;

    public float playerJumpSpeed;
    public float jumpForce;
    public float jumpButtonGracePeriod; // Gives the player a short grace period to jump so that they don't have to press the jump button at the perfect time to jump
    private float? jumpButtonPressedTime; // '?' means that this variable can either have a value or be null (in other words, this variable is "nullable")

    public float knockbackTime;
    public float knockbackForce;
    private float knockbackCounter;

    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;

    public bool isWalking;

    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Retrieve the characterController component

        originalStepOffset = characterController.stepOffset; // Store the original Step Offset
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get the player's horizontal movement input
        float verticalInput = Input.GetAxis("Vertical"); // Get the player's vertical movement input

        movementDirection = new Vector3(horizontalInput, 0, verticalInput); // Use horizontalInput and verticalInput to move the player

        // Stores the magnitude of the player's movement input from gamepads
        // Mathf.Clamp01 ensures magnitude is never above 1; magnitude will now always range from 0 to 1
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * playerSpeed;

        movementDirection.Normalize(); // Prevents faster player movement from moving diagonally

        ySpeed += Physics.gravity.y * Time.deltaTime; // Apply gravity to the player's y-axis movement

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time; // Time.time is the number of seconds since the game has started
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        // Checks if the player was last on the ground within the grace period
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;

            // Prevents gravity from building up while the player is on the ground
            // -0.5f is used instead of 0f because 0f causes ySpeed to fluctuate in the positive direction
            ySpeed = -0.5f;

            // Checking if the player last jumped within the grace period
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = playerJumpSpeed;
                jumpButtonPressedTime = null; // Reset
                lastGroundedTime = null; // Reset
            }
        }
        else
        {
            characterController.stepOffset = 0; // Prevents movement glitches when jumping into objects
        }

        // Move the player based on movementDirection
        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        // Among other things, rotate the player to face the direction they're currently moving
        if (movementDirection != Vector3.zero)
        {
            isWalking = true;

            // Retrieve the needed rotation
            // Forward direction = -movementDirection; up direction = y-axis = Vector3.up
            // movementDirection will make the player face opposite of the correct direction, so -movementDirection is used
            Quaternion toRotation = Quaternion.LookRotation(-movementDirection, Vector3.up);

            // Now rotate the player
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotationSpeed * Time.deltaTime);
        }
        else
        {
            isWalking = false;
        }
    }

    public void Knockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;

        movementDirection = direction * knockbackForce;
        movementDirection.y = knockbackForce; // The player will always be knocked up into the air
    }
}
