using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampCollision : MonoBehaviour
{
    GameObject player;
    Rigidbody ballRB;
    bool onRamp = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        ballRB = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (onRamp)
        {
            ballRB.AddForce(-Vector3.up * 50f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("Player hit the ramp");
            ballRB.drag -= 0.5f;
            onRamp = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player") Debug.Log("Player left the ramp");
        ballRB.drag += 0.5f;
        onRamp = false;
    }
}
