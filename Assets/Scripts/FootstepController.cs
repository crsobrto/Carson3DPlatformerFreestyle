using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] snowFootstepSounds;

    public float minTimeBetweenFootsteps = 0.3f;
    public float maxTimeBetweenFootsteps = 0.6f;
    private float timeSinceLastFootstep;

    public bool isWalking;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            if (Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
            {
                AudioClip snowFootstepSound = snowFootstepSounds[Random.Range(0, snowFootstepSounds.Length)]; // Play a random snow footstep sound from the snowFootstepSounds array
                audioSource.PlayOneShot(snowFootstepSound);

                timeSinceLastFootstep = Time.time;
            }
        }
    }

    public void StartWalking()
    {
        isWalking = true;
    }

    public void StopWalking()
    {
        isWalking = false;
    }
}
