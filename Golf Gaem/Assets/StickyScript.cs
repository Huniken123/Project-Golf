using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyScript : MonoBehaviour
{
    Rigidbody objbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Stuck!");
        objbody = obj.GetComponent<Rigidbody>();
        objbody.isKinematic = true;
    }

    private void OnTriggerExit(Collider other)
    {
        objbody.isKinematic = false;
    }
}
