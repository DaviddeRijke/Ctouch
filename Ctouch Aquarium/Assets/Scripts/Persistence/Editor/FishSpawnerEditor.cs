using UnityEditor;
using UnityEngine;

namespace Persistence.Editor
{
    [CustomEditor(typeof(FishSpawner))]
    public class FishSpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var fs = (FishSpawner) target;
            if (GUILayout.Button("Spawn fishes from file"))
            {
                fs.SpawnFromSaveFile();
            }
            base.OnInspectorGUI();
        }
    }
}