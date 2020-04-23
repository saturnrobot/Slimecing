using UnityEngine;

namespace Slimecing.Environment.Moving
{
    [System.Serializable]
    public struct RigidbodyMoverState
    {
        public Vector3 position { get; set; }
        public Quaternion rotation { get; set; }
        public Vector3 velocity { get; set; }
        public Vector3 angularVelocity { get; set; }

        public RigidbodyMoverState(Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
        {
            this.position = position;
            this.rotation = rotation;
            this.velocity = velocity;
            this.angularVelocity = angularVelocity;
        }
    }
}
