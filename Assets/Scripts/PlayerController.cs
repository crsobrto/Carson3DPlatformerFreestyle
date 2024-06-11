using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerController;

    private Vector3 movementDirection;

    public float playerSpeed;
    public float jumpForce;
    public float knockbackTime;
    public float knockbackForce;
    private float knockbackCounter;


    public bool isWalking;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get the player's horizontal movement input
        float verticalInput = Input.GetAxis("Vertical"); // Get the player's vertical movement input

        if (horizontalInput != 0 || verticalInput != 0)
        {
            isWalking = true;
        } else
        {
            isWalking = false;
        }

        movementDirection = new Vector3(horizontalInput, 0, verticalInput); // Use horizontalInput and verticalInput to move the player
        movementDirection.Normalize(); // Prevents faster player movement from moving diagonally

        transform.Translate(movementDirection * playerSpeed * Time.deltaTime); // Change the player's position based on movementDirection
    }

    public void Knockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;

        movementDirection = direction * knockbackForce;
        movementDirection.y = knockbackForce; // The player will always be knocked up into the air
    }
}
