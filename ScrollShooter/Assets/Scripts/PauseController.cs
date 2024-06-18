using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPause;
    public GameObject pausePanel;
    public GameObject gamePanel;

    private void Start()
    {
        isPause = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            gamePanel.SetActive(false);
            pausePanel.SetActive(true);
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            gamePanel.SetActive(true);
            pausePanel.SetActive(false);
            PlayGame();
        }
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        isPause = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPause = true;
    }
}
