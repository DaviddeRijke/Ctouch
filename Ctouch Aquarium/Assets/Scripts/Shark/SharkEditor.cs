using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shark))]
public class SharkEditor : Editor
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
