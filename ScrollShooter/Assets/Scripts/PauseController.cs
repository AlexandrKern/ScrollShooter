using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPause;
    public GameObject pausePanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!isPause)
        {
            pausePanel.SetActive(true);
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
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
