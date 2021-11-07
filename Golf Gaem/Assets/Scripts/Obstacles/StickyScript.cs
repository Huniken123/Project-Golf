using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyScript : MonoBehaviour
{
    Rigidbody objbody;

    public float offSec;
    bool active = true;
    
    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Stuck!");
        if (active)
        {
            objbody = obj.GetComponent<Rigidbody>();
            objbody.isKinematic = true;
        }
    }

    private void OnTriggerExit()
    {
        StartCoroutine(StickDelay());
    }

    private IEnumerator StickDelay()
    {
        active = false;
        yield return new WaitForSeconds(offSec);
        active = true;
    }
}
