using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalButtons : MonoBehaviour
{
    public Text lobbyText, m1Text, m2Text;
    public Text lobbyColText, m1ColText, m2ColText;
    public Text gradeText;

    int goodBoyPoints = 0;
    int lobbyPar = 3;
    int m1Par = 18;
    int m2Par = 8;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        lobbyText.text = "Lobby Score: " + ParManager.lobbyScore;
        if (ParManager.lobbyScore <= lobbyPar) { lobbyText.color = Color.yellow; goodBoyPoints++; }
        m1Text.text = "Maintenance 1 Score: " + ParManager.m1Score;
        if (ParManager.m1Score <= m1Par) { m1Text.color = Color.yellow; goodBoyPoints++; }
        m2Text.text = "Maintenance 2 Score: " + ParManager.m2Score;
        if (ParManager.m2Score <= m2Par) { m2Text.color = Color.yellow; goodBoyPoints++; }

        lobbyColText.text = "Collectibles: " + ParManager.lobbyCollected + "/" + ParManager.lobbyColTotal;
        if (ParManager.lobbyCollected == ParManager.lobbyColTotal) { lobbyColText.color = Color.yellow; goodBoyPoints++; }
        m1ColText.text = "Collectibles: " + ParManager.m1Collected + "/" + ParManager.m1ColTotal;
        if (ParManager.m1Collected == ParManager.m1ColTotal) { m1ColText.color = Color.yellow; goodBoyPoints++; }
        m2ColText.text = "Collectibles: " + ParManager.m2Collected + "/" + ParManager.m2ColTotal;
        if (ParManager.m2Collected == ParManager.m2ColTotal) { m2ColText.color = Color.yellow; goodBoyPoints++; }

        switch (goodBoyPoints)
        {
            case 0:
            case 1:
            case 2:
                gradeText.text = "Try again for a better score!";
                break;
            case 3:
            case 4:
                gradeText.text = "Great job!";
                break;
            case 5:
                gradeText.text = "EXCELLENT!";
                gradeText.color = Color.yellow;
                break;
            case 6:
                gradeText.text = "How's this even possible???";
                gradeText.color = Color.yellow;
                break;
            default:
                gradeText.text = "";
                break;
        }
    
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
