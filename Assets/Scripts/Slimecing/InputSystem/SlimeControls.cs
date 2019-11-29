// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Slimecing/InputSystem/SlimeControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Slimecing.InputSystem
{
    public class @SlimeControls : IInputActionCollection, IDisposable
    {
        private InputActionAsset asset;
        public @SlimeControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""SlimeControls"",
    ""maps"": [
        {
            ""name"": ""VSGameplay"",
            ""id"": ""24f634ee-5f8f-4707-89a7-23e8b1ecbcf4"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Button"",
                    ""id"": ""550962d9-e533-414f-aa32-f29e5dfa9019"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Button"",
                    ""id"": ""e4869e00-a1d9-457d-98b7-3520944918a3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""8e69cdda-7fed-470f-acaa-8a3ab63a63c2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""dff2a34d-d502-4dfe-8227-94a0f5ac22aa"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7bf42d99-f48b-4b91-ad01-155c5eeebb07"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""faa9dfee-c3af-4ef1-a36f-7ae201db94d2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f69a983c-d4f8-4dbe-bdce-ee54ee4e8fe7"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""492c4265-7d82-4a79-b795-e5117a26d1ff"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""ef047899-ff11-4d90-9e60-7ce7683a25a9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f3cd3157-c5b2-440e-88ea-46fedd54b294"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5ea49ba6-e713-4cc1-8260-d0459a6f549b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""82cb051e-4445-4323-be39-c955e64a53ca"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8385a5f7-d192-4142-a50c-7bf9c4d408f0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1abd2da0-6ed5-446b-88ba-1d1c22fcb787"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // VSGameplay
            m_VSGameplay = asset.FindActionMap("VSGameplay", throwIfNotFound: true);
            m_VSGameplay_Horizontal = m_VSGameplay.FindAction("Horizontal", throwIfNotFound: true);
            m_VSGameplay_Vertical = m_VSGameplay.FindAction("Vertical", throwIfNotFound: true);
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

        // VSGameplay
        private readonly InputActionMap m_VSGameplay;
        private IVSGameplayActions m_VSGameplayActionsCallbackInterface;
        private readonly InputAction m_VSGameplay_Horizontal;
        private readonly InputAction m_VSGameplay_Vertical;
        public struct VSGameplayActions
        {
            private @SlimeControls m_Wrapper;
            public VSGameplayActions(@SlimeControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Horizontal => m_Wrapper.m_VSGameplay_Horizontal;
            public InputAction @Vertical => m_Wrapper.m_VSGameplay_Vertical;
            public InputActionMap Get() { return m_Wrapper.m_VSGameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(VSGameplayActions set) { return set.Get(); }
            public void SetCallbacks(IVSGameplayActions instance)
            {
                if (m_Wrapper.m_VSGameplayActionsCallbackInterface != null)
                {
                    @Horizontal.started -= m_Wrapper.m_VSGameplayActionsCallbackInterface.OnHorizontal;
                    @Horizontal.performed -= m_Wrapper.m_VSGameplayActionsCallbackInterface.OnHorizontal;
                    @Horizontal.canceled -= m_Wrapper.m_VSGameplayActionsCallbackInterface.OnHorizontal;
                    @Vertical.started -= m_Wrapper.m_VSGameplayActionsCallbackInterface.OnVertical;
                    @Vertical.performed -= m_Wrapper.m_VSGameplayActionsCallbackInterface.OnVertical;
                    @Vertical.canceled -= m_Wrapper.m_VSGameplayActionsCallbackInterface.OnVertical;
                }
                m_Wrapper.m_VSGameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Horizontal.started += instance.OnHorizontal;
                    @Horizontal.performed += instance.OnHorizontal;
                    @Horizontal.canceled += instance.OnHorizontal;
                    @Vertical.started += instance.OnVertical;
                    @Vertical.performed += instance.OnVertical;
                    @Vertical.canceled += instance.OnVertical;
                }
            }
        }
        public VSGameplayActions @VSGameplay => new VSGameplayActions(this);
        public interface IVSGameplayActions
        {
            void OnHorizontal(InputAction.CallbackContext context);
            void OnVertical(InputAction.CallbackContext context);
        }
    }
}
