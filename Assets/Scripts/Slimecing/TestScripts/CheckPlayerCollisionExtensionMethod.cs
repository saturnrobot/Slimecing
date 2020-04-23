using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.TestScripts
{
    public static class CheckPlayerCollisionExtensionMethod
    {
        public static bool ColIsPlayer(this Collision col)
        {
            return col.gameObject.GetComponent<CharacterMovementController>() != null;
        }
    }
}
