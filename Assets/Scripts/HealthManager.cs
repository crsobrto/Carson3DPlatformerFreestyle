using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float invincibilityLength; // How long the player will be invincible
    public float flashLength = 0.1f;
    public float respawnLength;
    public float fadeSpeed;
    public float waitForFade;
    private float invincibilityCounter; // Counts how long the player has been invincible
    private float flashCounter;

    public bool isRespawning;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;

    private Vector3 respawnPoint;

    public PlayerController playerController;

    public Renderer playerRenderer;

    public GameObject playerGameObject;
    public GameObject deathEffect; // Particles that are played when the player dies
    public GameObject healEffect;

    public Image blackScreen;

    public CharacterController charController;

    private AudioSource soundControllerAudioSource;

    private AudioClip playerDeathSound;

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.Find("Player");

        charController = playerGameObject.GetComponent<CharacterController>();

        //playerAudio = FindObjectOfType<PlayerController>().GetComponent<AudioSource>();

        soundControllerAudioSource = GameObject.Find("SoundController").GetComponent<AudioSource>();

        playerDeathSound = FindObjectOfType<SoundController>().playerDeathSound;

        currentHealth = maxHealth; // Start the game with max health
        //gameManager.healthText.text = "Health: " + currentHealth;

        //player = FindObjectOfType<PlayerController>();

        respawnPoint = playerController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("isRespawning = " + isRespawning);

        FindObjectOfType<GameManager>().healthText.text = "Health: " + currentHealth; // Display the player's currentHealth

        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            // Make sure the player is visible once the flashCounter expires
            if (invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }

        // Fade screen to black
        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        // Fade screen from black
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;
            //gameManager.healthText.text = "Health: " + currentHealth;

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                playerController.Knockback(direction);

                invincibilityCounter = invincibilityLength;

                playerRenderer.enabled = false; // Make the player invisible

                flashCounter = flashLength;
            }
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount; // Give health to the player

        Instantiate(healEffect, new Vector3(playerController.transform.position.x, playerController.transform.position.y - 0.5f, playerController.transform.position.z),
            playerController.transform.rotation); // Play the player healEffect

        // Prevents the player from healing over the max health possible
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Respawn()
    {
        if (!isRespawning)
        {
            //healthManagerAudio.PlayOneShot(deathSound, 1.0f); // Play the deathSound
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        FindObjectOfType<PlayerController>().isWalking = false; // The player is not walking when they die

        isRespawning = true; // The player is currently respawning

        playerController.gameObject.SetActive(false); // Remove the player from the world
        //FindObjectOfType<PlayerController>().characterControllerActive = false;

        Instantiate(deathEffect, playerController.transform.position, playerController.transform.rotation); // Play the player deathEffect
        soundControllerAudioSource.PlayOneShot(playerDeathSound, 1.0f);

        charController.enabled = false;

        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;

        isRespawning = false;
        playerController.gameObject.SetActive(true);
        playerController.transform.position = respawnPoint;
        charController.enabled = true;
        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLength;
        playerRenderer.enabled = false; // Make the player invisible
        flashCounter = flashLength;
    }

    // Used to set checkpoints
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
