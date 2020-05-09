using AssetBundles;
using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GameFramework
{
    /*快捷键用法
    % = ctrl 
    # = Shift 
    & = Alt
    LEFT/RIGHT/UP/DOWN = 上下左右
    F1…F2 = F...
    HOME, END, PGUP, PGDN = 键盘上的特殊功能键
    特别注意的是，如果是键盘上的普通按键，比如a ~z，则要写成_a ~_z这种带_前缀的。
    */

    /// <summary>
    /// GameFrame框架工具菜单
    /// </summary>
    public partial class GameFrameToolsMenu
    {
        private const string LastScenePrefKey = "GameFrame.LastSceneOpen";

        #region 资源打包
        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        [MenuItem("★工具★/资源打包/资源包命名[GameRes\\BundleRes]")]
        public static void MakeAssetBundleNames()
        {
            ResBundleTools.MakeAssetBundleNames();
        }
        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        [MenuItem("★工具★/资源打包/其它/安卓Once音效Copy")]
        public static void CopyOnceAudio()
        {
            ResBundleTools.CopyOnceAuido();
            //ResBundleTools.MakeAssetBundleNames();
        }

        [MenuItem("★工具★/资源打包/其它/手动同步碰撞矩阵")]
        public static void CreateLayerCollisionMatrix()
        {
            ResBundleTools.CreateLayerCollisionMatrix();
            //ResBundleTools.MakeAssetBundleNames();
        }
        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        [MenuItem("★工具★/资源打包/其它/清除非打包资源资源包命名")]
        public static void RemoveAssetBundleNames()
        {
            ResBundleTools.RemoveAssetBundleNames();
        }
        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        [MenuItem("★工具★/资源打包/是否使用仿真模式")]
        public static void ToggleSimulationMode()
        {
            AssetBundleManager.SimulateAssetBundleInEditor = !AssetBundleManager.SimulateAssetBundleInEditor;
            ToolsHelper.Log("资源包防真模式:" + (AssetBundleManager.SimulateAssetBundleInEditor ? "开启" : "关闭"));
        }
        [MenuItem("★工具★/资源打包/是否使用仿真模式", true)]
        public static bool ToggleSimulationModeValidate()
        {
            Menu.SetChecked("★工具★/资源打包/是否使用仿真模式", AssetBundleManager.SimulateAssetBundleInEditor);
            return true;
        }
        //=====================================================        
        /// <summary>
        /// 清理冗余，即无此资源，却有AssetBundle的
        /// </summary>
        [MenuItem("★工具★/资源打包/其它/清理冗余")]
        public static void CleanAssetBundlesRedundancies()
        {
            ResBundleTools.CleanAssetBundlesRedundancies();
        }

        /// <summary>
        /// 生成AssetBundle文件信息
        /// </summary>
        [MenuItem("★工具★/资源打包/其它/生成ABFile")]
        public static void CreateAssetBundleFileInfo()
        {
             ResBundleTools.CreateAssetBundleFileInfo();           
        }

        ///// <summary>
        ///// 资源缓存清理
        ///// </summary>
        //[MenuItem("★工具★/资源打包/其它/Clear Cache")]
        //public static void ClearCache()
        //{
        //    Caching.ClearCache();
        //    ToolsHelper.Log("缓存清理完成!");
        //}
        /// <summary>
        /// 资源缓存清理
        /// </summary>
        //[MenuItem("★工具★/资源打包/其它/MkLink StreamingAssets")]
        //public static void MkLinkStreamingAssets()
        //{
        //    LinkHelper.MkLinkStreamingAssets();
        //}
        [MenuItem("★工具★/资源打包/其它/Link StreamingAssets")]
        public static void MkLinkStreamingAssets()
        {
            LinkHelper.IsLinkStreamingAssets = !LinkHelper.IsLinkStreamingAssets;
            LinkHelper.MkLinkStreamingAssets();
            ToolsHelper.Log("链接资源到StreamingAssets:" + (LinkHelper.IsLinkStreamingAssets ? "链接" : "关闭"));
        }
        [MenuItem("★工具★/资源打包/其它/Link StreamingAssets", true)]
        public static bool MkLinkStreamingAssetsValidate()
        {
            Menu.SetChecked("★工具★/资源打包/其它/Link StreamingAssets", LinkHelper.IsLinkStreamingAssets);
            return true;
        }
        //=====================================================

        /// <summary>
        /// 重新打包，删除原始文件
        /// </summary>
        [MenuItem("★工具★/资源打包/重新生成资源(Delete)")]
        public static void ReBuildAllAssetBundles()
        {
            ResBundleTools.ReBuildAllAssetBundles();
        }
        /// <summary>
        /// 导出资源
        /// </summary>
        [MenuItem("★工具★/资源打包/生成资源")]
        public static void BuildAllAssetBundles()
        {
            ResBundleTools.BuildAllAssetBundles();
        }


        /// <summary>
        /// 打开资源目录
        /// </summary>
        [MenuItem("★工具★/资源打包/Show in Explorer")]
        public static void ShowInExplorer()
        {
            ToolsHelper.ShowExplorer(ResBundleTools.GetExportPath());
        }
        #endregion

        #region 用户数据
        /// <summary>
        /// 清理PC用户资源数据
        /// </summary>
        [MenuItem("★工具★/用户数据/删除UserData")]
        public static void ClearPCPersistentData()
        {
            string DataPath = Application.dataPath;
            string userPathBas = DataPath.Substring(0,DataPath.LastIndexOf("/") + 1);
            userPathBas = Path.Combine(userPathBas, "LocalFile");
            if (Directory.Exists(userPathBas))
            {
                foreach (string dir in Directory.GetDirectories(userPathBas))
                    Directory.Delete(dir, true);
                ToolsHelper.Log("删除PersistentData完成！");
            }
        }
        /// <summary>
        /// 清理PlayerPrefs
        /// </summary>
        [MenuItem("★工具★/用户数据/删除缓存(PlayerPrefs)")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            ToolsHelper.Log("删除缓存(PlayerPrefs)完成！");
        }
        /// <summary>
        /// 打开户资源数据
        /// </summary>
        [MenuItem("★工具★/用户数据/Show in Explorer")]
        public static void ShowInExplorerPersistentData()
        {
            string DataPath = Application.dataPath;
            string userPathBas = DataPath.Substring(0, DataPath.LastIndexOf("/") + 1);
            userPathBas = Path.Combine(userPathBas, "LocalFile");
            ToolsHelper.ShowExplorer(userPathBas);
        }
        #endregion

        #region 工具
        
        [MenuItem("★工具★/打开游戏工具")]
        public static void OpenTools()
        {
            //CLog.Log(AppSetting.DataPath);
            string path = Path.Combine(AppSetting.DataPath, "../ProtoConfig/工具/Tools.exe");
            ToolsHelper.OpenEXE(path);
        }
        //[MenuItem("★工具★/打开热更项目文件夹")]
        //public static void OpenHotProject()
        //{
        //    string path = Path.Combine(System.Environment.CurrentDirectory, "../" + AppSetting.HotFixName);
        //    ToolsHelper.ShowExplorer(path);
        //}
        #endregion

        #region 多语言设置
        [MenuItem("★工具★/多语言[Editor]/重新加载多语言配置",false,0)]
        public static void ReLoadLangConfig()
        {
            LangService.Instance.LoadConfig();
            LangService.Instance.RefAllText();
        }
        [MenuItem("★工具★/多语言[Editor]/简体中文")]
        public static void LangSetZH_CN()
        {
            LangService.Instance.LangType = ELangType.ZH_CN;
        }
        [MenuItem("★工具★/多语言[Editor]/简体中文", true)]
        public static bool LangSetZH_CN_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/简体中文", LangService.Instance.LangType == ELangType.ZH_CN);
            return true;
        }
        [MenuItem("★工具★/多语言[Editor]/繁体中文")]
        public static void LangSetZH_TW()
        {
            LangService.Instance.LangType = ELangType.ZH_TW;
        }
        [MenuItem("★工具★/多语言[Editor]/繁体中文", true)]
        public static bool LangSetZH_TW_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/繁体中文", LangService.Instance.LangType == ELangType.ZH_TW);
            return true;
        }
        [MenuItem("★工具★/多语言[Editor]/英文")]
        public static void LangSetZH_EN()
        {
            LangService.Instance.LangType = ELangType.EN;
        }
        [MenuItem("★工具★/多语言[Editor]/英文", true)]
        public static bool LangSetZH_EN_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/英文", LangService.Instance.LangType == ELangType.EN);
            return true;
        }
        [MenuItem("★工具★/多语言[Editor]/日语")]
        public static void LangSetZH_JA()
        {
            LangService.Instance.LangType = ELangType.JA;
        }
        [MenuItem("★工具★/多语言[Editor]/日语", true)]
        public static bool LangSetZH_JA_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/日语", LangService.Instance.LangType == ELangType.JA);
            return true;
        }
        [MenuItem("★工具★/多语言[Editor]/韩语")]
        public static void LangSetZH_KO()
        {
            LangService.Instance.LangType = ELangType.KO;
        }
        [MenuItem("★工具★/多语言[Editor]/韩语", true)]
        public static bool LangSetZH_KO_Valide()
        {
            Menu.SetChecked("★工具★/多语言[Editor]/韩语", LangService.Instance.LangType == ELangType.KO);
            return true;
        }
        #endregion

        #region 场景切换       
        /// <summary>
        /// 打开主场景之前的一个场景
        /// </summary>
        [MenuItem("★工具★/打开上个场景 _F4")]
        public static void OpenLastScene()
        {
            var lastScene = EditorPrefs.GetString(LastScenePrefKey);
            if (!string.IsNullOrEmpty(lastScene))
                ToolsHelper.OpenScene(lastScene);
            else
                ToolsHelper.Error("Not found last scene!");
        }
        /// <summary>
        /// 打开主场景
        /// </summary>
        [MenuItem("★工具★/开始游戏 _F5")]
        public static void OpenMainScene()
        {
#if UNITY_5|| UNITY_2017_1_OR_NEWER
            var currentScene = EditorSceneManager.GetActiveScene().path;
#else
            var currentScene = EditorApplication.currentScene;
#endif
            var mainScene = "Assets/GameRes/BundleRes/Scene/MineGameMain.unity";
            if (mainScene != currentScene)
                EditorPrefs.SetString(LastScenePrefKey, currentScene);

            ToolsHelper.OpenScene(mainScene);

            if (!EditorApplication.isPlaying)
                EditorApplication.isPlaying = true;

        }

