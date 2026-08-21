using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Example
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class UIPolygon : Image
    {
        [Header("调试显示(Game窗口可见)")]
        public bool debugDrawArea = true;
        public Color debugColor = new Color(0f, 0.8f, 1f, 0.35f);

        private PolygonCollider2D _polygon = null;

        private PolygonCollider2D polygon
        {
            get
            {
                if (_polygon == null)
                    _polygon = GetComponent<PolygonCollider2D>();
                return _polygon;
            }
        }

        protected UIPolygon()
        {
            useLegacyMeshGeneration = true;
        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();

            if (!debugDrawArea)
                return;

            Vector2[] points = polygon.points;
            if (points == null || points.Length < 3)
                return;

            // 三角扇绘制多边形，Game窗口直接显示半透明色块
            int vertStart = toFill.currentVertCount;
            // 中心点
            toFill.AddVert(points[0], debugColor, new Vector2(0, 0));
            for (int i = 1; i < points.Length; i++)
            {
                toFill.AddVert(points[i], debugColor, new Vector2(0, 0));
            }
            // 构建三角形
            for (int i = 2; i < points.Length; i++)
            {
                toFill.AddTriangle(vertStart, vertStart + i - 1, vertStart + i);
            }
        }

        // 修复坐标，这是点击生效的核心
        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            RectTransform rt = rectTransform;
            // screen → rect本地坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rt, screenPoint, eventCamera, out Vector2 localPoint);
            // ❗关键：把UI本地坐标 转为 世界坐标，再给 OverlapPoint
            Vector2 worldPos = rt.TransformPoint(localPoint);
            bool hit = polygon.OverlapPoint(worldPos);

            Debug.Log($"local:{localPoint} world:{worldPos} hit={hit}");
            return hit;
        }

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();
            transform.position = Vector3.zero;
            float w = (rectTransform.sizeDelta.x * 0.5f) + 0.1f;
            float h = (rectTransform.sizeDelta.y * 0.5f) + 0.1f;
            polygon.points = new Vector2[]
            {
                new Vector2(-w,-h),new Vector2(w,-h),new Vector2(w,h),new Vector2(-w,h)
            };
        }
#endif
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(UIPolygon), true)]
    public class UIPolygonInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
#endif
}