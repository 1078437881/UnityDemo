using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Script_03_03
{
   // 标记编辑器加载自动执行
   // [InitializeOnLoadMethod]
   static void InitializeOnLoadMethod()
   {
      // 新版Project窗口绘制API：OnProjectWindowItem
      EditorApplication.projectWindowItemOnGUI -= DrawProjectButton;
      EditorApplication.projectWindowItemOnGUI += DrawProjectButton;
   }

   /// <summary>
   /// 项目资源每行绘制回调
   /// guid：资源唯一ID，r：该行矩形区域
   /// </summary>
   static void DrawProjectButton(string guid, Rect r)
   {
      // 判断是否是当前选中的资源
      Object selectObj = Selection.activeObject;
      if (selectObj == null) return;
        
      string selectPath = AssetDatabase.GetAssetPath(selectObj);
      string selectGuid = AssetDatabase.AssetPathToGUID(selectPath);
      if (guid != selectGuid) return;

      // 裁剪按钮绘制区域（最右侧）
      float btnWidth = 50f;
      Rect btnRect = new Rect(r);
      btnRect.x = r.xMax - btnWidth;
      btnRect.y += 2f;
      btnRect.width = btnWidth;
      btnRect.height -= 4f;

      // 保存原始颜色，绘制完恢复，避免全局变色
      Color oldColor = GUI.color;
      GUI.color = Color.red;
      if (GUI.Button(btnRect, "click"))
      {
         Debug.Log($"click : {selectObj.name}");
      }
      // 必须恢复颜色！
      GUI.color = oldColor;
   }
}
