using UnityEditor;
using UnityEngine;

namespace Persistence.Editor
{
    [CustomEditor(typeof(EditorSaveBehaviour))]
    public class EditorSaveBehaviourEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var esb = (EditorSaveBehaviour) target;

            if (GUILayout.Button("Save fish in scene to OwnedFish"))
            {
                esb.SaveAllInScene();
            }

            if (GUILayout.Button("Reset OwnedFish"))
            {
                esb.Reset();
            }

            if (GUILayout.Button("Reset"))
            {
                esb.Reset();
            }
            base.OnInspectorGUI();
        }
    }
}