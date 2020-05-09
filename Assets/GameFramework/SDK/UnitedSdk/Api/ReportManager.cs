using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

namespace DBTSDK
{
    public partial class ReportManager : MonoBehaviour
    {

        public static ReportManager Instance;
        Dictionary<MineGameModel, int> dicGameModelLoopTimer;
        private void Awake()
        {
            Instance = this;
            dicGameModelLoopTimer = new Dictionary<MineGameModel, int>();
            InitReportManager();
        }
        public void InitReportManager()
        {
            GameFramework.MsgDispatcher.AddEventListener(GameFramework.GlobalEventType.MineGameExitLogic, MineGameExitLogic);
           
        }
        void MineGameExitLogic(object[] Args) 
        {
            //清空计时器 
            foreach (var item in dicGameModelLoopTimer.Values)
            {
                GameFrameEntry.GetModule<TimeModule>().RemoveLoopTimer(item);
            }
            dicGameModelLoopTimer.Clear();
        }
        void AddGmaeModeTimer(MineGameModel mineGameModel) 
        {
            if (dicGameModelLoopTimer.ContainsKey(mineGameModel))
            {
                CLog.Error($"已存在当前模式[{mineGameModel}]的计时器");
                return;
            }
            if (dicGameModelLoopTimer.Count > 0)
            {
                CLog.Error($"模式计时器必须唯一，请先停掉之前的模式计时器");
                return;
            }

            int GameTimer = GameFrameEntry.GetModule<TimeModule>().AddLoopTimer(GameConst.ReportIntervalTimes, true, ReportGameTimes);

            dicGameModelLoopTimer.Add(mineGameModel, GameTimer);
        }

        void ReportGameTimes(int times)
        {
            DBTSDK.ReportManager.Instance.ReportGameModelPlayTime(times);
        }

        void StopGameModeTimer(MineGameModel mineGameModel)
        {
            if (!dicGameModelLoopTimer.ContainsKey(mineGameModel))
            {
                CLog.Error($"不存在当前模式[{mineGameModel}]的计时器");
                return;
            }
            int id;
            dicGameModelLoopTimer.TryGetValue(mineGameModel, out id);
            GameFrameEntry.GetModule<TimeModule>().RemoveLoopTimer(id);
            dicGameModelLoopTimer.Remove(mineGameModel);
        }


        /// <summary>
        /// 上报事件 OnEvent(key, label); 等价于OnEvent(key, label, 1);
        /// </summary>
        /// <param name="key">事件ID</param>
        /// <param name="label">事件Label</param>
        /// <param name="count">事件计数</param>
        public void ReportEvent(string key, string label, int count=0)
        {
            //Debug.Log($"调用代理层ReportEvent:key[{key}]--label[{label}]--count[{count}]");
            if (count == 0)
            {
                OnEvent(key, label);
            }
            else
            {
                OnEvent(key, label, count);
            }
        }
        /// <summary>
        /// 上报游戏时长
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ms"></param>
        /// <param name="label"></param>
         void ReportGameTimes(string key, int ms, string label="")
        {
            //Debug.Log($"调用代理层ReportGameTimes:key[{key}]--ms[{ms}]--label[{label}]");
            if (string.IsNullOrEmpty(label))
            {

                OnEventDuration(key, ms);
            }
            else
            {
                OnEventDuration(key, label, ms);
            }

        }

