#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//[CustomEditor(typeof(WolfUISystem.UIManager))]
public class UIManagerEditor : Editor
{
    SerializedProperty assignScreensManually;
    SerializedProperty screens;
    SerializedProperty bounds;

    private void OnEnable()
    {
        assignScreensManually = serializedObject.FindProperty("assignScreensManually");
        screens = serializedObject.FindProperty("screens");
        bounds = serializedObject.FindProperty("testing");
    }

    //public override void OnInspectorGUI()
    //{
    //serializedObject.Update();
    //EditorGUILayout.PropertyField(assignScreensManually);
    //if(assignScreensManually.boolValue)
    //{
    //    EditorGUILayout.PropertyField(screens);
    //}
    //else
    //{

    //}
    //EditorGUILayout.PropertyField(bounds);
    //serializedObject.ApplyModifiedProperties();
    //}
}
#endif