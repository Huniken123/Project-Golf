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

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;    
    }

    public void OnMouseUp()
    {
        if (isRestart)
        {
            SceneManager.LoadScene(1);
        }

        if (isMainMenu)
        {
            SceneManager.LoadScene(0);
        }

        if (isQuit)
        {
            Application.Quit();
        }
    }
}
