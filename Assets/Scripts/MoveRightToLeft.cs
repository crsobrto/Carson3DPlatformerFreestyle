using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightToLeft : MonoBehaviour
{
    public float speed = 5.0f;
    public float moveLength; // How long the object should move in a direction
    public float moveCounter;

    // Start is called before the first frame update
    void Start()
    {
        moveCounter = moveLength;
    }

    // Update is called once per frame
    void Update()
    {
        moveCounter -= Time.deltaTime; // Count down the moveCounter

        if (moveCounter > 0f)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);

            if ((int)moveCounter == (int)-moveLength)
            {
                moveCounter = moveLength;
            }
        }
    }
}