//        /// <summary>
//        /// 打开主场景
//        /// </summary>
//        [MenuItem("★工具★/关卡编辑器")]
//        public static void OpenEdtiroScene()
//        {
//#if UNITY_5|| UNITY_2017_1_OR_NEWER
//            var currentScene = EditorSceneManager.GetActiveScene().path;
//#else
//            var currentScene = EditorApplication.currentScene;
//#endif
//            var mainScene = "Assets/GameRes/ArtRes/MapEditor.unity";
//            if (mainScene != currentScene)
//                EditorPrefs.SetString(LastScenePrefKey, currentScene);

//            ToolsHelper.OpenScene(mainScene);
//            //设置关卡编辑分辨率
//            var type = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
//            var window = EditorWindow.GetWindow(type);
//            var SizeSelectionCallback = type.GetMethod("SizeSelectionCallback",
//                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
//            SizeSelectionCallback.Invoke(window, new object[] { 20, null });

//            if (!EditorApplication.isPlaying)
//                EditorApplication.isPlaying = true;

//        }
        #endregion

        #region 一键打包
        //[MenuItem("★工具★/一键打包/平台宏切换/AccountPwd(账号密码)")]
        //public static void ChangeAccountPwd()
        //{
        //    BuildAPKTools.ChagePlatformType(EPlatformType.AccountPwd);
        //}
        //[MenuItem("★工具★/一键打包/平台宏切换/AccountPwd(账号密码)", true)]
        //public static bool ChangeAccountPwd_Valide()
        //{
        //    Menu.SetChecked("★工具★/一键打包/平台宏切换/AccountPwd(账号密码)", AppSetting.PlatformType == EPlatformType.AccountPwd);
        //    return true;
        //}
        //[MenuItem("★工具★/一键打包/平台宏切换/HY_MC(互娱MyCard)")]
        //public static void ChangeHY_MC()
        //{
        //    BuildAPKTools.ChagePlatformType(EPlatformType.HY_MC);
        //} 
        //[MenuItem("★工具★/一键打包/平台宏切换/HY_MC(互娱MyCard)", true)]
        //public static bool ChangeHY_MC_Valide()
        //{
        //    Menu.SetChecked("★工具★/一键打包/平台宏切换/HY_MC(互娱MyCard)", AppSetting.PlatformType== EPlatformType.HY_MC);
        //    return true;
        //}
        //[MenuItem("★工具★/一键打包/平台宏切换/HY_GP(互娱GooglePay)")]
        //public static void ChangeHY_GP()
        //{
        //    BuildAPKTools.ChagePlatformType(EPlatformType.HY_GP);
        //}
        //[MenuItem("★工具★/一键打包/平台宏切换/HY_GP(互娱GooglePay)", true)]
        //public static bool ChangeHY_GP_Valide()
        //{
        //    Menu.SetChecked("★工具★/一键打包/平台宏切换/HY_GP(互娱GooglePay)", AppSetting.PlatformType == EPlatformType.HY_GP);
        //    return true;
        //}
        //[MenuItem("★工具★/一键打包/平台宏切换/HY_IOS(互娱IOS)")]
        //public static void ChangeHY_IOS()
        //{
        //    BuildAPKTools.ChagePlatformType(EPlatformType.HY_IOS);
        //}
        //[MenuItem("★工具★/一键打包/平台宏切换/HY_IOS(互娱IOS)", true)]
        //public static bool ChangeHY_IOS_Valide()
        //{
        //    Menu.SetChecked("★工具★/一键打包/平台宏切换/HY_IOS(互娱IOS)", AppSetting.PlatformType == EPlatformType.HY_IOS);
        //    return true;
        //}

        [MenuItem("★工具★/一键打包/测试包(安卓)")]
        public static void CreateSDKTestApp()
        {
            if ( EditorUserBuildSettings.activeBuildTarget!= BuildTarget.Android)
            {
                ToolsHelper.Log($"平台选择错误，目前平台不是Android，是{EditorUserBuildSettings.activeBuildTarget}");
                return;
            }
            BuildAPKTools.ModifyBuildSettingsScene();
            BuildAPKTools.BulidTarget(EPlatformType.SDK_Test);
            BuildAPKTools.ReductionBuildSettingsScene();
        }

        [MenuItem("★工具★/一键打包/上线包(安卓)")]
        public static void CreateSDK_ReleaseApp()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                ToolsHelper.Log($"平台选择错误，目前平台不是Android，是{EditorUserBuildSettings.activeBuildTarget}");
                return;
            }
            BuildAPKTools.ModifyBuildSettingsScene();
            BuildAPKTools.BulidTarget(EPlatformType.SDK_Release);
            BuildAPKTools.ReductionBuildSettingsScene();
        }

        [MenuItem("★工具★/一键打包/版本包(安卓)")]
        public static void CreateVersionPackageApp()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                ToolsHelper.Log($"平台选择错误，目前平台不是Android，是{EditorUserBuildSettings.activeBuildTarget}");
                return;
            }
            BuildAPKTools.ModifyBuildSettingsScene();
            BuildAPKTools.BulidTarget(EPlatformType.VersionPackage);
            BuildAPKTools.ReductionBuildSettingsScene();
        }

        [MenuItem("★工具★/一键打包/IOS")]
        public static void IOS_ReleaseApp()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                ToolsHelper.Log($"平台选择错误，目前平台不是iOS，是{EditorUserBuildSettings.activeBuildTarget}");
                return;
            }
            BuildAPKTools.ModifyBuildSettingsScene();
            BuildAPKTools.BulidTarget(EPlatformType.SDK_ReleaseIOS);
            BuildAPKTools.ReductionBuildSettingsScene();
        }
        #endregion
        #region SDK

        //[MenuItem("★工具★/SDK/生成SDK脚本-勿点")]
        //public static void CreateSDKScript()
        //{
        //    UIScriptExport.ExportSDKScript();
        //}
        #endregion
        #region 实用工具

        //[MenuItem("★工具★/实用工具/Remove预设Text黑体字体")]
        //public static void RemoveUIPrefabTextFont()
        //{
        //    ResBundleTools.RemoveUIPrefabTextFont();
        //}

        [MenuItem("★工具★/实用工具/UI预设Text丢失字体设置")]
        public static void SetUIPrefabTextFont()
        {
            ResBundleTools.SetUIPrefabTextFont();
        }

        #endregion
    }
}