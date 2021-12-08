using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableMove : MonoBehaviour
{
    float originalY;

    public float bobSpeed = 0.5f;

    public float rotateSpeed = 0.3f;
                                    

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * bobSpeed),
            transform.position.z);

        transform.Rotate(0, rotateSpeed, 0 * Time.deltaTime);
    }
}
