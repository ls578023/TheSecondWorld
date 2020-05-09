using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 模块入口
    /// </summary>
    public static class GameFrameEntry
    {
        private static readonly LinkedList<GameFrameModule> s_GameFrameworkModules = new LinkedList<GameFrameModule>();

        static bool GameFrameInitialize;

        public static string CurrAppName;
        public static string GameMenuSceneName = "";
        public static bool IsGoGameMenu=false;

        /// <summary>
        /// 开始某个游戏
        /// </summary>
        public static void StarGame(string AppName,string GameMenuScene="") 
        {
            if (!CheckAppInProject(AppName))
            {
                CLog.Error($"当前工程未包含[{AppName}]游戏");
                return;
            }
            if (!string.IsNullOrEmpty(GameMenuScene))
                GameMenuSceneName = GameMenuScene;
            MsgDispatcher.SendMessage(GlobalEventType.ReadyExitGame);
            IsGoGameMenu = false;
            GlobalData.MineAppName = AppName;
            //跳转到过度场景
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameFrameworkLoad");
        }
        /// <summary>
        /// 返回到游戏菜单
        /// </summary>
        public static void BackToGameGameMenu()
        {
            MsgDispatcher.SendMessage(GlobalEventType.ReadyExitGame);
            IsGoGameMenu = true;
            //跳转到过度场景
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameFrameworkLoad");
        }

        /// <summary>
        /// 检查APP是否在工程中
        /// </summary>
         static bool CheckAppInProject(string AppName) 
        {
            string AppABFiles = Application.streamingAssetsPath + "/" + AppSetting.PlatformName + "/" + AppName + "/" + AppSetting.ABFiles;
            return System.IO.File.Exists(AppABFiles);
        }

        /// <summary>
        /// 模块初始化
        /// </summary>
        public static async Task Initialize()
        {

            foreach (GameFrameModule module in s_GameFrameworkModules)
            {
                await module.Initialize();
            }
        }

        /// <summary>
        /// 模块启动
        /// </summary>
        public static void Start()
        {
            foreach (GameFrameModule module in s_GameFrameworkModules)
            {
                 module.Start();
            }
            GameFrameInitialize = true;
        }

        /// <summary>
        /// 模块轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (!GameFrameInitialize)
                return;
            for (LinkedListNode<GameFrameModule> current = s_GameFrameworkModules.First; current != null; current = current.Next)
            {
                current.Value.Update(elapseSeconds, realElapseSeconds);
            }
        }

        /// <summary>
        /// 模块轮询。在Update后调用
        /// </summary>
        public static void LateUpdate()
        {
            if (!GameFrameInitialize)
                return;
            for (LinkedListNode<GameFrameModule> current = s_GameFrameworkModules.First; current != null; current = current.Next)
            {
                current.Value.LateUpdate();
            }
        }


        public static void ShutdownModule<T>() where T : class
        {
            Type moduleType = GetModuleType<T>();
            foreach (GameFrameModule module in s_GameFrameworkModules)
            {
                if (module.GetType() == moduleType)
                {
                    s_GameFrameworkModules.Remove(module);
                    return;
                }
            }
        }

        /// <summary>
        /// 关闭并清理所有游戏框架模块。
        /// </summary>
        public static void Shutdown()
        {
            for (LinkedListNode<GameFrameModule> current = s_GameFrameworkModules.Last; current != null; current = current.Previous)
            {
                current.Value.Shutdown();
            }

            s_GameFrameworkModules.Clear();
        }

        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <typeparam name="T">要获取的游戏框架模块类型。</typeparam>
        /// <returns>要获取的游戏框架模块。</returns>
        /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
        public static T GetModule<T>() where T : class
        {
            Type moduleType = GetModuleType<T>();
            return GetModule(moduleType) as T;
        }

        private static Type GetModuleType<T>() where T : class
        {
            Type interfaceType = typeof(T);
            if (!interfaceType.FullName.StartsWith("GameFramework."))
            {
                throw new System.Exception(string.Format("You must get a Game Framework module, but '{0}' is not.", interfaceType.FullName));
            }

            string moduleName = string.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name);
            Type moduleType = Type.GetType(moduleName);
            if (moduleType == null)
            {
                throw new System.Exception(string.Format("Can not find Game Framework module type '{0}'.", moduleName));
            }
            return moduleType;
        }

        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <param name="moduleType">要获取的游戏框架模块类型。</param>
        /// <returns>要获取的游戏框架模块。</returns>
        /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
        private static GameFrameModule GetModule(Type moduleType)
        {
            foreach (GameFrameModule module in s_GameFrameworkModules)
            {
                if (module.GetType() == moduleType)
                {
                    return module;
                }
            }

            return CreateModule(moduleType);
        }

        /// <summary>
        /// 创建游戏框架模块。
        /// </summary>
        /// <param name="moduleType">要创建的游戏框架模块类型。</param>
        /// <returns>要创建的游戏框架模块。</returns>
        private static GameFrameModule CreateModule(Type moduleType)
        {
            GameFrameModule module = (GameFrameModule)Activator.CreateInstance(moduleType);
            if (module == null)
            {
                throw new System.Exception(string.Format("Can not create module '{0}'.", module.GetType().FullName));
            }

            LinkedListNode<GameFrameModule> current = s_GameFrameworkModules.First;
            while (current != null)
            {
                if (module.Priority > current.Value.Priority)
                {
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                s_GameFrameworkModules.AddBefore(current, module);
            }
            else
            {
                s_GameFrameworkModules.AddLast(module);
            }

            return module;
        }
    }
}
