using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    public float fadeSpeed;
    public float fadeLength;
    public float waitForFade;
    public float transitionLength;

    public bool isTransitioning; // Is the player transitioning to another scene?
    public bool isFadeInLeftPortal;
    public bool isFadeOutLeftPortal;

    public PlayerController playerController;

    public CharacterController charController;

    public GameObject portalGameObject;

    //public CanvasGroup canvasGroup;

    public Image leftPortalScreen;

    // Start is called before the first frame update
    void Start()
    {
        //portalGameObject = GetComponent<GameObject>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        charController = GameObject.Find("Player").GetComponent<CharacterController>(); // Get the player's Character Controller
    }

    private void Update()
    {
        // Fade screen to black
        if (isFadeInLeftPortal)
        {
            leftPortalScreen.color = new Color(leftPortalScreen.color.r, leftPortalScreen.color.g, leftPortalScreen.color.b,
                Mathf.MoveTowards(leftPortalScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (leftPortalScreen.color.a == 1f)
            {
                isFadeInLeftPortal = false;
            }
        }

        // Fade screen from black
        if (isFadeOutLeftPortal)
        {
            leftPortalScreen.color = new Color(leftPortalScreen.color.r, leftPortalScreen.color.g, leftPortalScreen.color.b,
                Mathf.MoveTowards(leftPortalScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (leftPortalScreen.color.a == 0f)
            {
                isFadeOutLeftPortal = false;
            }
        }
    }

    /*
    public void FadeInLeftPortal()
    {
        isFadeInLeftPortal = true;

        if (!isTransitioning)
        {
            StartCoroutine("ChangeScene");
        }
    }
    */

    public void FadeOutLeftPortal()
    {
        isFadeOutLeftPortal = true;
    }

    IEnumerator ChangeScene()
    {
        //Debug.Log("Coroutine Started.");

        isTransitioning = true;
        playerController.gameObject.SetActive(false); // Remove the player from the world

        //Debug.Log("playerController deactivated.");

        //Instantiate(deathEffect, playerController.transform.position, playerController.transform.rotation); // Play the player deathEffect
        charController.enabled = false;

        //Debug.Log("Bools changed.");

        yield return new WaitForSeconds(transitionLength); // The fade is about to start

        //Debug.Log("Waited for 2 seconds.");

        isFadeInLeftPortal = true; // The fade is happening now

        //SceneManager.LoadScene(1);

        yield return new WaitForSeconds(waitForFade); // Stay on the colored screen

        isFadeInLeftPortal = false;
        isFadeOutLeftPortal = true;

        isTransitioning = false; // The player is now in the new scene

        playerController.gameObject.SetActive(true);
        //playerController.transform.position = respawnPoint;
        charController.enabled = true;
        //currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Entered left portal.");

            StartCoroutine(ChangeScene());
        }

        else if (other.tag == "Right Portal")
        {
            Debug.Log("Entered right portal.");
        }
    }
}
