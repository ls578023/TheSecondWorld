using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameFramework;

//[RequireComponent(typeof(Text))]
public class UILangText : MonoBehaviour
{
    [SerializeField]
    private string key;

    private Text textTarget;
    void Start()
    {
        textTarget = gameObject.GetComponent<Text>();
        textTarget.text = GameFrameEntry.GetModule<LangModule>().Get(key);
    }

    public string Key
    {
        get { return key; }
        set
        {
            if (key != value)
            {
                key = value;
                Value = GameFrameEntry.GetModule<LangModule>().Get(key);
                if (textTarget != null) //重新查找值
                    textTarget.text = Value;
            }
        }
    }
    
    public void Refresh()
    {
        Value = GameFrameEntry.GetModule<LangModule>().Get(key); 
    }

    public string Value
    {
        get
        {
            if (textTarget == null)
                return string.Empty;
            return textTarget.text;
        }
        set
        {
            if (textTarget == null)
            {
                textTarget = gameObject.GetComponent<Text>();
            }
            textTarget.text = value;
        }
    }
}
