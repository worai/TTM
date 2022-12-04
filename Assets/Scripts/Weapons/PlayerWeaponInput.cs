// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Weapons/PlayerWeaponInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerWeaponInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerWeaponInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerWeaponInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9c406973-b8a3-4353-8447-8b215b8d6730"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""7204ba8d-4ecc-4437-8cfb-a0f8392fd8ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""a59147a4-983e-4d48-833e-0505f303bd3b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MakeReady"",
                    ""type"": ""Button"",
                    ""id"": ""c604cf40-9a2c-4238-ad9e-cab5c66c8a73"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Weapon1"",
                    ""type"": ""Button"",
                    ""id"": ""967bc6a6-4f10-442b-83dc-486a6341d48c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon2"",
                    ""type"": ""Button"",
                    ""id"": ""61c836a1-defd-4f10-8bec-eb9a7ca448a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Punch"",
                    ""type"": ""Button"",
                    ""id"": ""4bc2d7b3-bb73-4faf-9472-97aeb3e9f290"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""02c595cd-c480-4473-8b72-0f6a821974a2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e130c08-8cc3-49bb-9a8a-42d71f2f4fb6"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ef52896-3bd4-4b85-b1b4-f30afffdbb7e"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MakeReady"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb059343-acaf-4830-8666-bc87d004ebee"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c86fa89-3b41-4d1c-bf17-951563c3d686"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e281a00b-ba16-4682-b6de-7230d137f642"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Punch"",
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
        m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_MakeReady = m_Player.FindAction("MakeReady", throwIfNotFound: true);
        m_Player_Weapon1 = m_Player.FindAction("Weapon1", throwIfNotFound: true);
        m_Player_Weapon2 = m_Player.FindAction("Weapon2", throwIfNotFound: true);
        m_Player_Punch = m_Player.FindAction("Punch", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Fire;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_MakeReady;
    private readonly InputAction m_Player_Weapon1;
    private readonly InputAction m_Player_Weapon2;
    private readonly InputAction m_Player_Punch;
    public struct PlayerActions
    {
        private @PlayerWeaponInput m_Wrapper;
        public PlayerActions(@PlayerWeaponInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @MakeReady => m_Wrapper.m_Player_MakeReady;
        public InputAction @Weapon1 => m_Wrapper.m_Player_Weapon1;
        public InputAction @Weapon2 => m_Wrapper.m_Player_Weapon2;
        public InputAction @Punch => m_Wrapper.m_Player_Punch;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MakeReady.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMakeReady;
                @MakeReady.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMakeReady;
                @MakeReady.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMakeReady;
                @Weapon1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeapon1;
                @Weapon1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeapon1;
                @Weapon1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeapon1;
                @Weapon2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeapon2;
                @Weapon2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeapon2;
                @Weapon2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeapon2;
                @Punch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPunch;
                @Punch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPunch;
                @Punch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPunch;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MakeReady.started += instance.OnMakeReady;
                @MakeReady.performed += instance.OnMakeReady;
                @MakeReady.canceled += instance.OnMakeReady;
                @Weapon1.started += instance.OnWeapon1;
                @Weapon1.performed += instance.OnWeapon1;
                @Weapon1.canceled += instance.OnWeapon1;
                @Weapon2.started += instance.OnWeapon2;
                @Weapon2.performed += instance.OnWeapon2;
                @Weapon2.canceled += instance.OnWeapon2;
                @Punch.started += instance.OnPunch;
                @Punch.performed += instance.OnPunch;
                @Punch.canceled += instance.OnPunch;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMakeReady(InputAction.CallbackContext context);
        void OnWeapon1(InputAction.CallbackContext context);
        void OnWeapon2(InputAction.CallbackContext context);
        void OnPunch(InputAction.CallbackContext context);
    }
}
