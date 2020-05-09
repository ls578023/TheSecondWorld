//using LitJson;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using GameFramework;

[ExecuteInEditMode]
public class LangService
{
    private static Dictionary<string, LangServerConfig> dicLang = new Dictionary<string, LangServerConfig>();
    private static LangService _instance;
    public static LangService Instance
    {
        get { return _instance ?? (_instance = new LangService()); }
    }

    private static ELangType _LangType = ELangType.ZH_TW;
    public ELangType LangType
    {
        get
        {
            _LangType = (ELangType)EditorPrefs.GetInt("Editor_LangType", 0);
            return _LangType;
        }
        set
        {
            _LangType = value;
            EditorPrefs.SetInt("Editor_LangType", (int)value);
            PlayerPrefs.SetInt("ELangType", (int)value);
            RefAllText();
        }
    }

    public LangService()
    {
        LoadConfig();
    }

    public string Get(string key)
    {
        return Get(key, LangType);
    }
    public string Get(string key, ELangType type)
    {
        LangServerConfig config = null;
        string str = null;
        if (dicLang.TryGetValue(key, out config))
        {
            switch (type)
            {
                case ELangType.ZH_CN:
                    str = config.zh_cn;
                    break;
                case ELangType.ZH_TW:
                    str = config.zh_tw;
                    break;
                case ELangType.EN:
                    str = config.en;
                    break;
                case ELangType.JA:
                    str = config.ja;
                    break;
                case ELangType.KO:
                    str = config.ko;
                    break;
            }
        }
        if(str!=null)
            return str.Replace("\\n", "\n");
        return $"Not Find[{ key}]";
    }

    public void RefAllText()
    {
        GameObject go = GameObject.Find("UICanvas");
        UILangText[] list = go.GetComponentsInChildren<UILangText>();
        bool isGameRunState = false;
        if (Application.isPlaying )
            isGameRunState = true;
        for (int i = list.Length; --i >= 0;)
        {
            if (isGameRunState)
            {
                CLog.Log("RefAllText");
                list[i].Value = Get(list[i].Key);
            }
            else
            {
                list[i].Value = Get(list[i].Key);
                EditorUtility.SetDirty(list[i].gameObject);
            }
        }
    }

    /// <summary>
    /// 加载语言表
    /// </summary>
    public void LoadConfig()
    {
        dicLang.Clear();
        string assetPath = AppSetting.ConfigBundleDir.TrimEnd('/') + AppSetting.ExtName;
        string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetPath.ToLower(), "LanguageConfig");

        if (assetPaths.Length > 0)
        {
            UnityEngine.Object target = AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
            List<LangServerConfig> list = JsonMapper.ToObject<List<LangServerConfig>>(target.ToString());
            Debug.Log("多语言表内容" + list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                if (dicLang.ContainsKey(list[i].id))
                    CLog.Error($"表[LanguageConfig]中有相同键({list[i].id})");
                else
                    dicLang.Add(list[i].id, list[i]);
            }
        }
    }
}
