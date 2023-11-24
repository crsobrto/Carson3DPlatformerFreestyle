using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightToLeft : MonoBehaviour
{
    public float speed;
    public float xLeftBound;
    public float xRightBound;

    private Vector3 direction = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x <= xRightBound)
        {
            direction = Vector3.left;
        }
        else if (transform.position.x >= xLeftBound)
        {
            direction = Vector3.right;
        }
    }
}
