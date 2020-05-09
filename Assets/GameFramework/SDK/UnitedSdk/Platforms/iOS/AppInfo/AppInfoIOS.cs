//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace DBTSDK{
 public class AppInfoIOS{
#if UNITY_IOS

     [DllImport("__Internal")]
    internal static extern string getPackageName();


     [DllImport("__Internal")]
    internal static extern string getVersionName();


     [DllImport("__Internal")]
    internal static extern string getInstallVersion();


     [DllImport("__Internal")]
    internal static extern bool isInstallVersion();


     [DllImport("__Internal")]
    internal static extern string getAppChannel();


     [DllImport("__Internal")]
    internal static extern int getAppChannelId();


     [DllImport("__Internal")]
    internal static extern void setAppChannelId(int channelId);


     [DllImport("__Internal")]
    internal static extern bool isFirstStartVer();


     [DllImport("__Internal")]
    internal static extern int  getStartNum();


     [DllImport("__Internal")]
    internal static extern long  getFirstInstallTime();


     [DllImport("__Internal")]
    internal static extern int  getScreenWidth();


     [DllImport("__Internal")]
    internal static extern int  getScreenHeight();


     [DllImport("__Internal")]
    internal static extern int  getSystemLanguage();


     [DllImport("__Internal")]
    internal static extern string getDeviceId();


     [DllImport("__Internal")]
    internal static extern string getUUID();


     [DllImport("__Internal")]
    internal static extern bool isWifi();


     [DllImport("__Internal")]
    internal static extern bool isRootSystem();


     [DllImport("__Internal")]
    internal static extern string getOsCountryCode();


     [DllImport("__Internal")]
    internal static extern int  getPublishCountryCode();


     [DllImport("__Internal")]
    internal static extern string getPhoneInfo();


     [DllImport("__Internal")]
    internal static extern string getLocalMacAddress();


     [DllImport("__Internal")]
    internal static extern string getIDFA();


     [DllImport("__Internal")]
    internal static extern long  getAppBuildTime();


     [DllImport("__Internal")]
    internal static extern void showPreferences();


     [DllImport("__Internal")]
    internal static extern int  getSDAvailableSizeStatic();


     [DllImport("__Internal")]
    internal static extern int  getDesignModeStatic();


     [DllImport("__Internal")]
    internal static extern bool showMoreGameStatic();


     [DllImport("__Internal")]
    internal static extern string getOnlineConfigParams(string param);


     [DllImport("__Internal")]
    internal static extern string getOnlineConfigParamsByAppVer(string param);




/// <summary>
/// 获取当前App包名
 /// </summary>
public static string GetPackageName(){
        GameFramework.CLog.Log($"调用IOSgetPackageName: ");
        string Reuslt =getPackageName();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取当前版本号
 /// </summary>
public static string GetVersionName(){
        GameFramework.CLog.Log($"调用IOSgetVersionName: ");
        string Reuslt =getVersionName();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取安装版本号
 /// </summary>
public static string GetInstallVersion(){
        GameFramework.CLog.Log($"调用IOSgetInstallVersion: ");
        string Reuslt =getInstallVersion();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 判断当前版本是否为安装版本
 /// </summary>
public static bool IsInstallVersion(){
        GameFramework.CLog.Log($"调用IOSisInstallVersion: ");
        bool Reuslt =isInstallVersion();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取App渠道号
 /// </summary>
public static string GetAppChannel(){
        GameFramework.CLog.Log($"调用IOSgetAppChannel: ");
        string Reuslt =getAppChannel();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取App渠道ID，用于游戏A/B测试
 /// </summary>
public static int GetAppChannelId(){
        GameFramework.CLog.Log($"调用IOSgetAppChannelId: ");
        int Reuslt =getAppChannelId();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 设置渠道ID(老用户升级后，往往会把老用户设置为-1)
 /// </summary>
public static void SetAppChannelId(int channelId){
        GameFramework.CLog.Log($"调用IOSsetAppChannelId: channelId[{channelId}]");
        setAppChannelId(channelId);
}


/// <summary>
/// 是否第一次启动
 /// </summary>
public static bool IsFirstStartVer(){
        GameFramework.CLog.Log($"调用IOSisFirstStartVer: ");
        bool Reuslt =isFirstStartVer();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// App启动次数
 /// </summary>
public static int  GetStartNum(){
        GameFramework.CLog.Log($"调用IOSgetStartNum: ");
        int  Reuslt =getStartNum();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取首次运行时间
 /// </summary>
public static long  GetFirstInstallTime(){
        GameFramework.CLog.Log($"调用IOSgetFirstInstallTime: ");
        long  Reuslt =getFirstInstallTime();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取屏幕宽
 /// </summary>
public static int  GetScreenWidth(){
        GameFramework.CLog.Log($"调用IOSgetScreenWidth: ");
        int  Reuslt =getScreenWidth();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取屏幕高
 /// </summary>
public static int  GetScreenHeight(){
        GameFramework.CLog.Log($"调用IOSgetScreenHeight: ");
        int  Reuslt =getScreenHeight();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 返回是个int值，按照上面注释的顺序 和数字一一对应
 /// </summary>
public static int  GetSystemLanguage(){
        GameFramework.CLog.Log($"调用IOSgetSystemLanguage: ");
        int  Reuslt =getSystemLanguage();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取设备deviceid
 /// </summary>
public static string GetDeviceId(){
        GameFramework.CLog.Log($"调用IOSgetDeviceId: ");
        string Reuslt =getDeviceId();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取UUID
 /// </summary>
public static string GetUUID(){
        GameFramework.CLog.Log($"调用IOSgetUUID: ");
        string Reuslt =getUUID();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否wifi环境
 /// </summary>
public static bool IsWifi(){
        GameFramework.CLog.Log($"调用IOSisWifi: ");
        bool Reuslt =isWifi();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否越狱/root
 /// </summary>
public static bool IsRootSystem(){
        GameFramework.CLog.Log($"调用IOSisRootSystem: ");
        bool Reuslt =isRootSystem();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取设备地区
 /// </summary>
public static string GetOsCountryCode(){
        GameFramework.CLog.Log($"调用IOSgetOsCountryCode: ");
        string Reuslt =getOsCountryCode();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取App发布地区 0：国内，1：国外, 2:国际版
 /// </summary>
public static int  GetPublishCountryCode(){
        GameFramework.CLog.Log($"调用IOSgetPublishCountryCode: ");
        int  Reuslt =getPublishCountryCode();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取设备信息
 /// </summary>
public static string GetPhoneInfo(){
        GameFramework.CLog.Log($"调用IOSgetPhoneInfo: ");
        string Reuslt =getPhoneInfo();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取设备Mac地址
 /// </summary>
public static string GetLocalMacAddress(){
        GameFramework.CLog.Log($"调用IOSgetLocalMacAddress: ");
        string Reuslt =getLocalMacAddress();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取设备IDFA
 /// </summary>
public static string GetIDFA(){
        GameFramework.CLog.Log($"调用IOSgetIDFA: ");
        string Reuslt =getIDFA();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取App打包时间
 /// </summary>
public static long  GetAppBuildTime(){
        GameFramework.CLog.Log($"调用IOSgetAppBuildTime: ");
        long  Reuslt =getAppBuildTime();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 跳转到设置界面
 /// </summary>
public static void ShowPreferences(){
        GameFramework.CLog.Log($"调用IOSshowPreferences: ");
        showPreferences();
}


/// <summary>
/// 获取SD卡的可用大小 单位M
 /// </summary>
public static int  GetSDAvailableSizeStatic(){
        GameFramework.CLog.Log($"调用IOSgetSDAvailableSizeStatic: ");
        int  Reuslt =getSDAvailableSizeStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取Android审核参数 0:关闭审核(审核通过)  1：正在审核
 /// </summary>
public static int  GetDesignModeStatic(){
        GameFramework.CLog.Log($"调用IOSgetDesignModeStatic: ");
        int  Reuslt =getDesignModeStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 是否显示多玩法
 /// </summary>
public static bool ShowMoreGameStatic(){
        GameFramework.CLog.Log($"调用IOSshowMoreGameStatic: ");
        bool Reuslt =showMoreGameStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 获取在线参数
 /// </summary>
public static string GetOnlineConfigParams(string param){
        GameFramework.CLog.Log($"调用IOSgetOnlineConfigParams: param[{param}]");
        string Reuslt =getOnlineConfigParams(param);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 按版本号获取在线参数
 /// </summary>
public static string GetOnlineConfigParamsByAppVer(string param){
        GameFramework.CLog.Log($"调用IOSgetOnlineConfigParamsByAppVer: param[{param}]");
        string Reuslt =getOnlineConfigParamsByAppVer(param);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


#endif
}
}

