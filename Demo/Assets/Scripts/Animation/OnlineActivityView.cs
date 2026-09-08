using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 动画控制示例
///
/// 场景结构：
///   OnlineActivityView（本脚本，1800x900 面板）
///     ├─ ButtonLeft / ButtonRight / ButtonUp / ButtonDown（4 个方向按钮）
///     └─ Joker 图片（带 Animator，控制器 joker.controller）
///
/// joker.controller 的状态机：
///   - 4 个 Trigger 参数：Left / Right / Up / Down
///   - 4 个动画状态：JokerLeft / JokerRight / JokerUp / JokerDown
///     每个状态对应一段方向动画（JokerLeft.anim 等）
///
/// 本示例演示 3 种"动画控制"方式：
///   1. 点击按钮 → 触发 Animator 对应 Trigger（SetTrigger）
///   2. 键盘方向键 → 同样触发 Animator（Update 里监听）
///   3. 监听动画片段里抛出的动画事件（配合 AnimationEvent.cs 使用）
///
/// 使用方式：
///   - 4 个按钮已拖到 BtLeft / BtRight / BtUp / BtDown（场景里已绑定）；
///   - targetAnimator 留空时，会自动查找子物体上的 Animator（即 Joker 图片）。
/// </summary>
public class OnlineActivityView : MonoBehaviour
{
    public Button BtLeft, BtRight, BtUp, BtDown;

    [Tooltip("被控制的 Animator，留空自动查找子物体上的 Animator")]
    public Animator targetAnimator;

    // 与 joker.controller 中的 4 个 Trigger 参数一一对应
    private static readonly string[] Triggers = { "Left", "Right", "Up", "Down" };

    private void Start()
    {
        // 1. 获取被控制的 Animator（Joker 图片挂在子物体上，自动查找）
        if (targetAnimator == null)
            targetAnimator = GetComponentInChildren<Animator>();

        if (targetAnimator == null)
        {
            Debug.LogError("OnlineActivityView: 未找到 Animator，" +
                           "请在 Inspector 中把 Joker 的 Animator 拖到 targetAnimator 上");
            return;
        }

        // 2. 按钮绑定：点击 → 触发对应方向的动画
        BtLeft.onClick.AddListener(() =>
        {
            Play("Left");
        });
        BtRight.onClick.AddListener(() =>
        {
            
            Play("Right");
        });
        BtUp.onClick.AddListener(() =>
        {
            
            Play("Up");
        });
        BtDown.onClick.AddListener(() =>
        {
            
            Play("Down");
        });

        // 3. 监听动画片段事件（可选）
        //    在 Animation 窗口给 JokerXxx.anim 加事件帧，函数名填 AnimationListener.MyCustomEvent，
        //    动画播放到那一帧时就会回调下面的 OnAnimationEvent
        Animation.AnimationListener.animationEvent.AddListener(OnAnimationEvent);
    }

    private void Update()
    {
        // 键盘方向键控制（演示用，不需要可以删掉这段）
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Play("Left");
        else if (Input.GetKeyDown(KeyCode.RightArrow)) Play("Right");
        else if (Input.GetKeyDown(KeyCode.UpArrow)) Play("Up");
        else if (Input.GetKeyDown(KeyCode.DownArrow)) Play("Down");
    }

    /// <summary>
    /// 播放指定方向的动画。
    /// Trigger 触发一次后不会自动复位，所以播放前先把 4 个 Trigger 全部重置，
    /// 避免"按下 Left 后，下次按其他按钮被残留的 Trigger 干扰"。w
    /// </summary>
    private void Play(string trigger)
    {
        if (targetAnimator == null) return;

        for (int i = 0; i < Triggers.Length; i++)
        {
            targetAnimator.SetInteger(Triggers[i],0);
        }

        targetAnimator.SetInteger(trigger,1);
        Debug.LogFormat("OnlineActivityView: 触发动画 {0}", trigger);
    }

    /// <summary>动画事件回调：由动画片段事件帧调用 AnimationListener.MyCustomEvent 触发</summary>
    private void OnAnimationEvent(int value)
    {
        Debug.LogFormat("OnlineActivityView: 收到动画事件，value = {0}", value);
    }

    private void OnDestroy()
    {
        if (Animation.AnimationListener.animationEvent != null)
            Animation.AnimationListener.animationEvent.RemoveListener(OnAnimationEvent);
    }
}
