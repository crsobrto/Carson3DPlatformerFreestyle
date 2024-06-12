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

    private AudioSource soundControllerAudioSource;

    private AudioClip checkpointActivatedSound;

    public bool checkpointActivated = false;



    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();

        soundControllerAudioSource = GameObject.Find("SoundController").GetComponent<AudioSource>();

        checkpointActivatedSound = FindObjectOfType<SoundController>().checkpointActivatedSound;
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
        checkpointActivated = true;

        FindObjectOfType<GameManager>().checkpointText.text = "Checkpoint activated!"; // Tell the user that the checkpoint has been activated
        FindObjectOfType<GameManager>().StartCheckpointTextCountdown(); // Start the countdown
    }



    public void CheckpointOff()
    {
        theRenderer.material = checkpointOff;
        checkpointActivated = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && !checkpointActivated) //&& theRenderer.material != checkpointOn) // Equivalent to "other.tag == "Player"
        {
            Debug.Log(theRenderer.material);

            healthManager.SetSpawnPoint(transform.position); // Set the new spawn point to this checkpoint's location
            CheckpointOn();
            soundControllerAudioSource.PlayOneShot(checkpointActivatedSound, 1.0f); // Play the checkpointActivatedSound
        }
    }
}
