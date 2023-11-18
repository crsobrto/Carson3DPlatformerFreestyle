using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int gemValue;

    public bool powerupActive = false;

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
        if (other.tag == "Gem")
        {
            FindObjectOfType<GameManager>().AddGems(gemValue); // Find the objects that have the GameManager attached to them
            Instantiate(gemPickupEffect, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Powerup"))
        {
            powerupActive = true;
            FindObjectOfType<GameManager>().ActivatePowerup(powerupActive);
            Instantiate(powerupPickupEffect, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
