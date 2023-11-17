using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Transform pivot;

    [SerializeField] private Vector3 offset;

    public bool useOffsetValues;

    public float rotationSpeed; // How fast the camera will rotate around the player

    //[SerializeField] private Vector3 offset = new Vector3(0f, 5.12f, 4.94f);

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position; // Set the pivot's location to the target's location
        pivot.transform.parent = target.transform; // Make the pivot a child of the player so it will continually move with the player

        // Lock the cursor to the center of view and hide it from the player
        Cursor.lockState = CursorLockMode.Locked; // Press Escape to bring the cursor back in the Unity Editor
    }

    void LateUpdate()
    {
        // Rotate the target about the y-axis by getting the mouse's x-position
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        target.Rotate(0, horizontal, 0);

        // Rotate the pivot about the x-axis by getting the mouse's y-position
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;
        pivot.Rotate(-vertical, 0, 0); // -vertical inverts the camera controls

        // Move the camera based on the target's or the pivot's current rotation & the original offset
        float wantedYAngle = target.eulerAngles.y;
        float wantedXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(wantedXAngle, wantedYAngle, 0);// eulerAngles is a Vector3 and is used instead of Quaternion Angles
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        // If the camera's y-position goes below the target's y-position
        if (transform.position.y < target.position.y)
        {
            // Lock the camera's y-position to slightly below the target's y-position
            transform.position = new Vector3(transform.position.x, target.position.y - 0.2f, transform.position.z);
        }

        transform.LookAt(target);
    }
}
