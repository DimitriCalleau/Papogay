using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

    // Game Instance Singleton
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        mainCam = Camera.main;
    }
    #endregion
    //changeType (Bait, Status, Corpo)
    public DefineType setTypeTo;

    public Camera mainCam;
    public GameObject player;

    public GameState gameState = new GameState();
    public MenuManager menuManager = new MenuManager();

    public WaveManager waveManager = new WaveManager();
    public FirmeBuilder builder = new FirmeBuilder();

    public PlayerStats playerStats = new PlayerStats();

    public List<Location> allLocations;
    public List<GameObject> allBaits;//Tous les baits du jeu


    #region Events
    public event Action StartWave;
    public void EventStartWave()
    {
        if (StartWave != null)
        {
            StartWave();
        }
    }

    public event Action EndWave;
    public void EventEndWave()
    {
        if (EndWave != null)
        {
            EndWave();
        }
    }

    public event Action StartGame;
    public void EventStartGame()
    {
        if (StartGame != null)
        {
            StartGame();
        }
    }

    public event Action Win;
    public void EventWin()
    {
        if (Win != null)
        {
            Win();
        }
    }

    public event Action Lose;
    public void EventLose()
    {
        if (Lose != null)
        {
            Lose();
        }
    }
    #endregion
}