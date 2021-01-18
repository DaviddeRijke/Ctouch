using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class Buyable : MonoBehaviour
    {
        private FishType _type;
        public FishType Type
        {
            get => _type;
            set
            {
                nameText.text = value.FishModelUID;
                priceText.text = value.price.ToString();
                _type = value;
            }
        }

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;
 
        public void Buy()
        {
        }
    }
}