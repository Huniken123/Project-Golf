using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MMenu;
    public GameObject OMenu;
    public GameObject HTP;

    public void PrePlay()
    {
        MMenu.SetActive(false);
        HTP.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("NLobby");
    }
    
    public void HTPBack()
    {
        MMenu.SetActive(true);
        HTP.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        MMenu.SetActive(false);
        OMenu.SetActive(true);
    }
    
    public void OptionsBack()
    {
        MMenu.SetActive(true);
        OMenu.SetActive(false);
    }
}
