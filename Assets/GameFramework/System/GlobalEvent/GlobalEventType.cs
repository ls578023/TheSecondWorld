using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
    public class GlobalEventType
    {
        #region 广告SDK
        /// <summary>
        /// 通知播放视频广告按钮状态 string status  0：不显示，1：显示，置灰，不可点 ，2：显示，点亮，可点击
        /// </summary>
        public const string VideoButtonStatus = "VideoButtonStatus";
        #endregion

        /// <summary>
        /// 全局音量改变 0-VoiceType 1-音量
        /// </summary>
        public const string GlobalVolumeChange = "GlobalVolumeChange";
        /// <summary>
        /// 游戏暂停
        /// </summary>
        public const string GamePause = "GamePause";

        /// <summary>
        /// 预加载开始
        /// </summary>
        public const string PreLoadStart = "PreLoadStart";
        /// <summary>
        /// 音效开始播放
        /// </summary>
        public const string AudioPlayStart = "AudioPlayStart";

        /// <summary>
        /// 音效播放结束
        /// </summary>
        public const string AudioPlayEnd = "AudioPlayEnd";
        /// <summary>
        /// 场景加载完成
        /// </summary>
        public const string SceneLoadOver = "SceneLoadOver";

        /// <summary>
        /// 进入游戏
        /// </summary>
        public const string EnterGame = "EnterGame";
        /// <summary>
        /// 准备退出游戏 点了退出/主页按钮后调用
        /// </summary>
        public const string ReadyExitGame = "ReadyExitGame";

        /// <summary>
        /// 退出之前游戏业务逻辑处理
        /// </summary>
        public const string MineGameExitLogic = "MineGameExitLogic";
        /// <summary> 
        /// 退出游戏 客户端完成数据保存后
        /// </summary>
        public const string ExitGame = "ExitGame";
        /// <summary> 
        /// 返回框架主界面
        /// </summary>
        public const string BackGameCollection = "BackGameCollection";
        /// <summary>
        /// 按下安卓返回键
        /// </summary>
        public const string AndroidBackKey = "AndroidBackKey";
        /// <summary>
        /// 打开UI
        /// </summary>
        public const string OpenUI = "OpenUI";
        /// <summary>
        /// 关闭UI
        /// </summary>
        public const string CloseUI = "CloseUI";
        /// <summary>
        /// 游戏进入新的一天 0-第几天
        /// </summary>
        public const string GameEnterNewDay = "GameEnterNewDay";


        public const string MainPanel_StartGame = "MainPanel_StartGame";

    }
}


