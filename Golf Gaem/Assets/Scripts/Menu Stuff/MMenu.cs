using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject OMenu;
    public GameObject HTP;
    public GameObject Credits;
    public GameObject loadScreen;
    public Slider slider;

    public void PrePlay()
    {
        mainMenu.SetActive(false);
        HTP.SetActive(true);
    }

    public void PlayGame()
    {
        StartCoroutine(AsyncLoad("LOBBY"));
    }

    public void HTPBack()
    {
        mainMenu.SetActive(true);
        HTP.SetActive(false);
    }

    public void CreditsMenu()
    {
        mainMenu.SetActive(false);
        Credits.SetActive(true);
    }

    public void CreditsBack()
    {
        mainMenu.SetActive(true);
        Credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        OMenu.SetActive(true);
    }
    
    public void OptionsBack()
    {
        mainMenu.SetActive(true);
        OMenu.SetActive(false);
    }

    public void InvertMouseX()
    {
        Settings.invertMouseX = !Settings.invertMouseX;
        Debug.Log(Settings.invertMouseX);
    }

    public void InvertMouseY()
    {
        Settings.invertMouseY = !Settings.invertMouseY;
        Debug.Log(Settings.invertMouseY);
    }

    IEnumerator AsyncLoad(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }

}
