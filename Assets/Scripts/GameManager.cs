using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class GameManager : MonoBehaviour
{
    public int currentGems; // Keeps track of how many gems the player has

    public float powerupLength;
    public float powerupCounter;
    public float checkpointTextLength;

    private bool powerupActive = false;
    public bool foundAllGems = false;

    public TextMeshProUGUI gemText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI checkpointText;

    public CharacterController charController;

    private PlayerController playerController;

    private HealthManager healthManager;

    private FootstepController footstepController;

    // Start is called before the first frame update
    void Start()
    {
        charController = FindObjectOfType<PlayerController>().GetComponent<CharacterController>();

        playerController = FindObjectOfType<PlayerController>();

        healthManager = FindObjectOfType<HealthManager>();

        footstepController = FindObjectOfType<FootstepController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("GameManager isRespawning = " + healthManager.isRespawning);

        if (playerController.isWalking && charController.isGrounded && !healthManager.isRespawning)
        {
            //Debug.Log("Player is walking.");
            footstepController.StartWalking();
        }
        else
        {
            //Debug.Log("Player is not walking.");
            footstepController.StopWalking();
        }

        if (powerupActive == true)
        {
            powerupCounter -= Time.deltaTime;
            powerupText.text = "Powerup active!!: " + (int)powerupCounter; // Display the powerupCounter to the player
        }
    }

    public void IsWalking()
    {
        
    }

public void AddGems(int gemsToAdd)
    {
        currentGems += gemsToAdd;

        if (currentGems < FindObjectOfType<Gem>().gems.Length)
        {
            gemText.text = "Gems: " + currentGems;
        }
        else
        {
            gemText.text = "Found all " + FindObjectOfType<Gem>().gems.Length + " gems in this level!!";
        }
    }

    public void ActivatePowerup(Collider powerupCollider)
    {
        powerupActive = true; // Enable the powerup
        powerupCounter = powerupLength + 1;
        FindObjectOfType<PlayerController>().jumpForce *= 3; // Increase the player's jumpForce

        powerupCollider.GetComponent<Renderer>().enabled = false; // Make the powerup invisible
        powerupCollider.enabled = false; // Make the powerup uninteractable

        StartCoroutine(PowerupCountdownRoutine(powerupCollider)); // Start the countdown
    }

    public void StartCheckpointTextCountdown()
    {
        StartCoroutine(CheckpointTextCountdownRoutine());
    }

    IEnumerator PowerupCountdownRoutine(Collider powerupCollider)
    {
        yield return new WaitForSeconds(powerupLength + 1);

        powerupActive = false;
        powerupText.text = "";
        FindObjectOfType<PlayerController>().jumpForce /= 3;

        powerupCollider.GetComponent<Renderer>().enabled = true; // Make the powerup visible again
        powerupCollider.enabled = true; // Make the powerup interactable again
    }

    public IEnumerator CheckpointTextCountdownRoutine()
    {
        yield return new WaitForSeconds(checkpointTextLength);

        FindObjectOfType<GameManager>().checkpointText.text = ""; // Remove the checkpointText from the screen
    }
}
