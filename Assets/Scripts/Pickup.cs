using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int gemValue;

    public GameObject gemPickupEffect;
    public GameObject powerupPickupEffect;

    private AudioSource soundControllerAudioSource;

    private AudioClip gemPickupSound;
    private AudioClip powerupPickupSound;

    // Start is called before the first frame update
    void Start()
    {
        soundControllerAudioSource = GameObject.Find("SoundController").GetComponent<AudioSource>(); // Get the SoundController's Audio Source

        gemPickupSound = FindObjectOfType<SoundController>().gemPickupSound;
        powerupPickupSound = FindObjectOfType<SoundController>().powerupPickupSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Picking up gems
        if (other.tag == "Gem")
        {
            FindObjectOfType<GameManager>().AddGems(gemValue); // Find the objects that have the GameManager attached to them
            Instantiate(gemPickupEffect, transform.position, transform.rotation);
            soundControllerAudioSource.PlayOneShot(gemPickupSound, 1.0f); // Play the gemPickupSound
            Destroy(other.gameObject);
        }

        // Picking up powerups
        else if (other.gameObject.CompareTag("Powerup"))
        {
            FindObjectOfType<GameManager>().ActivatePowerup(other);
            Instantiate(powerupPickupEffect, transform.position, transform.rotation);
            soundControllerAudioSource.PlayOneShot(powerupPickupSound, 1.0f); // Play the powerupPickupSound
        }


    }
}
