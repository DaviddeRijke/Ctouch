using UnityEngine;

namespace Shop
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