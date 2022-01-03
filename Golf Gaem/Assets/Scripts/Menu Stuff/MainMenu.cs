using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MMenu;
    public GameObject OMenu;
    public GameObject HTP;
    public GameObject Credits;
    public GameObject loadScreen;
    public Slider slider;

    public static bool invertMouseX = false;
    public static bool invertMouseY = false;
    public static bool flipMouseDrag = false;
    public static float mouseSensitivity;

    public void PrePlay()
    {
        MMenu.SetActive(false);
        HTP.SetActive(true);
    }

    public void PlayGame()
    {
        StartCoroutine(AsyncLoad("LOBBY"));
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

    public void InvertMouseX()
    {
        invertMouseX = !invertMouseX;
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
