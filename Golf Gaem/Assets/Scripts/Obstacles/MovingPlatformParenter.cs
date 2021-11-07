using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformParenter : MonoBehaviour
{
    internal GameObject ball;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball) ball.transform.parent.parent = transform;
        ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ball)
        {
            ball.transform.parent.parent = null;
        }
    }
}
