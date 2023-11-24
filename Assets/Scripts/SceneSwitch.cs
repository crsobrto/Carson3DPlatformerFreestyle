using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    //public bool isTransitioning; // Is the player transitioning to another scene?

    public float transitionLength;

    private SceneSwitchFadeInOut fade;

    public GameObject portalGameObject;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<SceneSwitchFadeInOut>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (portalGameObject.tag == "Left Portal" && other.tag == "Player")
        {
            //Debug.Log("Entered left portal.");
            StartCoroutine(ChangeScene(portalGameObject));
        }

        else if (portalGameObject.tag == "Right Portal" && other.tag == "Player")
        {
            //Debug.Log("Entered right portal.");
            StartCoroutine(ChangeScene(portalGameObject));
        }
    }

    IEnumerator ChangeScene(GameObject portal)
    {
        if (portal.tag == "Left Portal")
        {
            fade.FadeInLeftPortal();
            yield return new WaitForSeconds(transitionLength);

            SceneManager.LoadScene(1); // Load Level 1
        }
        else if (portal.tag == "Right Portal")
        {
            //Debug.Log("IEnumerator for Right Portal was called.");
            fade.FadeInRightPortal();
            yield return new WaitForSeconds(transitionLength);

            SceneManager.LoadScene(1); // Load Level 1
        }
    }
}
