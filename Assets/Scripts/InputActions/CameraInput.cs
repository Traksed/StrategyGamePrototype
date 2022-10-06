//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/Scripts/InputActions/CameraInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CameraInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraInput"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""2628ea4e-d81d-470b-8bae-db520c697460"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""78bdcbf6-b729-472d-a1ac-345dba1f969c"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""c504c4ad-9e6e-4066-a5b1-4d8ea376b22a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""22531a6b-a684-4041-8d35-42f64c2a12eb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7a3eb625-9854-456d-a545-9d7d5164b1ce"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8547c0a0-7c48-4d07-8752-c103123bc925"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ec8b64f-3eb8-4d84-8ef6-e657f608fc14"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Touchscreen"",
            ""bindingGroup"": ""Touchscreen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Move = m_Camera.FindAction("Move", throwIfNotFound: true);
        m_Camera_TouchPress = m_Camera.FindAction("TouchPress", throwIfNotFound: true);
        m_Camera_TouchPosition = m_Camera.FindAction("TouchPosition", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Move;
    private readonly InputAction m_Camera_TouchPress;
    private readonly InputAction m_Camera_TouchPosition;
    public struct CameraActions
    {
        private @CameraInput m_Wrapper;
        public CameraActions(@CameraInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Camera_Move;
        public InputAction @TouchPress => m_Wrapper.m_Camera_TouchPress;
        public InputAction @TouchPosition => m_Wrapper.m_Camera_TouchPosition;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @TouchPress.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPress;
                @TouchPress.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPress;
                @TouchPress.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPress;
                @TouchPosition.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPosition;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @TouchPress.started += instance.OnTouchPress;
                @TouchPress.performed += instance.OnTouchPress;
                @TouchPress.canceled += instance.OnTouchPress;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    private int m_TouchscreenSchemeIndex = -1;
    public InputControlScheme TouchscreenScheme
    {
        get
        {
            if (m_TouchscreenSchemeIndex == -1) m_TouchscreenSchemeIndex = asset.FindControlSchemeIndex("Touchscreen");
            return asset.controlSchemes[m_TouchscreenSchemeIndex];
        }
    }
    public interface ICameraActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
    }
}