using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Script_03_12_Hierarchy
    {
        [MenuItem("GameObject/3D Object/Lock/Lock",false,0)]
        static void Lock()
        {
            if (Selection.gameObjects != null)
            {
                foreach (var gameObj in Selection.gameObjects)
                {
                    gameObj.hideFlags = HideFlags.NotEditable;
                }
            }
        }
        
        [MenuItem("GameObject/3D Object/Lock/UnLock",false,1)]
        static void UnLock()
        {
            if (Selection.gameObjects != null)
            {
                foreach (var gameObj in Selection.gameObjects)
                {
                    gameObj.hideFlags = HideFlags.None;
                }
            }
        }
    }
}