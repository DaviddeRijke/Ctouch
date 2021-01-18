using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shop
{
        public class BuyableFactory : MonoBehaviour
        {
                public string PathToFolder = "SO/FishTypes";
                public Buyable Prefab;
                public Transform Parent;
                
                public List<Buyable> Load()
                {

                        var res = Resources.LoadAll<FishType>(PathToFolder);
                        var ps = new List<Buyable>();
                        foreach (var fishType in res)
                        {
                                var p = Instantiate(Prefab, Parent);
                                p.Type = fishType;
                                ps.Add(p);
                        }
                        return ps;
                }

                public List<Buyable> Load2()
                {
                        var res2 = FishTypePricePairExtensions.LoadFishTypePricePairs();

                        var ps = new List<Buyable>();
                        foreach (var ftpp in res2)
                        {
                                var p = Instantiate(Prefab, Parent);
                                //p
                        }

                        return ps;
                }
        }
}