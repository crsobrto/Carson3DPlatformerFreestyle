using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int gemValue;

    public GameObject gemPickupEffect;
    public GameObject powerupPickupEffect;

    // Start is called before the first frame update
    void Start()
    {

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
            Destroy(other.gameObject);
        }

        // Picking up powerups
        else if (other.gameObject.CompareTag("Powerup"))
        {
            FindObjectOfType<GameManager>().ActivatePowerup(other);
            Instantiate(powerupPickupEffect, transform.position, transform.rotation);
        }
    }
}
