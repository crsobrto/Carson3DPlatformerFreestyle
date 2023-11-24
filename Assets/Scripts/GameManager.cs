using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentGems; // Keeps track of how many gems the player has

    public float powerupLength;
    public float powerupCounter;
    public float checkpointTextLength;

    public bool powerupActive = false;

    public TextMeshProUGUI gemText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI checkpointText;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupActive == true)
        {
            powerupCounter -= Time.deltaTime;
            powerupText.text = "Powerup active!!: " + (int)powerupCounter; // Display the powerupCounter to the player
        }
    }

    public void AddGems(int gemsToAdd)
    {
        currentGems += gemsToAdd;
        gemText.text = "Gems: " + currentGems;
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
