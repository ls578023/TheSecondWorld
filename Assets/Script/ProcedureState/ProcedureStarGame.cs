
using GameFramework;
using GameFramework.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

/*命名空间请更改成自己游戏得命名空间*/
namespace TheSecondWorld.Procedure
{
    public class ProcedureStarGame : ProcedureBase
    {

        private ProcedureOwner m_ProcedureOwner;
        TouchTrail touchTrail;
        int GameTimer = 0;
        protected internal override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            m_ProcedureOwner = procedureOwner;
        }

        /// <summary>
        /// 进入状态时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameFramework.MsgDispatcher.AddEventListener(GameFramework.GlobalEventType.ReadyExitGame, ReadyExitGame);
            //DBTSDK.AppUserManager.Instance.ShowAntiAddiction(false, -50,"#12507a");
            //GameTimer = GameFrameEntry.GetModule<TimeModule>().AddLoopTimer(GameConst.ReportIntervalTimes, true, ReportGameTimes);
            //DBTSDK.ReportManager.Instance.StartGameModel(MineGameModel.None);
            StarGame();
        }
        void ReportGameTimes(int times) 
        {
            DBTSDK.ReportManager.Instance.ReportGamePlayTime(times);
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
         void StarGame()
        {
            ShowMainPanelOrStarGame();
            //GameFramework.GameFrameEntry.GetModule<GameFramework.UIModule>().Show<UI.MainMenu.MainSceneTopUI>();
            //GameFramework.GameFrameEntry.GetModule<GameFramework.UIModule>().Show<UI.MainMenu.MainSceneFunUI>();
            //GameFramework.UIAudioManager.Instance.Play(GameFramework.AudioCompositeType.Parallel, TSW.AudioDefine.BGM_Main01);

            GotoScene();
        }
        public virtual async System.Threading.Tasks.Task Await()
        {
            //await new WaitUntil(() => { return GameFrameEntry.GetModule<UIModule>().GetUI<UI.MainMenu.LoadingUI>() == null; });
        }
        async void GotoScene() 
        {
            //await GameFramework.LoadHelper.LoadScene("GameScene");
            

            await Load();
            touchTrail = new TouchTrail();

            await Await();
        }

        void MainPanel_StartGame(object[] arg)
        {
            bool isInit = (bool)arg[0];
            if (!isInit)
                return;
            DBTSDK.AppUserManager.Instance.CloseAntiAddiction();

            //await new WaitForEndOfFrame();

            //Guide.GuideSystem.I.StartGuideSystem();

            //Module.ModuleOpenControl.I.SendGuideModuleOpen();
            //if (!Guide.GuideSystem.I.guideIsRuning())
            //{
            //    OfflineControl.I.EnterGameCheckOffline();
            //}
            //MineGameMain.EnterGame = true;
            //Player.PlayerControl.I.ActiveUpdate = true;
        }

        private  void ShowMainPanelOrStarGame()
        {

#if !GameColletion
            GameFrameEntry.GetModule<UIModule>().Show<UI.MainUI.MainUI>();
#endif
        }
        private async System.Threading.Tasks.Task Load()
        {
            string[] str = { /*  "prefabs/QCGC_Plane.prefab",*/
                //"prefabs/seatClose.prefab",
                //"prefabs/seatOpen.prefab",
                //"ui/mainmenu/mainpanelui.prefab.unity3d"
            };
            preLength = str.Length;
            for (int i = 0; i < str.Length; i++)
            {
                LoadHelper.LoadAsset<GameObject>(str[i], Load, LoadId);
            }
        }

        float progressLength;
        float preLength;
        private void Load(int id, GameObject obj)
        {
            idList.Remove(id);
            progressLength = (float)(preLength - idList.Count + 1) / (float)preLength;
        }
        List<int> idList = new List<int>();
        private void LoadId(int id)
        {
            idList.Add(id);
        }

        void ReadyExitGame(object[] Args)
        {
            MsgDispatcher.SendMessage(GameFramework.GlobalEventType.MineGameExitLogic);
            ChangeState<ProcedureExitGame>(m_ProcedureOwner);
        }

        /// <summary>
        /// 状态轮询时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }
        /// <summary>
        /// 离开状态时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="isShutdown">是否是关闭状态机时触发。</param>
        protected internal override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameFrameEntry.GetModule<TimeModule>().RemoveLoopTimer(GameTimer);
            touchTrail.TrailDestory();
            touchTrail = null;
            GameFramework.MsgDispatcher.RemoveEventListener(GameFramework.GlobalEventType.ReadyExitGame, ReadyExitGame);
            MsgDispatcher.RemoveEventListener(GameFramework.GlobalEventType.MainPanel_StartGame, MainPanel_StartGame);
        }

        /// <summary>
        /// 状态销毁时调用
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}
