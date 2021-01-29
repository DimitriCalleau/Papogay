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
                    ""name"": ""Place"",
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
                    ""action"": ""Place"",
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
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_Place = m_Actions.FindAction("Place", throwIfNotFound: true);
        m_Actions_SwitchBaitRight = m_Actions.FindAction("SwitchBaitRight", throwIfNotFound: true);
        m_Actions_SwitchBaitLeft = m_Actions.FindAction("SwitchBaitLeft", throwIfNotFound: true);
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
    private readonly InputAction m_Actions_Place;
    private readonly InputAction m_Actions_SwitchBaitRight;
    private readonly InputAction m_Actions_SwitchBaitLeft;
    public struct ActionsActions
    {
        private @InputPapogay m_Wrapper;
        public ActionsActions(@InputPapogay wrapper) { m_Wrapper = wrapper; }
        public InputAction @Place => m_Wrapper.m_Actions_Place;
        public InputAction @SwitchBaitRight => m_Wrapper.m_Actions_SwitchBaitRight;
        public InputAction @SwitchBaitLeft => m_Wrapper.m_Actions_SwitchBaitLeft;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @Place.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlace;
                @Place.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlace;
                @Place.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlace;
                @SwitchBaitRight.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitRight;
                @SwitchBaitRight.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitRight;
                @SwitchBaitRight.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitRight;
                @SwitchBaitLeft.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitLeft;
                @SwitchBaitLeft.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitLeft;
                @SwitchBaitLeft.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchBaitLeft;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Place.started += instance.OnPlace;
                @Place.performed += instance.OnPlace;
                @Place.canceled += instance.OnPlace;
                @SwitchBaitRight.started += instance.OnSwitchBaitRight;
                @SwitchBaitRight.performed += instance.OnSwitchBaitRight;
                @SwitchBaitRight.canceled += instance.OnSwitchBaitRight;
                @SwitchBaitLeft.started += instance.OnSwitchBaitLeft;
                @SwitchBaitLeft.performed += instance.OnSwitchBaitLeft;
                @SwitchBaitLeft.canceled += instance.OnSwitchBaitLeft;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);
    public interface IActionsActions
    {
        void OnPlace(InputAction.CallbackContext context);
        void OnSwitchBaitRight(InputAction.CallbackContext context);
        void OnSwitchBaitLeft(InputAction.CallbackContext context);
    }
}
