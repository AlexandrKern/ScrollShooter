using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public Text staticText; // ����� "����:"
    public Text scoreText; // ����� � ������������� �������
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    public float normalFontSize = 14f;
    public float enlargedFontSize = 20f;
    public float animationDuration = 0.5f;

    private int displayedScore = 0;
    private Coroutine scoreCoroutine;

    void Start()
    {
        UpdateScoreText();
        staticText.text = "S C O R E:"; // ������������� ����� ��� ������������ ��������
    }

    public void AddScore(int points)
    {
        score += points;
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
        }
        scoreCoroutine = StartCoroutine(AnimateScore());
    }

    private IEnumerator AnimateScore()
    {
        float elapsedTime = 0f;
        float currentFontSize = normalFontSize;
        Color currentColor = normalColor;

        while (displayedScore < score)
        {
            displayedScore++;
            UpdateScoreText();

            // �������� ��������� ������� � ����� ������
            elapsedTime = 0f;
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

            // ������� ����� � ����������� ������� � �����
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

            yield return new WaitForSeconds(0.01f); // �������� ��������
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