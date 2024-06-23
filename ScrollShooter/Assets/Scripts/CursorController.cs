using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    private void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SwitchingCursor(false);
        }
        else
        {
            SwitchingCursor(true);
        }
       
    }
    public void SwitchingCursor(bool isPausePanel)
    {

        if (!isPausePanel)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}


