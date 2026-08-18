using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    
    [CustomEditor(typeof(Camera))]
    public class Script_03_16_Scene:UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            Camera camera = target as Camera;
            if (camera != null)
            {
                Handles.color = Color.red;
                Handles.Label(camera.transform.position,camera.transform.position.ToString());
                Handles.BeginGUI();
                GUI.backgroundColor = Color.red;
                if(GUILayout.Button("Click",GUILayout.Width(200f)))
                {
                    Debug.LogFormat("Click = {0}",camera.name);
                }
                GUILayout.Label("Label");
                Handles.EndGUI();
            }
        }
    }
}