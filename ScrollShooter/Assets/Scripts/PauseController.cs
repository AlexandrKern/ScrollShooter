using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPause;
    public GameObject pausePanel;
    public GameObject gamePanel;
    public CursorController cursorController;

    private void Start()
    {
        isPause = false;
    }
    private void Update()
    {
        if (pausePanel!=null||gamePanel!=null)
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
      
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        isPause = false;
        if (cursorController!=null)
        {
            cursorController.SwitchingCursor(isPause);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPause = true;
        if (cursorController != null)
        {
            cursorController.SwitchingCursor(isPause);
        }
    }
}
