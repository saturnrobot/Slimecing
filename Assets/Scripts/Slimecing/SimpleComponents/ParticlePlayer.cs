using UnityEngine;

namespace Slimecing.Particles {
    public class ParticlePlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleToBePlayed;

        public void PlayParticles()
        {
            particleToBePlayed?.Play();
        }
    }
}
