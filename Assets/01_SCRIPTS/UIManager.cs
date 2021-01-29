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

    public Reward reward = new Reward();
    public Shop shop = new Shop();
    public BaitInventory inventory = new BaitInventory();

    public GameObject inventorySlotPrefab;
    public GameObject shopSlotPrefab;
    public GameObject shopPanel;
    public GameObject inventoryPanel;
    public GameObject selectionTruc;

    public List<Slot> allCurrentBaits;//Slots de l'inventaire et du shop en meme temps 
    public void AddFirstTraps()
    {
        Debug.Log("First Traps");
        reward.AddOrUpgradeBait(GameManager.Instance.allBaits[0], BaitTypes.PaperBoy, 10);
        reward.AddOrUpgradeBait(GameManager.Instance.allBaits[1], BaitTypes.FruitBox, 10);
        reward.AddOrUpgradeBait(GameManager.Instance.allBaits[2], BaitTypes.Sign, 10);
        inventory.SwitchBaitSelection(true);
        GameManager.Instance.EventStartGame();
    }
    public void AddReward()
    {
        Debug.Log("Reward");
        reward.RewardSelection();
        reward.AddOrUpgradeBait(reward.selectedReward, reward.loots[reward.loots.Count - 1], 10);
        reward.loots.Clear();
        GameManager.Instance.EventStartWave();
    }
    void OnEnable()
    {
        if (InputEvents.Instance != null)
        {
            InputEvents.Instance.SwitchSelection += inventory.SwitchBaitSelection;
        }
    }
    void OnDisable()
    {
        InputEvents.Instance.SwitchSelection -= inventory.SwitchBaitSelection;
    }
}
