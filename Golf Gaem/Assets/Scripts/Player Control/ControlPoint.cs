using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    
    // camera
    float xRot, yRot = 0f;
    public float rotationSpeed = 2f;

    // ball & shooting
    public Rigidbody ball;
    float shootPower;
    float shotdivider = 7;                 // Tobey - This is the float that is used to divide the shootPower equation
    Vector3 ballLastShot;                  // stores respawn point
    [SerializeField] float killboxY = -10; // change this depending on level, also maybe get rid of eventually because this is a bad system
    float dragStartPos, dragReleasePos;    // Tobey - The Y position of where the player presses then releases on the screen
    bool isShooting, isShot;               // Tobey -  Checks if the player is shooting or has been shot

    // ball trajectory ui
    public LineRenderer line;
    public float lineLength = 4f;          // make this scale based on shot power

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;     // Tobey - Confines the cursor into the window
        isShooting = false; isShot = false;
    }

    private void Update()
    {
        transform.position = ball.position;
        #region Camera Movement
        xRot += Input.GetAxis("Mouse X") * rotationSpeed;
        if (!isShooting) yRot += Input.GetAxis("Mouse Y") * rotationSpeed;
        // lock camera y axis while shooting
        if (yRot < -35f) yRot = -35f;
        if (yRot > 35f) yRot = 35f;
        transform.rotation = Quaternion.Euler(yRot, xRot, 0f); // cam control
        #endregion

        ball.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);     //  Tobey - Sets the Y axis of the ball to the Y axis of the Control Point

        if (Input.GetMouseButtonDown(0) && !isShooting) DragStart();
        if (Input.GetMouseButtonDown(0))
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * lineLength);
        }

        //  Tobey - This is a workaround for releasing the button in Update and Fixed Update
        if (Input.GetMouseButtonUp(0)) isShot = true; line.gameObject.SetActive(false);

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
            line.gameObject.SetActive(true);
        }
    }

    void DragStart()    //  Tobey - Sets up the properties for dragging the ball
    {
        dragStartPos = Input.mousePosition.y;
        ballLastShot = ball.transform.position;
        Debug.Log("Ball spawn stored. Location: " + ballLastShot);
        isShooting = true;
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
            dragReleasePos = Input.mousePosition.y;
            shootPower = (dragStartPos - dragReleasePos) / shotdivider;

            if (shootPower >= 1)
            {
                if(ball.isKinematic==true) ball.isKinematic = false;
                ball.AddForce(transform.forward * (shootPower * 50));
            }

            isShot = false;

            Debug.Log("Ball released: " + Input.mousePosition);

            if (shootPower >= 1) Debug.Log("Shot power: " + shootPower);
            else Debug.Log("Shot wasn't strong enough");

            //  Tobey - Prevents numbers from being stored after the equation
            dragStartPos = 0; dragReleasePos = 0;
        }
    }
}
