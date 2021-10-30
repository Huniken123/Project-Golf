using UnityEngine;
using UnityEngine.UI;

public class DisplayShootPower : MonoBehaviour
{
    public Text shootPowerText;
    public ControlPoint cPoint;

    private void Start()
    {
        shootPowerText = GetComponent<Text>();
    }

    private void Update()
    {
        shootPowerText.text = cPoint.shootPower.ToString();
    }
}
