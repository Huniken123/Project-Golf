using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    #region Variables

    [Header("Camera:")]
    float xRot, yRot = 0f;
    public float rotationSpeed = 2f;

    [Header("Ball and Shooting:")]
    public Rigidbody ball;
    internal Renderer rend;
    internal float shootPower;
    Vector3 ballLastShot;                  // stores respawn point
    [SerializeField] float killboxY = -10; // change this depending on level, also maybe get rid of eventually because this is a bad system
    Vector3 dragStartPos, dragReleasePos;  // start and end points of where the cursor is to calculate shootPower (v3s to make world camera space work)
    bool isShooting, isShot;               // Tobey -  Checks if the player is shooting or has been shot
    int shotCount = 0;                     // keeps track of how many times the ball was shot. Does nothing currently

    // TODO: Make right click pretend the y value is way lower than it actually is so that you can do a proper arc shot

    [Header("Ball trajectory UI:")]
    internal LineRenderer line;
    public float lineLength = 4f;          // make this scale based on shot power

    #endregion

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;     // Tobey - Confines the cursor into the window
        isShooting = false; isShot = false;
        line = GetComponent<LineRenderer>();
        rend = ball.GetComponent<Renderer>();
        line.enabled = false;
    }

    private void Update()
    {
        transform.position = ball.position;
        #region Camera Movement
        xRot += Input.GetAxis("Mouse X") * rotationSpeed;
        if (!isShooting) yRot += Input.GetAxis("Mouse Y") * rotationSpeed;
        // lock camera y axis while shooting
        if (yRot < -30f) yRot = -30f;
        if (yRot > 30f) yRot = 30f;
        transform.rotation = Quaternion.Euler(yRot, xRot, 0f); // cam control
        ball.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);     //  Tobey - Sets the Y axis of the ball to the Y axis of the Control Point
        #endregion

        if (Input.GetMouseButtonDown(0) && !isShooting && ball.velocity == Vector3.zero) DragStart();
        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * lineLength);
            dragReleasePos = Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, Camera.main.nearClipPlane + 7f));
            shootPower = (dragStartPos.y - dragReleasePos.y);
            if (shootPower < 0) shootPower = 0;
            if (shootPower >= 5) shootPower = 5;
        }

        if (ball.velocity == new Vector3(0, 0, 0)) rend.material.color = Color.white;
        else rend.material.color = Color.black; // visual way of showing if the player can hit the ball or not

        //if (isShooting) Debug.Log("isShooting is true");
        //else Debug.Log("isShooting is not true");

        Debug.Log("isShooting: " + isShooting);
        Debug.Log("isShot: " + isShot);


        //  Tobey - This is a workaround for releasing the button in Update and Fixed Update
        if (Input.GetMouseButtonUp(0) && ball.velocity == Vector3.zero) isShot = true;

        if (ball.position.y <= killboxY)
        {
            ball.position = ballLastShot;
            Debug.LogWarning("Ball out of bounds, respawning at " + ballLastShot);
            ball.velocity = new Vector3(0,0,0);
        }

        #region Tobey's ball trajectory code that doesn't work (might get rid of this)
        //  Tobey - This is code related to the Line Trajectory UI. I'd recommend commenting it out as it doesn't fully work at the moment.
        /*#region Line Trajectory Code
        Vector3 forceV = ball.velocity * shootPower;

        if(Input.GetMouseButton(0))
        {
            LineTrajectory.Instance.UpdateTrajectory(forceVector: forceV, ball, startingPoint: transform.position);
        }*/
        #endregion
    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButtonDown(0) && isShooting)
        {
            DragRelease();
        }
    }

    void DragStart()    //  Tobey - Sets up the properties for dragging the ball
    {
        Cursor.lockState = CursorLockMode.Confined;
        dragStartPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, Camera.main.nearClipPlane + 7f));
        ballLastShot = ball.transform.position;
        //Debug.Log("Ball spawn stored. Location: " + ballLastShot);
        isShooting = true;
        //Debug.Log("Drag start position: " + dragStartPos.y);
        line.enabled = true;
    }

    void DragRelease()  //  Tobey - Sets up the properties for releasing the ball
    {
        if (isShot)
        {
            isShooting = false;
            ball.velocity = new Vector3(0,0,0); // Tobey - Fixes a glitch and also stops any ball momentum with a single click. Potentially good for precise platforming if worked on more.
            // maybe keep this in for space level
        }

        if(isShooting == false)
        {
            if (shootPower >= 1)
            {
                ball.isKinematic = false;
                ball.AddForce(transform.forward * (shootPower * 300));
                if (shootPower < 5) Debug.Log("Shot power: " + shootPower);
                else Debug.Log("Max shot power");
                shotCount++;
            }
            else Debug.Log("Shot wasn't strong enough");

            shootPower = 0f;
            line.enabled = false;
            isShot = false;

            Cursor.lockState = CursorLockMode.Locked;

            //Debug.Log("Ball released: " + Input.mousePosition);

            //Debug.Log("Drag release position: " + dragReleasePos);

            //  Tobey - Prevents numbers from being stored after the equation
            dragReleasePos = new Vector3(0, 0, 0); dragStartPos = new Vector3(0, 0, 0);
        }
    }
}
