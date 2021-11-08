using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Rigidbody platform;
    [SerializeField] Transform startTransform;
    [SerializeField] Transform endTransform;
    [SerializeField] float platformSpeed;
    Vector3 direction;
    Transform destination;

    private void Awake()
    {
        SetDestination(startTransform);
    }

    private void FixedUpdate()
    {
        platform.MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime)
        {
            SetDestination(destination == startTransform ? endTransform : startTransform);
        }
    }

    void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - platform.position).normalized;
    }
}
