// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/DronesCameraInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DronesCameraInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DronesCameraInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DronesCameraInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4d719194-9ec9-4dc3-b0ee-c8b95f384462"",
            ""actions"": [
                {
                    ""name"": ""AddDrone"",
                    ""type"": ""Button"",
                    ""id"": ""f45812df-a610-400c-8886-b2ab350237b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RemoveDrone"",
                    ""type"": ""Button"",
                    ""id"": ""01421d54-6ebd-4e5d-801f-d7a0d11f1726"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RemoveAllDrones"",
                    ""type"": ""Button"",
                    ""id"": ""b613f8b0-999f-4fa3-a584-bd37218afb73"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DownLeft"",
                    ""type"": ""Button"",
                    ""id"": ""b9931175-3c55-49ae-aa1a-19f7a76143c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""40bf6550-c8f5-424a-b542-1f678ca68fb2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DownRight"",
                    ""type"": ""Button"",
                    ""id"": ""558b3b4e-37ee-4984-95ea-88e630f6e3e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""5da47eaa-f01b-4216-8c61-e013105e8942"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleCentre"",
                    ""type"": ""Button"",
                    ""id"": ""ce37c8a9-3b27-42be-8ce1-e461a3ab8128"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""75e0002c-8ebf-4194-9aff-9e0797d9f6e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpLeft"",
                    ""type"": ""Button"",
                    ""id"": ""7c686e4c-2854-4666-a3b6-53105cc2872c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""d0555075-b157-464b-ba1f-ff2d7d4c25bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpRight"",
                    ""type"": ""Button"",
                    ""id"": ""3fcc86dc-8d89-4b8e-8b88-10ba2db88671"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""06d730d5-5351-4435-866e-450aa81ef164"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22921991-59db-4f59-9300-786e1288b1eb"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RemoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47b2fe59-93d4-4893-b6ea-e6f20929d6ac"",
                    ""path"": ""<Keyboard>/numpad0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RemoveAllDrones"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66ae04a1-f4eb-4fb4-93cb-f0fcf6b3d77a"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e885aec-8a67-4f93-bc9d-09c301735093"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d72a284-86e0-4f11-8d50-0b55da72c7f0"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a49a5485-4804-485e-8b80-d0a08ffcc338"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a97cfed-e0b7-422c-b99e-e109a3593fa7"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MiddleCentre"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b87092c-458a-409d-846b-bfc43a631a77"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6453d4b-0489-4b27-b755-b8b2ee1ca6ca"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d1ef743-7b37-4967-bf28-e712f9544560"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b56f2834-e8b0-44ec-a9f3-2eea05a88d62"",
                    ""path"": ""<Keyboard>/numpad9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_AddDrone = m_Player.FindAction("AddDrone", throwIfNotFound: true);
        m_Player_RemoveDrone = m_Player.FindAction("RemoveDrone", throwIfNotFound: true);
        m_Player_RemoveAllDrones = m_Player.FindAction("RemoveAllDrones", throwIfNotFound: true);
        m_Player_DownLeft = m_Player.FindAction("DownLeft", throwIfNotFound: true);
        m_Player_Down = m_Player.FindAction("Down", throwIfNotFound: true);
        m_Player_DownRight = m_Player.FindAction("DownRight", throwIfNotFound: true);
        m_Player_Left = m_Player.FindAction("Left", throwIfNotFound: true);
        m_Player_MiddleCentre = m_Player.FindAction("MiddleCentre", throwIfNotFound: true);
        m_Player_Right = m_Player.FindAction("Right", throwIfNotFound: true);
        m_Player_UpLeft = m_Player.FindAction("UpLeft", throwIfNotFound: true);
        m_Player_Up = m_Player.FindAction("Up", throwIfNotFound: true);
        m_Player_UpRight = m_Player.FindAction("UpRight", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_AddDrone;
    private readonly InputAction m_Player_RemoveDrone;
    private readonly InputAction m_Player_RemoveAllDrones;
    private readonly InputAction m_Player_DownLeft;
    private readonly InputAction m_Player_Down;
    private readonly InputAction m_Player_DownRight;
    private readonly InputAction m_Player_Left;
    private readonly InputAction m_Player_MiddleCentre;
    private readonly InputAction m_Player_Right;
    private readonly InputAction m_Player_UpLeft;
    private readonly InputAction m_Player_Up;
    private readonly InputAction m_Player_UpRight;
    public struct PlayerActions
    {
        private @DronesCameraInput m_Wrapper;
        public PlayerActions(@DronesCameraInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @AddDrone => m_Wrapper.m_Player_AddDrone;
        public InputAction @RemoveDrone => m_Wrapper.m_Player_RemoveDrone;
        public InputAction @RemoveAllDrones => m_Wrapper.m_Player_RemoveAllDrones;
        public InputAction @DownLeft => m_Wrapper.m_Player_DownLeft;
        public InputAction @Down => m_Wrapper.m_Player_Down;
        public InputAction @DownRight => m_Wrapper.m_Player_DownRight;
        public InputAction @Left => m_Wrapper.m_Player_Left;
        public InputAction @MiddleCentre => m_Wrapper.m_Player_MiddleCentre;
        public InputAction @Right => m_Wrapper.m_Player_Right;
        public InputAction @UpLeft => m_Wrapper.m_Player_UpLeft;
        public InputAction @Up => m_Wrapper.m_Player_Up;
        public InputAction @UpRight => m_Wrapper.m_Player_UpRight;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @AddDrone.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAddDrone;
                @AddDrone.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAddDrone;
                @AddDrone.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAddDrone;
                @RemoveDrone.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRemoveDrone;
                @RemoveDrone.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRemoveDrone;
                @RemoveDrone.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRemoveDrone;
                @RemoveAllDrones.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRemoveAllDrones;
                @RemoveAllDrones.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRemoveAllDrones;
                @RemoveAllDrones.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRemoveAllDrones;
                @DownLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDownLeft;
                @DownLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDownLeft;
                @DownLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDownLeft;
                @Down.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @DownRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDownRight;
                @DownRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDownRight;
                @DownRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDownRight;
                @Left.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @MiddleCentre.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMiddleCentre;
                @MiddleCentre.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMiddleCentre;
                @MiddleCentre.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMiddleCentre;
                @Right.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @UpLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpLeft;
                @UpLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpLeft;
                @UpLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpLeft;
                @Up.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @UpRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpRight;
                @UpRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpRight;
                @UpRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUpRight;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AddDrone.started += instance.OnAddDrone;
                @AddDrone.performed += instance.OnAddDrone;
                @AddDrone.canceled += instance.OnAddDrone;
                @RemoveDrone.started += instance.OnRemoveDrone;
                @RemoveDrone.performed += instance.OnRemoveDrone;
                @RemoveDrone.canceled += instance.OnRemoveDrone;
                @RemoveAllDrones.started += instance.OnRemoveAllDrones;
                @RemoveAllDrones.performed += instance.OnRemoveAllDrones;
                @RemoveAllDrones.canceled += instance.OnRemoveAllDrones;
                @DownLeft.started += instance.OnDownLeft;
                @DownLeft.performed += instance.OnDownLeft;
                @DownLeft.canceled += instance.OnDownLeft;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @DownRight.started += instance.OnDownRight;
                @DownRight.performed += instance.OnDownRight;
                @DownRight.canceled += instance.OnDownRight;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @MiddleCentre.started += instance.OnMiddleCentre;
                @MiddleCentre.performed += instance.OnMiddleCentre;
                @MiddleCentre.canceled += instance.OnMiddleCentre;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @UpLeft.started += instance.OnUpLeft;
                @UpLeft.performed += instance.OnUpLeft;
                @UpLeft.canceled += instance.OnUpLeft;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @UpRight.started += instance.OnUpRight;
                @UpRight.performed += instance.OnUpRight;
                @UpRight.canceled += instance.OnUpRight;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnAddDrone(InputAction.CallbackContext context);
        void OnRemoveDrone(InputAction.CallbackContext context);
        void OnRemoveAllDrones(InputAction.CallbackContext context);
        void OnDownLeft(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnDownRight(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnMiddleCentre(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnUpLeft(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnUpRight(InputAction.CallbackContext context);
    }
}
