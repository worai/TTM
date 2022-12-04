// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/UI/UIInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UIInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIInput"",
    ""maps"": [
        {
            ""name"": ""User"",
            ""id"": ""cfcd6365-e68f-4906-8111-2a89441badab"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""0ff9c8e8-23a9-4a81-83bc-509ee04f4c84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hints"",
                    ""type"": ""Button"",
                    ""id"": ""c4378170-b6f4-4f6f-b4d9-d2b5c5f7a7cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2192b162-ae97-4794-8aec-cae1309ab605"",
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
                    ""id"": ""4c7684fc-4414-42f5-9c94-1fcc55c1ad53"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hints"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // User
        m_User = asset.FindActionMap("User", throwIfNotFound: true);
        m_User_Escape = m_User.FindAction("Escape", throwIfNotFound: true);
        m_User_Hints = m_User.FindAction("Hints", throwIfNotFound: true);
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

    // User
    private readonly InputActionMap m_User;
    private IUserActions m_UserActionsCallbackInterface;
    private readonly InputAction m_User_Escape;
    private readonly InputAction m_User_Hints;
    public struct UserActions
    {
        private @UIInput m_Wrapper;
        public UserActions(@UIInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_User_Escape;
        public InputAction @Hints => m_Wrapper.m_User_Hints;
        public InputActionMap Get() { return m_Wrapper.m_User; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserActions set) { return set.Get(); }
        public void SetCallbacks(IUserActions instance)
        {
            if (m_Wrapper.m_UserActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_UserActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnEscape;
                @Hints.started -= m_Wrapper.m_UserActionsCallbackInterface.OnHints;
                @Hints.performed -= m_Wrapper.m_UserActionsCallbackInterface.OnHints;
                @Hints.canceled -= m_Wrapper.m_UserActionsCallbackInterface.OnHints;
            }
            m_Wrapper.m_UserActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Hints.started += instance.OnHints;
                @Hints.performed += instance.OnHints;
                @Hints.canceled += instance.OnHints;
            }
        }
    }
    public UserActions @User => new UserActions(this);
    public interface IUserActions
    {
        void OnEscape(InputAction.CallbackContext context);
        void OnHints(InputAction.CallbackContext context);
    }
}
