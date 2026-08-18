using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_05_02_button : MonoBehaviour
{
    public Button Button1,Button2;
    public Toggle[] Toggles;
    public TextMeshProUGUI TestText;
    public Image Image;
    

    void Start()
    {
        Button1.onClick.AddListener(delegate()
        {
            Onclick(Button1.gameObject);
        });
        Button2.onClick.AddListener(delegate()
        {
            Onclick(Button2.gameObject);
        });
        UGUIEventListener.Get(Image.gameObject).onClick = Onclick;
        UGUIEventListener.Get(TestText.gameObject).onClick = Onclick;
        
        foreach (var toggle in Toggles)
        {
            toggle.onValueChanged.AddListener(delegate(bool selected)
            {
                Debug.LogFormat("toggle={0} selected = {1}",toggle.name,selected);
            });
        }
    }

    void Onclick(GameObject gameObj)
    {
        if (gameObj == Button1.gameObject)
        {
            Debug.Log("点击了按钮Button1");
        }
        else if(gameObj == Button2.gameObject)
        {
            Debug.Log("点击了按钮Button2");
        }
        else if(gameObj == TestText.gameObject)
        {
            Debug.Log("点击了文本TestText");
        }
        else if(gameObj == Image.gameObject)
        {
            Debug.Log("点击了图片Image");
        }
    }
}
