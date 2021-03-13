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
        playerInputs.Actions.SwitchBait.performed += ctx => OnSwitchSelection(ctx.ReadValue<Vector2>());
        playerInputs.Actions.RotateTrapRight.performed += ctx => OnRotateBait(true);
        playerInputs.Actions.RotateTrapLeft.performed += ctx => OnRotateBait(false);
        playerInputs.Actions.Inventory.performed += ctx => OnOpenInventory();
        playerInputs.Actions.Shop.performed += ctx => OnOpenShop();
        playerInputs.Actions.Map.started += ctx => OnOpenMap();
        playerInputs.Actions.Map.canceled += ctx => OnOpenMap();

        playerInputs.Actions.MoveCam.performed += ctx => OnMoveCam(ctx.ReadValue<Vector2>()) ;
        playerInputs.Actions.MoveCam.canceled += ctx => OnMoveCam(Vector2.zero);

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

    public event Action<bool> RotateBait;
    void OnRotateBait(bool _whichWay)
    {
        if (RotateBait != null)
        {
            RotateBait(_whichWay);
        }
    }

    //InventorySelection
    public event Action<Vector2> SwitchSelection;
    void OnSwitchSelection(Vector2 _whitchWay)
    {
        if (SwitchSelection != null)
        {
            SwitchSelection(_whitchWay);
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
    }    //InventorySelection
    public event Action OpenShop;
    void OnOpenShop()
    {
        if (OpenShop != null)
        {
            OpenShop();
        }
    }
    public event Action OpenMap;
    void OnOpenMap()
    {
        if (OpenMap != null)
        {
            OpenMap();
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

    public event Action<Vector2> MoveCam;
    void OnMoveCam(Vector2 camMovement)
    {
        if (MoveCam != null)
        {
            MoveCam(camMovement);
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
