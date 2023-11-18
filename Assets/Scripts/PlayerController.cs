using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody playerRb;
    public CharacterController playerController;

    public float speed; // Movement speed
    public float rotateSpeed; // How fast the player will rotate
    public float jumpForce;
    public float gravityModifier;

    public GameObject playerModel;

    public Animator anim;

    public Transform pivot;
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
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirection.y, Input.GetAxis("Vertical") * speed);
        float yStore = moveDirection.y; // Store the player's current y-position
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * speed; // Prevents the player from gaining speed by moving diagonally
        moveDirection.y = yStore; // Restore the player's y-position to fix gravity issues

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
                moveDirection.y = 0f; // Prevents gravity from continually building up as the player is grounded
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

        // Rotate the player in different directions based on which direction the camera is looking
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime); // Allows smoother movemet transitions for the player
        }

        // Set up the animation triggers
        anim.SetBool("Grounded", playerController.isGrounded);
        anim.SetFloat("Speed_f", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
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
}
