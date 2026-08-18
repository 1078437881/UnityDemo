using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int testValue;
    
    public int testValue1;
    
    [System.Serializable]
    public class CustomDataType
    {
        public int nestValue;
    }
    
    public CustomDataType testValue2;
    
    void Start()
    {
        Debug.Log("Hello World! Start"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
