using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RobotKinematicsTypeB))]
public class TypeBEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RobotKinematicsTypeB rk = (RobotKinematicsTypeB)target;

        if (GUILayout.Button("DoAll"))
        {
            rk.SegmentFollow();
        }

    }
}
