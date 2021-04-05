using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
        CursorState(false);
        }
    #endregion

    public List<GameObject> allBaits;//Tous les baits du jeu
    public List<Slot> allCurrentBaits;//Slots de l'inventaire et du shop en meme temps 

    public Reward reward = new Reward();
    public Shop shop = new Shop();
    public BaitInventory inventory = new BaitInventory();
    public BaitManager baitManager = new BaitManager();

    public PreviewBait preview = new PreviewBait();

    public GameObject inventorySlotPrefab;
    public GameObject shopSlotPrefab;
    public GameObject upgradeSlotPrefab;
    public GameObject baitSpawnerPrefab;
    //[Header("Panels")]
    #region Panels
    public GameObject startPanel;
    public GameObject rewardPanel;
    public GameObject rewardButton;
    public GameObject firstTrapsButton;
    public GameObject inventoryPanel;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject optionPanel;
    public GameObject controlPanel;
    public GameObject creditsPanel;
    public GameObject mapPanel;
    #endregion

    [Header("Shop")]
    public GameObject shopPanel;
    public GameObject shopLayout;
    public GameObject upgradeLayout;
    public TextMeshProUGUI baitDescription;

    [Header("Money")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI shopGoldText;

    [Header("HealthCare")]
    public GameObject healthBarHolder;
    public Image healthBarBG;
    public Image healthBar;
    public GameObject damageIndicatorPanel;
    public GameObject deathfadePanel;
    public float healthBarClosingTime;
    float healthBarClosingTimer;

    [Header("Wavestats")]
    public TextMeshProUGUI waveIndexIndicator;
    public Image allyEntityBar;
    public Image enemyEntityBar;

    public float locationDetectionRange;
    [SerializeField]
    LayerMask locationLayer = -1;
    [HideInInspector]
    public Location closestLocation, selectedLocation;
    [HideInInspector]
    public bool inventoryOpened, shopOpened, mapOpened;

    public float timeBetweenBaits;


    public TextMeshProUGUI buildDebug;
    void Start()
    {
        preview.InitPreview();
        UIManager.Instance.CursorState(false);
    }
    void Update()
    {
        if(baitManager.cooldownTimer > 0)
        {
            baitManager.cooldownTimer -= Time.deltaTime;
        }
        goldText.text = GameManager.Instance.playerStats.gold.ToString();
        shopGoldText.text = GameManager.Instance.playerStats.gold.ToString();
        healthBar.fillAmount = GameManager.Instance.playerStats.healthPercentage;

        if (GameManager.Instance.playerStats.timerIndicator >= 0)
        {
            damageIndicatorPanel.SetActive(false);
        }
        else
        {
            GameManager.Instance.playerStats.timerIndicator -= Time.deltaTime;
        }

        if(healthBarClosingTimer > 0)
        {
            healthBarClosingTimer -= Time.deltaTime;
            CloseHealthBar();
        }
    }
    void FixedUpdate()
    {
        if (inventoryOpened == true)
        {
            Collider[] allLocations = Physics.OverlapSphere(GameManager.Instance.baitManager.transform.position, locationDetectionRange, locationLayer);

            float minDist = Mathf.Infinity;
            for (int i = 0; i < allLocations.Length; i++)
            {
                float dist = Vector3.Distance(GameManager.Instance.baitManager.transform.position, allLocations[i].transform.position);
                if (dist <= minDist)
                {
                    minDist = dist;
                    closestLocation = allLocations[i].gameObject.GetComponent<Location>();
                    if (closestLocation.state != LocationState.Occupied)
                    {
                        selectedLocation = closestLocation;
                    }
                }
            }

            if(allLocations.Length <= 0)
            {
                preview.HidePreview(false);
                selectedLocation = null;
            }
            else
            {
                preview.HidePreview(true);
            }

            if(selectedLocation != null)
            {
                preview.MovePreview(selectedLocation, baitManager.baitRotation, inventory.selection.baitPrefab.GetComponent<MeshFilter>().sharedMesh);
                UIManager.Instance.inventory.selection.UpdatePreviewMesh();
            }
        }
        shop.DetectCloseShop();
    }
     
    public void AddFirstTraps()
    {
        reward.AddBait(BaitType.MarketStand, 10, 0);
        inventory.SwitchBaitSelection(new Vector2(0, 1));
        GameManager.Instance.EventStartWave();
        GameManager.Instance.gameState.SetPause(false);
        GameManager.Instance.gameState.start = true;
        rewardButton.SetActive(true);
        rewardPanel.SetActive(false);
        firstTrapsButton.SetActive(false);
        CursorState(true);
    }
    public void AddReward()
    {
        reward.AddBait(reward.loots[GameManager.Instance.waveManager.waveindex], 10, reward.goldReward[GameManager.Instance.waveManager.waveindex]);
        shop.baitHasBeenTaken = true;
        rewardPanel.SetActive(false);
        GameManager.Instance.waveManager.IncreaseWaveIndex();
    }

    public GameObject PickBait(BaitType type)
    {
        GameObject selection = null;
        for (int i = 0; i < allBaits.Count; i++)
        {
            if (allBaits[i].GetComponent<Baits>().type == type)
            {
                selection = allBaits[i];
            }
        }

        if (selection != null)
        {
            return selection;
        }
        else
            return null;
    }
    public void Play()
    {
        allCurrentBaits.Clear();
        MenuBaseState(true);
        GameManager.Instance.playerStats.SetHealth();
        inventory.selectionIndex = 0;
        inventory.oldSelection = null;
        inventory.selection = null;
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < upgradeLayout.transform.childCount; i++)
        {
            Destroy(upgradeLayout.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < shopLayout.transform.childCount; i++)
        {
            Destroy(shopLayout.transform.GetChild(i).gameObject);
        }
    }
    public void OpenCloseShop()
    {
        switch (shopOpened)
        {
            case true:
                CloseShop();
                GameManager.Instance.gameState.SetPause(false);
                break;
            case false:
                if (shop.closeToShop == true)
                {
                    if (shop.hasNewBaitToAdd == true)
                    {
                        rewardPanel.SetActive(true);
                    }
                    shopPanel.SetActive(true);
                    CursorState(false);
                    shopOpened = true;
                    GameManager.Instance.gameState.SetPause(true); 
                    inventoryPanel.SetActive(true);
                }
                break;
        }

    }
    public void CloseShop()
    {
        if (shopOpened == true)
        {
            if (shop.hasNewBaitToAdd && shop.baitHasBeenTaken)
            {
                GameManager.Instance.EventStartWave();
                shop.hasNewBaitToAdd = false;
                shop.baitHasBeenTaken = false;
            }
            rewardPanel.SetActive(false);
            shopPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            CursorState(true);
            shopOpened = false;
        }
    }

    void SkipWave()
    {
        GameManager.Instance.EventEndWave();
        shopOpened = true;
        shop.hasNewBaitToAdd = true;
        shop.baitHasBeenTaken = true;
        AddReward(); 
        CloseShop(); 

    }

    public void OpenCloseMap()
    {
        switch (mapOpened)
        {
            case true:
                mapPanel.SetActive(false);
                mapOpened = false;
                break;
            case false:
                mapPanel.SetActive(true);
                mapOpened = true;
                break;
        }

    }

    public void OpenHealthBar()
    {
        healthBarHolder.SetActive(true);
        Color bghealthcolor = healthBarBG.color;
        bghealthcolor.a = 100;
        healthBarBG.color = bghealthcolor;
        Color healthcolor = healthBar.color;
        healthcolor.a = 100;
        healthBar.color = healthcolor;

        healthBarClosingTimer = healthBarClosingTime;
    }

    public void UpdateInventorySize()
    {
        int nbSlots = inventoryPanel.transform.childCount;
        float xOffset = inventoryPanel.GetComponent<GridLayoutGroup>().spacing.x;
        float slotSize = inventoryPanel.GetComponent<GridLayoutGroup>().cellSize.x;
        float panelHeight = inventoryPanel.GetComponent<RectTransform>().rect.height;
        float panelWidth = nbSlots * xOffset + nbSlots * slotSize + xOffset;
        inventoryPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(panelWidth, panelHeight);
    } 
    public void CloseHealthBar()
    {
        float closingTimePercentage = healthBarClosingTimer/healthBarClosingTime;

        Color bghealthcolor = healthBarBG.color;
        bghealthcolor.a = closingTimePercentage;
        healthBarBG.color = bghealthcolor;
        Color healthcolor = healthBar.color;
        healthcolor.a = closingTimePercentage;
        healthBar.color = healthcolor;

        if (healthBarClosingTimer <= 0)
        {
            healthBarHolder.SetActive(false);
        }
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
                    CursorState(false);
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
            CursorState(true);
        }
        optionPanel.SetActive(false);
        controlPanel.SetActive(false);
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
        MenuBaseState(false);
        GameManager.Instance.gameState.start = false;
        GameManager.Instance.gameState.SetPause(true);
        CursorState(false);
    }

    void MenuBaseState(bool state)
    {
        switch (state)
        {
            case true://Lance la partie
                rewardPanel.SetActive(true);
                firstTrapsButton.SetActive(true);
                startPanel.SetActive(false);
                break;
            case false://Retour au menu start
                startPanel.SetActive(true);
                rewardPanel.SetActive(false);
                firstTrapsButton.SetActive(false);
                break;
        }

        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        rewardButton.SetActive(false);
        inventoryPanel.SetActive(false);
        shopPanel.SetActive(false);
        optionPanel.SetActive(false);
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mapPanel.SetActive(false);

        deathfadePanel.SetActive(true);
        Color c = deathfadePanel.GetComponent<Image>().color;
        c.a = 0;
        deathfadePanel.GetComponent<Image>().color = c;
    }

    void OpenWinPanel()
    {
        winPanel.SetActive(true);
        GameManager.Instance.gameState.SetPause(true);
        GameManager.Instance.gameState.start = false;
        CursorState(false);
    }

    void OpenLosePanel()
    {
        losePanel.SetActive(true);
        GameManager.Instance.gameState.SetPause(true);
        GameManager.Instance.gameState.start = false;
        CursorState(false);
    }

    public void CursorState(bool lockedUnlock)
    {
        switch (lockedUnlock)
        {
            case true:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case false:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
    void OnEnable()
    {
        InputEvents.Instance.SwitchSelection += inventory.SwitchBaitSelection;
        InputEvents.Instance.Place += baitManager.PlaceTrap;
        InputEvents.Instance.RotateBait += baitManager.RotateSelectedBait;
        InputEvents.Instance.OpenInventory += inventory.OpenInventory;
        InputEvents.Instance.SetPause += Pause;
        InputEvents.Instance.OpenShop += OpenCloseShop;
        InputEvents.Instance.SetPause += CloseShop;
        InputEvents.Instance.OpenMap += OpenCloseMap;
        GameManager.Instance.Win += OpenWinPanel;
        GameManager.Instance.Lose += OpenLosePanel;
        InputEvents.Instance.Skip += SkipWave;
    }
    void OnDisable()
    {
        InputEvents.Instance.SwitchSelection -= inventory.SwitchBaitSelection;
        InputEvents.Instance.Place -= baitManager.PlaceTrap;
        InputEvents.Instance.RotateBait -= baitManager.RotateSelectedBait;
        InputEvents.Instance.OpenInventory -= inventory.OpenInventory;
        InputEvents.Instance.SetPause -= Pause;
        InputEvents.Instance.OpenShop -= OpenCloseShop;
        InputEvents.Instance.SetPause -= CloseShop;
        InputEvents.Instance.OpenMap -= OpenCloseMap;
        GameManager.Instance.Win -= OpenWinPanel;
        GameManager.Instance.Lose -= OpenLosePanel;
        InputEvents.Instance.Skip -= SkipWave;
    }
}
