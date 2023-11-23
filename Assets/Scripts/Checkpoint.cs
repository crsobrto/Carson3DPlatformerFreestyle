using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    //public float checkpointTextCounter = FindObjectOfType<GameManager>().checkpointTextCounter;

    public HealthManager healthManager;

    public Renderer theRenderer;

    public Material checkpointOff;
    public Material checkpointOn;

    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckpointOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }

        theRenderer.material = checkpointOn;

        FindObjectOfType<GameManager>().checkpointText.text = "Checkpoint activated!"; // Tell the user that the checkpoint has been activated
        FindObjectOfType<GameManager>().StartCheckpointTextCountdown(); // Start the countdown
    }

    public void CheckpointOff()
    {
        theRenderer.material = checkpointOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) // Equivalent to "other.tag == "Player"
        {
            healthManager.SetSpawnPoint(transform.position); // Set the new spawn point to this checkpoint's location
            CheckpointOn();
        }
    }
}
