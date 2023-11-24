using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitchFadeInOut : MonoBehaviour
{
    public static SceneSwitchFadeInOut Instance;

    public float fadeSpeed;

    public bool isFadeInLeftPortal = false;
    public bool isFadeOutLeftPortal = false;
    public bool isFadeInRightPortal = false;
    public bool isFadeOutRightPortal = false;

    public Image leftPortalScreen;
    public Image rightPortalScreen;
    //public static Image staticLeftPortalScreen;
    //public static Image staticRightPortalScreen;

    /*
    // Implementing data persistence
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        /*
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        leftPortalScreen = leftPortalScreen;
        staticRightPortalScreen = rightPortalScreen;
        */
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
    }

    public void FadeInLeftPortal()
    {
        isFadeInLeftPortal = true;
        //MainManager.Instance.isFadeInLeftPortal = isFadeInLeftPortal;
    }

    public void FadeOutLeftPortal()
    {
        Debug.Log("FadeOutLeftPortal was called.");
        isFadeOutLeftPortal = true;
        //MainManager.Instance.isFadeOutLeftPortal = isFadeOutLeftPortal;
    }

    public void FadeInRightPortal()
    {
        isFadeInRightPortal = true;
        //MainManager.Instance.isFadeInRightPortal = isFadeInRightPortal;

        //Debug.Log("FadeInRightPortal was called.");
    }

    public void FadeOutRightPortal()
    {
        Debug.Log("FadeOutRightPortal was called.");
        isFadeOutRightPortal = true;
        //MainManager.Instance.isFadeOutRightPortal = isFadeOutRightPortal;
    }
}
