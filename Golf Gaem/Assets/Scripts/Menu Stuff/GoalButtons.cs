using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalButtons : MonoBehaviour
{
    public bool isRestart;
    public bool isMainMenu;
    public bool isQuit;
    public Text lobbyText, m1Text, m2Text;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        lobbyText.text = "Lobby Score: " + ParManager.lobbyScore;
        m1Text.text = "Maintenance 1 Score: " + ParManager.m1Score;
        m2Text.text = "Maintenance 2 Score: " + ParManager.m2Score;
    }

    public void OnMouseUp()
    {
        if (isRestart)
        {
            ParManager.lobbyScore = 0;
            ParManager.m1Score = 0;
            ParManager.m2Score = 0;
            SceneManager.LoadScene(1);
        }

        if (isMainMenu)
        {
            ParManager.lobbyScore = 0;
            ParManager.m1Score = 0;
            ParManager.m2Score = 0;
            SceneManager.LoadScene(0);
        }

        if (isQuit)
        {
            Application.Quit();
        }
    }
}
