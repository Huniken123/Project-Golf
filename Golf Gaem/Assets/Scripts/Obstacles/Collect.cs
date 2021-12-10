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

    public AudioClip collectSound;



    void Awake()
    {
        theScore = 0;
        totalCollect = 0;
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
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        //yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
