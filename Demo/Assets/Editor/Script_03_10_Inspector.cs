using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    // [CustomEditor(typeof(Transform))]
    public class Script_03_10_Inspector:UnityEditor.Editor
    {
        private UnityEditor.Editor mEditor;

        // private void OnEnable()
        // {
        //     mEditor = UnityEditor.Editor.CreateEditor(target,
        //         Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.TransformInspector",true));
        // }
        //
        // public override void OnInspectorGUI()
        // {
        //     if (GUILayout.Button("拓展按钮上"))
        //     {
        //     }
        //     GUI.enabled = false;
        //     mEditor.OnInspectorGUI();
        //     GUI.enabled = true;
        //     if (GUILayout.Button("拓展按钮下"))
        //     {
        //     }
        //     // base.OnInspectorGUI();
        // }
    }
}