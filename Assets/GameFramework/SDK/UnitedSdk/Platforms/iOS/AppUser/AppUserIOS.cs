//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace DBTSDK{
 public class AppUserIOS{
#if UNITY_IOS

     [DllImport("__Internal")]
    internal static extern bool isShowShare();


     [DllImport("__Internal")]
    internal static extern int getShareMode();


     [DllImport("__Internal")]
    internal static extern void shareImage(string title,string content,string url,string imgFile);


     [DllImport("__Internal")]
    internal static extern void shareText(string title,string content,string url);


     [DllImport("__Internal")]
    internal static extern string getShareUrl();


     [DllImport("__Internal")]
    internal static extern bool showLogin();


     [DllImport("__Internal")]
    internal static extern void loginComStatic(int inOrOut);


     [DllImport("__Internal")]
    internal static extern bool isShowFeedback();


     [DllImport("__Internal")]
    internal static extern void showFeedback();


     [DllImport("__Internal")]
    internal static extern bool isRequestLocationInEeaOrUnknownStatic();


     [DllImport("__Internal")]
    internal static extern void showGDPRDialogStatic();


     [DllImport("__Internal")]
    internal static extern bool isShowEvaluate();


     [DllImport("__Internal")]
    internal static extern int getEvaluateMode();


     [DllImport("__Internal")]
    internal static extern void showEvaluate();




/// <summary>
/// 是否显示分享按钮，用于部分市场在打包脚本中控制变量
 /// </summary>
public static bool IsShowShare(){
        GameFramework.CLog.Log($"调用IOSisShowShare: ");
        bool Reuslt =isShowShare();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取分享方式 1：DBT复制粘贴分享;  2：系统分享
 /// </summary>
public static int GetShareMode(){
        GameFramework.CLog.Log($"调用IOSgetShareMode: ");
        int Reuslt =getShareMode();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// U3D分享图片 title 分享标题 content 分享内容 url 分享URL imgFile 图片路径
 /// </summary>
public static void ShareImage(string title,string content,string url,string imgFile){
        GameFramework.CLog.Log($"调用IOSshareImage: title[{title}]content[{content}]url[{url}]imgFile[{imgFile}]");
        shareImage(title,content,url,imgFile);
}


/// <summary>
/// U3D分享文本  title 分享标题 content 分享内容 分享URL
 /// </summary>
public static void ShareText(string title,string content,string url){
        GameFramework.CLog.Log($"调用IOSshareText: title[{title}]content[{content}]url[{url}]");
        shareText(title,content,url);
}


/// <summary>
/// 获取分享URL 地址
 /// </summary>
public static string GetShareUrl(){
        GameFramework.CLog.Log($"调用IOSgetShareUrl: ");
        string Reuslt =getShareUrl();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否显示登陆入口
 /// </summary>
public static bool ShowLogin(){
        GameFramework.CLog.Log($"调用IOSshowLogin: ");
        bool Reuslt =showLogin();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 一键登录
 /// </summary>
public static void LoginComStatic(int inOrOut){
        GameFramework.CLog.Log($"调用IOSloginComStatic: inOrOut[{inOrOut}]");
        loginComStatic(inOrOut);
}


/// <summary>
/// 是否显示反馈按钮，用于部分市场在打包脚本中控制变量
 /// </summary>
public static bool IsShowFeedback(){
        GameFramework.CLog.Log($"调用IOSisShowFeedback: ");
        bool Reuslt =isShowFeedback();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 显示反馈界面
 /// </summary>
public static void ShowFeedback(){
        GameFramework.CLog.Log($"调用IOSshowFeedback: ");
        showFeedback();
}


/// <summary>
/// 是否为欧盟用户,游戏中用于控制是否显示按钮 true:欧盟地区; false：非欧盟地区
 /// </summary>
public static bool IsRequestLocationInEeaOrUnknownStatic(){
        GameFramework.CLog.Log($"调用IOSisRequestLocationInEeaOrUnknownStatic: ");
        bool Reuslt =isRequestLocationInEeaOrUnknownStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 显示GDPR用户协议,游戏中按钮点击事件执行内容
 /// </summary>
public static void ShowGDPRDialogStatic(){
        GameFramework.CLog.Log($"调用IOSshowGDPRDialogStatic: ");
        showGDPRDialogStatic();
}


/// <summary>
/// 是否显示评价
 /// </summary>
public static bool IsShowEvaluate(){
        GameFramework.CLog.Log($"调用IOSisShowEvaluate: ");
        bool Reuslt =isShowEvaluate();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取评价类型
 /// </summary>
public static int GetEvaluateMode(){
        GameFramework.CLog.Log($"调用IOSgetEvaluateMode: ");
        int Reuslt =getEvaluateMode();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 显示评价
 /// </summary>
public static void ShowEvaluate(){
        GameFramework.CLog.Log($"调用IOSshowEvaluate: ");
        showEvaluate();
}


#endif
}
}

