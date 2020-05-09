
using GameFramework.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameFramework;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

/*命名空间请更改成自己游戏得命名空间*/
namespace TheSecondWorld.Procedure
{
    public class ProcedureExitGame : ProcedureBase
    {

        private ProcedureOwner m_ProcedureOwner;

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
            ExitGame();
        }
        /// <summary>
        /// 退出游戏
        /// </summary>
        void ExitGame()
        {
            GameFramework.UserDataPrefs.OnSave();
            /*游戏自身逻辑处理完后调用退出游戏事件*/
            MsgDispatcher.SendMessage(GameFramework.GlobalEventType.ExitGame);
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
        }

        /// <summary>
        /// 状态销毁时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}
