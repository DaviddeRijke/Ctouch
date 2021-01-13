using UnityEngine;
using UnityEngine.Events;

namespace PrototypeShop
{
    [CreateAssetMenu(fileName= "Currency")]
    public class Currency : ScriptableObject
    {
        public UnityEvent OnUpdate = new UnityEvent();

        public float Value { get; private set; }

        public void AddCurrency(float f)
        {
            Value += f;
            OnUpdate.Invoke();
        }

        public void RemoveCurrency(float f)
        {
            Value -= f;
            OnUpdate.Invoke();
        }

        public bool CanAfford(float price)
        {
            return Value >= price;
        }

    }
}
