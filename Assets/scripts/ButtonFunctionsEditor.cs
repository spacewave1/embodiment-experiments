using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonFunctions))]
public class ButtonFunctionsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ButtonFunctions shootTarget = (ButtonFunctions) target;

        if (GUILayout.Button("Call"))
        {
            shootTarget.call();
        }
    }
}

#endif