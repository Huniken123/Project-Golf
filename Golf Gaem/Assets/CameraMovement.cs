using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float xRot, yRot = 0f;
    public float camLookSpeed = 2f;
    GameObject player;
    ControlPoint cPoint;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cPoint = player.GetComponent<ControlPoint>();
    }

    void Update()
    {
        xRot += Input.GetAxis("Mouse X") * camLookSpeed;
        //if (!cPoint.isShooting)
        yRot += Input.GetAxis("Mouse Y") * camLookSpeed;
        // lock camera y axis while shooting
        if (yRot < -25f) yRot = -25f;
        if (yRot > 30f) yRot = 30f;
        transform.rotation = Quaternion.Euler(yRot, xRot, 0f); // cam control
    }
}
