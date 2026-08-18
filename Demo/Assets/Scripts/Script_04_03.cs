using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_04_03 : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake 用于初始化并且永远只会执行一次");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable 在脚本每次激活时执行一次");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start 在初始化后的下一帧执行，并且永远只会执行一次");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update 每一帧执行时，都会立即调用此方法");
    }

    private void LateUpdate()
    {
        Debug.Log("LateUpdate Update方法执行后，都会调用此方法");
    }

    private void FixedUpdate()
    {
        //Editor->Project Settings->Time 菜单项，即可打开Time Manager
        Debug.Log("FixedUpdate 固定更新，默认情况下，系统都会每0.02秒调用一次，具体的间隔时间可以在TimeManager中配置");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable 在脚本每次反激活后，执行一次");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy 用于脚本反初始化并且永远只会执行一次");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit 应用程序退出时执行一次");
    }
}
