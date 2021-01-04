using System.Collections;
using System.Collections.Generic;
using PrototypeShop;
using UnityEngine;

public class Store : MonoBehaviour
{
    public Currency Currency;

    public Buyable[] ShopItems;

    public void Buy(Buyable b)
    {
        if (Currency.CanAfford(b.Price))
        {
            Currency.RemoveCurrency(b.Price);
            //Inventory.Add(b)
        }
    }



}
