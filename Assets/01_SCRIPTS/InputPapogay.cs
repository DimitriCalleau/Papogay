// GENERATED AUTOMATICALLY FROM 'Assets/01_SCRIPTS/InputPapogay.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputPapogay : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputPapogay()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputPapogay"",
    ""maps"": [
        {
            ""name"": ""Actions"",
            ""id"": ""8b8115c6-6d8c-4a52-bdf0-636fdac8079f"",
            ""actions"": [
                {
                    ""name"": ""PlaceBait"",
                    ""type"": ""Button"",
                    ""id"": ""d6d697d6-fb24-4935-9304-ca5a1c1c97dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchBaitRight"",
                    ""type"": ""Button"",
                    ""id"": ""4e9ccf5b-093b-4bd5-8c16-9b0185947b2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchBaitLeft"",
                    ""type"": ""Button"",
                    ""id"": ""ecfd8a0f-23de-4438-97ec-8e4c605acac0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateTrap"",
                    ""type"": ""Value"",
                    ""id"": ""8fb76f3a-fdd0-4c8d-9232-2d96e714515d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""4c2f5bd2-e27a-478f-bcee-693307e785eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""a5eb572d-7707-4e7e-889c-ce821aab619b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""812b16c9-8ffc-409b-a999-0edc44dcf1be"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceBait"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b9dcec7-3128-4034-9afd-22216c6417a4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchBaitRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b0e3c05-96c0-4846-8907-52857d777c07"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchBaitLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""818eb4d1-cb25-48b1-a55a-bc4cde9a96f5"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTrap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3517cf6-1ca2-4bc1-a160-a6c274b1e0ae"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d19e6abe-8610-4a62-8540-385cf5249a32"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_PlaceBait = m_Actions.FindAction("PlaceBait", throwIfNotFound: true);
        m_Actions_SwitchBaitRight = m_Actions.FindAction("SwitchBaitRight", throwIfNotFound: true);
        m_Actions_SwitchBaitLeft = m_Actions.FindAction("SwitchBaitLeft", throwIfNotFound: true);
        m_Actions_RotateTrap = m_Actions.FindAction("RotateTrap", throwIfNotFound: true);
        m_Actions_Inventory = m_Actions.FindAction("Inventory", throwIfNotFound: true);
        m_Actions_Escape = m_Actions.FindAction("Escape", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Actions
    private readonly InputActionMap m_Actions;
    private IActionsActions m_ActionsActionsCallbackInterface;
    private readonly InputAction m_Actions_PlaceBait;
    private readonly InputAction m_Actions_SwitchBaitRight;
    private readonly InputAction m_Actions_SwitchBaitLeft;
    private readonly InputAction m_Actions_RotateTrap;
    private readonly InputAction m_Actions_Inventory;
    private readonly InputAction m_Actions_Escape;
    public struct ActionsActions
    {
        private @InputPapogay m_Wrapper;
        public ActionsActions(@InputPapogay wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlaceBait => m_Wrapper.m_Actions_PlaceBait;
        public InputAction @SwitchBaitRight => m_Wrapper.m_Actions_SwitchBaitRight;
        public InputAction @SwitchBaitLeft => m_Wrapper.m_Actions_SwitchBaitLeft;
        public InputAction @RotateTrap => m_Wrapper.m_Actions_RotateTrap;
        public InputAction @Inventory => m_Wrapper.m_Actions_Inventory;
        public InputAction @Escape => m_Wrapper.m_Actions_Escape;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @PlaceBait.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlaceBait;
                @PlaceBait.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlaceBait;
                @PlaceBait.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlaceBait;
                @SwitchBaitRight.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitRight;
                @SwitchBaitRight.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitRight;
                @SwitchBaitRight.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitRight;
                @SwitchBaitLeft.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitLeft;
                @SwitchBaitLeft.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitLeft;
                @SwitchBaitLeft.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitLeft;
                @RotateTrap.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrap;
                @RotateTrap.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrap;
                @RotateTrap.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrap;
                @Inventory.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInventory;
                @Escape.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEscape;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlaceBait.started += instance.OnPlaceBait;
                @PlaceBait.performed += instance.OnPlaceBait;
                @PlaceBait.canceled += instance.OnPlaceBait;
                @SwitchBaitRight.started += instance.OnSwitchBaitRight;
                @SwitchBaitRight.performed += instance.OnSwitchBaitRight;
                @SwitchBaitRight.canceled += instance.OnSwitchBaitRight;
                @SwitchBaitLeft.started += instance.OnSwitchBaitLeft;
                @SwitchBaitLeft.performed += instance.OnSwitchBaitLeft;
                @SwitchBaitLeft.canceled += instance.OnSwitchBaitLeft;
                @RotateTrap.started += instance.OnRotateTrap;
                @RotateTrap.performed += instance.OnRotateTrap;
                @RotateTrap.canceled += instance.OnRotateTrap;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);
    public interface IActionsActions
    {
        void OnPlaceBait(InputAction.CallbackContext context);
        void OnSwitchBaitRight(InputAction.CallbackContext context);
        void OnSwitchBaitLeft(InputAction.CallbackContext context);
        void OnRotateTrap(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
    }
}
