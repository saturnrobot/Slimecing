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
        public InputActionAsset asset { get; }
        public @SlimeControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""SlimeControls"",
    ""maps"": [
        {
            ""name"": ""VsGameplay"",
            ""id"": ""a0766e30-e847-4bfe-b5bc-cde1c9883be5"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Button"",
                    ""id"": ""8abfc885-0de5-41b1-8abb-45b2ff94793e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Button"",
                    ""id"": ""795a2a8f-8cdf-4247-8e61-45697d87c257"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""24e32e59-8146-430e-84f2-a1b70931ae4d"",
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
                    ""id"": ""704f98c2-def6-4d1f-919a-f55afc789a69"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7930672c-a277-4b7d-abd4-36e6dc8a2f60"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""1795ec87-9b67-4d39-9dc3-1340cb616993"",
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
                    ""id"": ""0935a6f8-0eef-416b-a799-3bd4bf1a7427"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5b09c630-5af5-43d1-9aa5-53f006f852db"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""81786b8d-0478-4026-822f-27bbb95d6069"",
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
                    ""id"": ""c7c2af17-1134-4d80-894b-b672947e3154"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""45d948f8-6196-4d84-b6d2-910feb90be8d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f63e4496-f181-4d11-b089-95ea60655e50"",
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
                    ""id"": ""043fe824-d52a-42b9-82c3-12aec1a16dd5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7898eedc-f9ff-4760-b4f4-e701c57d1998"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""AbilityButtons"",
            ""id"": ""d85b4990-0594-4026-95e2-1724c2874fd5"",
            ""actions"": [
                {
                    ""name"": ""DashAbility"",
                    ""type"": ""Button"",
                    ""id"": ""355e076c-fecd-4a86-980c-3e1360ebc1bc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JumpAbility"",
                    ""type"": ""Button"",
                    ""id"": ""4769900b-900e-4e04-8ca7-4cd6537ca9d8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""45fb3c07-00a4-4e33-8fce-247e7a176e69"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""DashAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64d92f1e-5e22-44a1-a81e-43c57ff6bd1c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DashAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f4585e7-329f-4e05-be72-04adbaff785a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""hold(duration=0.2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""JumpAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64f8c14b-8e49-42aa-ab74-cfe2ba394090"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""hold(duration=0.2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""JumpAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""All Control Schemes"",
            ""bindingGroup"": ""All Control Schemes"",
            ""devices"": []
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // VsGameplay
            m_VsGameplay = asset.FindActionMap("VsGameplay", throwIfNotFound: true);
            m_VsGameplay_Horizontal = m_VsGameplay.FindAction("Horizontal", throwIfNotFound: true);
            m_VsGameplay_Vertical = m_VsGameplay.FindAction("Vertical", throwIfNotFound: true);
            // AbilityButtons
            m_AbilityButtons = asset.FindActionMap("AbilityButtons", throwIfNotFound: true);
            m_AbilityButtons_DashAbility = m_AbilityButtons.FindAction("DashAbility", throwIfNotFound: true);
            m_AbilityButtons_JumpAbility = m_AbilityButtons.FindAction("JumpAbility", throwIfNotFound: true);
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
        
        // VsGameplay
        private readonly InputActionMap m_VsGameplay;
        private IVsGameplayActions m_VsGameplayActionsCallbackInterface;
        private readonly InputAction m_VsGameplay_Horizontal;
        private readonly InputAction m_VsGameplay_Vertical;
        public struct VsGameplayActions
        {
            private @SlimeControls m_Wrapper;
            public VsGameplayActions(@SlimeControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Horizontal => m_Wrapper.m_VsGameplay_Horizontal;
            public InputAction @Vertical => m_Wrapper.m_VsGameplay_Vertical;
            public InputActionMap Get() { return m_Wrapper.m_VsGameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(VsGameplayActions set) { return set.Get(); }
            public void SetCallbacks(IVsGameplayActions instance)
            {
                if (m_Wrapper.m_VsGameplayActionsCallbackInterface != null)
                {
                    @Horizontal.started -= m_Wrapper.m_VsGameplayActionsCallbackInterface.OnHorizontal;
                    @Horizontal.performed -= m_Wrapper.m_VsGameplayActionsCallbackInterface.OnHorizontal;
                    @Horizontal.canceled -= m_Wrapper.m_VsGameplayActionsCallbackInterface.OnHorizontal;
                    @Vertical.started -= m_Wrapper.m_VsGameplayActionsCallbackInterface.OnVertical;
                    @Vertical.performed -= m_Wrapper.m_VsGameplayActionsCallbackInterface.OnVertical;
                    @Vertical.canceled -= m_Wrapper.m_VsGameplayActionsCallbackInterface.OnVertical;
                }
                m_Wrapper.m_VsGameplayActionsCallbackInterface = instance;
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
        public VsGameplayActions @VsGameplay => new VsGameplayActions(this);

        // AbilityButtons
        private readonly InputActionMap m_AbilityButtons;
        private IAbilityButtonsActions m_AbilityButtonsActionsCallbackInterface;
        private readonly InputAction m_AbilityButtons_DashAbility;
        private readonly InputAction m_AbilityButtons_JumpAbility;
        public struct AbilityButtonsActions
        {
            private @SlimeControls m_Wrapper;
            public AbilityButtonsActions(@SlimeControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @DashAbility => m_Wrapper.m_AbilityButtons_DashAbility;
            public InputAction @JumpAbility => m_Wrapper.m_AbilityButtons_JumpAbility;
            public InputActionMap Get() { return m_Wrapper.m_AbilityButtons; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(AbilityButtonsActions set) { return set.Get(); }
            public void SetCallbacks(IAbilityButtonsActions instance)
            {
                if (m_Wrapper.m_AbilityButtonsActionsCallbackInterface != null)
                {
                    @DashAbility.started -= m_Wrapper.m_AbilityButtonsActionsCallbackInterface.OnDashAbility;
                    @DashAbility.performed -= m_Wrapper.m_AbilityButtonsActionsCallbackInterface.OnDashAbility;
                    @DashAbility.canceled -= m_Wrapper.m_AbilityButtonsActionsCallbackInterface.OnDashAbility;
                    @JumpAbility.started -= m_Wrapper.m_AbilityButtonsActionsCallbackInterface.OnJumpAbility;
                    @JumpAbility.performed -= m_Wrapper.m_AbilityButtonsActionsCallbackInterface.OnJumpAbility;
                    @JumpAbility.canceled -= m_Wrapper.m_AbilityButtonsActionsCallbackInterface.OnJumpAbility;
                }
                m_Wrapper.m_AbilityButtonsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @DashAbility.started += instance.OnDashAbility;
                    @DashAbility.performed += instance.OnDashAbility;
                    @DashAbility.canceled += instance.OnDashAbility;
                    @JumpAbility.started += instance.OnJumpAbility;
                    @JumpAbility.performed += instance.OnJumpAbility;
                    @JumpAbility.canceled += instance.OnJumpAbility;
                }
            }
        }
        public AbilityButtonsActions @AbilityButtons => new AbilityButtonsActions(this);
        private int m_AllControlSchemesSchemeIndex = -1;
        public InputControlScheme AllControlSchemesScheme
        {
            get
            {
                if (m_AllControlSchemesSchemeIndex == -1) m_AllControlSchemesSchemeIndex = asset.FindControlSchemeIndex("All Control Schemes");
                return asset.controlSchemes[m_AllControlSchemesSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface IVsGameplayActions
        {
            void OnHorizontal(InputAction.CallbackContext context);
            void OnVertical(InputAction.CallbackContext context);
        }

        public interface IAbilityButtonsActions
        {
            void OnDashAbility(InputAction.CallbackContext context);
            void OnJumpAbility(InputAction.CallbackContext context);
        }
    }
}
