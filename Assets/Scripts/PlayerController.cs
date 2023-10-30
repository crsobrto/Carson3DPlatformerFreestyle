using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody playerRb;
    public CharacterController playerController;

    public float speed; // Movement speed
    public float jumpForce;
    public float gravityModifier;
    //public bool isOnGround = false;

    private Vector3 moveDirection;
    /*
    private float horizontalInput;
    private float forwardInput;
    */

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        //playerRb = GetComponent<Rigidbody>();
        //Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        // Allow the player to move on the x and z axes, keeping their current y-axis velocity
        //playerRb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y, Input.GetAxis("Vertical") * speed);

        /*
        if (Input.GetButtonDown("Jump")) // "Jump" is the spacebar
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z); // Make the player jump
        }
        */

        // Allow the player to move on the x and z axes and jump smoothly on the y axis
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirection.y, Input.GetAxis("Vertical") * speed);

        // If the player is currently on the ground
        if (playerController.isGrounded)
        {
            // If the player presses the spacebar
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce; // Make the player jump
            }
            else
            {
                moveDirection.y = 0;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityModifier * Time.deltaTime); // Apply gravity to the player's current y-position
        playerController.Move(moveDirection * Time.deltaTime); // Move the player based on moveDirection and Time.deltaTime

            /*
            if (isOnGround)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                forwardInput = Input.GetAxis("Vertical");

                transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput * -1);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);


                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    isOnGround = false;
                }
            }
            */
        }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with enemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
