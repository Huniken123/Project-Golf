using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    ControlPoint ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<ControlPoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") ball.Respawn();
    }
}
