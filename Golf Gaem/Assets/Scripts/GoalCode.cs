using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoalCode : MonoBehaviour
{
    public bool finalGoal;
    public string nextSceneName;

    // Update is called once per frame
    void OnTriggerEnter()
    {
        if (finalGoal)
        {
            gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("A Winner Is You");
            Application.Quit();
        }

        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
