using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Reward reward = new Reward();
    public Shop shop = new Shop();

    public BaitInventory inventory = new BaitInventory();
    public PlayerStats playerStats = new PlayerStats();

    public List<Location> allLocations;
}