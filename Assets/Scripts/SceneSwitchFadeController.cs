using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchFadeController : MonoBehaviour
{
    SceneSwitchFadeInOut fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<SceneSwitchFadeInOut>();

        fade.FadeOutLeftPortal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
