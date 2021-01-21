using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class Icon : MonoBehaviour
    {
        public Image img;

        public void Import(string path)
        {
            var o = Resources.Load("FishModels/Icons/" + 
                                   path.ToLower()
                                       .Substring(0, path.IndexOf("_prefab" + "", StringComparison.Ordinal)), typeof(Texture2D)) as Texture2D;
            var s = Sprite.Create(o, new Rect(0, 0, o.width, o.height), new Vector2(0.5f, 0.5f));
            img.sprite = s;
        }
    }
}