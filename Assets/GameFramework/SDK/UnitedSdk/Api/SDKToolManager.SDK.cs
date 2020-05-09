//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK
{
public partial class SDKToolManager
{
    
    /// <summary>
    /// 调用手机震动 milliseconds 震动时间（毫秒）shakeLevel 震动强弱 0：低强度(默认), 1：中等强度, 2：高强度 需要添加权限 <uses-permission android:name="android.permission.VIBRATE" />
    /// </summary>
    public  void VibrateStatic(long milliseconds,int shakeLevel){
        GameFramework.CLog.Log($"调用代理层vibrateStatic:milliseconds[{milliseconds}]shakeLevel[{shakeLevel}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.VibrateStatic(milliseconds,shakeLevel);
        #elif UNITY_IOS 
        SDKToolIOS.VibrateStatic(milliseconds,shakeLevel);
        #endif

    }


    /// <summary>
    /// 弹出Toast提示
    /// </summary>
    public  void ShowToast(string msg){
        GameFramework.CLog.Log($"调用代理层showToast:msg[{msg}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.ShowToast(msg);
        #elif UNITY_IOS 
        SDKToolIOS.ShowToast(msg);
        #endif

    }


    /// <summary>
    /// 展示退出弹框
    /// </summary>
    public  void  ShowExitDialog(){
        GameFramework.CLog.Log($"调用代理层showExitDialog:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.ShowExitDialog();
        #elif UNITY_IOS 
        SDKToolIOS.ShowExitDialog();
        #endif

    }


    /// <summary>
    /// 打开URL
    /// </summary>
    public  void OpenUrl(string url){
        GameFramework.CLog.Log($"调用代理层openUrl:url[{url}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.OpenUrl(url);
        #elif UNITY_IOS 
        SDKToolIOS.OpenUrl(url);
        #endif

    }


    /// <summary>
    /// 拷贝字符串到剪切板
    /// </summary>
    public  void CopyMsgToClipboard(string msg){
        GameFramework.CLog.Log($"调用代理层copyMsgToClipboard:msg[{msg}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.CopyMsgToClipboard(msg);
        #elif UNITY_IOS 
        SDKToolIOS.CopyMsgToClipboard(msg);
        #endif

    }


    /// <summary>
    /// 获得剪贴板内容, 未获取到值得情况下，等待5 * 100ms后再返回
    /// </summary>
    public  string GetClipboardMsg(){
        GameFramework.CLog.Log($"调用代理层getClipboardMsg:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.GetClipboardMsg();
        #elif UNITY_IOS
        Result = SDKToolIOS.GetClipboardMsg();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 拷贝图片到相册 copyFile:图片路径
    /// </summary>
    public  void Copy2SystemDCIM(string copyFile){
        GameFramework.CLog.Log($"调用代理层copy2SystemDCIM:copyFile[{copyFile}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.Copy2SystemDCIM(copyFile);
        #elif UNITY_IOS 
        SDKToolIOS.Copy2SystemDCIM(copyFile);
        #endif

    }


    /// <summary>
    /// 打开应用市场并跳转App详情页 packageName:需要跳转的App包名
    /// </summary>
    public  void GotoAppStore(string packageName){
        GameFramework.CLog.Log($"调用代理层gotoAppStore:packageName[{packageName}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.GotoAppStore(packageName);
        #elif UNITY_IOS 
        SDKToolIOS.GotoAppStore(packageName);
        #endif

    }


    /// <summary>
    /// 获得网络类型  wifi、2g、3g、4g、5g
    /// </summary>
    public  string GetNetworkTypeStatic(){
        GameFramework.CLog.Log($"调用代理层getNetworkTypeStatic:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.GetNetworkTypeStatic();
        #elif UNITY_IOS
        Result = SDKToolIOS.GetNetworkTypeStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否显示隐私政策和用户协议
    /// </summary>
    public  bool IsShowPolicy(){
        GameFramework.CLog.Log($"调用代理层isShowPolicy:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.IsShowPolicy();
        #elif UNITY_IOS
        Result = SDKToolIOS.IsShowPolicy();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 跳转到到用户协议
    /// </summary>
    public  void GotoTermsServiceStatic(){
        GameFramework.CLog.Log($"调用代理层gotoTermsServiceStatic:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.GotoTermsServiceStatic();
        #elif UNITY_IOS 
        SDKToolIOS.GotoTermsServiceStatic();
        #endif

    }


    /// <summary>
    /// 跳转到到隐私政策
    /// </summary>
    public  void GotoPrivacyPolicyStatic(){
        GameFramework.CLog.Log($"调用代理层gotoPrivacyPolicyStatic:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.GotoPrivacyPolicyStatic();
        #elif UNITY_IOS 
        SDKToolIOS.GotoPrivacyPolicyStatic();
        #endif

    }


    /// <summary>
    /// 打开三方分享App id :三方分享AppID
    /// </summary>
    public  void OpenApp(int id){
        GameFramework.CLog.Log($"调用代理层openApp:id[{id}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.OpenApp(id);
        #elif UNITY_IOS 
        SDKToolIOS.OpenApp(id);
        #endif

    }


    /// <summary>
    /// 判断三方分享App是否安装 新浪微博:0 微信：1 QQ：2 QQ空间：3 Facebook：4 Twitter：5
    /// </summary>
    public  bool IsAppInstalled(int id){
        GameFramework.CLog.Log($"调用代理层isAppInstalled:id[{id}]");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.IsAppInstalled(id);
        #elif UNITY_IOS
        Result = SDKToolIOS.IsAppInstalled(id);
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否显示实名注册，用于部分市场在打包脚本中控制变量 true: 显示;  false:不显示
    /// </summary>
    public  bool IsShowRealNameRegistration(){
        GameFramework.CLog.Log($"调用代理层isShowRealNameRegistration:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.IsShowRealNameRegistration();
        #elif UNITY_IOS
        Result = SDKToolIOS.IsShowRealNameRegistration();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否显示联系方式  true: 显示;  false:不显示
    /// </summary>
    public  bool IsShowContactInformation(){
        GameFramework.CLog.Log($"调用代理层isShowContactInformation:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.IsShowContactInformation();
        #elif UNITY_IOS
        Result = SDKToolIOS.IsShowContactInformation();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取使用时长 存在20s的误差 返回App使用时长， 单位s
    /// </summary>
    public  int GetDurationTimeStatic(){
        GameFramework.CLog.Log($"调用代理层getDurationTimeStatic:");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.GetDurationTimeStatic();
        #elif UNITY_IOS
        Result = SDKToolIOS.GetDurationTimeStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否需要在线下载资源 true需要，默认为不需要
    /// </summary>
    public  bool OnlineResStatic(){
        GameFramework.CLog.Log($"调用代理层onlineResStatic:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.OnlineResStatic();
        #elif UNITY_IOS
        Result = SDKToolIOS.OnlineResStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// U3D游戏进入游戏
    /// </summary>
    public  void  U3DGameStart(){
        GameFramework.CLog.Log($"调用代理层u3DGameStart:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.U3DGameStart();
        #elif UNITY_IOS 
        SDKToolIOS.U3DGameStart();
        #endif

    }


    /// <summary>
    /// 预加载音效
    /// </summary>
    public  void  PreloadEffect(string path){
        GameFramework.CLog.Log($"调用代理层preloadEffect:path[{path}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.PreloadEffect(path);
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 播放音效 path：音效文件路径 isLoop:是否循环
    /// </summary>
    public  int PlayEffect(string path,bool isLoop){
        GameFramework.CLog.Log($"调用代理层playEffect:path[{path}]isLoop[{isLoop}]");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.PlayEffect(path,isLoop);
        #elif UNITY_IOS
        
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 播放音效 ：path：音效文件路径 isLoop：是否循环 pitch：振幅频率  通常为1.0f        pan:立体声效应 0.0f-1.0f之间 gain：音量0.0f-1.0f之间
    /// </summary>
    public  int PlayEffect(string path,bool isLoop,float pitch,float pan,float gain){
        GameFramework.CLog.Log($"调用代理层playEffect:path[{path}]isLoop[{isLoop}]pitch[{pitch}]pan[{pan}]gain[{gain}]");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.PlayEffect(path,isLoop,pitch,pan,gain);
        #elif UNITY_IOS
        
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 暂停播放
    /// </summary>
    public  void PauseEffect(int soundId){
        GameFramework.CLog.Log($"调用代理层pauseEffect:soundId[{soundId}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.PauseEffect(soundId);
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 回复播放
    /// </summary>
    public  void ResumeEffect(int soundId){
        GameFramework.CLog.Log($"调用代理层resumeEffect:soundId[{soundId}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.ResumeEffect(soundId);
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 停止播放
    /// </summary>
    public  void StopEffect(int soundId){
        GameFramework.CLog.Log($"调用代理层stopEffect:soundId[{soundId}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.StopEffect(soundId);
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 获取音效音量
    /// </summary>
    public  float GetEffectsVolume(){
        GameFramework.CLog.Log($"调用代理层getEffectsVolume:");
        
        float Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.GetEffectsVolume();
        #elif UNITY_IOS
        
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 设置音效音量
    /// </summary>
    public  void SetEffectsVolume(float volume){
        GameFramework.CLog.Log($"调用代理层setEffectsVolume:volume[{volume}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.SetEffectsVolume(volume);
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 从内部缓冲区卸载预加载效果。
    /// </summary>
    public  void UnloadEffect(string path){
        GameFramework.CLog.Log($"调用代理层unloadEffect:path[{path}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.UnloadEffect(path);
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 暂停所有音效
    /// </summary>
    public  void PauseAllEffects(){
        GameFramework.CLog.Log($"调用代理层pauseAllEffects:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.PauseAllEffects();
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 恢复所有音效
    /// </summary>
    public  void ResumeAllEffects(){
        GameFramework.CLog.Log($"调用代理层resumeAllEffects:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.ResumeAllEffects();
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 停止所有音效
    /// </summary>
    public  void StopAllEffects(){
        GameFramework.CLog.Log($"调用代理层stopAllEffects:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.StopAllEffects();
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 设置音效焦点
    /// </summary>
    public  void SetAudioFocus(bool isAudioFocus){
        GameFramework.CLog.Log($"调用代理层setAudioFocus:isAudioFocus[{isAudioFocus}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.SetAudioFocus(isAudioFocus);
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 释放所有音效
    /// </summary>
    public  void End(){
        GameFramework.CLog.Log($"调用代理层end:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.End();
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 获取当前距离第一次启动的天数,当天为：1
    /// </summary>
    public  int GetDaysByFirstLaunch(){
        GameFramework.CLog.Log($"调用代理层getDaysByFirstLaunch:");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.GetDaysByFirstLaunch();
        #elif UNITY_IOS
        Result = SDKToolIOS.GetDaysByFirstLaunch();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否是测试版本
    /// </summary>
    public  bool IsDebugVersion(){
        GameFramework.CLog.Log($"调用代理层isDebugVersion:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.IsDebugVersion();
        #elif UNITY_IOS
        Result = SDKToolIOS.IsDebugVersion();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否显示“版号信息”文字按钮，0：显示，1：不显示；未配置/未获取到在线参数默认为0
    /// </summary>
    public  int GetGameBanHaoType(){
        GameFramework.CLog.Log($"调用代理层getGameBanHaoType:");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = SDKToolAndroid.GetGameBanHaoType();
        #elif UNITY_IOS
        Result = SDKToolIOS.GetGameBanHaoType();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 点击“版号信息”按钮 展示界面
    /// </summary>
    public  void ShowGameBanHao(){
        GameFramework.CLog.Log($"调用代理层showGameBanHao:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        SDKToolAndroid.ShowGameBanHao();
        #elif UNITY_IOS 
        SDKToolIOS.ShowGameBanHao();
        #endif

    }


}
}

