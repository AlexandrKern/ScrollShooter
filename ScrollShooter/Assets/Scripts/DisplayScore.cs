using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        scoreText.text = "S C O R E:   " + PlayerScore.score;
    }
}
