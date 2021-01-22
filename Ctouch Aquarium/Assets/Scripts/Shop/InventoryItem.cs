using System;
using UnityEngine;
using UnityEngine.UI;
using FishDataFolder;
using TMPro;
using UnityEngine.Events;
namespace Shop
{
    public class InventoryItem : MonoBehaviour
    {
        public Button Button;
        public UnityAction<Fish, GameObject> OnClick;
        public Icon Icon;
        public TextMeshProUGUI FishNameText;

        private Fish _f;
        public Fish f
        {
            get => _f;
            set {
                if (value != null)
                {
                    Icon.Import(value.gameObject.name);
                    FishNameText.text = value.fishName;
                }
                _f = value;
            }
        }

        private void OnEnable()
        {
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(() =>OnClick(f, gameObject));
        }
    }
}