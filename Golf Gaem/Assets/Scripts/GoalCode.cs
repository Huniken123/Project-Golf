using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        Debug.Log("A Winner Is You");
    }
}
