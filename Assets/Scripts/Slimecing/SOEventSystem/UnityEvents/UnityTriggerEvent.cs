﻿using Slimecing.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Slimecing.SOEventSystem.UnityEvents
{
    [System.Serializable] public class UnityTriggerEvent : UnityEvent<Collider> { }
}