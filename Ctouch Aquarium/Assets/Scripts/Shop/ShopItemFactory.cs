using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Shop.FishTypePricePairExtensions;

namespace Shop
{
        public class ShopItemFactory : MonoBehaviour
        {
                public ShopItem Prefab;
                public Transform Parent;

                public Shop Target;

                private void Awake()
                {
                        Load();
                }

                public void Load()
                {
                        var res2 = LoadFishTypePricePairs();
                        var ps = new List<ShopItem>();
                        foreach (var ftpp in res2.OrderBy(r => r.Price))
                        {
                                var p = Instantiate(Prefab, Parent);
                                p.nameText.text = ftpp.ModelUID;
                                p.priceText.text = ftpp.Price.ToString();
                                p.ModelUID = ftpp.ModelUID;
                                p.GetComponent<Icon>().Import(ftpp.ModelUID);
                                p.Price = ftpp.Price;
                                p.Window = Target;
                        }
                }
        }
}