using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyScript : MonoBehaviour
{
    Rigidbody objbody;
    bool stickIgnore = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider obj)
    {
        if (!stickIgnore)
        {
            Debug.Log("Stuck!");
            objbody = obj.GetComponent<Rigidbody>();
            objbody.isKinematic = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    IEnumerator AntiBounce()
    {
        stickIgnore = true;
        yield return new WaitForSeconds(3);
        stickIgnore = false;
    }

    private void OnTriggerExit()
    {
        StartCoroutine(AntiBounce());
    }
}
