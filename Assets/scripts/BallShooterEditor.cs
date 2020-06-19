﻿using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BallShooter))]
public class BallShooterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BallShooter shootTarget = (BallShooter) target;

        if (GUILayout.Button("shoot"))
        {
            BallShooter shooter;
            shootTarget.Shoot();
        }
    }
}

#endif