using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive = 1;

    private AudioSource soundControllerAudioSource;

    private AudioClip playerDamagedSound;

    // Start is called before the first frame update
    void Start()
    {
        soundControllerAudioSource = FindObjectOfType<SoundController>().GetComponent<AudioSource>();

        playerDamagedSound = FindObjectOfType<SoundController>().playerDamagedSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized; // Restricts hitDirection to not be too big

            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection); // Apply damage to the object that has the HealthManager script applied to it

            if (FindObjectOfType<HealthManager>().currentHealth > 0)
            {
                soundControllerAudioSource.PlayOneShot(playerDamagedSound, 1.0f);
            }
        }
    }
}
