using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collect : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;
    public List<GameObject> collectList = new List<GameObject>();
    public int totalCollect;

    public AudioClip collectSound;

    void Awake()
    {
        theScore = 0;
        totalCollect = 0;
        scoreText = GameObject.FindWithTag("Score Text");
    }

    void Start()
    {
        //scoreText = GameObject.FindGameObjectWithTag("Score Text");

        foreach (GameObject collectableObjs in GameObject.FindGameObjectsWithTag("Collectable"))
        {
            collectList.Add(collectableObjs);
        }

        foreach (var x in collectList)
        {
            totalCollect += 1;
        }
        //Must put one extra collectable in scene somewhere uncollectable so that the script stays active.
        totalCollect -= 1;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                ParManager.lobbyColTotal = totalCollect;
                break;
            case 2:
                ParManager.m1ColTotal = totalCollect;
                break;
            case 3:
                ParManager.m2ColTotal = totalCollect;
                break;
            default:
                Debug.LogWarning("Something is wrong with the collectible counter. Are the scene's buildIndex values correct?");
                break;
        }

    }

    void Update()
    {
        scoreText.GetComponent<Text>().text = "Score: " + theScore + "/" + totalCollect;

        if (theScore == totalCollect)
        {
            Debug.Log("All collected");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("+1");
        theScore += 1;
        ControlPoint.shotCount -= 2;
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        //yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
