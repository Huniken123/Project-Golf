using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionCheck : MonoBehaviour
{
    // IN PROGRESS

    public ControlPoint controlPoint;
    BallCollisionCheck bC;

    private void Awake()
    {
        bC = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            ControlPoint.isGrounded = true;
        }
    }
}
