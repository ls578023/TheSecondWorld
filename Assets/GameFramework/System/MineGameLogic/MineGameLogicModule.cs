using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 游戏各个模块逻辑控制模块
    /// </summary>
    internal class MineGameLogicModule : GameFrameModule
    {
        List<IControl> controls;
        internal override int Priority => ModulePriority.GameModuleLogicModule;
        internal override async Task Initialize()
        {
            //await new WaitForEndOfFrame();
            controls = new List<IControl>();
            CLog.Log("初始化GameModuleLogicModule完成");
            return;
        }

        /// <summary>
        /// 注册模块
        /// </summary>
        /// <param name="control"></param>
        public void RegisterLogicCtr(IControl control)
        {
            controls.Add(control);
        }
        /// <summary>
        /// 初始化模块
        /// </summary>
        public void InitCtrl()
        {
            //模块数据初始化
            DataManagerPool.Instance.OnInit();
            foreach (var item in controls)
            {
                item.OnInit();
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var item in controls)
            {
                item.Updata(elapseSeconds, realElapseSeconds);
            }
        }

        internal override void Shutdown()
        {
            DataManagerPool.Instance.OnRelease();
            foreach (var item in controls)
            {
                item.OnDispose();
            }
            controls.Clear();
        }

        internal override void Start()
        {

        }
    }
}
