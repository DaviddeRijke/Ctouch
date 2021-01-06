using UnityEngine;

namespace DefaultNamespace
{
    public class Spawn : MonoBehaviour
    {
        public SpawnableFish prefab;
        public Transform root;
        public Transform parent;
        public void SpawnFish()
        {
            var f = Instantiate(prefab, parent);
            f.transform.position = root.position;
        }
    }
}