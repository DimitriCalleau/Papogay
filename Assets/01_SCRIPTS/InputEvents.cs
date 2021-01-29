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

        playerInputs.Actions.Place.performed += ctx => OnPlaceBait();
        playerInputs.Actions.SwitchBaitRight.performed += ctx => OnSwitchSelection(true);
        playerInputs.Actions.SwitchBaitLeft.performed += ctx => OnSwitchSelection(false);
    }

    //Place Bait
    public event Action OnPlace;
    void OnPlaceBait()
    {
        if(OnPlace != null)
        {
            OnPlace();
        }
    }
    //InventorySelection
    public event Action<bool> SwitchSelection;
    void OnSwitchSelection(bool _witchSide)
    {
        if(SwitchSelection != null)
        {
            SwitchSelection(_witchSide);
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
