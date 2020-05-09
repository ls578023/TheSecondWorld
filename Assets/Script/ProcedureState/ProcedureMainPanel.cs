using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework.Procedure;
using GameFramework.Fsm;
using GameFramework;

namespace TheSecondWorld.Procedure
{
    public  class ProcedureMainPanel: ProcedureBase
    {
        ProcedureOwner owner;
        protected internal override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            owner = procedureOwner;
        }
        protected internal override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            DBTSDK.AppUserManager.Instance.ShowAntiAddiction(true, 1);
            MsgDispatcher.AddEventListener(GameFramework.GlobalEventType.MainPanel_StartGame, MainPanel_StartGame);

            ShowMainPanelOrStarGame();
        }
        protected internal override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }
        protected internal override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            MsgDispatcher.RemoveEventListener(GameFramework.GlobalEventType.MainPanel_StartGame, MainPanel_StartGame);
        }
        protected internal override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }

        private void ShowMainPanelOrStarGame() 
        {

#if GameColletion
            ChangeState<ProcedureStarGame>(owner);
#else
            //GameFrameEntry.GetModule<UIModule>().Show<UI.MainMenu.MainPanelUI>();
#endif
        }

        private void MainPanel_StartGame(object[] arg)
        {
            ChangeState<ProcedureStarGame>(owner);
        }
    }
}
