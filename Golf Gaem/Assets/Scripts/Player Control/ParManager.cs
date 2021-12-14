using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParManager : MonoBehaviour
{
    Text text;
    public static int lobbyScore, m1Score, m2Score;
    public static int shotLimit;
    int lobbyPar = 3;
    int m1Par = 20;
    int m2Par = 8;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Shots: " + ControlPoint.shotCount + "/" + shotLimit;
    }
}
