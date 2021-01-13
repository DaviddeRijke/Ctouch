using UnityEngine;

namespace PrototypeShop
{
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
}
