using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip checkpointActivatedSound;
    public AudioClip gemPickupSound;
    public AudioClip playerDamagedSound;
    public AudioClip playerDeathSound;
    public AudioClip playerJumpSound;
    public AudioClip portalSound;
    public AudioClip powerupPickupSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
