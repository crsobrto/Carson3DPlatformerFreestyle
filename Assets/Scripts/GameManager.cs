using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class GameManager : MonoBehaviour
{
    public int currentGems; // Keeps track of how many gems the player has

    public float powerupLength;
    public float powerupCounter;

    public bool powerup = false;

    public TextMeshProUGUI gemText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (powerupCounter > 0)
        {
            powerupCounter -= Time.deltaTime;
            powerupText.text = "Powerup active!!: " + (int)powerupCounter;
        }
        else
        {
            powerup = false;
            powerupText.text = "";
        }
    }

    public void AddGems(int gemsToAdd)
    {
        currentGems += gemsToAdd;
        gemText.text = "Gems: " + currentGems;
    }

    public void ActivatePowerup()
    {
        powerup = true;
        powerupCounter = powerupLength + 1;
    }
}
