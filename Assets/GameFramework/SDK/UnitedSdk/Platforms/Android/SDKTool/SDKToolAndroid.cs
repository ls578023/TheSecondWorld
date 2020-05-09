//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK{
 public class SDKToolAndroid{
  
/// <summary>
/// 调用手机震动 milliseconds 震动时间（毫秒）shakeLevel 震动强弱 0：低强度(默认), 1：中等强度, 2：高强度 需要添加权限 <uses-permission android:name="android.permission.VIBRATE" />
 /// </summary>
public static void VibrateStatic(long milliseconds,int shakeLevel)
    {
        GameFramework.CLog.Log($"调用安卓vibrateStatic: milliseconds[{milliseconds}]shakeLevel[{shakeLevel}]");
        AndroidClass.GameActHelper.CallStatic("vibrateStatic",milliseconds,shakeLevel);
    }
  
/// <summary>
/// 弹出Toast提示
 /// </summary>
public static void ShowToast(string msg)
    {
        GameFramework.CLog.Log($"调用安卓showToast: msg[{msg}]");
        AndroidClass.GameActHelper.CallStatic("showToast",msg);
    }
  
/// <summary>
/// 展示退出弹框
 /// </summary>
public static void  ShowExitDialog()
    {
        GameFramework.CLog.Log($"调用安卓showExitDialog: ");
        AndroidClass.GameActHelper.CallStatic("showExitDialog");
    }
  
/// <summary>
/// 打开URL
 /// </summary>
public static void OpenUrl(string url)
    {
        GameFramework.CLog.Log($"调用安卓openUrl: url[{url}]");
        AndroidClass.GameActHelper.CallStatic("openUrl",url);
    }
  
/// <summary>
/// 拷贝字符串到剪切板
 /// </summary>
public static void CopyMsgToClipboard(string msg)
    {
        GameFramework.CLog.Log($"调用安卓copyMsgToClipboard: msg[{msg}]");
        AndroidClass.GameActHelper.CallStatic("copyMsgToClipboard",msg);
    }
  
/// <summary>
/// 获得剪贴板内容, 未获取到值得情况下，等待5 * 100ms后再返回
 /// </summary>
public static string GetClipboardMsg()
    {
        GameFramework.CLog.Log($"调用安卓getClipboardMsg: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getClipboardMsg");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 拷贝图片到相册 copyFile:图片路径
 /// </summary>
public static void Copy2SystemDCIM(string copyFile)
    {
        GameFramework.CLog.Log($"调用安卓copy2SystemDCIM: copyFile[{copyFile}]");
        AndroidClass.GameActHelper.CallStatic("copy2SystemDCIM",copyFile);
    }
  
/// <summary>
/// 打开应用市场并跳转App详情页 packageName:需要跳转的App包名
 /// </summary>
public static void GotoAppStore(string packageName)
    {
        GameFramework.CLog.Log($"调用安卓gotoAppStore: packageName[{packageName}]");
        AndroidClass.GameActHelper.CallStatic("gotoAppStore",packageName);
    }
  
/// <summary>
/// 获得网络类型  wifi、2g、3g、4g、5g
 /// </summary>
public static string GetNetworkTypeStatic()
    {
        GameFramework.CLog.Log($"调用安卓getNetworkTypeStatic: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getNetworkTypeStatic");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否显示隐私政策和用户协议
 /// </summary>
public static bool IsShowPolicy()
    {
        GameFramework.CLog.Log($"调用安卓isShowPolicy: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isShowPolicy");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 跳转到到用户协议
 /// </summary>
public static void GotoTermsServiceStatic()
    {
        GameFramework.CLog.Log($"调用安卓gotoTermsServiceStatic: ");
        AndroidClass.GameActHelper.CallStatic("gotoTermsServiceStatic");
    }
  
/// <summary>
/// 跳转到到隐私政策
 /// </summary>
public static void GotoPrivacyPolicyStatic()
    {
        GameFramework.CLog.Log($"调用安卓gotoPrivacyPolicyStatic: ");
        AndroidClass.GameActHelper.CallStatic("gotoPrivacyPolicyStatic");
    }
  
/// <summary>
/// 打开三方分享App id :三方分享AppID
 /// </summary>
public static void OpenApp(int id)
    {
        GameFramework.CLog.Log($"调用安卓openApp: id[{id}]");
        AndroidClass.GameActHelper.CallStatic("openApp",id);
    }
  
/// <summary>
/// 判断三方分享App是否安装 新浪微博:0 微信：1 QQ：2 QQ空间：3 Facebook：4 Twitter：5
 /// </summary>
public static bool IsAppInstalled(int id)
    {
        GameFramework.CLog.Log($"调用安卓isAppInstalled: id[{id}]");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isAppInstalled",id);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否显示实名注册，用于部分市场在打包脚本中控制变量 true: 显示;  false:不显示
 /// </summary>
public static bool IsShowRealNameRegistration()
    {
        GameFramework.CLog.Log($"调用安卓isShowRealNameRegistration: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isShowRealNameRegistration");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否显示联系方式  true: 显示;  false:不显示
 /// </summary>
public static bool IsShowContactInformation()
    {
        GameFramework.CLog.Log($"调用安卓isShowContactInformation: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isShowContactInformation");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取使用时长 存在20s的误差 返回App使用时长， 单位s
 /// </summary>
public static int GetDurationTimeStatic()
    {
        GameFramework.CLog.Log($"调用安卓getDurationTimeStatic: ");
        int Reuslt = AndroidClass.GameActHelper.CallStatic<int>("getDurationTimeStatic");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否需要在线下载资源 true需要，默认为不需要
 /// </summary>
public static bool OnlineResStatic()
    {
        GameFramework.CLog.Log($"调用安卓onlineResStatic: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("onlineResStatic");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// U3D游戏进入游戏
 /// </summary>
public static void  U3DGameStart()
    {
        GameFramework.CLog.Log($"调用安卓u3DGameStart: ");
        AndroidClass.GameActHelper.CallStatic("u3DGameStart");
    }
  
/// <summary>
/// 预加载音效
 /// </summary>
public static void  PreloadEffect(string path)
    {
        GameFramework.CLog.Log($"调用安卓preloadEffect: path[{path}]");
        AndroidClass.GameActHelper.CallStatic("preloadEffect",path);
    }
  
/// <summary>
/// 播放音效 path：音效文件路径 isLoop:是否循环
 /// </summary>
public static int PlayEffect(string path,bool isLoop)
    {
        GameFramework.CLog.Log($"调用安卓playEffect: path[{path}]isLoop[{isLoop}]");
        int Reuslt = AndroidClass.GameActHelper.CallStatic<int>("playEffect",path,isLoop);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 播放音效 ：path：音效文件路径 isLoop：是否循环 pitch：振幅频率  通常为1.0f        pan:立体声效应 0.0f-1.0f之间 gain：音量0.0f-1.0f之间
 /// </summary>
public static int PlayEffect(string path,bool isLoop,float pitch,float pan,float gain)
    {
        GameFramework.CLog.Log($"调用安卓playEffect: path[{path}]isLoop[{isLoop}]pitch[{pitch}]pan[{pan}]gain[{gain}]");
        int Reuslt = AndroidClass.GameActHelper.CallStatic<int>("playEffect",path,isLoop,pitch,pan,gain);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 暂停播放
 /// </summary>
public static void PauseEffect(int soundId)
    {
        GameFramework.CLog.Log($"调用安卓pauseEffect: soundId[{soundId}]");
        AndroidClass.GameActHelper.CallStatic("pauseEffect",soundId);
    }
  
/// <summary>
/// 回复播放
 /// </summary>
public static void ResumeEffect(int soundId)
    {
        GameFramework.CLog.Log($"调用安卓resumeEffect: soundId[{soundId}]");
        AndroidClass.GameActHelper.CallStatic("resumeEffect",soundId);
    }
  
/// <summary>
/// 停止播放
 /// </summary>
public static void StopEffect(int soundId)
    {
        GameFramework.CLog.Log($"调用安卓stopEffect: soundId[{soundId}]");
        AndroidClass.GameActHelper.CallStatic("stopEffect",soundId);
    }
  
/// <summary>
/// 获取音效音量
 /// </summary>
public static float GetEffectsVolume()
    {
        GameFramework.CLog.Log($"调用安卓getEffectsVolume: ");
        float Reuslt = AndroidClass.GameActHelper.CallStatic<float>("getEffectsVolume");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 设置音效音量
 /// </summary>
public static void SetEffectsVolume(float volume)
    {
        GameFramework.CLog.Log($"调用安卓setEffectsVolume: volume[{volume}]");
        AndroidClass.GameActHelper.CallStatic("setEffectsVolume",volume);
    }
  
/// <summary>
/// 从内部缓冲区卸载预加载效果。
 /// </summary>
public static void UnloadEffect(string path)
    {
        GameFramework.CLog.Log($"调用安卓unloadEffect: path[{path}]");
        AndroidClass.GameActHelper.CallStatic("unloadEffect",path);
    }
  
/// <summary>
/// 暂停所有音效
 /// </summary>
public static void PauseAllEffects()
    {
        GameFramework.CLog.Log($"调用安卓pauseAllEffects: ");
        AndroidClass.GameActHelper.CallStatic("pauseAllEffects");
    }
  
/// <summary>
/// 恢复所有音效
 /// </summary>
public static void ResumeAllEffects()
    {
        GameFramework.CLog.Log($"调用安卓resumeAllEffects: ");
        AndroidClass.GameActHelper.CallStatic("resumeAllEffects");
    }
  
/// <summary>
/// 停止所有音效
 /// </summary>
public static void StopAllEffects()
    {
        GameFramework.CLog.Log($"调用安卓stopAllEffects: ");
        AndroidClass.GameActHelper.CallStatic("stopAllEffects");
    }
  
/// <summary>
/// 设置音效焦点
 /// </summary>
public static void SetAudioFocus(bool isAudioFocus)
    {
        GameFramework.CLog.Log($"调用安卓setAudioFocus: isAudioFocus[{isAudioFocus}]");
        AndroidClass.GameActHelper.CallStatic("setAudioFocus",isAudioFocus);
    }
  
/// <summary>
/// 释放所有音效
 /// </summary>
public static void End()
    {
        GameFramework.CLog.Log($"调用安卓end: ");
        AndroidClass.GameActHelper.CallStatic("end");
    }
  
/// <summary>
/// 获取当前距离第一次启动的天数,当天为：1
 /// </summary>
public static int GetDaysByFirstLaunch()
    {
        GameFramework.CLog.Log($"调用安卓getDaysByFirstLaunch: ");
        int Reuslt = AndroidClass.GameActHelper.CallStatic<int>("getDaysByFirstLaunch");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否是测试版本
 /// </summary>
public static bool IsDebugVersion()
    {
        GameFramework.CLog.Log($"调用安卓isDebugVersion: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isDebugVersion");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否显示“版号信息”文字按钮，0：显示，1：不显示；未配置/未获取到在线参数默认为0
 /// </summary>
public static int GetGameBanHaoType()
    {
        GameFramework.CLog.Log($"调用安卓getGameBanHaoType: ");
        int Reuslt = AndroidClass.GameActHelper.CallStatic<int>("getGameBanHaoType");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 点击“版号信息”按钮 展示界面
 /// </summary>
public static void ShowGameBanHao()
    {
        GameFramework.CLog.Log($"调用安卓showGameBanHao: ");
        AndroidClass.GameActHelper.CallStatic("showGameBanHao");
    }

}
}

