using UnityEngine;

public class PauseController : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}
