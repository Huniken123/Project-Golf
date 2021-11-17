using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallParam : MonoBehaviour
{
    Rigidbody rb;
    public float angVel;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.maxAngularVelocity = angVel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
