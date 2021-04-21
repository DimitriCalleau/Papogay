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
                    ""name"": ""SwitchBait"",
                    ""type"": ""Value"",
                    ""id"": ""4e9ccf5b-093b-4bd5-8c16-9b0185947b2a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateTrapRight"",
                    ""type"": ""Value"",
                    ""id"": ""8fb76f3a-fdd0-4c8d-9232-2d96e714515d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateTrapLeft"",
                    ""type"": ""Value"",
                    ""id"": ""a53b201a-c6d7-4178-b7b0-3815d31148dc"",
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
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ff6e6a16-d07d-4f5b-8be4-31590a246fd4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""76d5f322-2a6f-4b83-9571-92269ca30971"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCam"",
                    ""type"": ""Value"",
                    ""id"": ""0044689e-13e2-4deb-af8f-a44374165cb7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shop"",
                    ""type"": ""Button"",
                    ""id"": ""d718f7d2-6a81-4937-8248-8e2b605ea8b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""14450eb9-06f1-4e3f-8589-52e33a6ef4cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""dd0b75f7-8b32-4a6e-9fb1-78942763c766"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""812b16c9-8ffc-409b-a999-0edc44dcf1be"",
                    ""path"": ""<Mouse>/leftButton"",
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
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchBait"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""818eb4d1-cb25-48b1-a55a-bc4cde9a96f5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTrapRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3517cf6-1ca2-4bc1-a160-a6c274b1e0ae"",
                    ""path"": ""<Mouse>/rightButton"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""19a73597-fa87-4b4b-b3c1-0b899a74de2e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ZQSD"",
                    ""id"": ""b1c7f9fc-9193-4bbd-b2bc-0bee26ba4782"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""55527adf-b2d2-470d-8d07-7b8bd580d29f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0a57cc0c-55b2-477f-9be7-7069b97d112f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f9b5c270-d0de-486a-a58a-5a5338edf07d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1fc77cab-784e-410c-a3f5-d75a76f28d4f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""93c43abb-66fd-4c94-9d71-b5a7ccea3f3f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7070ed4-3ba3-4f4e-9cf2-0410735f1f89"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""637188b9-8fab-4ddc-9b2e-2b4f28fd8c4e"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73aa1ce0-9abc-4b96-a40c-267e9c99b0ad"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTrapLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c487fdc-f731-4041-bc1e-96aaf32f018d"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
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
        m_Actions_SwitchBait = m_Actions.FindAction("SwitchBait", throwIfNotFound: true);
        m_Actions_RotateTrapRight = m_Actions.FindAction("RotateTrapRight", throwIfNotFound: true);
        m_Actions_RotateTrapLeft = m_Actions.FindAction("RotateTrapLeft", throwIfNotFound: true);
        m_Actions_Inventory = m_Actions.FindAction("Inventory", throwIfNotFound: true);
        m_Actions_Escape = m_Actions.FindAction("Escape", throwIfNotFound: true);
        m_Actions_Move = m_Actions.FindAction("Move", throwIfNotFound: true);
        m_Actions_Roll = m_Actions.FindAction("Roll", throwIfNotFound: true);
        m_Actions_MoveCam = m_Actions.FindAction("MoveCam", throwIfNotFound: true);
        m_Actions_Shop = m_Actions.FindAction("Shop", throwIfNotFound: true);
        m_Actions_Map = m_Actions.FindAction("Map", throwIfNotFound: true);
        m_Actions_Skip = m_Actions.FindAction("Skip", throwIfNotFound: true);
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
    private readonly InputAction m_Actions_SwitchBait;
    private readonly InputAction m_Actions_RotateTrapRight;
    private readonly InputAction m_Actions_RotateTrapLeft;
    private readonly InputAction m_Actions_Inventory;
    private readonly InputAction m_Actions_Escape;
    private readonly InputAction m_Actions_Move;
    private readonly InputAction m_Actions_Roll;
    private readonly InputAction m_Actions_MoveCam;
    private readonly InputAction m_Actions_Shop;
    private readonly InputAction m_Actions_Map;
    private readonly InputAction m_Actions_Skip;
    public struct ActionsActions
    {
        private @InputPapogay m_Wrapper;
        public ActionsActions(@InputPapogay wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlaceBait => m_Wrapper.m_Actions_PlaceBait;
        public InputAction @SwitchBait => m_Wrapper.m_Actions_SwitchBait;
        public InputAction @RotateTrapRight => m_Wrapper.m_Actions_RotateTrapRight;
        public InputAction @RotateTrapLeft => m_Wrapper.m_Actions_RotateTrapLeft;
        public InputAction @Inventory => m_Wrapper.m_Actions_Inventory;
        public InputAction @Escape => m_Wrapper.m_Actions_Escape;
        public InputAction @Move => m_Wrapper.m_Actions_Move;
        public InputAction @Roll => m_Wrapper.m_Actions_Roll;
        public InputAction @MoveCam => m_Wrapper.m_Actions_MoveCam;
        public InputAction @Shop => m_Wrapper.m_Actions_Shop;
        public InputAction @Map => m_Wrapper.m_Actions_Map;
        public InputAction @Skip => m_Wrapper.m_Actions_Skip;
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
                @SwitchBait.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBait;
                @SwitchBait.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBait;
                @SwitchBait.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBait;
                @RotateTrapRight.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrapRight;
                @RotateTrapRight.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrapRight;
                @RotateTrapRight.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrapRight;
                @RotateTrapLeft.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrapLeft;
                @RotateTrapLeft.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrapLeft;
                @RotateTrapLeft.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateTrapLeft;
                @Inventory.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInventory;
                @Escape.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEscape;
                @Move.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Roll.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRoll;
                @MoveCam.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveCam;
                @MoveCam.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveCam;
                @MoveCam.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveCam;
                @Shop.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @Shop.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @Shop.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @Map.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMap;
                @Skip.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSkip;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlaceBait.started += instance.OnPlaceBait;
                @PlaceBait.performed += instance.OnPlaceBait;
                @PlaceBait.canceled += instance.OnPlaceBait;
                @SwitchBait.started += instance.OnSwitchBait;
                @SwitchBait.performed += instance.OnSwitchBait;
                @SwitchBait.canceled += instance.OnSwitchBait;
                @RotateTrapRight.started += instance.OnRotateTrapRight;
                @RotateTrapRight.performed += instance.OnRotateTrapRight;
                @RotateTrapRight.canceled += instance.OnRotateTrapRight;
                @RotateTrapLeft.started += instance.OnRotateTrapLeft;
                @RotateTrapLeft.performed += instance.OnRotateTrapLeft;
                @RotateTrapLeft.canceled += instance.OnRotateTrapLeft;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @MoveCam.started += instance.OnMoveCam;
                @MoveCam.performed += instance.OnMoveCam;
                @MoveCam.canceled += instance.OnMoveCam;
                @Shop.started += instance.OnShop;
                @Shop.performed += instance.OnShop;
                @Shop.canceled += instance.OnShop;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);
    public interface IActionsActions
    {
        void OnPlaceBait(InputAction.CallbackContext context);
        void OnSwitchBait(InputAction.CallbackContext context);
        void OnRotateTrapRight(InputAction.CallbackContext context);
        void OnRotateTrapLeft(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnMoveCam(InputAction.CallbackContext context);
        void OnShop(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnSkip(InputAction.CallbackContext context);
    }
}
