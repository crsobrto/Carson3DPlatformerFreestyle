using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
    private AudioSource playerAudio;

    public AudioClip snowSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {

        }
    }
}
