using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Marker_Details), true), CanEditMultipleObjects]
public class Marker_Details_Editor : Editor
{
    private SerializedProperty _detailsString;
    private void OnEnable()
    {
        EditorStyles.textArea.wordWrap = true;
        _detailsString = serializedObject.FindProperty("_details");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Details");

        _detailsString.stringValue = EditorGUILayout.TextArea(
            _detailsString.stringValue,
            EditorStyles.textArea);

        serializedObject.ApplyModifiedProperties();
    }
}
