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

    public float jumpForce;
    public float knockbackTime;
    public float knockbackForce;
    private float knockbackCounter;


    public bool isWalking;

    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Retrieve the characterController component
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get the player's horizontal movement input
        float verticalInput = Input.GetAxis("Vertical"); // Get the player's vertical movement input

        movementDirection = new Vector3(horizontalInput, 0, verticalInput); // Use horizontalInput and verticalInput to move the player

        // Stores the magnitude of the player's movement input from gamepads
        // Mathf.Clamp01 ensures magnitude is never above 1; magnitude will now always range from 0 to 1
        float magnitude = Mathf.Clamp01(movementDirection.magnitude)*playerSpeed;
        
        movementDirection.Normalize(); // Prevents faster player movement from moving diagonally

        // Move the player based on movementDirection
        // Time.deltaTime is built into SimpleMove
        characterController.SimpleMove(movementDirection * magnitude);

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
