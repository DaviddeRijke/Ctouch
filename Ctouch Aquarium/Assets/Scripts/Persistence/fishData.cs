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
            Rotation = parent.rotation;
            LocalPosition = transform.position;
            LocalRotation = transform.rotation;
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