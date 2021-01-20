using System;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Shop.FishTypePricePairExtensions;

namespace Shop
{
        public class ShopItemFactory : MonoBehaviour
        {
                public ShopItem Prefab;
                public Transform Parent;

                public MenuWindow Target;

                private void Awake()
                {
                        Load();
                }

                public void Load()
                {
                        var res2 = LoadFishTypePricePairs();
                        var ps = new List<ShopItem>();
                        foreach (var ftpp in res2)
                        {
                                var p = Instantiate(Prefab, Parent);
                                p.nameText.text = ftpp.ModelUID;
                                p.priceText.text = ftpp.Price.ToString();
                                p.ModelUID = ftpp.ModelUID;
                                p.Price = ftpp.Price;
                                p.Window = Target;
                        }
                }
        }
}