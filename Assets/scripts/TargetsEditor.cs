﻿using System.Collections;
using System.Collections.Generic;
 
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Targets))]
public class TargetsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Targets myPunchingTarget = (Targets) target;

        if (GUILayout.Button("Flicker"))
        {
            myPunchingTarget.flicker();
        }
    }
}

#endif