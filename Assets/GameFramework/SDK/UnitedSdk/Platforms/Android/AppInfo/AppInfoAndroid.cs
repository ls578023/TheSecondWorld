//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK{
 public class AppInfoAndroid{
  
/// <summary>
/// 获取当前App包名
 /// </summary>
public static string GetPackageName()
    {
        GameFramework.CLog.Log($"调用安卓getPackageName: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getPackageName");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取当前版本号
 /// </summary>
public static string GetVersionName()
    {
        GameFramework.CLog.Log($"调用安卓getVersionName: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getVersionName");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取安装版本号
 /// </summary>
public static string GetInstallVersion()
    {
        GameFramework.CLog.Log($"调用安卓getInstallVersion: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getInstallVersion");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 判断当前版本是否为安装版本
 /// </summary>
public static bool IsInstallVersion()
    {
        GameFramework.CLog.Log($"调用安卓isInstallVersion: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isInstallVersion");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取App渠道号
 /// </summary>
public static string GetAppChannel()
    {
        GameFramework.CLog.Log($"调用安卓getAppChannel: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getAppChannel");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取App渠道ID，用于游戏A/B测试
 /// </summary>
public static int GetAppChannelId()
    {
        GameFramework.CLog.Log($"调用安卓getAppChannelId: ");
        int Reuslt = AndroidClass.GameActHelper.CallStatic<int>("getAppChannelId");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 设置渠道ID(老用户升级后，往往会把老用户设置为-1)
 /// </summary>
public static void SetAppChannelId(int channelId)
    {
        GameFramework.CLog.Log($"调用安卓setAppChannelId: channelId[{channelId}]");
        AndroidClass.GameActHelper.CallStatic("setAppChannelId",channelId);
    }
  
/// <summary>
/// 是否第一次启动
 /// </summary>
public static bool IsFirstStartVer()
    {
        GameFramework.CLog.Log($"调用安卓isFirstStartVer: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isFirstStartVer");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// App启动次数
 /// </summary>
public static int  GetStartNum()
    {
        GameFramework.CLog.Log($"调用安卓getStartNum: ");
        int  Reuslt = AndroidClass.GameActHelper.CallStatic<int >("getStartNum");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取首次运行时间
 /// </summary>
public static long  GetFirstInstallTime()
    {
        GameFramework.CLog.Log($"调用安卓getFirstInstallTime: ");
        long  Reuslt = AndroidClass.GameActHelper.CallStatic<long >("getFirstInstallTime");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取屏幕宽
 /// </summary>
public static int  GetScreenWidth()
    {
        GameFramework.CLog.Log($"调用安卓getScreenWidth: ");
        int  Reuslt = AndroidClass.GameActHelper.CallStatic<int >("getScreenWidth");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取屏幕高
 /// </summary>
public static int  GetScreenHeight()
    {
        GameFramework.CLog.Log($"调用安卓getScreenHeight: ");
        int  Reuslt = AndroidClass.GameActHelper.CallStatic<int >("getScreenHeight");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 返回是个int值，按照上面注释的顺序 和数字一一对应
 /// </summary>
public static int  GetSystemLanguage()
    {
        GameFramework.CLog.Log($"调用安卓getSystemLanguage: ");
        int  Reuslt = AndroidClass.GameActHelper.CallStatic<int >("getSystemLanguage");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取设备deviceid
 /// </summary>
public static string GetDeviceId()
    {
        GameFramework.CLog.Log($"调用安卓getDeviceId: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getDeviceId");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取UUID
 /// </summary>
public static string GetUUID()
    {
        GameFramework.CLog.Log($"调用安卓getUUID: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getUUID");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否wifi环境
 /// </summary>
public static bool IsWifi()
    {
        GameFramework.CLog.Log($"调用安卓isWifi: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isWifi");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否越狱/root
 /// </summary>
public static bool IsRootSystem()
    {
        GameFramework.CLog.Log($"调用安卓isRootSystem: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("isRootSystem");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取设备地区
 /// </summary>
public static string GetOsCountryCode()
    {
        GameFramework.CLog.Log($"调用安卓getOsCountryCode: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getOsCountryCode");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取App发布地区 0：国内，1：国外, 2:国际版
 /// </summary>
public static int  GetPublishCountryCode()
    {
        GameFramework.CLog.Log($"调用安卓getPublishCountryCode: ");
        int  Reuslt = AndroidClass.GameActHelper.CallStatic<int >("getPublishCountryCode");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取设备信息
 /// </summary>
public static string GetPhoneInfo()
    {
        GameFramework.CLog.Log($"调用安卓getPhoneInfo: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getPhoneInfo");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取设备Mac地址
 /// </summary>
public static string GetLocalMacAddress()
    {
        GameFramework.CLog.Log($"调用安卓getLocalMacAddress: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getLocalMacAddress");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取设备AndroidID
 /// </summary>
public static string GetAndroidId()
    {
        GameFramework.CLog.Log($"调用安卓getAndroidId: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getAndroidId");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取设备IMEI (Android)
 /// </summary>
public static string GetIMEI()
    {
        GameFramework.CLog.Log($"调用安卓getIMEI: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getIMEI");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取设备IMSI (Android)
 /// </summary>
public static string GetIMSI()
    {
        GameFramework.CLog.Log($"调用安卓getIMSI: ");
        string Reuslt = AndroidClass.GameActHelper.CallStatic<string>("getIMSI");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取App打包时间
 /// </summary>
public static long  GetAppBuildTime()
    {
        GameFramework.CLog.Log($"调用安卓getAppBuildTime: ");
        long  Reuslt = AndroidClass.GameActHelper.CallStatic<long >("getAppBuildTime");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 跳转到设置界面
 /// </summary>
public static void ShowPreferences()
    {
        GameFramework.CLog.Log($"调用安卓showPreferences: ");
        AndroidClass.GameActHelper.CallStatic("showPreferences");
    }
  
/// <summary>
/// 获取SD卡的可用大小 单位M
 /// </summary>
public static int  GetSDAvailableSizeStatic()
    {
        GameFramework.CLog.Log($"调用安卓getSDAvailableSizeStatic: ");
        int  Reuslt = AndroidClass.GameActHelper.CallStatic<int >("getSDAvailableSizeStatic");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取Android审核参数 0:关闭审核(审核通过)  1：正在审核
 /// </summary>
public static int  GetDesignModeStatic()
    {
        GameFramework.CLog.Log($"调用安卓getDesignModeStatic: ");
        int  Reuslt = AndroidClass.GameActHelper.CallStatic<int >("getDesignModeStatic");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 是否显示多玩法
 /// </summary>
public static bool ShowMoreGameStatic()
    {
        GameFramework.CLog.Log($"调用安卓showMoreGameStatic: ");
        bool Reuslt = AndroidClass.GameActHelper.CallStatic<bool>("showMoreGameStatic");
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 获取在线参数
 /// </summary>
public static string GetOnlineConfigParams(string param)
    {
        GameFramework.CLog.Log($"调用安卓getOnlineConfigParams: param[{param}]");
        string Reuslt = AndroidClass.BaseActivityHelper.CallStatic<string>("getOnlineConfigParams",param);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }
  
/// <summary>
/// 按版本号获取在线参数
 /// </summary>
public static string GetOnlineConfigParamsByAppVer(string param)
    {
        GameFramework.CLog.Log($"调用安卓getOnlineConfigParamsByAppVer: param[{param}]");
        string Reuslt = AndroidClass.BaseActivityHelper.CallStatic<string>("getOnlineConfigParamsByAppVer",param);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
    }

}
}

