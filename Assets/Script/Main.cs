using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Procedure;
using GameFramework.Fsm;
using GameFramework.ObjectPool;

namespace TheSecondWorld
{
    public class Main : MonoBehaviour
    {
        public string AppName = "TheSecondWorld";
        void Awake()
        {
            DBTSDK.DBTSDKManager.InitSDK();
#if GameColletion
            AppName = GameFramework.GlobalData.MineAppName;
#endif
            AppSetting.MineGameName = AppName;
            DontDestroyOnLoad(this);
            GameFrameEntry.GetModule<VersionMondule>();
            GameFrameEntry.GetModule<AssetbundleModule>();
            GameFrameEntry.GetModule<UIModule>();
            GameFrameEntry.GetModule<TimeModule>();
            GameFrameEntry.GetModule<LangModule>();
            GameFrameEntry.GetModule<EffectModule>();
            GameFrameEntry.GetModule<FsmManager>();
            GameFrameEntry.GetModule<ProcedureManager>();
            GameFrameEntry.GetModule<MineGameLogicModule>(); 
            GameFrameEntry.GetModule<ObjectPoolManager>();
        }

        async void Start()
        {
            DontDestroyOnLoad(this);
            await GameFrameEntry.Initialize();
            GameFrameEntry.Start();
            await new WaitForEndOfFrame();
            await ConfigMgr.Instance.Initialize();
            //提前设置多语言环境
            GameFrameEntry.GetModule<LangModule>().SetLocalLangLibrary(LocalELangLibrary.ZH_CN, ELangType.ZH_CN);
            GamePath.Instance.FindMineGamePath();
            RegisterProcedure();
            SetLanguageData();
            SetEffectData();
            RegisterLogicCtr();


            GameFrameEntry.GetModule<MineGameLogicModule>().InitCtrl();
            GameFrameEntry.GetModule<ProcedureManager>().StartProcedure<Procedure.ProcedurePreLoad>();
        }

        void RegisterProcedure()
        {
            List<ProcedureBase> procedureBases = new List<ProcedureBase>();
            procedureBases.Add(new Procedure.ProcedurePreLoad());
            procedureBases.Add(new Procedure.ProcedureStarGame());
            procedureBases.Add(new Procedure.ProcedureExitGame());
            GameFrameEntry.GetModule<ProcedureManager>().Register(procedureBases.ToArray());
        }

        void SetLanguageData()
        {
            Dictionary<string, LangData> dicLanDatas = new Dictionary<string, LangData>();
            foreach (var item in ConfigMgr.Instance.dicLanguage.Values)
            {
                LangData langData = new LangData();
                langData.id = item.id;
                langData.zh_cn = item.zh_cn;
                langData.zh_tw = item.zh_tw;
                langData.en = item.en;
                langData.ja = item.ja;
                langData.ko = item.ko;
                dicLanDatas.Add(langData.id, langData);
            }
            GameFrameEntry.GetModule<LangModule>().SetLangData(dicLanDatas);

        }

        void SetEffectData()
        {
            Dictionary<int, EffectData> dicEffectDatas = new Dictionary<int, EffectData>();
            foreach (var item in ConfigMgr.Instance.dicEffect.Values)
            {
                EffectData effectData = new EffectData();
                effectData.id = item.id;
                effectData.type = item.type;
                effectData.res = item.res;
                effectData.duration = item.duration;
                dicEffectDatas.Add(effectData.id, effectData);
            }

            GameFrameEntry.GetModule<EffectModule>().SetEffectData(dicEffectDatas);
        }

        void RegisterLogicCtr()
        {
            GameFrameEntry.GetModule<MineGameLogicModule>().RegisterLogicCtr(TaskSystem.TaskCtrl.I);
            GameFrameEntry.GetModule<MineGameLogicModule>().RegisterLogicCtr(ModelCtrl.I);
        }


        private void OnApplicationFocus(bool focus)
        {
            Debug.Log("OnApplicationFocus：" + focus);
            //if (!EnterGame)
            //    return;
            //if (!GlobalData.IsLookVideo)//看视频不算暂停
            //    MsgDispatcher.SendMessage(GlobalEventType.GamePause, !focus);
            if (!focus)
            {
                DataManagerPool.Instance.SaveData();
                Debug.Log("储存数据");
            }
        }

        private void OnApplicationQuit()
        {
#if !UNITY_EDITOR
            //UserDataPrefs.OnSave();
            DataManagerPool.Instance.SaveData();
#endif
        }
    }
}