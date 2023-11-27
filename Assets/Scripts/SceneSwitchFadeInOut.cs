using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitchFadeInOut : MonoBehaviour
{
    public float fadeSpeed;

    public bool isFadeInLeftPortal = false;
    public bool isFadeOutLeftPortal = false;
    public bool isFadeInRightPortal = false;
    public bool isFadeOutRightPortal = false;
    public bool isFadeInMainPortal = false;
    public bool isFadeOutMainPortal = false;
    public bool isFadeInExitPortal = false;
    public bool isFadeOutExitPortal = false;

    public Image mainPortalScreen;
    public Image leftPortalScreen;
    public Image rightPortalScreen;
    public Image exitPortalScreen;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Fade into leftPortalScreen
        if (isFadeInLeftPortal)
        {
            leftPortalScreen.color = new Color(leftPortalScreen.color.r, leftPortalScreen.color.g, leftPortalScreen.color.b,
                Mathf.MoveTowards(leftPortalScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (leftPortalScreen.color.a == 1f)
            {
                isFadeInLeftPortal = false;
            }
        }

        // Fade out of leftPortalScreen
        else if (isFadeOutLeftPortal)
        {
            leftPortalScreen.color = new Color(leftPortalScreen.color.r, leftPortalScreen.color.g, leftPortalScreen.color.b,
                Mathf.MoveTowards(leftPortalScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (leftPortalScreen.color.a == 0f)
            {
                isFadeOutLeftPortal = false;
            }
        }

        // Fade into rightPortalScreen
        else if (isFadeInRightPortal)
        {
            rightPortalScreen.color = new Color(rightPortalScreen.color.r, rightPortalScreen.color.g, rightPortalScreen.color.b,
                Mathf.MoveTowards(rightPortalScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (rightPortalScreen.color.a == 1f)
            {
                isFadeInRightPortal = false;
            }
        }

        // Fade out of rightPortalScreen
        else if (isFadeOutRightPortal)
        {
            rightPortalScreen.color = new Color(rightPortalScreen.color.r, rightPortalScreen.color.g, rightPortalScreen.color.b,
                Mathf.MoveTowards(rightPortalScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (rightPortalScreen.color.a == 0f)
            {
                isFadeOutRightPortal = false;
            }
        }

        // Fade into mainPortalScreen
        else if (isFadeInMainPortal)
        {
            mainPortalScreen.color = new Color(mainPortalScreen.color.r, mainPortalScreen.color.g, mainPortalScreen.color.b,
                Mathf.MoveTowards(mainPortalScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (mainPortalScreen.color.a == 1f)
            {
                isFadeInMainPortal = false;
            }
        }

        // Fade out of mainPortalScreen
        else if (isFadeOutMainPortal)
        {
            mainPortalScreen.color = new Color(mainPortalScreen.color.r, mainPortalScreen.color.g, mainPortalScreen.color.b,
                Mathf.MoveTowards(mainPortalScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (mainPortalScreen.color.a == 0f)
            {
                isFadeOutMainPortal = false;
            }
        }

        else if (isFadeInExitPortal)
        {
            exitPortalScreen.color = new Color(exitPortalScreen.color.r, exitPortalScreen.color.g, exitPortalScreen.color.b,
                Mathf.MoveTowards(exitPortalScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (exitPortalScreen.color.a == 1f)
            {
                isFadeInExitPortal = false;
            }
        }

        else if (isFadeOutExitPortal)
        {
            exitPortalScreen.color = new Color(exitPortalScreen.color.r, exitPortalScreen.color.g, exitPortalScreen.color.b,
                Mathf.MoveTowards(exitPortalScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (exitPortalScreen.color.a == 0f)
            {
                isFadeOutExitPortal = false;
            }
        }
    }

    public void FadeInLeftPortal()
    {
        isFadeInLeftPortal = true;
    }

    public void FadeInRightPortal()
    {
        isFadeInRightPortal = true;
    }

    public void FadeInMainPortal()
    {
        isFadeInMainPortal = true;
    }

    public void FadeInExitPortal()
    {
        isFadeInExitPortal = true;
    }
}
