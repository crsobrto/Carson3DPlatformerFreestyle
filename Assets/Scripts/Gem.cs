using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Gem[] gems;

    // Start is called before the first frame update
    void Start()
    {
        gems = FindObjectsOfType<Gem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
