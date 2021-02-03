using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance = null;

    // Game Instance Singleton
    public static UIManager Instance
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
    }
    #endregion

    public List<GameObject> allBaits;//Tous les baits du jeu
    public List<Slot> allCurrentBaits;//Slots de l'inventaire et du shop en meme temps 

    public Reward reward = new Reward();
    public Shop shop = new Shop();
    public BaitInventory inventory = new BaitInventory();
    public BaitManager baitManager = new BaitManager();

    public GameObject inventorySlotPrefab;
    public GameObject shopSlotPrefab;

    [Header("Panels")]
    #region Panels
    public GameObject startPanel;
    public GameObject rewardPanel;
    public GameObject shopPanel;
    public GameObject inventoryPanel;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject optionPanel;
    public GameObject creditsPanel;
    #endregion
    public GameObject preview;

    Location closestLocation;
    [HideInInspector]
    public Location selectedLocation;
    [HideInInspector]
    public bool inventoryOpened;

    public float timeBetweenBaits;

    void Update()
    {
        if(baitManager.cooldownTimer > 0)
        {
            baitManager.cooldownTimer -= Time.deltaTime;
        }
        Debug.Log(GameManager.Instance.gameState.pause);
    }
    void FixedUpdate()
    {
        if(inventoryOpened == true)
        {
            float minDist = Mathf.Infinity;
            for (int i = 0; i < GameManager.Instance.allLocations.Count; i++)
            {
                float dist = Vector3.Distance(GameManager.Instance.baitManager.transform.position, GameManager.Instance.allLocations[i].transform.position);
                if (dist <= minDist)
                {
                    minDist = dist;
                    closestLocation = GameManager.Instance.allLocations[i];
                    if (closestLocation.occupied == false)
                    {
                        selectedLocation = closestLocation;
                    }
                }
            }
            baitManager.MovePreview(selectedLocation, baitManager.baitRotation);
        }
    }

    public void AddFirstTraps()
    {
        reward.AddOrUpgradeBait(allBaits[0], BaitTypes.PaperBoy, 10);
        reward.AddOrUpgradeBait(allBaits[1], BaitTypes.FruitBox, 10);
        reward.AddOrUpgradeBait(allBaits[2], BaitTypes.Sign, 10);
        inventory.SwitchBaitSelection(true);
        GameManager.Instance.EventStartWave();
    }
    public void AddReward()
    {
        reward.RewardSelection();
        reward.AddOrUpgradeBait(reward.selectedReward, reward.loots[reward.loots.Count - 1], 10);
        reward.selectedReward = null;
        reward.loots.Clear();
        GameManager.Instance.EventStartWave();
    }

    public void Play()
    {
        startPanel.SetActive(false);
        GameManager.Instance.gameState.SetPause(false);
        GameManager.Instance.gameState.start = true;
    }
    public void OpenCloseShop()
    {

    }
    public void CloseShop()
    {

    }

    public void Pause()
    {
        switch (GameManager.Instance.gameState.pause)
        {
            case true:
                Resume();
                break;
            case false:
                if(GameManager.Instance.gameState.start == true)
                {
                    GameManager.Instance.gameState.SetPause(true);
                    pausePanel.SetActive(true);
                }
                break;
        }
    }

    public void Resume()
    {
        if (GameManager.Instance.gameState.start == true)
        {
            GameManager.Instance.gameState.SetPause(false);
            pausePanel.SetActive(false);
        }
        optionPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void BackToLastPanel(GameObject panel)
    {
        panel.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.gameState.start = false;
        startPanel.SetActive(true);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    public void ResetMenus()
    {
        BackToMainMenu();
        allCurrentBaits.Clear();
    }
    void OnEnable()
    {
        InputEvents.Instance.SwitchSelection += inventory.SwitchBaitSelection;
        InputEvents.Instance.Place += baitManager.PlaceTrap;
        InputEvents.Instance.Rotate += baitManager.RotateSelectedBait;
        InputEvents.Instance.OpenInventory += inventory.OpenInventory;
        InputEvents.Instance.SetPause += Pause;
        InputEvents.Instance.SetPause += CloseShop;
    }
    void OnDisable()
    {
        InputEvents.Instance.SwitchSelection -= inventory.SwitchBaitSelection;
        InputEvents.Instance.Place -= baitManager.PlaceTrap;
        InputEvents.Instance.Rotate -= baitManager.RotateSelectedBait;
        InputEvents.Instance.OpenInventory -= inventory.OpenInventory;
        InputEvents.Instance.SetPause -= Pause;
        InputEvents.Instance.SetPause -= CloseShop;
    }
}
