using System;
using UnityEngine;

namespace Example
{
    /// <summary>
    /// UI局部排序组件
    /// 作用：给当前GameObject自动附加局部Canvas，开启overrideSorting，实现一组UI独立渲染层级
    /// 专门解决UGUI内部粒子特效被UI遮挡的经典问题；同时支持代码动态修改sortingOrder
    /// 注意：一个物体仅挂载1份，禁止父子嵌套挂载，会产生额外Canvas，打断UI合批，增加DrawCall
    /// </summary>
    [AddComponentMenu("UI/UIOrder")]
    public class UIOrder : MonoBehaviour
    {
        /// <summary>
        /// 局部渲染排序序号，数字越大渲染越靠上层
        /// </summary>
        [SerializeField]
        private int _sortingOrder = 0;

        /// <summary>
        /// 对外访问排序序号属性；赋值后自动刷新层级，无需手动调用Refresh
        /// </summary>
        public int SortingOrder
        {
            get => _sortingOrder;
            set
            {
                // 值发生变化才执行刷新，避免重复开销
                if (_sortingOrder != value)
                {
                    _sortingOrder = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 缓存当前物体的局部Canvas组件，脚本自动创建/获取，面板隐藏
        /// </summary>
        private Canvas _canvas = null;

        /// <summary>
        /// 获取Canvas；为空则自动获取，不存在就AddComponent新建；设置为编辑器不可编辑
        /// </summary>
        private Canvas GetCanvas
        {
            get
            {
                if (_canvas == null)
                {
                    // 尝试获取物体上已存在的Canvas
                    _canvas = gameObject.GetComponent<Canvas>();
                    // 当前物体没有Canvas，自动新增
                    if (_canvas == null)
                    {
                        _canvas = gameObject.AddComponent<Canvas>();
                    }
                    // 设置标记：Inspector面板该Canvas组件灰掉，禁止手动修改参数
                    _canvas.hideFlags = HideFlags.NotEditable;
                }
                return _canvas;
            }
        }

        /// <summary>
        /// 物体唤醒时执行一次，保证运行时/预制体实例化后层级立刻生效
        /// </summary>
        private void Awake()
        {
            Refresh();
        }

        /// <summary>
        /// 刷新排序：更新Canvas层级，同步所有子物体粒子渲染器的sortingOrder
        /// </summary>
        public void Refresh()
        {
            // 防御保护：物体已经销毁直接返回，避免空引用报错
            if (this == null) return;

            // 开启Canvas独立排序，不受父Canvas层级控制
            GetCanvas.overrideSorting = true;
            GetCanvas.sortingOrder = _sortingOrder;

            // 遍历所有子物体（包含关闭的物体），同步粒子渲染器排序，解决UI内粒子遮挡
            foreach (var particle in transform.GetComponentsInChildren<ParticleSystemRenderer>(true))
            {
                particle.sortingOrder = _sortingOrder;
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// 编辑器模式：Inspector参数修改时触发刷新，只在编辑器执行，打包会被剔除
        /// </summary>
        void OnValidate()
        {
            Refresh();
        }

        /// <summary>
        /// 组件重置（Reset）时触发刷新，只在编辑器执行，打包会被剔除
        /// </summary>
        void Reset()
        {
            Refresh();
        }
#endif
    }
}
