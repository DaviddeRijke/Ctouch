using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shark))]
public class SharkEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        var shark = (Shark)target;
        if (GUILayout.Button("Save shark"))
        {
            shark.SaveShark();
        }
        base.OnInspectorGUI();
    }
}
