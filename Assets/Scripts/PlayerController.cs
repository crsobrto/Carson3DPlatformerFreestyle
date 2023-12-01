using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerController;

    //private FootstepController footstepController;

    public float speed; // Movement speed
    public float rotateSpeed; // How fast the player will rotate
    public float jumpForce;
    public float gravityModifier;
    public float knockbackForce;
    public float knockbackTime;
    public float offGroundTime;
    private float knockbackCounter;

    public bool characterControllerActive = true;
    public bool isWalking = false;

    public GameObject playerModel;

    public Animator anim;

    public Transform pivot;

    private AudioSource playerAudio;

    private Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();

        //footstepController = GameObject.Find("FootstepManager").GetComponent<FootstepController>();

        //playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        /*
        offGroundTime += Time.deltaTime;

        if (offGroundTime < 0.2f)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
            offGroundTime = 0f;
        }
        */

        if (knockbackCounter <= 0)
        {
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
        }
        else
        {
            knockbackCounter -= Time.deltaTime; // Make the knockbackCounter count down
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityModifier * Time.deltaTime); // Apply gravity to the player's current y-position
        playerController.Move(moveDirection * Time.deltaTime); // Move the player based on moveDirection and Time.deltaTime

        // Rotate the player in different directions based on which direction the camera is looking
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime); // Allows smoother movemet transitions for the player
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        // Set up the animation triggers
        anim.SetBool("Grounded", playerController.isGrounded);
        anim.SetFloat("Speed_f", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

    public void Knockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;

        moveDirection = direction * knockbackForce;
        moveDirection.y = knockbackForce; // The player will always be knocked up into the air
    }
}
