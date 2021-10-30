using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointOld : MonoBehaviour
{
    float xRot, yRot = 0f;

    public Rigidbody ball;

    public float rotationSpeed = 2f;
    public float shootPower = 30f;
    public float lineLength = 4f;
    //dragback strength edit the latter two vars

    public LineRenderer line;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        transform.position = ball.position;

        xRot += Input.GetAxis("Mouse X") * rotationSpeed;
        yRot += Input.GetAxis("Mouse Y") * rotationSpeed;
        if (yRot < -35f) yRot = -35f;
        if (yRot > 35f) yRot = 35f;
        transform.rotation = Quaternion.Euler(yRot, xRot, 0f);

        if (Input.GetMouseButton(0))
        {
            line.gameObject.SetActive(true);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * lineLength);
        }
        if (Input.GetMouseButtonUp(0))
        {
            ball.velocity = transform.forward * shootPower;
            line.gameObject.SetActive(false);
        }
    }
}
