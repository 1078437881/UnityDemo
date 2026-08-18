using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Script_03_13_Context
    {    
        // [MenuItem("CONTEXT/Transform/New Context 1")]
        public static void NewContext1(MenuCommand command)
        {
            Debug.Log(command.context.name);
        }
        
        // [MenuItem("CONTEXT/Transform/New Context 2")]
        public static void NewContext2(MenuCommand command)
        {
            Debug.Log(command.context.name);
        }
    }
}