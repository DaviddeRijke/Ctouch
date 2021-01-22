using UnityEngine;

namespace Shop
{
    public class test : MonoBehaviour
    {
        private void Awake()
        {
            FishTypePricePairExtensions.SaveFishTypePricePairs();
        }
    }
}