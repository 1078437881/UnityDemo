using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Example
{
    public class Script_5_11 : MonoBehaviour, ICanvasRaycastFilter
    {
        [Tooltip("需要被挖孔高亮的目标UI")]
        public RectTransform target;
        public Canvas canvas;

        private Vector4 m_Center;
        private Material m_Material;
        private float m_TargetRadius;
        private float m_CurrentRadius;

        private readonly Vector3[] m_CornerArr = new Vector3[4];
        private float m_SmoothVelocity;
        private RectTransform m_SelfRt;

        private void Awake()
        {
            m_SelfRt = GetComponent<RectTransform>();
            Recalculate();
        }

        public void Recalculate()
        {
            if (target == null)
            {
                Debug.LogError("target 没有赋值");
                return;
            }
            Image img = GetComponent<Image>();
            if(img == null)
            {
                Debug.LogError("当前物体缺少Image组件！脚本要挂在带Image的UI物体上");
                return;
            }
            if (canvas == null)
            {
                Debug.LogError("canvas没有赋值");
                return;
            }
            Camera cam = canvas.worldCamera;
            if (cam == null)
            {
                Debug.LogError("Canvas的Render Camera为空！Canvas模式必须是Screen Space‑Camera并且赋值相机");
                return;
            }

            target.GetWorldCorners(m_CornerArr);
            Vector3 targetWorldCenter = (m_CornerArr[0] + m_CornerArr[2]) * 0.5f;

            // 计算外接圆半径
            Vector2 p0 = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, m_CornerArr[0]);
            Vector2 p2 = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, m_CornerArr[2]);
            m_TargetRadius = Vector2.Distance(p0, p2) / 2f;

            if (m_TargetRadius < 1f)
                m_TargetRadius = 100f;

            // 世界坐标 → Mask自身RectTransform局部坐标（给Shader用）
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, targetWorldCenter);
            bool ok = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_SelfRt,
                screenPos,
                canvas.worldCamera,
                out Vector2 localSelfPos);

            if (!ok)
            {
                Debug.LogWarning("坐标转换失败");
                return;
            }

            m_Center = new Vector4(localSelfPos.x, localSelfPos.y, 0, 0);

            m_Material = GetComponent<Image>().material;
            m_Material.SetVector("_Center", m_Center);

            m_CurrentRadius = 0f;
            m_Material.SetFloat("_Slider", m_CurrentRadius);
            m_SmoothVelocity = 0f;

            Debug.Log($"Recalculate | Radius:{m_TargetRadius}, LocalCenter:{localSelfPos}");
        }

        private void Update()
        {
            if (m_Material == null) return;

            float val = Mathf.SmoothDamp(m_CurrentRadius, m_TargetRadius, ref m_SmoothVelocity, 0.3f);
            if (!Mathf.Approximately(val, m_CurrentRadius))
            {
                m_CurrentRadius = val;
                m_Material.SetFloat("_Slider", m_CurrentRadius);
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Test 重新计算挖孔"))
            {
                Recalculate();
            }
        }

        // 射线过滤：孔洞内部可穿透点击，外部拦截
        public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_SelfRt,
                screenPoint,
                eventCamera,
                out Vector2 localPos);

            float dist = Vector2.Distance(localPos, m_Center);
            return dist >= m_CurrentRadius;
        }
    }
}