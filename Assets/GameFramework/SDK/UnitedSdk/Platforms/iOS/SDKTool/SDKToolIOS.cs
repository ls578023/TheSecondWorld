//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace DBTSDK{
 public class SDKToolIOS{
#if UNITY_IOS

     [DllImport("__Internal")]
    internal static extern void vibrateStatic(long milliseconds,int shakeLevel);


     [DllImport("__Internal")]
    internal static extern void showToast(string msg);


     [DllImport("__Internal")]
    internal static extern void  showExitDialog();


     [DllImport("__Internal")]
    internal static extern void openUrl(string url);


     [DllImport("__Internal")]
    internal static extern void copyMsgToClipboard(string msg);


     [DllImport("__Internal")]
    internal static extern string getClipboardMsg();


     [DllImport("__Internal")]
    internal static extern void copy2SystemDCIM(string copyFile);


     [DllImport("__Internal")]
    internal static extern void gotoAppStore(string packageName);


     [DllImport("__Internal")]
    internal static extern string getNetworkTypeStatic();


     [DllImport("__Internal")]
    internal static extern bool isShowPolicy();


     [DllImport("__Internal")]
    internal static extern void gotoTermsServiceStatic();


     [DllImport("__Internal")]
    internal static extern void gotoPrivacyPolicyStatic();


     [DllImport("__Internal")]
    internal static extern void openApp(int id);


     [DllImport("__Internal")]
    internal static extern bool isAppInstalled(int id);


     [DllImport("__Internal")]
    internal static extern bool isShowRealNameRegistration();


     [DllImport("__Internal")]
    internal static extern bool isShowContactInformation();


     [DllImport("__Internal")]
    internal static extern int getDurationTimeStatic();


     [DllImport("__Internal")]
    internal static extern bool onlineResStatic();


     [DllImport("__Internal")]
    internal static extern void  u3DGameStart();


     [DllImport("__Internal")]
    internal static extern int getDaysByFirstLaunch();


     [DllImport("__Internal")]
    internal static extern bool isDebugVersion();


     [DllImport("__Internal")]
    internal static extern int getGameBanHaoType();


     [DllImport("__Internal")]
    internal static extern void showGameBanHao();




/// <summary>
/// 调用手机震动 milliseconds 震动时间（毫秒）shakeLevel 震动强弱 0：低强度(默认), 1：中等强度, 2：高强度 需要添加权限 <uses-permission android:name="android.permission.VIBRATE" />
 /// </summary>
public static void VibrateStatic(long milliseconds,int shakeLevel){
        GameFramework.CLog.Log($"调用IOSvibrateStatic: milliseconds[{milliseconds}]shakeLevel[{shakeLevel}]");
        vibrateStatic(milliseconds,shakeLevel);
}


/// <summary>
/// 弹出Toast提示
 /// </summary>
public static void ShowToast(string msg){
        GameFramework.CLog.Log($"调用IOSshowToast: msg[{msg}]");
        showToast(msg);
}


/// <summary>
/// 展示退出弹框
 /// </summary>
public static void  ShowExitDialog(){
        GameFramework.CLog.Log($"调用IOSshowExitDialog: ");
        showExitDialog();
}


/// <summary>
/// 打开URL
 /// </summary>
public static void OpenUrl(string url){
        GameFramework.CLog.Log($"调用IOSopenUrl: url[{url}]");
        openUrl(url);
}


/// <summary>
/// 拷贝字符串到剪切板
 /// </summary>
public static void CopyMsgToClipboard(string msg){
        GameFramework.CLog.Log($"调用IOScopyMsgToClipboard: msg[{msg}]");
        copyMsgToClipboard(msg);
}


/// <summary>
/// 获得剪贴板内容, 未获取到值得情况下，等待5 * 100ms后再返回
 /// </summary>
public static string GetClipboardMsg(){
        GameFramework.CLog.Log($"调用IOSgetClipboardMsg: ");
        string Reuslt =getClipboardMsg();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 拷贝图片到相册 copyFile:图片路径
 /// </summary>
public static void Copy2SystemDCIM(string copyFile){
        GameFramework.CLog.Log($"调用IOScopy2SystemDCIM: copyFile[{copyFile}]");
        copy2SystemDCIM(copyFile);
}


/// <summary>
/// 打开应用市场并跳转App详情页 packageName:需要跳转的App包名
 /// </summary>
public static void GotoAppStore(string packageName){
        GameFramework.CLog.Log($"调用IOSgotoAppStore: packageName[{packageName}]");
        gotoAppStore(packageName);
}


/// <summary>
/// 获得网络类型  wifi、2g、3g、4g、5g
 /// </summary>
public static string GetNetworkTypeStatic(){
        GameFramework.CLog.Log($"调用IOSgetNetworkTypeStatic: ");
        string Reuslt =getNetworkTypeStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否显示隐私政策和用户协议
 /// </summary>
public static bool IsShowPolicy(){
        GameFramework.CLog.Log($"调用IOSisShowPolicy: ");
        bool Reuslt =isShowPolicy();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 跳转到到用户协议
 /// </summary>
public static void GotoTermsServiceStatic(){
        GameFramework.CLog.Log($"调用IOSgotoTermsServiceStatic: ");
        gotoTermsServiceStatic();
}


/// <summary>
/// 跳转到到隐私政策
 /// </summary>
public static void GotoPrivacyPolicyStatic(){
        GameFramework.CLog.Log($"调用IOSgotoPrivacyPolicyStatic: ");
        gotoPrivacyPolicyStatic();
}


/// <summary>
/// 打开三方分享App id :三方分享AppID
 /// </summary>
public static void OpenApp(int id){
        GameFramework.CLog.Log($"调用IOSopenApp: id[{id}]");
        openApp(id);
}


/// <summary>
/// 判断三方分享App是否安装 新浪微博:0 微信：1 QQ：2 QQ空间：3 Facebook：4 Twitter：5
 /// </summary>
public static bool IsAppInstalled(int id){
        GameFramework.CLog.Log($"调用IOSisAppInstalled: id[{id}]");
        bool Reuslt =isAppInstalled(id);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否显示实名注册，用于部分市场在打包脚本中控制变量 true: 显示;  false:不显示
 /// </summary>
public static bool IsShowRealNameRegistration(){
        GameFramework.CLog.Log($"调用IOSisShowRealNameRegistration: ");
        bool Reuslt =isShowRealNameRegistration();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否显示联系方式  true: 显示;  false:不显示
 /// </summary>
public static bool IsShowContactInformation(){
        GameFramework.CLog.Log($"调用IOSisShowContactInformation: ");
        bool Reuslt =isShowContactInformation();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取使用时长 存在20s的误差 返回App使用时长， 单位s
 /// </summary>
public static int GetDurationTimeStatic(){
        GameFramework.CLog.Log($"调用IOSgetDurationTimeStatic: ");
        int Reuslt =getDurationTimeStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否需要在线下载资源 true需要，默认为不需要
 /// </summary>
public static bool OnlineResStatic(){
        GameFramework.CLog.Log($"调用IOSonlineResStatic: ");
        bool Reuslt =onlineResStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// U3D游戏进入游戏
 /// </summary>
public static void  U3DGameStart(){
        GameFramework.CLog.Log($"调用IOSu3DGameStart: ");
        u3DGameStart();
}


/// <summary>
/// 获取当前距离第一次启动的天数,当天为：1
 /// </summary>
public static int GetDaysByFirstLaunch(){
        GameFramework.CLog.Log($"调用IOSgetDaysByFirstLaunch: ");
        int Reuslt =getDaysByFirstLaunch();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否是测试版本
 /// </summary>
public static bool IsDebugVersion(){
        GameFramework.CLog.Log($"调用IOSisDebugVersion: ");
        bool Reuslt =isDebugVersion();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否显示“版号信息”文字按钮，0：显示，1：不显示；未配置/未获取到在线参数默认为0
 /// </summary>
public static int GetGameBanHaoType(){
        GameFramework.CLog.Log($"调用IOSgetGameBanHaoType: ");
        int Reuslt =getGameBanHaoType();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 点击“版号信息”按钮 展示界面
 /// </summary>
public static void ShowGameBanHao(){
        GameFramework.CLog.Log($"调用IOSshowGameBanHao: ");
        showGameBanHao();
}


#endif
}
}

