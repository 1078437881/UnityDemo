using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Script_03_04_1
    {
        // [InitializeOnLoadMethod]
        static void InitOnLoad()
        {
            //全局监听Project视图下的资源是否发生变化（添加，删除和移动）
            // 先解绑，防止多次注册
            EditorApplication.projectChanged -= OnProjectChange;
            EditorApplication.projectChanged += OnProjectChange;
        }
        /// <summary>
        /// Project窗口资源发生变化：新建、删除、移动、导入、重命名都会触发
        /// </summary>
        static void OnProjectChange()
        {
            Debug.Log("【Project窗口资源发生变更】");
        }
    }
}