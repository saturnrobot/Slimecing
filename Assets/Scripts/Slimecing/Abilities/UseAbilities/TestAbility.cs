using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Abilities.UseAbilities
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
