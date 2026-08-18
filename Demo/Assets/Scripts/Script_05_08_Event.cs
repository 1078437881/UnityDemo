
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class MyEvent : UnityEvent<int, string>
    {
    }

    public class Script_05_08_Event:MonoBehaviour
    {
        public UnityAction<int, string> action1;
        public UnityAction<int, string> action2;
        public MyEvent MyEvent = new MyEvent();

        public void RunMyEvent1(int a, string b)
        {
            Debug.Log(string.Format("RunMyEvent1,{0},{1}",a,b));
        }
        
        public void RunMyEvent2(int a, string b)
        {
            Debug.Log(string.Format("RunMyEvent2,{0},{1}",a,b));
        }

        void Start()
        {
            //也可以使用+=，但是+=操作执行多次后，如果没有对应的-=，就会有隐患
            action1 = RunMyEvent1;
            action2 = RunMyEvent2;
            
            MyEvent.AddListener(action1);
            MyEvent.AddListener(action2);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("按下键盘 A");
                action1.Invoke(0,"a");
                action2.Invoke(1,"b");
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                MyEvent.Invoke(100,"a & b");
            }
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy，移除所有的action监听");
            MyEvent.RemoveListener(action1);
            MyEvent.RemoveListener(action2);
            MyEvent.RemoveAllListeners();
        }
    }
