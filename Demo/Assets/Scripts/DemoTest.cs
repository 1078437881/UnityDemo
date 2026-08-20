using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DemoTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int testValue;

    public int testValue1;

    public TextMeshProUGUI textShow;
    public TMP_InputField inputField;

    [System.Serializable]
    public class CustomDataType
    {
        public int nestValue;
    }

    public CustomDataType testValue2;

    void Start()
    {
        inputField.onValueChanged.AddListener((content => 
            textShow.text = content));
        inputField.onValidateInput = delegate(string input, int charIndex, char addedChar)
        {
            if (addedChar == 'b')
            {
                addedChar = '*';
            }

            return addedChar;
        };
    }

    // Update is called once per frame
    void Update()
    {
    }
}