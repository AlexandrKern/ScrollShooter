using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static int score;
    public Text staticText; // Текст "Очки:"
    public Text scoreText; // Текст с изменяющимися цифрами
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    public float normalFontSize = 14f;
    public float enlargedFontSize = 20f;
    public float animationDuration = 0.5f; // Длительность анимации для моргания
    public float scoreUpdateInterval = 0.01f; // Интервал обновления очков

    private int displayedScore;
    private Coroutine scoreCoroutine;

    void Start()
    {
        Debug.Log("ScoreManager Start");
        UpdateScoreText();
        staticText.text = "S C O R E:"; // Устанавливаем текст для статического элемента
    }

    public void AddScore(int points)
    {
        Debug.Log("AddScore called with points: " + points);
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
            Debug.Log("Updating score: " + displayedScore);
            UpdateScoreText();

            // Анимация изменения размера и цвета текста
            StartCoroutine(AnimateScore());

            yield return new WaitForSeconds(scoreUpdateInterval); // скорость начисления очков
        }

        scoreCoroutine = null; // Остановить корутину после завершения начисления очков
    }

    private IEnumerator AnimateScore()
    {
        float elapsedTime = 0f;

        // Увеличение размера и изменение цвета
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            scoreText.fontSize = (int)Mathf.Lerp(normalFontSize, enlargedFontSize, t);
            scoreText.color = Color.Lerp(normalColor, highlightColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Возвращение к нормальному размеру и цвету
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
            Debug.Log("Score text updated to: " + displayedScore);
        }
        else
        {
            Debug.LogWarning("Score text is null!");
        }
    }
}
