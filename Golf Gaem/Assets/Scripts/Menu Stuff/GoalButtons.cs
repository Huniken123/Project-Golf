using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalButtons : MonoBehaviour
{
    public Text lobbyText, m1Text, m2Text;
    public Text lobbyColText, m1ColText, m2ColText;

    int lobbyPar = 3;
    int m1Par = 20;
    int m2Par = 8;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        lobbyText.text = "Lobby Score: " + ParManager.lobbyScore;
        if (ParManager.lobbyScore <= lobbyPar) lobbyText.color = Color.yellow;
        m1Text.text = "Maintenance 1 Score: " + ParManager.m1Score;
        if (ParManager.m1Score <= m1Par) m1Text.color = Color.yellow;
        m2Text.text = "Maintenance 2 Score: " + ParManager.m2Score;
        if (ParManager.m2Score <= m2Par) m2Text.color = Color.yellow;

        lobbyColText.text = "Collectibles: " + ParManager.lobbyCollected + "/" + ParManager.lobbyColTotal;
        if (ParManager.lobbyCollected == ParManager.lobbyColTotal) lobbyColText.color = Color.yellow;
        m1ColText.text = "Collectibles: " + ParManager.m1Collected + "/" + ParManager.m1ColTotal;
        if (ParManager.m1Collected == ParManager.m1ColTotal) m1ColText.color = Color.yellow;
        m2ColText.text = "Collectibles: " + ParManager.m2Collected + "/" + ParManager.m2ColTotal;
        if (ParManager.m2Collected == ParManager.m2ColTotal) m2ColText.color = Color.yellow;
    }

    public void GoalRestart()
    {
        ParManager.lobbyScore = 0;
        ParManager.m1Score = 0;
        ParManager.m2Score = 0;
        SceneManager.LoadScene(1);
    }

    public void GoalMMenu()
    {
        ParManager.lobbyScore = 0;
        ParManager.m1Score = 0;
        ParManager.m2Score = 0;
        SceneManager.LoadScene(0);
    }

    public void GoalQuit()
    {
        Application.Quit();
    }
}
