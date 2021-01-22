using System;
using System.Collections.Generic;
using FishDataFolder;
using UnityEngine;

namespace Persistence
{
    [Serializable]
    public struct FishData
    {
        public string ModelUID;
        public string Name;
        public bool IsEaten;
        public bool IsInInventory;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;

        private static string suffix = "prefab";
        public FishData(Fish f)
        {
            var name = f.name;
            if (!name.EndsWith("prefab"))
            {
                name = name.Substring(0, name.IndexOf(suffix, StringComparison.Ordinal) + suffix.Length);
            }
            ModelUID = name;
            Name = f.fishName;
            var transform = f.transform;
            var parent = transform.parent;
            Position = parent.position;
            Rotation = parent.localRotation;
            LocalPosition = transform.localPosition;
            LocalRotation = transform.localRotation;
            IsEaten = f.isEaten;
            Debug.Log(f.transform.position);
            Debug.Log(f.transform.parent.position);
            IsInInventory = f.isInInventory;
        }
    }

    public struct ListContainer<T>
    {
        public List<T> dataList;

        public ListContainer(List<T> _dataList)
        {
            dataList = _dataList;
        }
    }
}