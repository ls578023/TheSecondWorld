//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK
{
public partial class AppUserManager
{
    
    /// <summary>
    /// 是否显示分享按钮，用于部分市场在打包脚本中控制变量
    /// </summary>
    public  bool IsShowShare(){
        GameFramework.CLog.Log($"调用代理层isShowShare:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppUserAndroid.IsShowShare();
        #elif UNITY_IOS
        Result = AppUserIOS.IsShowShare();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取分享方式 1：DBT复制粘贴分享;  2：系统分享
    /// </summary>
    public  int GetShareMode(){
        GameFramework.CLog.Log($"调用代理层getShareMode:");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppUserAndroid.GetShareMode();
        #elif UNITY_IOS
        Result = AppUserIOS.GetShareMode();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// U3D分享图片 title 分享标题 content 分享内容 url 分享URL imgFile 图片路径
    /// </summary>
    public  void ShareImage(string title,string content,string url,string imgFile){
        GameFramework.CLog.Log($"调用代理层shareImage:title[{title}]content[{content}]url[{url}]imgFile[{imgFile}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppUserAndroid.ShareImage(title,content,url,imgFile);
        #elif UNITY_IOS 
        AppUserIOS.ShareImage(title,content,url,imgFile);
        #endif

    }


    /// <summary>
    /// U3D分享文本  title 分享标题 content 分享内容 分享URL
    /// </summary>
    public  void ShareText(string title,string content,string url){
        GameFramework.CLog.Log($"调用代理层shareText:title[{title}]content[{content}]url[{url}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppUserAndroid.ShareText(title,content,url);
        #elif UNITY_IOS 
        AppUserIOS.ShareText(title,content,url);
        #endif

    }


    /// <summary>
    /// 获取分享URL 地址
    /// </summary>
    public  string GetShareUrl(){
        GameFramework.CLog.Log($"调用代理层getShareUrl:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppUserAndroid.GetShareUrl();
        #elif UNITY_IOS
        Result = AppUserIOS.GetShareUrl();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否显示登陆入口
    /// </summary>
    public  bool ShowLogin(){
        GameFramework.CLog.Log($"调用代理层showLogin:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppUserAndroid.ShowLogin();
        #elif UNITY_IOS
        Result = AppUserIOS.ShowLogin();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 一键登录
    /// </summary>
    public  void LoginComStatic(int inOrOut){
        GameFramework.CLog.Log($"调用代理层loginComStatic:inOrOut[{inOrOut}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppUserAndroid.LoginComStatic(inOrOut);
        #elif UNITY_IOS 
        AppUserIOS.LoginComStatic(inOrOut);
        #endif

    }


    /// <summary>
    /// 是否显示反馈按钮，用于部分市场在打包脚本中控制变量
    /// </summary>
    public  bool IsShowFeedback(){
        GameFramework.CLog.Log($"调用代理层isShowFeedback:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppUserAndroid.IsShowFeedback();
        #elif UNITY_IOS
        Result = AppUserIOS.IsShowFeedback();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 显示反馈界面
    /// </summary>
    public  void ShowFeedback(){
        GameFramework.CLog.Log($"调用代理层showFeedback:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppUserAndroid.ShowFeedback();
        #elif UNITY_IOS 
        AppUserIOS.ShowFeedback();
        #endif

    }


    /// <summary>
    /// 显示App升级弹框，游戏只管在需求的时机调用，平台层会处理弹出逻辑
    /// </summary>
    public  void ShowUpdateDalogStatic(){
        GameFramework.CLog.Log($"调用代理层showUpdateDalogStatic:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppUserAndroid.ShowUpdateDalogStatic();
        #elif UNITY_IOS 
        
        #endif

    }


    /// <summary>
    /// 是否为欧盟用户,游戏中用于控制是否显示按钮 true:欧盟地区; false：非欧盟地区
    /// </summary>
    public  bool IsRequestLocationInEeaOrUnknownStatic(){
        GameFramework.CLog.Log($"调用代理层isRequestLocationInEeaOrUnknownStatic:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppUserAndroid.IsRequestLocationInEeaOrUnknownStatic();
        #elif UNITY_IOS
        Result = AppUserIOS.IsRequestLocationInEeaOrUnknownStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 显示GDPR用户协议,游戏中按钮点击事件执行内容
    /// </summary>
    public  void ShowGDPRDialogStatic(){
        GameFramework.CLog.Log($"调用代理层showGDPRDialogStatic:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppUserAndroid.ShowGDPRDialogStatic();
        #elif UNITY_IOS 
        AppUserIOS.ShowGDPRDialogStatic();
        #endif

    }


    /// <summary>
    /// 是否显示评价
    /// </summary>
    public  bool IsShowEvaluate(){
        GameFramework.CLog.Log($"调用代理层isShowEvaluate:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppUserAndroid.IsShowEvaluate();
        #elif UNITY_IOS
        Result = AppUserIOS.IsShowEvaluate();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取评价类型
    /// </summary>
    public  int GetEvaluateMode(){
        GameFramework.CLog.Log($"调用代理层getEvaluateMode:");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppUserAndroid.GetEvaluateMode();
        #elif UNITY_IOS
        Result = AppUserIOS.GetEvaluateMode();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 显示评价
    /// </summary>
    public  void ShowEvaluate(){
        GameFramework.CLog.Log($"调用代理层showEvaluate:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppUserAndroid.ShowEvaluate();
        #elif UNITY_IOS 
        AppUserIOS.ShowEvaluate();
        #endif

    }


}
}

