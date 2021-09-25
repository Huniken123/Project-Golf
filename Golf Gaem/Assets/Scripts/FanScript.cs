using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public string windAxis = "y";
    public float fanSpeed = 5;
    public float fanCap = 100;
    bool validDir = false;
    
    Transform origin;

    void Start()
    {
        origin = gameObject.transform.Find("FanOrigin");
        Debug.Log(origin.name);

        switch (windAxis)
        {
            case "x":
            case "y":
            case "z":
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
        float fanForce;
        float dist = 0;

        if (validDir)
        {
            switch (windAxis)
            {
                case "x":
                    dist = Mathf.Abs(distVect.x);
                    fanForce = fanSpeed / (dist * dist);
                    if (fanForce > fanCap) fanForce = fanCap;
                    objBody.AddForce(transform.right * fanForce);
                    Debug.Log("x force " + fanForce);
                    break;
                case "y":
                    dist = Mathf.Abs(distVect.y);
                    fanForce = fanSpeed / (dist * dist);
                    if(fanForce > fanCap)
                    { fanForce = fanCap; }
                    objBody.AddForce(transform.up * fanForce);
                    Debug.Log("y force " + fanForce);
                    break;
                case "z":
                    dist = Mathf.Abs(distVect.z);
                    fanForce = fanSpeed / (dist * dist);
                    if (fanForce > fanCap) fanForce = fanCap;
                    objBody.AddForce(transform.forward * fanForce);
                    Debug.Log("z force " + fanForce);
                    break;
                default:
                    validDir = false;
                    break;

            }
        }
    }
}
