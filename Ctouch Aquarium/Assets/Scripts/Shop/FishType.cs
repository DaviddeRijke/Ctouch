using System;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "fishType", menuName = "Fish", order = 0)]
    public class FishType : ScriptableObject
    {
        public float price;
        public string FishModelUID;

    }
}