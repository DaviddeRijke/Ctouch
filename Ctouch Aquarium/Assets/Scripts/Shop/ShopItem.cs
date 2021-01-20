using System;
using Shop.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopItem : MenuItem
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;

        public string ModelUID;
        public float Price;

        public Button BuyButton;

        void Awake()
        {
            BuyButton.onClick.AddListener(Buy);
        }

        private void Buy()
        {
            if (!BuyButton.IsInteractable()) return;
            ((Shop)Window).BuyShopItem(this);
        }

        public override void Refresh()
        {
            BuyButton.interactable = Window.Validate(this);
        }
    }
}