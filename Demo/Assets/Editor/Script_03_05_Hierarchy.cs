using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Script_03_05_Hierarchy
    {
        // [MenuItem("GameObject/My Create/Cube",false,0)]
        static void CreateCube()
        {
            //创建立方体
            GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
    }
}