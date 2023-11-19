using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Start the game with max health

        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        currentHealth -= damage;

        player.Knockback(direction);
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
