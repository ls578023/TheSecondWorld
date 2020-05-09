
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    internal class LangModule : GameFrameModule
    {
        internal override int Priority => ModulePriority.LangModule;
        /// <summary>
        /// 默认语言
        /// </summary>
        private ELangType defaultType = ELangType.ZH_CN;
        private LangData config;
        Dictionary<string, LangData> dicLang;
        /// <summary>
        /// 本地语言库
        /// </summary>
        private LocalELangLibrary localELangLibrary= LocalELangLibrary.ZH_CN;

        internal override async Task Initialize()
        {
            dicLang = new Dictionary<string, LangData>();
            //await new WaitForEndOfFrame();

            CLog.Log("初始化LangModule完成");
            return;
        }
        /// <summary>
        /// 设置本地拥有的语言库
        /// </summary>
        public void SetLocalLangLibrary(LocalELangLibrary LangLibrary, ELangType AppDefaultLangType= ELangType.ZH_CN) 
        {
            localELangLibrary = LangLibrary;

#if UNITY_EDITOR
            defaultType = (ELangType)PlayerPrefs.GetInt("ELangType", (int)ELangType.ZH_CN);
#else

            defaultType = AppDefaultLangType;
            LocalELangLibrary defaultLangNeedLangLibrary= LocalELangLibrary.EN;
            int SysytemLangType = DBTSDK.AppInfoManager.Instance.GetSystemLanguage();
            CLog.Log("系统语言：" + SysytemLangType);
            switch (SysytemLangType)
            {
                case 0:
                    defaultType = ELangType.EN;
                    defaultLangNeedLangLibrary = LocalELangLibrary.EN;
                    break;
                case 1:
                    defaultType = ELangType.ZH_CN;
                    defaultLangNeedLangLibrary = LocalELangLibrary.ZH_CN;
                    break;
                case 2:
                    defaultType = ELangType.ZH_TW;
                    defaultLangNeedLangLibrary = LocalELangLibrary.ZH_TW;
                    break;
                case 10:
                    defaultType = ELangType.KO;
                    defaultLangNeedLangLibrary = LocalELangLibrary.KO;
                    break;
                case 11:
                    defaultType = ELangType.JA;
                    defaultLangNeedLangLibrary = LocalELangLibrary.JA;
                    break;
            }
            if (defaultType == AppDefaultLangType)
            {
                CLog.Log("不属于5大语言，使用默认语言：" + AppDefaultLangType);
                return;
            }
            //判断当前选择的语言，本地是否拥有语言配置
            if (!localELangLibrary.HasFlag(defaultLangNeedLangLibrary))
            {
                Debug.Log($"本地未配置{defaultLangNeedLangLibrary}语言库，准备回退到默认语言库--{AppDefaultLangType}");
                if(defaultLangNeedLangLibrary== LocalELangLibrary.ZH_CN)
                {
                    //如果发生APP不包含简体资源 优先寻找繁体资源  然后再找默认资源
                    if (localELangLibrary.HasFlag(LocalELangLibrary.ZH_TW))
                    {
                        Debug.Log($"本地配置繁体语言库，简体手机使用繁体语言库");
                        defaultType = ELangType.ZH_TW;
                        return;
                    }
                }
                defaultType = AppDefaultLangType;
                CLog.Log($"本地版本库没有当前语言[{defaultLangNeedLangLibrary}]：改成使用默认语言[{defaultType}]");
            }
#endif
        }

        public void SetLangData(Dictionary<string, LangData> datas) 
        {
            dicLang = datas;
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="type"></param>
        public ELangType LangType
        {
            get
            {
                return defaultType;
            }
            set
            {
                if (defaultType != value)
                {
                    defaultType = value;                    
                    GameFrameEntry.GetModule<UIModule>().RefreshLang();
                    PlayerPrefs.SetInt("ELangType", (int)value);
                    PlayerPrefs.Save();
                }
            }           
        }
        /// <summary>
        /// 跟据Key值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return Get(key, defaultType).Replace("\\n", "\n");
        }
        public string GetFormat(string key, params object[] args)
        {
            return GetFormat(key, defaultType, args);
        }
        public string GetFormat(string key, ELangType type,params object[] args)
        {
            string str = Get(key, type).Replace("\\n", "\n");
            str = string.Format(str, args);
            return str;
        }
        public string Get(string key, ELangType type)
        {
            if (dicLang.TryGetValue(key, out config))
            {
                switch (type)
                {
                    case ELangType.ZH_CN:
                        return config.zh_cn;
                    case ELangType.ZH_TW:
                        return config.zh_tw;
                    case ELangType.EN:
                        return config.en;
                    case ELangType.JA:
                        return config.ja;
                    case ELangType.KO:
                        return config.ko;
                }
            }
            return $"{ key}";
        }

        internal override void Start()
        {

        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

        internal override void Shutdown()
        {
            dicLang.Clear();
        }
    }
}

//以下为语言和返回值的对应关系
/*
ENGLISH = 0,
CHINESE_CN,1      //简体中文
CHINESE = CHINESE_CN  1,
CHINESE_TW,   2   //繁体中文
FRENCH, 3
ITALIAN, 4
GERMAN, 5
SPANISH_ES,  6    //西班牙语
SPANISH = SPANISH_ES,6
SPANISH_MX,   7    //西班牙语(墨西哥)
DUTCH,8
RUSSIAN,9
KOREAN,     10       //朝鲜语
JAPANESE,  11      //日语
JAPANESE_JP = JAPANESE, 11
HUNGARIAN, 12
PORTUGUESE, 13
ARABIC, 14
NORWEGIAN, 15
POLISH, 16
TURKISH, 17
UKRAINIAN, 18
ROMANIAN, 19
BULGARIAN, 20
HINDI,  21          //印度
INDONESIAN, 22       //印尼
VIETNAMESE,  23      //越南
BENGALI 24
*/
