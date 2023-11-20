using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
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
