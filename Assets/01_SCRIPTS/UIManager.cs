using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    //[Header("Panels")]
    #region Panels
    public GameObject startPanel;
    public GameObject rewardPanel;
    public GameObject rewardButton;
    public GameObject firstTrapsButton;
    public GameObject shopPanel;
    public GameObject inventoryPanel;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject optionPanel;
    public GameObject creditsPanel;
    #endregion

    public float locationDetectionRange;
    [SerializeField]
    LayerMask locationLayer = -1;
    Location closestLocation;
    [HideInInspector]
    public Location selectedLocation;
    [HideInInspector]
    public bool inventoryOpened;

    public float timeBetweenBaits;
    void Start()
    {
        preview.InitPreview();
    }
    void Update()
    {
        if(baitManager.cooldownTimer > 0)
        {
            baitManager.cooldownTimer -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if(inventoryOpened == true)
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
                    if (closestLocation.occupied == false)
                    {
                        selectedLocation = closestLocation;
                    }
                }
            }
            preview.MovePreview(selectedLocation,  baitManager.baitRotation, inventory.selection.baitPrefab.GetComponent<MeshFilter>().sharedMesh);
            UIManager.Instance.inventory.selection.UpdatePreviewMesh();
        }
    }

    public void AddFirstTraps()
    {
        reward.AddOrUpgradeBait(BaitType.PaperBoy, 10);
        reward.AddOrUpgradeBait(BaitType.FruitBox, 10);
        reward.AddOrUpgradeBait(BaitType.Sign, 10);
        inventory.SwitchBaitSelection(true);
        GameManager.Instance.EventStartWave();
        GameManager.Instance.gameState.SetPause(false);
        GameManager.Instance.gameState.start = true;
        rewardButton.SetActive(true);
        rewardPanel.SetActive(false);
        firstTrapsButton.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void AddReward()
    {
        reward.AddOrUpgradeBait(reward.loots[reward.loots.Count - 1], 10);
        reward.loots.Clear();
        GameManager.Instance.EventStartWave();
        rewardPanel.SetActive(false);
        GameManager.Instance.gameState.SetPause(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
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

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        MenuBaseState(false);
        GameManager.Instance.gameState.start = false;
        GameManager.Instance.gameState.SetPause(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void MenuBaseState(bool state)
    {
        switch (state)
        {
            case true://Lance la partie
                startPanel.SetActive(false);
                rewardPanel.SetActive(true);
                firstTrapsButton.SetActive(true);
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
        creditsPanel.SetActive(false);
    }

    void OpenWinPanel()
    {
        winPanel.SetActive(true);
        GameManager.Instance.gameState.SetPause(true);
        GameManager.Instance.gameState.start = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OpenLosePanel()
    {
        losePanel.SetActive(true);
        GameManager.Instance.gameState.SetPause(true);
        GameManager.Instance.gameState.start = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void OnEnable()
    {
        InputEvents.Instance.SwitchSelection += inventory.SwitchBaitSelection;
        InputEvents.Instance.Place += baitManager.PlaceTrap;
        InputEvents.Instance.Rotate += baitManager.RotateSelectedBait;
        InputEvents.Instance.OpenInventory += inventory.OpenInventory;
        InputEvents.Instance.SetPause += Pause;
        InputEvents.Instance.SetPause += CloseShop;
        GameManager.Instance.Win += OpenWinPanel;
        GameManager.Instance.Lose += OpenLosePanel;
    }
    void OnDisable()
    {
        InputEvents.Instance.SwitchSelection -= inventory.SwitchBaitSelection;
        InputEvents.Instance.Place -= baitManager.PlaceTrap;
        InputEvents.Instance.Rotate -= baitManager.RotateSelectedBait;
        InputEvents.Instance.OpenInventory -= inventory.OpenInventory;
        InputEvents.Instance.SetPause -= Pause;
        InputEvents.Instance.SetPause -= CloseShop;
        GameManager.Instance.Win -= OpenWinPanel;
        GameManager.Instance.Lose -= OpenLosePanel;
    }
}
