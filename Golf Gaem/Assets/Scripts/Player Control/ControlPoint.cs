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
    Renderer ballRend;
    Vector3 dragStartPos, dragReleasePos;  // start and end points of where the cursor is to calculate shootPower (v3s to make world camera space work)
    bool isShooting, isShot;               // Tobey -  Checks if the player is shooting or has been shot
    internal float shootPower;             // force ball gets shot at
    bool arcShot = false;                  // if true, then do arc shot. if not, don't

    [Header("Respawning:")]
    Vector3 ballLastShot;                  // stores respawn point
    [SerializeField] float killboxY = -10; // change this depending on level, also maybe get rid of eventually because this is a bad system
    int shotCount = 0;                     // keeps track of how many times the ball was shot. Does nothing currently

    [Header("Ball trajectory UI:")]
    internal LineRenderer line;
    public float lineLength;

    #endregion

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;     // Tobey - Confines the cursor into the window
        isShooting = false; isShot = false;
        line = GetComponent<LineRenderer>();
        ballRend = ball.GetComponent<Renderer>();
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
        //if (arcShot) ball.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 10);
        //else ball.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);     //  Tobey - Sets the Y axis of the ball to the Y axis of the Control Point
        #endregion
        // look in here again for right click thing

        #region Controls
        if (Input.GetMouseButtonDown(0) && !isShooting && ball.velocity == Vector3.zero) DragStart();
        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * lineLength);
            dragReleasePos = Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, Camera.main.nearClipPlane + 7f));
            shootPower = (dragStartPos.y - dragReleasePos.y);
            if (shootPower < 0) shootPower = 0;
            if (shootPower >= 4) shootPower = 4;
        }
        if (Input.GetMouseButtonDown(1) && isShooting) CancelShot();
        //  Tobey - This is a workaround for releasing the button in Update and Fixed Update
        if (Input.GetMouseButtonUp(0) && isShooting) isShot = true;

        if (Input.GetKeyDown(KeyCode.Space) && isShooting && !arcShot) arcShot = true;
        if (Input.GetKeyDown(KeyCode.Space) && isShooting && arcShot) arcShot = false;
        #endregion

        if (ball.velocity.magnitude <= 0.005f) { ball.velocity = Vector3.zero; ball.angularVelocity = Vector3.zero; }

        if (ball.velocity == new Vector3(0, 0, 0)) ballRend.material.color = Color.white;
        else ballRend.material.color = Color.black; // visual way of showing if the player can hit the ball or not

        if (ball.position.y <= killboxY) Respawn();
    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButtonDown(0) && isShooting)
        {
            DragRelease();
            lineLength = shootPower * 2;
        }
    }

    void DragStart()    //  Tobey - Sets up the properties for dragging the ball
    {
        Cursor.lockState = CursorLockMode.Confined;
        dragStartPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, Camera.main.nearClipPlane + 7f));
        ballLastShot = ball.transform.position;
        isShooting = true;
        line.enabled = true;
        //Debug.Log("Ball spawn stored. Location: " + ballLastShot);
        //Debug.Log("Drag start position: " + dragStartPos.y);
    }

    void DragRelease()  //  Tobey - Sets up the properties for releasing the ball
    {
        if (isShot)
        {
            isShooting = false;
            ball.velocity = new Vector3(0,0,0); // Tobey - Fixes a glitch and also stops any ball momentum with a single click while trying to shoot in the air, assuming that's on
            isShot = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (isShooting == false)
        {
            if (shootPower >= 0.1f)
            {
                ball.isKinematic = false; // i think this is here for sticky wall purposes
                ball.AddForce(transform.forward * (shootPower * 400));
                if (shootPower < 3.99f) Debug.Log("Shot power: " + shootPower);
                else Debug.Log("Max shot power");
                shotCount++;
            }
            else Debug.Log("Shot wasn't strong enough");

            CancelShot();
        }
    }

    void Respawn()
    {
        ball.position = ballLastShot;
        Debug.LogWarning("Ball out of bounds, respawning at " + ballLastShot);
        ball.velocity = Vector3.zero;
        shootPower = 0;
    }

    void CancelShot()
    {
        line.enabled = false;
        shootPower = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        dragReleasePos = new Vector3(0, 0, 0); dragStartPos = new Vector3(0, 0, 0);
        arcShot = false;
        isShot = false; isShooting = false;
    }
}
