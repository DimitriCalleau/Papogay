using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEvents : MonoBehaviour
{
    InputPapogay playerInputs;
    private static InputEvents instance = null;
    // Game Instance Singleton
    public static InputEvents Instance
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

        playerInputs = new InputPapogay();

        playerInputs.Actions.PlaceBait.performed += ctx => OnPlaceBait();
        playerInputs.Actions.SwitchBaitRight.performed += ctx => OnSwitchSelection(true);
        playerInputs.Actions.SwitchBaitLeft.performed += ctx => OnSwitchSelection(false);
        playerInputs.Actions.RotateTrap.performed += ctx => OnRotateBait(ctx.ReadValue<Vector2>());
        playerInputs.Actions.Inventory.performed += ctx => OnOpenInventory();

        if (GameManager.Instance != null && GameManager.Instance.gameState.start == true && GameManager.Instance.gameState.pause == false)
        {

        }
        playerInputs.Actions.Escape.performed += ctx => OnPause();
    }

    //Place Bait
    public event Action Place;
    void OnPlaceBait()
    {
        if (Place != null)
        {
            Place();
        }
    }

    public event Action<Vector2> Rotate;
    void OnRotateBait(Vector2 _whichWay)
    {
        if (Rotate != null)
        {
            Rotate(_whichWay);
        }
    }

    //InventorySelection
    public event Action<bool> SwitchSelection;
    void OnSwitchSelection(bool _witchSide)
    {
        if (SwitchSelection != null)
        {
            SwitchSelection(_witchSide);
        }
    }
    //InventorySelection
    public event Action OpenInventory;
    void OnOpenInventory()
    {
        if (OpenInventory != null)
        {
            OpenInventory();
        }
    }

    public event Action SetPause;
    void OnPause()
    {
        if (SetPause != null)
        {
            SetPause();
        }
    }

    private void OnEnable()
    {
        playerInputs.Actions.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Actions.Disable();
    }
}
