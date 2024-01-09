using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerInfoEditor : MonoBehaviour
{
    [CustomEditor(typeof(PlayerInfoScript))]
    public class SomeScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
        }
    }
}
