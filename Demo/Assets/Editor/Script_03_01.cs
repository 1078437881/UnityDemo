using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Script_03_01
{
   [MenuItem("Assets/My Tools/Tools 1",false,1)]
   static void MyTools1()
   {
      Debug.Log(Selection.activeObject.name);
   }
   
   [MenuItem("Assets/My Tools/Tools 2",false,2)]
   static void MyTools2()
   {
      Debug.Log(Selection.activeObject.name);
   }
}
