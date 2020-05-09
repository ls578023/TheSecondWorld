using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 全局数据类 配合UserDataPrefs存储需要本地化的数据
    /// 同时提供小游戏SDK属性接口
    /// </summary>
    public class GlobalData
    {
        /// <summary>
        /// 全局音乐音量
        /// </summary>
        public static float MusicGameVolume
        {
            get { return UserDataPrefs.GetFloat(UserDataKey.MusicGameVolume, 1); }
            set { UserDataPrefs.SetFloat(UserDataKey.MusicGameVolume, value); }
        }   /// <summary>
            /// 全局音效音量
            /// </summary>
        public static float SoundGameVolume
        {
            get { return UserDataPrefs.GetFloat(UserDataKey.SoundGameVolume, 1); }
            set { UserDataPrefs.SetFloat(UserDataKey.SoundGameVolume, value); }
        }
        /// <summary>
        /// 本地存储安装天数 注意刚刚进入游戏时，Day和InstallDay可能不一样，当进行本地存储和SDK端提供的数值比较后，2个值就相等了
        /// </summary>
        public static int Day
        {
            get { return UserDataPrefs.GetInt(UserDataKey.Day, 0); }
            set { UserDataPrefs.SetInt(UserDataKey.Day, value); }
        }

        public static bool IsUseApplicationFocus = true;
        public static string MineAppName;

        /// <summary>
        /// SDK 获取第几天
        /// </summary>
        public static int InstallDay
        {
            get
            {
#if UNITY_EDITOR
                return 1;
#else
                return DBTSDK.SDKToolManager.Instance.GetDaysByFirstLaunch();
#endif
            }
        }

        /// <summary>
        /// 是否开启视频广告
        /// </summary>
        public static bool ActiveVideoBtn
        {
            get
            {
                return DBTSDK.SDKOpenFunction.AdsVideo.IsOpen();
            }
        }
        /// <summary>
        /// 是否是安装/发布版本
        /// </summary>
        public static bool IsReleaseVersion
        {
            get
            {
#if VersionPackage //如果是版本包默认是发布版
                return true;
#else
                return !DBTSDK.SDKToolManager.Instance.IsDebugVer;
#endif
            }
        }
        /// <summary>
        /// 是否是在看视频
        /// </summary>
        public static bool IsLookVideo { get; set; }


#region Debug模式
        private static bool jumpVideo;
        /// <summary>
        /// 是否跳过视频
        /// </summary>
        public static bool JumpVideo { 
            get 
            {
                if (IsReleaseVersion)
                    return false;
                else
                    return jumpVideo;
            }
            set => jumpVideo = value; }


        private static bool hideVideo;
        /// <summary>
        /// 是否跳过视频
        /// </summary>
        public static bool HideVideo
        {
            get
            {
                if (IsReleaseVersion)
                    return false;
                else
                    return hideVideo;
            }
            set => hideVideo = value;
        }

#endregion

        /// <summary>
        /// 检测是否跨天
        /// </summary>
        public static void CheckNewDay()
        {
            //发布版关闭log
            CLog.SetIsShowLog(!IsReleaseVersion);
            if (Day != InstallDay)
            {
                Day = InstallDay;
                MsgDispatcher.SendMessage(GlobalEventType.GameEnterNewDay, Day);
            }
        }
        /// <summary>
        /// 游戏是否手动暂停了
        /// </summary>
        public static bool IsGamePause { get;private set; }
        /// <summary>
        /// 全局函数 暂停游戏
        /// </summary>
        /// <param name="i"></param>
        public static void PauseGame(bool i) 
        {
            IsGamePause = i;
            Time.timeScale = i ? 0 : 1;
        }
    }
}
