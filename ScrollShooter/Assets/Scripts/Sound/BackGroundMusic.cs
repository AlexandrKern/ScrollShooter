using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMusic : MonoBehaviour
{
    private int sceneIndex;
    private void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        switch (sceneIndex)
        {
            case 0:
                AudioManager.instance.PlayMusic("StartMusic");
                break;
            case 1:
                AudioManager.instance.PlayMusic("GameMusic");
                break;
            case 2:
                AudioManager.instance.PlayMusic("GameOverMusic");
                break;
            case 3:
                AudioManager.instance.PlayMusic("VictoryMusic");
                break;
            default:
                break;
        }
    }

}
