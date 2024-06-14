using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public Text staticText; // Текст "Очки:"
    public Text scoreText; // Текст с изменяющимися цифрами
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    public float normalFontSize = 14f;
    public float enlargedFontSize = 20f;
    public float animationDuration = 0.5f; // Длительность анимации для моргания
    public float scoreUpdateInterval = 0.01f; // Интервал обновления очков

    private int displayedScore = 0;
    private Coroutine scoreCoroutine;

    void Start()
    {
        UpdateScoreText();
        staticText.text = "S C O R E:"; // Устанавливаем текст для статического элемента
    }

    public void AddScore(int points)
    {
        score += points;
        if (scoreCoroutine == null)
        {
            scoreCoroutine = StartCoroutine(UpdateScore());
        }
    }

    private IEnumerator UpdateScore()
    {
        while (displayedScore < score)
        {
            displayedScore++;
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
        float currentFontSize = normalFontSize;
        Color currentColor = normalColor;

        // Увеличение размера и изменение цвета
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            currentFontSize = Mathf.Lerp(normalFontSize, enlargedFontSize, t);
            currentColor = Color.Lerp(normalColor, highlightColor, t);
            scoreText.fontSize = (int)currentFontSize;
            scoreText.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Возвращение к нормальному размеру и цвету
        elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            currentFontSize = Mathf.Lerp(enlargedFontSize, normalFontSize, t);
            currentColor = Color.Lerp(highlightColor, normalColor, t);
            scoreText.fontSize = (int)currentFontSize;
            scoreText.color = currentColor;

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
