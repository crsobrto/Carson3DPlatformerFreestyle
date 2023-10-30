using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;

    [SerializeField] private Vector3 offset;
    public bool useOffsetValues;
    //[SerializeField] private Vector3 offset = new Vector3(0f, 5.12f, 4.94f);

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position - offset;

        transform.LookAt(target);
    }
}
