using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    float xRot, yRot = 0f;

    public Rigidbody ball;

    public float rotationSpeed = 2f;
    [SerializeField]float shootPower;
    public float lineLength = 4f;
    //dragback strength edit the latter two vars

    public LineRenderer line;

    float dragStartPos;     // Tobey - The Y position of where the player pressed the screen
    float dragReleasePos;   // Tobey - The Y position of where the player released the screen

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

        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            DragStart();

            Debug.Log("ball pressed: " + Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isShot = true;
        }

        //LineRendererUpdate();
    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButtonDown(0) && isShooting)
        {
            //ball.velocity = transform.forward * shootPower;
            //line.gameObject.SetActive(false);
            DragRelease();
            
        }
    }

    void DragStart()
    {
        dragStartPos = Input.mousePosition.y;
        isShooting = true;
    }

    void DragRelease()
    {
        if (isShot)
        {
            isShooting = false;
            ball.velocity = new Vector3(0,0,0);
        }


        if(isShooting == false)
        {
            dragReleasePos = Input.mousePosition.y;
            shootPower = (dragStartPos - dragReleasePos) / 10;
            ball.velocity = (transform.forward * (shootPower));
            isShot = false;
            Debug.Log("ball released: " + Input.mousePosition);
            Debug.Log(shootPower);

            dragStartPos = 0;
            dragReleasePos = 0;
        }
    }

    /*
    void LineRendererUpdate()
    {
        Vector3 forceInit = new Vector3(0, Input.mousePosition.y - dragStartPos / 10, 0);
        Vector3 forceV = (new Vector3(-forceInit.y, forceInit.y, 0) * shootPower);

        if (Input.GetMouseButton(0))
        {
            LineTrajectory.Instance.UpdateTrajectory(forceVector: forceV, rigidbody: ball, startingPoint: ball.transform.position);
        }

        
        if (Input.mousePosition.y > dragStartPos + 3.5f)
        {
            LineTrajectory.Instance.HideLine();
        }
        
    }
    */
}
