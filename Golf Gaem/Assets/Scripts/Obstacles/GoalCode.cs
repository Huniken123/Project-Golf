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
            StartCoroutine(EndApp());
        }

        else
        {
            SceneManager.LoadScene(nextSceneName);
        }

        IEnumerator EndApp()
        {
            yield return new WaitForSeconds(1.0f);
            Application.Quit();
        }
    }
}
