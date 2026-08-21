using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Example
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class UIPolygon:Image
    {
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
        //设置只响应，不进行渲染
        protected UIPolygon()
        {
            useLegacyMeshGeneration = true;
        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();
            //base.OnPopulateMesh(toFill);
        }

        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            return polygon.OverlapPoint(eventCamera.ScreenToWorldPoint(screenPoint));
        }
        
#if UNITY_EDITOR
        protected override void Reset()
        {
            //重置不规则区域
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
    [CustomEditor(typeof(UIPolygon),true)]
    public class UIPolygonInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            //什么都不写，用于隐藏面板显示
        }
    }
#endif
}