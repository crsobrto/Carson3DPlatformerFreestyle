using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public bool isFadeInLeftPortal;
    public bool isFadeOutLeftPortal;
    public bool isFadeInRightPortal;
    public bool isFadeOutRightPortal;

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
}
