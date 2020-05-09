//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK{
 public class AppUserAndroid{
  
/// <summary>
/// 是否显示分享按钮，用于部分市场在打包脚本中控制变量
 /// </summary>
public static bool IsShowShare()
    {
        GameFramework.CLog.Log($"调用安卓isShowShare: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isShowShare");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取分享方式 1：DBT复制粘贴分享;  2：系统分享
 /// </summary>
public static int GetShareMode()
    {
        GameFramework.CLog.Log($"调用安卓getShareMode: ");
        int Reuslt = AndroidClass.GameActHelper.CallStatic<int>("getShareMode");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// U3D分享图片 title 分享标题 content 分享内容 url 分享URL imgFile 图片路径
 /// </summary>
public static void ShareImage(string title,string content,string url,string imgFile)
    {
        GameFramework.CLog.Log($"调用安卓shareImage: title[{title}]content[{content}]url[{url}]imgFile[{imgFile}]");
        AndroidClass.GameActHelper.CallStatic("shareImage",title,content,url,imgFile);
    }
  
/// <summary>
/// U3D分享文本  title 分享标题 content 分享内容 分享URL
 /// </summary>
public static void ShareText(string title,string content,string url)
    {
        GameFramework.CLog.Log($"调用安卓shareText: title[{title}]content[{content}]url[{url}]");
        AndroidClass.GameActHelper.CallStatic("shareText",title,content,url);
    }
  
/// <summary>
/// 获取分享URL 地址
 /// </summary>
public static string GetShareUrl()
    {
        GameFramework.CLog.Log($"调用安卓getShareUrl: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getShareUrl");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否显示登陆入口
 /// </summary>
public static bool ShowLogin()
    {
        GameFramework.CLog.Log($"调用安卓showLogin: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("showLogin");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 一键登录
 /// </summary>
public static void LoginComStatic(int inOrOut)
    {
        GameFramework.CLog.Log($"调用安卓loginComStatic: inOrOut[{inOrOut}]");
        AndroidClass.GameActHelper.CallStatic("loginComStatic",inOrOut);
    }
  
/// <summary>
/// 是否显示反馈按钮，用于部分市场在打包脚本中控制变量
 /// </summary>
public static bool IsShowFeedback()
    {
        GameFramework.CLog.Log($"调用安卓isShowFeedback: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isShowFeedback");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 显示反馈界面
 /// </summary>
public static void ShowFeedback()
    {
        GameFramework.CLog.Log($"调用安卓showFeedback: ");
        AndroidClass.GameActHelper.CallStatic("showFeedback");
    }
  
/// <summary>
/// 显示App升级弹框，游戏只管在需求的时机调用，平台层会处理弹出逻辑
 /// </summary>
public static void ShowUpdateDalogStatic()
    {
        GameFramework.CLog.Log($"调用安卓showUpdateDalogStatic: ");
        AndroidClass.GameActHelper.CallStatic("showUpdateDalogStatic");
    }
  
/// <summary>
/// 是否为欧盟用户,游戏中用于控制是否显示按钮 true:欧盟地区; false：非欧盟地区
 /// </summary>
public static bool IsRequestLocationInEeaOrUnknownStatic()
    {
        GameFramework.CLog.Log($"调用安卓isRequestLocationInEeaOrUnknownStatic: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isRequestLocationInEeaOrUnknownStatic");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 显示GDPR用户协议,游戏中按钮点击事件执行内容
 /// </summary>
public static void ShowGDPRDialogStatic()
    {
        GameFramework.CLog.Log($"调用安卓showGDPRDialogStatic: ");
        AndroidClass.GameActHelper.CallStatic("showGDPRDialogStatic");
    }
  
/// <summary>
/// 是否显示评价
 /// </summary>
public static bool IsShowEvaluate()
    {
        GameFramework.CLog.Log($"调用安卓isShowEvaluate: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isShowEvaluate");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取评价类型
 /// </summary>
public static int GetEvaluateMode()
    {
        GameFramework.CLog.Log($"调用安卓getEvaluateMode: ");
        int Reuslt = AndroidClass.GameActHelper.CallStatic<int>("getEvaluateMode");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 显示评价
 /// </summary>
public static void ShowEvaluate()
    {
        GameFramework.CLog.Log($"调用安卓showEvaluate: ");
        AndroidClass.GameActHelper.CallStatic("showEvaluate");
    }

}
}

