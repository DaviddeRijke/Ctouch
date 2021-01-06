using System;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "resource")]
    public class Currency : ScriptableObject
    {
        public float Value;

        public bool CanAfford(float Price)
        {
            return Value > Price;
        }
    }
}