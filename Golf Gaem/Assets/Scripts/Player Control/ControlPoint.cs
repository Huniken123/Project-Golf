using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPoint : MonoBehaviour
{
    #region Variables

    [Header("Camera:")]
    float xRot, yRot = 0f;
    public float camLookSpeed = 2f;

    [Header("Ball and Shooting:")]
    public Rigidbody ball;
    Renderer ballRend;
    Vector3 dragStartPos, dragReleasePos;  // start and end points of where the cursor is to calculate shootPower (v3s to make world camera space work)
    bool isShooting, isShot;               // Tobey -  Checks if the player is shooting or has been shot
    internal float shootPower;             // force ball gets shot at (in update/UI, don't touch this)
    public int shootMult = 4;              // multiplier for shot (mess with this)
    public GameObject lossText;            // text to display when you lose

    public AudioClip[] audioClips;         //audio stuff
    public AudioSource audioSource;
    public AudioListener audioListener;

    [Header("Respawning:")]
    Vector3 ballLastShot;                  // stores respawn point
    [SerializeField] float killboxY = -10; // change this depending on level, also maybe get rid of eventually because this is a bad system
    public static int shotCount = 0;       // keeps track of how many times the ball was shot

    [Header("Ball trajectory UI:")]
    internal LineRenderer line;
    public float lineLength;
    TrailRenderer ballTrail;
    public GameObject starVFXObj;
    ParticleSystem ps;

    #endregion

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;     // Tobey - Confines the cursor into the window
        isShooting = false; isShot = false;
        line = GetComponent<LineRenderer>();
        ballRend = ball.GetComponent<Renderer>();
        ballTrail = ball.GetComponent<TrailRenderer>();
        ps = starVFXObj.GetComponent<ParticleSystem>();
        line.enabled = false;

        audioListener = GetComponent<AudioListener>();  //audio stuff
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position = ball.position;
        if (PauseMenu.GameIsPaused == false) CameraMovement();
        // look in here again for right click thing
        //Vector3 eulerRotation = transform.rotation.eulerAngles;
        //transform.rotation = Quaternion.Euler(0, eulerRotation.y, eulerRotation.z);
        // These last two lines kinda veer the shooting the way we want it but break the camera currently so I'm commenting it out
        if (shotCount < 0) { shotCount = 0; }

        #region Controls
        if (Input.GetMouseButtonDown(0) && !isShooting && ball.velocity == Vector3.zero) DragStart();
        // if (Input.GetMouseButtonDown(1) && isShooting) CancelShot();
        //  Tobey - This is a workaround for releasing the button in Update and Fixed Update
        if (Input.GetMouseButtonUp(0) && isShooting) isShot = true;
        if (Input.GetKeyDown(KeyCode.R)) Respawn();
        #endregion
    }

    private void FixedUpdate()
    {
        ShotPreview();

        if (!Input.GetMouseButtonDown(0) && isShooting)
        {
            DragRelease();
            lineLength = shootPower * 2;
        }

        if (ball.velocity.sqrMagnitude <= 0.05f && !RampCollision.onRamp)
        { 
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
            if (shotCount >= ParManager.shotLimit) StartCoroutine(LossState());
        }

        if (ball.velocity == new Vector3(0, 0, 0)) ballRend.material.color = Color.white;
        else ballRend.material.color = Color.red; // visual way of showing if the player can hit the ball or not

        if (ball.position.y <= killboxY) Respawn();
    }

    void CameraMovement()
    {
        xRot += Input.GetAxis("Mouse X") * camLookSpeed;
        if (!isShooting) yRot += Input.GetAxis("Mouse Y") * camLookSpeed;
        // lock camera y axis while shooting
        if (yRot < -25f) yRot = -25f;
        if (yRot > 30f) yRot = 30f;
        transform.rotation = Quaternion.Euler(yRot, xRot, 0f); // cam control
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
            ballTrail.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            isShot = false;
        }

        if (isShooting == false)
        {
            ball.isKinematic = false; // i think this is here for sticky wall purposes
            if (shootPower < 2.5f) { ball.AddForce(transform.forward * (shootPower * shootMult * 1.8f)); StarVFX(); PlayRandom(); }
            else if (shootPower < 2f) { ball.AddForce(transform.forward * (shootPower * shootMult * 1.6f)); StarVFX(); PlayRandom(); }
            else if (shootPower < 1.5f) { ball.AddForce(transform.forward * (shootPower * shootMult * 1.4f)); StarVFX(); PlayRandom(); }
            else if (shootPower < 1f) { ball.AddForce(transform.forward * (shootPower * shootMult * 1.2f)); StarVFX(); PlayRandom(); }
            else if (shootPower < 0.01f) { ball.AddForce(transform.forward * (shootPower * shootMult)); StarVFX(); PlayRandom(); }
            else if (shootPower >= 2.5f) { ball.AddForce(transform.forward * (shootPower * shootMult * 2f)); StarVFX(); PlayRandom(); }
            else Debug.Log("Shot wasn't strong enough");

            Debug.Log("Shot power: " + (shootPower * shootMult));

            shotCount++;

            CancelShot();
        }
    }

    void ShotPreview()
    {
        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * lineLength);
            dragReleasePos = Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, Camera.main.nearClipPlane + 7f));
            shootPower = (dragStartPos.y - dragReleasePos.y);
            if (shootPower < 0) shootPower = 0;
            if (shootPower >= 4) shootPower = 4;
        }
    }

    public void Respawn()
    {
        if (shotCount >= ParManager.shotLimit) StartCoroutine(LossState());
        else
        {
            ballTrail.enabled = false;
            ball.position = ballLastShot;
            Debug.LogWarning("Ball out of bounds, respawning at " + ballLastShot);
            ball.velocity = Vector3.zero;
            shootPower = 0;
        }
        
    }

    void CancelShot()
    {
        line.enabled = false;
        shootPower = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        dragReleasePos = new Vector3(0, 0, 0); dragStartPos = new Vector3(0, 0, 0);
        isShot = false; isShooting = false;
    }

    void StarVFX()
    {
        Debug.Log(ballLastShot);
        Instantiate(starVFXObj, ballLastShot, transform.rotation, null);
    }

    IEnumerator LossState()
    {
        lossText.SetActive(true);
        yield return new WaitForSeconds(3f);
        shotCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayRandom()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

}
