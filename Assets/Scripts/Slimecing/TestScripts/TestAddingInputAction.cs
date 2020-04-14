using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;

namespace Slimecing.TestScripts
{
    public class TestAddingInputAction : MonoBehaviour
    {
        //[SerializeField] internal InputActionAsset _inputActionAsset;
        [SerializeField] private InputAction inputAction;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private InputActionReference inputActionReference;
        /*private void Start()
        {
            foreach (var binding in inputAction.bindings)
            {
                inputActionReference.action.AddBinding(binding).WithInteractions(inputAction.interactions)
                    .WithProcessors(inputAction.processors);
            }
        }*/
    }
}
