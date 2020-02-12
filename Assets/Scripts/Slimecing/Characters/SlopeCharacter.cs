using System;
using Slimecing.Character;
using UnityEngine;

namespace Slimecing.Characters
{
   [RequireComponent(typeof(CharacterMovementController))]
   public class SlopeCharacter : MonoBehaviour
   {
      [SerializeField] private float maxGroundAngle;

      private float groundAngle;
      private RaycastHit hitInfo;

      private void Update()
      {
         CalculateGroundAngle();
      }

      private void CalculateGroundAngle()
      {
         throw new NotImplementedException();
      }
   }
}
