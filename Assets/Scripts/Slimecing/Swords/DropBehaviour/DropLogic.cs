using UnityEngine;

namespace Slimecing.Swords.DropBehaviour
{
    public abstract class DropLogic : MonoBehaviour
    {
        [SerializeField] private LayerMask dropLayer;

        public virtual void Drop()
        {
            gameObject.layer = dropLayer;
        }
    }
}
