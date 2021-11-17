using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;
    public List<GameObject> collectList = new List<GameObject>();
    public int totalCollect;

    public AudioSource collectSound;

    void Start()
    {
        foreach (GameObject collectableObjs in GameObject.FindGameObjectsWithTag("Collectable"))
        {
            collectList.Add(collectableObjs);
        }

        foreach (var x in collectList)
        {
            totalCollect += 1;
        }
    }

    void Update()
    {
        scoreText.GetComponent<Text>().text = "Score: " + theScore + "/" + totalCollect;
    }


    void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        theScore += 1;
        Destroy(gameObject);
    }
}
