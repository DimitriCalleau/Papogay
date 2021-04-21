using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public bool start;
    public bool pause = true;

    public void SetPause(bool pauseState)
    {
        pause = pauseState;
        switch (pauseState)
        {
            case true:
                Time.timeScale = 0;
                break;
            case false:
                Time.timeScale = 1;
                break;
        }
    }

    public void WinLose(bool winOrLose) //win true, lose false
    {
        SetPause(true);
        start = false;

        switch (winOrLose)
        {
            case true:
                UIManager.Instance.winPanel.SetActive(true);
                break;
            case false:
                UIManager.Instance.losePanel.SetActive(true);
                break;
        }
    }
}
