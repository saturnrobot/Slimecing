﻿using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public interface IOrbitalTickEveryFrame
    {
        void TickUpdate(GameObject owner, GameObject orbital);
    }
}
