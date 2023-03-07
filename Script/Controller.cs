// GENERATED AUTOMATICALLY FROM 'Assets/Script/Controller.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controller : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""Move"",
            ""id"": ""e4870af5-8407-4b01-9610-6f3d350f79bb"",
            ""actions"": [
                {
                    ""name"": ""Press"",
                    ""type"": ""Button"",
                    ""id"": ""9200d730-a598-4de5-85c1-1a991fb15c2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Double"",
                    ""type"": ""Button"",
                    ""id"": ""6e4c25c1-2b33-4d2d-86d7-4f4ce82654ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4aba1f53-ce74-4935-b0de-13ff6c082ca3"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fd8c14b-b704-4e5a-a6ab-5b1d6a128407"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Double"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Move
        m_Move = asset.FindActionMap("Move", throwIfNotFound: true);
        m_Move_Press = m_Move.FindAction("Press", throwIfNotFound: true);
        m_Move_Double = m_Move.FindAction("Double", throwIfNotFound: true);
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

    // Move
    private readonly InputActionMap m_Move;
    private IMoveActions m_MoveActionsCallbackInterface;
    private readonly InputAction m_Move_Press;
    private readonly InputAction m_Move_Double;
    public struct MoveActions
    {
        private @Controller m_Wrapper;
        public MoveActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Press => m_Wrapper.m_Move_Press;
        public InputAction @Double => m_Wrapper.m_Move_Double;
        public InputActionMap Get() { return m_Wrapper.m_Move; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MoveActions set) { return set.Get(); }
        public void SetCallbacks(IMoveActions instance)
        {
            if (m_Wrapper.m_MoveActionsCallbackInterface != null)
            {
                @Press.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnPress;
                @Press.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnPress;
                @Press.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnPress;
                @Double.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnDouble;
                @Double.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnDouble;
                @Double.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnDouble;
            }
            m_Wrapper.m_MoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Press.started += instance.OnPress;
                @Press.performed += instance.OnPress;
                @Press.canceled += instance.OnPress;
                @Double.started += instance.OnDouble;
                @Double.performed += instance.OnDouble;
                @Double.canceled += instance.OnDouble;
            }
        }
    }
    public MoveActions @Move => new MoveActions(this);
    public interface IMoveActions
    {
        void OnPress(InputAction.CallbackContext context);
        void OnDouble(InputAction.CallbackContext context);
    }
}
