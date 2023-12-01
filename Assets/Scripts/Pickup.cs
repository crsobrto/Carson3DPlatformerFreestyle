using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int gemValue;

    public GameObject gemPickupEffect;
    public GameObject powerupPickupEffect;

    private AudioSource playerAudio;
    private AudioClip gemPickupSound;
    private AudioClip powerupPickupSound;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = FindObjectOfType<PlayerController>().GetComponent<AudioSource>();

        gemPickupSound = FindObjectOfType<PlayerController>().gemPickupSound;
        powerupPickupSound = FindObjectOfType<PlayerController>().powerupPickupSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Picking up gems
        if (other.tag == "Gem")
        {
            FindObjectOfType<GameManager>().AddGems(gemValue); // Find the objects that have the GameManager attached to them
            Instantiate(gemPickupEffect, transform.position, transform.rotation);
            playerAudio.PlayOneShot(gemPickupSound, 1.0f); // Play the gemPickupSound
            Destroy(other.gameObject);
        }

        // Picking up powerups
        else if (other.gameObject.CompareTag("Powerup"))
        {
            FindObjectOfType<GameManager>().ActivatePowerup(other);
            Instantiate(powerupPickupEffect, transform.position, transform.rotation);
            playerAudio.PlayOneShot(powerupPickupSound, 1.0f); // Play the powerupPickupSound
        }


    }
}
