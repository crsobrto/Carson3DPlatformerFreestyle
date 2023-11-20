using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float invincibilityLength; // How long the player will be invincible
    private float invincibilityCounter; // Counts how long the player has been invincible
    private float flashCounter;
    public float flashLength = 0.1f;
    public float respawnLength;
    public float fadeSpeed;
    public float waitForFade;

    private bool isRespawning;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;

    private Vector3 respawnPoint;

    public PlayerController playerController;

    public Renderer playerRenderer;

    public GameObject playerGameObject;
    public GameObject deathEffect; // Particles that are played when the player dies

    public Image blackScreen;

    public CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.Find("Player");

        charController = playerGameObject.GetComponent<CharacterController>();

        currentHealth = maxHealth; // Start the game with max health

        //player = FindObjectOfType<PlayerController>();

        respawnPoint = playerController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        // Fade screen from black
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
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

    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true; // The player is currently respawning
        playerController.gameObject.SetActive(false); // Remove the player from the world
        Instantiate(deathEffect, playerController.transform.position, playerController.transform.rotation); // Play the player deathEffect
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

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        // Prevents the player from healing over the max health possible
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Used to set checkpoints
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
