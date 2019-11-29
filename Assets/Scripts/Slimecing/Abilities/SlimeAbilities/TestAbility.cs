using Slimecing.Character;
using UnityEngine;

namespace Slimecing.Abilities
{
    [CreateAssetMenu(fileName = "TestAbility", menuName = "Abilities/TestAbility")]
    public class TestAbility : Ability
    {
        public override void Use(AbilityUser aUser)
        {
            Debug.Log(aUser.name + " used " + abilityName);
        }
    }
}
