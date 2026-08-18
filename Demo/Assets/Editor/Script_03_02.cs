using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Script_03_02
{
    [MenuItem("Assets/Create/My Create/Cube",false,2)]
    static void MyTools1()
    {
        GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
   
    [MenuItem("Assets/Create/My Create/Sphere",false,1)]
    static void MyTools2()
    {
        GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }
}
