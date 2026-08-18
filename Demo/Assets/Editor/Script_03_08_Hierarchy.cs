using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class Script_03_08_Hierarchy
    {
        // 覆盖系统原生Image菜单，优先级10对齐UI分组
        [MenuItem("GameObject/UI/Image(关闭射线)")]
        static void CreatImage()
        {
            // 入口日志，判断函数是否触发
            Debug.Log("【自定义创建Image】函数已运行");

            Transform selectTrans = Selection.activeTransform;
            // 校验1：是否选中物体
            if (selectTrans == null)
            {
                Debug.LogError("错误：未选中任何物体！");
                EditorUtility.DisplayDialog("提示", "请先选中Canvas下的UI物体", "确定");
                return;
            }
            // 校验2：是否在Canvas层级
            Canvas parentCanvas = selectTrans.GetComponentInParent<Canvas>();
            if (parentCanvas == null)
            {
                Debug.LogError("错误：选中物体不在Canvas内！");
                EditorUtility.DisplayDialog("提示", "选中对象不属于UI画布层级", "确定");
                return;
            }

            // 1. 创建物体+Image组件
            GameObject imgObj = new GameObject("image");
            Image image = imgObj.AddComponent<Image>();
            Debug.Log($"SetParent前 raycastTarget = {image.raycastTarget}");

            // 2. 先设置父物体（会重置raycastTarget为true）
            imgObj.transform.SetParent(selectTrans, false);
            Debug.Log($"SetParent后 raycastTarget = {image.raycastTarget}");

            // 3. 最后修改射线开关，永久生效
            image.raycastTarget = false;
            Debug.Log($"最终设置后 raycastTarget = {image.raycastTarget}");

            // 支持撤销 Ctrl+Z
            Undo.RegisterCreatedObjectUndo(imgObj, "创建无射线Image");
            // 自动选中新建物体
            Selection.activeGameObject = imgObj;
        }
    }
}