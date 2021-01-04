using System;
using TMPro;
using UnityEngine;

namespace PrototypeShop
{
    public class CurrencyUI : MonoBehaviour
    {
        public TextMeshProUGUI tmp_Currency;
        public Currency Target;

        private void Awake()
        {
            Target.OnUpdate.AddListener(Update);
        }

        private void Update()
        {
            tmp_Currency.text = $"{Target.Value}";
        }
    }
}