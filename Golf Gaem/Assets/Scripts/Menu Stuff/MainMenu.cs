using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MMenu;
    public GameObject OMenu;
    public GameObject HTP;
    public GameObject Credits;

    public void PrePlay()
    {
        MMenu.SetActive(false);
        HTP.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("LOBBY");
    }
    
    public void HTPBack()
    {
        MMenu.SetActive(true);
        HTP.SetActive(false);
    }

    public void CreditsMenu()
    {
        MMenu.SetActive(false);
        Credits.SetActive(true);
    }

    public void CreditsBack()
    {
        MMenu.SetActive(true);
        Credits.SetActive(false);
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
