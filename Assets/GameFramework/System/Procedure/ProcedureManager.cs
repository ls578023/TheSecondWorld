
using GameFramework.Fsm;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace GameFramework.Procedure
{
    /// <summary>
    /// 流程管理器。
    /// </summary>
    internal sealed class ProcedureManager : GameFrameModule, IProcedureManager
    {
        private IFsmManager m_FsmManager;
        private IFsm<IProcedureManager> m_ProcedureFsm;

        /// <summary>
        /// 初始化流程管理器的新实例。
        /// </summary>
        public ProcedureManager()
        {
            m_FsmManager = null;
            m_ProcedureFsm = null;
        }

        internal override async Task Initialize()
        {
            //List<Type> availableProcedureTypes = GetProceduresType();
            //ProcedureBase[] procedures = new ProcedureBase[availableProcedureTypes.Count];
            //for (int i = 0; i < availableProcedureTypes.Count; i++)
            //{
            //    Type procedureType = availableProcedureTypes[i];
            //    if (procedureType == null)
            //    {
            //        CLog.Error(string.Format("Can not find procedure type '{0}'.", availableProcedureTypes[i]));
            //        continue;
            //    }
            //    procedures[i] = (ProcedureBase)Activator.CreateInstance(procedureType);
            //}
            //Initialize(GameFrameEntry.GetModule<FsmManager>(), procedures);
            //await new UnityEngine.WaitForEndOfFrame();
            CLog.Log("初始化ProcedureManager完成");
            return;

        }
        /// <summary>
        /// 初始化流程管理器。
        /// </summary>
        /// <param name="fsmManager">有限状态机管理器。</param>
        /// <param name="procedures">流程管理器包含的流程。</param>
        public void Register(params ProcedureBase[] procedures)
        {
            m_FsmManager = GameFrameEntry.GetModule<FsmManager>();
            if (m_FsmManager == null)
            {
                throw new GameFrameworkException("FSM manager is invalid.");
            }
            m_ProcedureFsm = m_FsmManager.CreateFsm(this, procedures);
        }

        internal override void Start()
        {

        }

        /// <summary>
        /// 获取游戏框架模块优先级。
        /// </summary>
        /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
        internal override int Priority => ModulePriority.ProcedurePriority;

        /// <summary>
        /// 获取当前流程。
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                if (m_ProcedureFsm == null)
                {
                    throw new GameFrameworkException("You must initialize procedure first.");
                }

                return (ProcedureBase)m_ProcedureFsm.CurrentState;
            }
        }

        /// <summary>
        /// 获取当前流程持续时间。
        /// </summary>
        public float CurrentProcedureTime
        {
            get
            {
                if (m_ProcedureFsm == null)
                {
                    throw new GameFrameworkException("You must initialize procedure first.");
                }

                return m_ProcedureFsm.CurrentStateTime;
            }
        }

        /// <summary>
        /// 流程管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

        /// <summary>
        /// 关闭并清理流程管理器。
        /// </summary>
        internal override void Shutdown()
        {
            if (m_FsmManager != null)
            {
                if (m_ProcedureFsm != null)
                {
                    m_FsmManager.DestroyFsm(m_ProcedureFsm);
                    m_ProcedureFsm = null;
                }

                m_FsmManager = null;
            }
        }

        

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <typeparam name="T">要开始的流程类型。</typeparam>
        public void StartProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            m_ProcedureFsm.Start<T>();
        }

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <param name="procedureType">要开始的流程类型。</param>
        public void StartProcedure(Type procedureType)
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            m_ProcedureFsm.Start(procedureType);
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <typeparam name="T">要检查的流程类型。</typeparam>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            return m_ProcedureFsm.HasState<T>();
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <param name="procedureType">要检查的流程类型。</param>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure(Type procedureType)
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            return m_ProcedureFsm.HasState(procedureType);
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <typeparam name="T">要获取的流程类型。</typeparam>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            return m_ProcedureFsm.GetState<T>();
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <param name="procedureType">要获取的流程类型。</param>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure(Type procedureType)
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            return (ProcedureBase)m_ProcedureFsm.GetState(procedureType);
        }


        private List<System.Type> GetProceduresType()
        {
            List<System.Type> availableProcedureTypes = new List<Type>();
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            Type[] types = assembly.GetExportedTypes();
            foreach (Type t in types)
            {

                var attrs = (ProcedureTypeAttribute[])t.GetCustomAttributes(typeof(ProcedureTypeAttribute), false);
                if (attrs.Length == 0)
                    continue;
                System.Type pType = attrs[0].ProcedureType;
                availableProcedureTypes.Add(pType);


            }
            return availableProcedureTypes;
        }

    }
}
