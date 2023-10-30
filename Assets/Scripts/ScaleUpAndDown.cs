using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpAndDown : MonoBehaviour
{
    private Vector3 scaleUp = new Vector3(0, 1.0f, 0);

    public bool scale;
    public int i;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {

        if (scale)
        {
            ScaleUp();
        }
        else
        {
            ScaleDown();
        }

    }

    private void ScaleUp()
    {
        gameObject.transform.localScale += scaleUp * Time.deltaTime;
    }

    private void ScaleDown()
    {
        gameObject.transform.localScale += (scaleUp * -1) * Time.deltaTime;
    }

    IEnumerator Wait() // Potential stack overflow
    {
        scale = true;
        yield return new WaitForSeconds(7);
        scale = false;
        yield return new WaitForSeconds(7);
        StartCoroutine(Wait());
    }
}
