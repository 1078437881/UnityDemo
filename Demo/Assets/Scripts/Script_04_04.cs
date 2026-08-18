using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    /**
     * 脚本更新与协程任务
     *
     * 在协程任务启动过程中，如果需要重新启动它，必须停掉之前的协程。
     * 每次启动协程时，StartCoroutine（）将返回这个协程的对象，需要停止时，使用StopCoroutine()传入对象即可。
     * 当然也可以调用StopAllCoroutines（）停止这个脚本启动的所有协程任务
     */
    public class Script_04_04 : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(CreateCube());
        }

        IEnumerator CreateCube()
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = Vector3.one*i;
            }

            yield return new WaitForSeconds(1f);
        }

        private Coroutine m_Coroutine = null;

        private void OnGUI()
        {
            if (GUILayout.Button("StartCoroutine"))
            {
                if (m_Coroutine != null)
                {
                    StopCoroutine(m_Coroutine);
                }

                m_Coroutine = StartCoroutine(CreateCube());
                StartCoroutine(CreateCube());
            }

            if (GUILayout.Button("StopCoroutine"))
            {
                if (m_Coroutine != null)
                {
                    StopCoroutine(m_Coroutine);
                }
            }
        }
    }
}