using System.Collections.Generic;
using Persistence;
using Shop.Menu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shop
{
    public class Shop : MenuWindow
    {
        public Score score; //Valuta
        public bool OrderByName; //if false, order by value ascending (default)
        
        public FishSpawner spawn;

        public void BuyShopItem(ShopItem shopItem)
        {
            if (score.score < shopItem.Price)
            {
                InvokeRefresh();
                return;
            }
            
            score.score -= shopItem.Price;
            spawn.SpawnNewFish(shopItem.ModelUID, "undefined");;

            InvokeRefresh();
            Close();
        }
        
        public override bool Validate(MenuItem i)
        {
            return ((ShopItem) i).Price <= score.score;
        }
    }
}
