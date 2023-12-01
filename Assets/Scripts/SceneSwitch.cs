using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    public float transitionLength;

    private SceneSwitchFadeInOut fade;

    public GameObject portalGameObject;

    private AudioSource playerAudio;

    private AudioClip portalSound;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<SceneSwitchFadeInOut>();

        playerAudio = FindObjectOfType<PlayerController>().GetComponent<AudioSource>();

        portalSound = FindObjectOfType<PlayerController>().portalSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(ChangeSceneRoutine(portalGameObject));
            playerAudio.PlayOneShot(portalSound, 1.0f);
        }
    }

    IEnumerator ChangeSceneRoutine(GameObject portal)
    {
        if (portal.tag == "Left Portal")
        {
            fade.FadeInLeftPortal();

            yield return new WaitForSeconds(transitionLength);

            SceneManager.LoadScene(2); // Load Level 1
        }
        else if (portal.tag == "Right Portal")
        {
            fade.FadeInRightPortal();

            yield return new WaitForSeconds(transitionLength);

            SceneManager.LoadScene(3); // Load Level 2
        }
        else if (portal.tag == "Main Portal")
        {
            fade.FadeInMainPortal();

            yield return new WaitForSeconds(transitionLength);

            SceneManager.LoadScene(1); // Load Level Selection
        }

        else if (portal.tag == "Center Portal")
        {
            fade.FadeInExitPortal();
            
            yield return new WaitForSeconds(transitionLength);

            Cursor.lockState = CursorLockMode.Confined; // Unlock the cursor so the player can use it in the Main Meu
            SceneManager.LoadScene(0); // Load Main Menu
        }
    }
}
