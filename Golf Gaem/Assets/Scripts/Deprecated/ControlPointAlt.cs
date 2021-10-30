using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointAlt : MonoBehaviour
{

    //HOLY SHIT DO NOT USE BRUH NO!!!!

    float xRot, yRot = 0f;

    public Rigidbody ball;

    public float rotationSpeed = 2f;
    float shootPower;
    public float lineLength = 4f;
    [SerializeField] float shotdivider;
    //dragback strength edit the latter two vars

    public LineRenderer line;

    float dragStartPos;     // Tobey - The Y position of where the player pressed the screen
    float dragReleasePos;   // Tobey - The Y position of where the player released the screen

    //Vector3 dragStartPosV;
    //Vector3 dragReleasePosV;

    bool isShooting;        // Tobey -  Checks if the player is shooting
    bool isShot;            // Tobey - Checks if the ball has been shot

    private void Start()
    {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;     // Tobey - Confines the cursor into the window
        isShooting = false;
        isShot = false;
    }

    private void Update()
    {
        transform.position = ball.position;


        xRot += Input.GetAxis("Mouse X") * rotationSpeed;
        yRot += Input.GetAxis("Mouse Y") * rotationSpeed;
        if (yRot < -35f) yRot = -35f;
        if (yRot > 35f) yRot = 35f;
        transform.rotation = Quaternion.Euler(yRot, xRot, 0f);

        ball.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            DragStart();

            Debug.Log("ball pressed: " + Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isShot = true;
        }

    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButtonDown(0) && isShooting)
        {
            DragRelease();
        }
    }

    void DragStart()
    {
        dragStartPos = Input.mousePosition.y;
        //dragStartPosV = Input.mousePosition;
        isShooting = true;

    }

    void DragRelease()
    {
        if (isShot)
        {
            isShooting = false;
            //ball.velocity = new Vector3(0, 0, 0);
        }


        if (isShooting == false)
        {
            dragReleasePos = Input.mousePosition.y;
            //dragReleasePosV = Input.mousePosition;
            shootPower = (dragStartPos - dragReleasePos);
            ball.velocity = new Vector3(1 * shootPower / shotdivider, 0, 0);
            isShot = false;


            Debug.Log("ball released: " + Input.mousePosition);

            Debug.Log(shootPower);

            dragStartPos = 0;
            dragReleasePos = 0;
        }
    }
}
