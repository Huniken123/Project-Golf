using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public string windAxis = "y";
    public float fanCap;
    public float speed;
    bool validDir = false;
    
    Transform origin;

    void Start()
    {
        origin = gameObject.transform.Find("FanOrigin");
        Debug.Log(origin.name);

        switch (windAxis)
        {
            case "x": case "y": case "z":
                validDir = true;
                break;
            default:
                Debug.LogError("Invalid wind direction! Must be x y or z");
                validDir = false;
                break;
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        fanCode(obj);
    }
    void OnTriggerStay(Collider obj)
    {
        fanCode(obj);
    }

    void fanCode(Collider obj)
    {
        Vector3 distVect = (obj.transform.position - gameObject.transform.position);
        Rigidbody objBody = obj.GetComponent<Rigidbody>();
        Vector3 dirVect = Vector3.zero;
        string forceName =  "unknown";
        float fanForce;
        float dist = 0;

        if (validDir)
        {
            switch (windAxis)
            {
                case "x":
                    dist = distVect.x;
                    dirVect = transform.right;
                    forceName = "x force";
                    break;
                case "y":
                    dist = distVect.y;
                    dirVect = transform.up;
                    forceName = "y force";
                    break;
                case "z":
                    dist = distVect.z;
                    dirVect = transform.forward;
                    forceName = "z force";
                    break;
                default:
                    validDir = false;
                    break;
            }

            dist = Mathf.Abs(dist);
            fanForce = speed / (dist * dist);
            if (fanForce > fanCap) fanForce = fanCap;
            objBody.AddForce(dirVect * fanForce);
            Debug.Log(forceName + " " + fanForce);
        }
    }
}
