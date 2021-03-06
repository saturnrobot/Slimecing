﻿using System.Collections;
using Slimecing.SOEventSystem.Events;
using UnityEngine;

namespace Slimecing.SimpleComponents
{
    public class Visible : MonoBehaviour
    {
        [SerializeField] private GameObjectEvent onThingAppear;
        [SerializeField] private GameObjectEvent onThingDisappear;

        private void OnEnable()
        {
            StartCoroutine(ShowObject());
        }

        private IEnumerator ShowObject() {
            yield return new WaitForSeconds(0.05f);
            onThingAppear.Raise(gameObject);
        }
        private void OnDisable()
        {
            onThingDisappear.Raise(gameObject);
        }
    }
}
