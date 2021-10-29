using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public string windAxis = "y";
    public float fanCap;
    public float speed;
    
    internal bool validDir = false;
    internal bool fanOn = false;

    [Header("Fan Timing")]
    public bool alwaysOn;
    public float timeOff;
    public float timeOn;

    internal ParticleSystem ParSys;
    internal Transform Origin;
    
    void Start()
    {
        Origin = gameObject.transform.Find("FanOrigin");
        ParSys = Origin.gameObject.GetComponent<ParticleSystem>();

        if (alwaysOn)
            FanActiveCode();
        else
            StartCoroutine(fanTiming());

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
        if (validDir && fanOn)
        {
            Rigidbody objBody = obj.GetComponent<Rigidbody>();
            Vector3 distVect = (obj.transform.position - gameObject.transform.position);
            Vector3 dirVect = Vector3.zero;
            string forceName = "unknown";
            float fanForce = 0;
            float dist = 0;

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

            fanForce = speed / (dist * dist);
            if (fanForce > fanCap) fanForce = fanCap;
            objBody.AddForce(dirVect * fanForce);
            Debug.Log(forceName + " " + fanForce);
        }
    }

    internal void FanActiveCode()
    {
        fanOn = true; //fan affects objects within trigger
        ParSys.Play(); //fan particle effect starts generating
    }

    internal void FanInactiveCode()
    {
        fanOn = false; //fan no longer affects objects within trigger
        ParSys.Stop(); //fan particle effect stops generating
    }

    IEnumerator fanTiming()
    {
        while (true)
        {
            FanActiveCode();
            yield return new WaitForSeconds(timeOn); //delays code for length than fan is on

            FanInactiveCode();
            yield return new WaitForSeconds(timeOff); //delays code for length fan is off
            //code loops to the start of while statement once delay has passed
        }
    }
}
