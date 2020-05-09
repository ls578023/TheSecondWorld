using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBTSDK
{
    public class SdkDefine 
    {
        public const string AdsClassName = "com.pdragon.ad.AdsManagerTemplate";
        public const string ReportClassName = "com.pdragon.common.BaseActivityHelper";
    }
    /// <summary>
    /// 激励视频标志位
    /// </summary>
    public enum VideFlag
    {
        /// <summary>
        /// 视频送道具
        /// </summary>
        Flag1 = 0,
        /// <summary>
        /// 视频复活
        /// </summary>
        Flag2 = 1,
        /// <summary>
        /// 视频解释玩法/功能
        /// </summary>
        Flag3 = 2,

    }

    /// <summary>
    /// SDK功能开关
    /// </summary>
    public enum SDKOpenFunction
    {
        /// <summary>
        /// 安装版本 true-是
        /// </summary>
        InstallVersion,
        /// <summary>
        /// 是否第一次启动 true-是
        /// </summary>
        FirstStartVer,
        /// <summary>
        /// 是否wifi环境 true-是
        /// </summary>
        IsWifi,
        /// <summary>
        /// 是否越狱/root true-是
        /// </summary>
        isRootSystem,
        /// <summary>
        /// 是否显示多玩法 true-是
        /// </summary>
        ShowMoreGameStatic,
        /// <summary>
        /// 广告视频是否允许出现 已实现按钮方法
        /// </summary>
        AdsVideo,
        /// <summary>
        /// 获取Android审核参数 true 关闭审核 false开启审核
        /// </summary>
        DesignMode,
        /// <summary>
        /// 是否显示分享按钮 true-是 已实现按钮方法
        /// </summary>
        ShowShare,
        /// <summary>
        /// 是否显示登陆入口 true-是 已实现按钮方法
        /// </summary>
        ShowLogin,
        /// <summary>
        /// 是否显示反馈按钮 true-是 已实现按钮方法
        /// </summary>
        ShowFeedback,
        /// <summary>
        /// 是否为欧盟用户 true-是 已实现按钮方法
        /// </summary>
        GDPRUser,
        /// <summary>
        /// 是否显示隐私政策 已实现按钮方法
        /// </summary>
        ShowPolicy,
        /// <summary>
        /// 是否显示用户协议 已实现按钮方法
        /// </summary>
        ShowUserProtocol,
        /// <summary>
        /// 是否显示实名注册
        /// </summary>
        ShowRealName,
        /// <summary>
        /// 是否显示联系方式
        /// </summary>
        ShowContactInfo,
        /// <summary>
        /// 视频是否准备好
        /// </summary>
        VideoIsReady,
        /// <summary>
        /// 是否显示评价
        /// </summary>
        ShowEvaluate,
        /// <summary>
        /// 是否显示游戏版号信息 已实现按钮方法
        /// </summary>
        ShowGameBanHao
    }
}
