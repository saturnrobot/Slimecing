using Unity.Entities;
using UnityEngine;


public class PlayerMovementSystem : ComponentSystem
{
    private struct InputFilter
    {
        public Rigidbody Rigidbody;
        public InputComponent InputComponent;
    }
    protected override void OnUpdate()
    {

    }
}
