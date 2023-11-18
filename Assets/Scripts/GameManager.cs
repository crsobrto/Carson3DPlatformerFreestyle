using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentGems; // Keeps track of how many gems the player has

    public bool powerup = false;

    public TextMeshProUGUI gemText;
    public TextMeshProUGUI powerupText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddGems(int gemsToAdd)
    {
        currentGems += gemsToAdd;
        gemText.text = "Gems: " + currentGems;
    }

    public void ActivatePowerup(bool powerupActive)
    {
        powerup = powerupActive;
        powerupText.text = "Powerup active!!";
    }
}
