using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitSound : MonoBehaviour
{
    public AudioSource tickSource;

    void Start()
    {
        tickSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            tickSource.Play();
        }
    }
}
