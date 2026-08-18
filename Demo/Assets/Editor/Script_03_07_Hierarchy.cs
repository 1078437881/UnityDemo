using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Script_03_07_Hierarchy
    {
        [MenuItem("Window/Test/yusong")]
        static void Test()
        {
            Debug.Log("我点击了 yusong");
        }

        [MenuItem("Window/Test/momo")]
        static void Test1()
        {
            Debug.Log("我点击了 momo");
        }

        [MenuItem("Window/Test/鱼松/Momo")]
        static void Test2()
        {
            Debug.Log("我点击了 Momo");
        }

        // [InitializeOnLoadMethod]
        static void StartInitOnLoad()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyGUI;
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            // 条件1：鼠标当前位置在当前这一行物体的矩形范围内
            // 条件2：Event.current.button == 1 代表【鼠标右键】（0左键，1右键，2中键）
            // 条件3：EventType.MouseUp 鼠标抬起瞬间触发
            if (Event.current != null && selectionRect.Contains(Event.current.mousePosition) &&
                Event.current.button == 1 && Event.current.type <= EventType.MouseUp)
            {
                // 通过物体唯一ID，转成GameObject对象
                GameObject selectedGameObj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

                // 判断物体不为空
                if (selectedGameObj)
                {
                    // 获取鼠标坐标
                    Vector2 mousePosition = Event.current.mousePosition;

                    // 关键：弹出我们上面定义好的 Window/Test 菜单
                    EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), "Window/Test",
                        null);

                    // Event.current.Use(); 消耗本次鼠标事件，阻止Unity原生右键菜单弹出
                    Event.current.Use();
                }
            }
        }
    }
}