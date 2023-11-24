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

        if (fade.isFadeInLeftPortal)
        {
            fade.leftPortalScreen.color = new Color(fade.leftPortalScreen.color.r, fade.leftPortalScreen.color.g, fade.leftPortalScreen.color.b, 255f);
            fade.FadeOutLeftPortal();
        }
        else if (fade.isFadeInRightPortal)
        {
            fade.rightPortalScreen.color = new Color(fade.rightPortalScreen.color.r, fade.rightPortalScreen.color.g, fade.rightPortalScreen.color.b, 255f);
            fade.FadeOutRightPortal();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