        /// <summary>
        /// 开启关卡
        /// </summary>
        public void StartLevel(int LevelId) 
        {
            //游戏开始
            ReportEvent(ReportDefine.starttimes, AppSetting.MineGameName);
            if(AppSetting.mineGameModel!= MineGameModel.None)
            {
                ReportEvent(ReportDefine.starttimesGameModel+ AppSetting.MineGameName,AppSetting.mineGameModel.ToString());
            }
            //如果目前是关卡模式
            if(AppSetting.mineGameModel== MineGameModel.LevelModel)
            {
                ReportEvent(AppSetting.MineGameName+ReportDefine.levelstarttimes, LevelId.ToString());
                OnEvenToOnlyOnce(AppSetting.MineGameName + ReportDefine.levelFirsstarttimes, LevelId.ToString());
                //int level = UserDataPrefs.GetInt(AppSetting.MineGameName + UserDataKey.StartLevleCount, 0);
                //if (LevelId > level)
                //{
                //    UserDataPrefs.SetInt(AppSetting.MineGameName + UserDataKey.StartLevleCount, LevelId);
                //    ReportEvent(AppSetting.MineGameName + ReportDefine.levelFirsstarttimes, LevelId.ToString());
                //    UserDataPrefs.OnSave();
                //}
                    
            }
        }
        /// <summary>
        /// 结束关卡
        /// </summary>
        public void OverLevel(int LevelId,bool IsSuccess) 
        {
            //游戏结束
            ReportEvent(ReportDefine.overtimes, AppSetting.MineGameName);
            if (AppSetting.mineGameModel != MineGameModel.None)
            {
                ReportEvent(ReportDefine.overtimesGameModel + AppSetting.MineGameName, AppSetting.mineGameModel.ToString());
            }
            //如果目前是关卡模式
            if (AppSetting.mineGameModel == MineGameModel.LevelModel)
            {
                if (!IsSuccess)
                    return;
                ReportEvent(AppSetting.MineGameName + ReportDefine.levelovertimes, LevelId.ToString());
                OnEvenToOnlyOnce(AppSetting.MineGameName + ReportDefine.levelFirstovertimes, LevelId.ToString());
                //int level = UserDataPrefs.GetInt(AppSetting.MineGameName + UserDataKey.OverLevleCount, 0);
                //if (LevelId > level)
                //{
                //    UserDataPrefs.SetInt(AppSetting.MineGameName + UserDataKey.OverLevleCount, LevelId);
                //    ReportEvent(AppSetting.MineGameName + ReportDefine.levelFirsstarttimes, LevelId.ToString());
                //    UserDataPrefs.OnSave();
                //}

            }
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        public void StartMineGame()
        {
            OnEvenToOnlyOnce(ReportDefine.playpeople, AppSetting.MineGameName);
            //int ID = UserDataPrefs.GetInt(AppSetting.MineGameName + UserDataKey.Playpeople, 0);
            //if (ID == 0)
            //{
            //    UserDataPrefs.SetInt(AppSetting.MineGameName + UserDataKey.Playpeople, 1);
            //    ReportEvent(ReportDefine.playpeople,AppSetting.MineGameName);
            //    UserDataPrefs.OnSave();
            //}
        }

        /// <summary>
        /// 开始某个模式
        /// </summary>
        public void StartGameModel(MineGameModel mineGameModel)
        {
            if(AppSetting.mineGameModel!= MineGameModel.None)
            {
                CLog.Log($"离开模式[{AppSetting.mineGameModel}]");
                StopGameModeTimer(AppSetting.mineGameModel);
            }
            AppSetting.mineGameModel = mineGameModel;
            OnEvenToOnlyOnce(ReportDefine.playpeopleGameModel + AppSetting.MineGameName, mineGameModel.ToString());
            AdsManager.Instance.GameName = AppSetting.MineGameName + "," + AppSetting.mineGameModel;

            CLog.Log($"开始模式[{AppSetting.mineGameModel}]");
            if (AppSetting.mineGameModel != MineGameModel.None)
            {
                AddGmaeModeTimer(mineGameModel);
            }
            //int ID = UserDataPrefs.GetInt(AppSetting.MineGameName + mineGameModel, 0);
            //if (ID == 0)
            //{
            //    UserDataPrefs.SetInt(AppSetting.MineGameName + mineGameModel, 1);
            //    ReportEvent(ReportDefine.playpeopleGameModel+AppSetting.MineGameName, mineGameModel.ToString());
            //    UserDataPrefs.OnSave();
            //}
        }
        /// <summary>
        /// 上报游戏时长
        /// </summary>
        /// <param name="times"></param>
        public void ReportGamePlayTime(int times) 
        {
           ReportGameTimes(ReportDefine.playtime, times*1000, AppSetting.MineGameName);
        }
        /// <summary>
        /// 上报游戏模块时长
        /// </summary>
        /// <param name="times"></param>
        public void ReportGameModelPlayTime(int times)
        {
            ReportGameTimes(ReportDefine.playtimeGameModel+AppSetting.MineGameName, times * 1000, AppSetting.mineGameModel.ToString());
        }

        internal void ReportVideo(string videFlag) 
        {
            VideFlag d = (VideFlag)System.Enum.Parse(typeof(VideFlag), videFlag);
            switch (d)
            {
                case VideFlag.Flag1:
                    ReportEvent(ReportDefine.Videoreward, AppSetting.MineGameName);
                    if(AppSetting.mineGameModel!= MineGameModel.None)
                    {
                        ReportEvent(ReportDefine.VideorewardGameModel+AppSetting.MineGameName, AppSetting.mineGameModel.ToString());
                    }
                    break;
                case VideFlag.Flag2:
                    ReportEvent(ReportDefine.Videoalive, AppSetting.MineGameName);
                    if (AppSetting.mineGameModel != MineGameModel.None)
                    {
                        ReportEvent(ReportDefine.VideoaliveGameModel + AppSetting.MineGameName, AppSetting.mineGameModel.ToString());
                    }
                    break;
                case VideFlag.Flag3:
                    ReportEvent(ReportDefine.VideoUnlock, AppSetting.MineGameName);
                    if (AppSetting.mineGameModel != MineGameModel.None)
                    {
                        ReportEvent(ReportDefine.VideoUnlockGameModel + AppSetting.MineGameName, AppSetting.mineGameModel.ToString());
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///  只上报一次事件
        /// </summary>
        public void OnEvenToOnlyOnce(string event_id, string label)
        {
            //OnEventOnlyOnce(event_id, label);
            OnEventOnlyOnce(event_id, label, 1);

        }


        /// <summary>
        ///  只上报一次事件
        /// </summary>
        public void OnEvenToOnlyOnce(string event_id, string label, int n)
        {
            OnEventOnlyOnce(event_id, label, n);

        }

        void Start()
        {

        }

    }
}
