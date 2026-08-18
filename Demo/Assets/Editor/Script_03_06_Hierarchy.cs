using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Script_03_06_Hierarchy
    {
        // [InitializeOnLoadMethod]
        static void InitOnLoad()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= WindowOnGuI;
            EditorApplication.hierarchyWindowItemOnGUI += WindowOnGuI;
        }

        static void WindowOnGuI(int instanceId , Rect selectionRect)
        {
            //在Hierarchy视图中选择一个资源
            if (Selection.activeObject && instanceId == Selection.activeObject.GetInstanceID())
            {
                float width = 50f;
                float height = 20f;
                selectionRect.x += (selectionRect.width - width);
                selectionRect.width = width;
                selectionRect.height = height;
                //点击事件
                if (GUI.Button(selectionRect, AssetDatabase.LoadAssetAtPath<Texture>("Assets/unity.png")))
                {
                    Debug.LogFormat("click : {0}",Selection.activeObject.name);
                }
            }
        }
    }
    
}