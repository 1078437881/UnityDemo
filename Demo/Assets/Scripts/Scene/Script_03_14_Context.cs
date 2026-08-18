
using UnityEngine;
//没搞太明白
// 编辑器库仅在编辑模式引入
#if UNITY_EDITOR
using UnityEditor;
#endif

// 运行时脚本，移出 Editor 命名空间
public class Script_03_14_Context : MonoBehaviour
{
    public string contextName;
    // ===== 编辑器右键菜单：组件头部右键 CONTEXT =====
#if UNITY_EDITOR
    // [MenuItem("CONTEXT/Script_03_14_Context/New Context 1")]
    private static void NewContext2(MenuCommand command)
    {
        Script_03_14_Context target = command.context as Script_03_14_Context;
        if (target == null) return;
        target.contextName = "hello world!";
    }
#endif

    // ===== 组件右键菜单（挂载在脚本内部） =====
#if UNITY_EDITOR
    // [ContextMenu("Remove Component")]
    private void RemoveComponent()
    {
        Debug.Log("Remove Component");
        // 
        EditorApplication.delayCall += () =>
        {
            DestroyImmediate(this);
        };
    }
#endif
}
