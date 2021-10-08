using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    float xRot, yRot = 0f;

    public Rigidbody ball;

    public float rotationSpeed = 2f;
    float shootPower;   //  Tobey - This now holds the equation for (dragStartPos - dragReleasePos / shotDivider)
    public float lineLength = 4f;
    [SerializeField] float shotdivider; //  Tobey - This is the float that is used to divide the shootPower equation

    public LineRenderer line;

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


        xRot += Input.GetAxis("Mouse X") * rotationSpeed;
        yRot += Input.GetAxis("Mouse Y") * rotationSpeed;
        if (yRot < -35f) yRot = -35f;
        if (yRot > 35f) yRot = 35f;
        transform.rotation = Quaternion.Euler(yRot, xRot, 0f);

        ball.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);     //  Tobey - Sets the Y axis of the ball to the Y axis of the Control Point

        if (Input.GetMouseButtonDown(0) && !isShooting)
        {
            DragStart();

            //Debug.Log("ball pressed: " + Input.mousePosition);
        }

        //  Tobey - This is a workaround for releasing the button in Update and Fixed Update
        if (Input.GetMouseButtonUp(0))
        {
            isShot = true;
        }

        //  Tobey - This is code related to the Line Trajectory UI. I'd recommend commenting it out as it doesn't fully work at the moment.
        #region Line Trajectory Code
        Vector3 forceV = ball.velocity * shootPower;

        if(Input.GetMouseButton(0))
        {
            LineTrajectory.Instance.UpdateTrajectory(forceVector: forceV, ball, startingPoint: transform.position);
        }
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
        dragStartPos = Input.mousePosition.y;
        isShooting = true;
        
    }

    void DragRelease()  //  Tobey - Sets up the properties for releasing the ball
    {
        if (isShot)
        {
            isShooting = false;
            ball.velocity = new Vector3(0,0,0); // Tobey - Fixes a glitch and also stops any ball momentum with a single click. Potentially good for precise platforming if worked on more.
        }


        if(isShooting == false)
        {
            dragReleasePos = Input.mousePosition.y;
            shootPower = (dragStartPos - dragReleasePos) / shotdivider;

            ball.velocity = transform.forward * shootPower;
            isShot = false;


            Debug.Log("ball released: " + Input.mousePosition);

            Debug.Log(shootPower);

            //  Tobey - Prevents numbers from being stored after the equation
            dragStartPos = 0;   
            dragReleasePos = 0;
        }
    }
}
