using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene
{
    public class ScreenSwitch:MonoBehaviour
    {
        public Button Button;


        private void Start()
        {
            Button.onClick.AddListener(delegate()
            {
                LaodSceneAsync();
            });
        }


        //同步加载，直接跳转（会卡帧）
        public void LoadTargetScene()
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }

        
        // 异步加载（推荐，不会卡顿，可以做加载进度条）
        public void LaodSceneAsync()
        {
            StartCoroutine(LoadSceneCoroutine("Scenes/SampleScene"));
        }

        IEnumerator LoadSceneCoroutine(string sceneName)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;
            while (op.progress < 0.9f)
            {
                Debug.Log($"加载进度{op.progress}");
                yield return null;
            }

            op.allowSceneActivation = true;
        }
    }
}