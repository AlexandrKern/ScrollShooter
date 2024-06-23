using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static int score;
    public Text staticText;
    public Text scoreText;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    public float normalFontSize = 14f;
    public float enlargedFontSize = 20f;
    public float animationDuration = 0.5f; 
    public float scoreUpdateInterval = 0.01f; 

    private int displayedScore;
    private Coroutine scoreCoroutine;

    void Start()
    {
        UpdateScoreText();
        staticText.text = "S C O R E:";
    }

    public void AddScore(int points)
    {

        score += points;
        if (scoreCoroutine == null)
        {
            scoreCoroutine = StartCoroutine(UpdateScore());
        }
        UpdateScoreText();
    }

    private IEnumerator UpdateScore()
    {
        while (displayedScore < score)
        {
            displayedScore++;
           
            UpdateScoreText();

            StartCoroutine(AnimateScore());

            yield return new WaitForSeconds(scoreUpdateInterval); 
        }

        scoreCoroutine = null; 
    }

    private IEnumerator AnimateScore()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            scoreText.fontSize = (int)Mathf.Lerp(normalFontSize, enlargedFontSize, t);
            scoreText.color = Color.Lerp(normalColor, highlightColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            scoreText.fontSize = (int)Mathf.Lerp(enlargedFontSize, normalFontSize, t);
            scoreText.color = Color.Lerp(highlightColor, normalColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = displayedScore.ToString();
            
        }
    }
}
