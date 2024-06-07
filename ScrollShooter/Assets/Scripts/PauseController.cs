using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPause;
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
