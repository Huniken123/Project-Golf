using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    float xRot, yRot = 0f;
    public float rotationSpeed = 2f;

    public Rigidbody ball;

    float shootPower;   //  Tobey - This now holds the equation for (dragStartPos - dragReleasePos / shotDivider)
    [SerializeField] float shotdivider = 7; //  Tobey - This is the float that is used to divide the shootPower equation
    Vector3 ballLastShot;
    [SerializeField] float killboxY = -10; //change this depending on level

    public LineRenderer line;
    public float lineLength = 4f;

    float dragStartPos;     // Tobey - The Y position of where the player pressed the screen
    float dragReleasePos;   // Tobey - The Y position of where the player released the screen

    bool isShooting;        // Tobey -  Checks if the player is shooting
    bool isShot;            // Tobey - Checks if the ball has been shot

    private void Start()
    {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;     // Tobey - Confines the cursor into the window
        isShooting = false;     //  Tobey - Checks if the mouse button is being held down
        isShot = false;         //  Tobey - Checks if the the mouse button has been released
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

        //  Tobey - This is code related to the Line Trajectory UI. I'd recommend commenting it out as it doesn't fully work at the moment.
        /*#region Line Trajectory Code
        Vector3 forceV = ball.velocity * shootPower;

        if(Input.GetMouseButton(0))
        {
            LineTrajectory.Instance.UpdateTrajectory(forceVector: forceV, ball, startingPoint: transform.position);
        }
        #endregion*/
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

            if (shootPower >= 1) ball.velocity = transform.forward * shootPower;
            isShot = false;

            Debug.Log("Ball released: " + Input.mousePosition);

            if (shootPower >= 1) Debug.Log(shootPower);
            else Debug.Log("Shot wasn't strong enough");

            //  Tobey - Prevents numbers from being stored after the equation
            dragStartPos = 0; dragReleasePos = 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("OutOfBounds")) ball.transform.position = ballLastShot;
    }
}
