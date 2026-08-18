using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Script_05_03_ScrollRect : ScrollRect
{
    [Header("摇杆最大半径")]
    public float mRadius = 0f;
    // 输出给外部的摇杆方向 (-1~1)
    public Vector2 Dir { get; private set; }

    private RectTransform rootRt;

    protected override void Start()
    {
        base.Start();
        rootRt = GetComponent<RectTransform>();

        // 自动绑定 Viewport / Content
        viewport = transform.Find("Viewport")?.GetComponent<RectTransform>();
        content = viewport?.Find("Content")?.GetComponent<RectTransform>();

        // 打印初始化信息
        Debug.Log($"【初始化】Viewport:{(viewport != null ? "绑定成功" : "为空！！")} Content:{(content != null ? "绑定成功" : "为空！！")}");

        if (rootRt != null)
        {
            mRadius = rootRt.sizeDelta.x * 0.5f;
            Debug.Log($"【初始化】摇杆底盘宽度:{rootRt.sizeDelta.x} 计算半径mRadius={mRadius}");
        }

        // 摇杆固定设置
        inertia = false;
        movementType = MovementType.Clamped;
        horizontal = true;
        vertical = true;
    }

    // 鼠标/手指按下时触发
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        Debug.Log($"【开始拖拽】按下屏幕坐标: {eventData.position}");
    }

    // 拖拽中每帧执行（核心逻辑+日志）
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        if (content == null)
        {
            Debug.LogError("【拖拽报错】content为空，直接退出拖拽逻辑！");
            return;
        }

        Vector2 contentPos = content.anchoredPosition;
        Debug.Log($"【拖拽中】滑块当前坐标 anchoredPosition = {contentPos} 模长:{contentPos.magnitude} 限制半径:{mRadius}");

        // 圆形边界限制
        if (contentPos.magnitude > mRadius)
        {
            contentPos = contentPos.normalized * mRadius;
            SetContentAnchoredPosition(contentPos);
            Debug.Log($"【超出边界】修正后坐标: {contentPos}");
        }

        // 归一化方向
        Dir = contentPos / mRadius;
        Debug.Log($"【摇杆方向】Dir = {Dir}");
    }

    // 松开拖拽
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        Debug.Log($"【结束拖拽】松开，滑块复位到中心");
        content.anchoredPosition = Vector2.zero;
        Dir = Vector2.zero;
    }
}