using UnityEngine;
using UnityEngine.UI;

public class DisplayVelocity : MonoBehaviour
{
    public Text velocityText;
    public ControlPoint cPoint;

    private void Start()
    {
        velocityText = GetComponent<Text>();
    }

    private void Update()
    {
        velocityText.text = cPoint.ball.velocity.ToString();
    }
}
