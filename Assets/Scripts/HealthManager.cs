using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float invincibilityLength; // How long the player will be invincible
    private float invincibilityCounter; // Counts how long the player has been invincible
    private float flashCounter;
    public float flashLength = 0.1f;
    public float respawnLength;

    private bool isRespawning;

    private Vector3 respawnPoint;

    public PlayerController playerController;

    public Renderer playerRenderer;

    public GameObject playerGameObject;

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
        charController.enabled = false;

        yield return new WaitForSeconds(respawnLength);

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
}
