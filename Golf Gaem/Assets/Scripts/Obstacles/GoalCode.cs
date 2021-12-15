using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GoalCode : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public string nextSceneName;
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("ControlPoint");
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                ParManager.lobbyScore = ControlPoint.shotCount;
                ControlPoint.shotCount = 0;
                Debug.Log("Lobby Score: " + ParManager.lobbyScore);
                ParManager.shotLimit = 50;
                ParManager.lobbyCollected = Collect.theScore;
                break;
            case 2:
                ParManager.m1Score = ControlPoint.shotCount;
                ControlPoint.shotCount = 0;
                Debug.Log("M1 Score: " + ParManager.m1Score);
                ParManager.shotLimit = 35;
                ParManager.m1Collected = Collect.theScore;
                break;
            case 3:
                ParManager.m2Score = ControlPoint.shotCount;
                ControlPoint.shotCount = 0;
                Debug.Log("M2 Score: " + ParManager.m2Score);
                ParManager.shotLimit = 20;
                ParManager.m2Collected = Collect.theScore;
                break;
            default:
                Debug.LogWarning("Something is wrong with the par counter. Are the scene's buildIndex values correct?");
                break;
        }
        player.GetComponent<ControlPoint>().ball.velocity = Vector3.zero;
        StartCoroutine(AsyncLoad(nextSceneName));
        
        IEnumerator AsyncLoad (string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            loadScreen.SetActive(true);
            
            while(!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                slider.value = progress;
                yield return null;
            }
        }
    }
}
