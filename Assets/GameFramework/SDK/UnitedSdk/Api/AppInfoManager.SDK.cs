//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK
{
public partial class AppInfoManager
{
    
    /// <summary>
    /// 获取当前App包名
    /// </summary>
    public  string GetPackageName(){
        GameFramework.CLog.Log($"调用代理层getPackageName:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetPackageName();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetPackageName();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取当前版本号
    /// </summary>
    public  string GetVersionName(){
        GameFramework.CLog.Log($"调用代理层getVersionName:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetVersionName();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetVersionName();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取安装版本号
    /// </summary>
    public  string GetInstallVersion(){
        GameFramework.CLog.Log($"调用代理层getInstallVersion:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetInstallVersion();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetInstallVersion();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 判断当前版本是否为安装版本
    /// </summary>
    public  bool IsInstallVersion(){
        GameFramework.CLog.Log($"调用代理层isInstallVersion:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.IsInstallVersion();
        #elif UNITY_IOS
        Result = AppInfoIOS.IsInstallVersion();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取App渠道号
    /// </summary>
    public  string GetAppChannel(){
        GameFramework.CLog.Log($"调用代理层getAppChannel:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetAppChannel();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetAppChannel();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取App渠道ID，用于游戏A/B测试
    /// </summary>
    public  int GetAppChannelId(){
        GameFramework.CLog.Log($"调用代理层getAppChannelId:");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetAppChannelId();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetAppChannelId();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 设置渠道ID(老用户升级后，往往会把老用户设置为-1)
    /// </summary>
    public  void SetAppChannelId(int channelId){
        GameFramework.CLog.Log($"调用代理层setAppChannelId:channelId[{channelId}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppInfoAndroid.SetAppChannelId(channelId);
        #elif UNITY_IOS 
        AppInfoIOS.SetAppChannelId(channelId);
        #endif

    }


    /// <summary>
    /// 是否第一次启动
    /// </summary>
    public  bool IsFirstStartVer(){
        GameFramework.CLog.Log($"调用代理层isFirstStartVer:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.IsFirstStartVer();
        #elif UNITY_IOS
        Result = AppInfoIOS.IsFirstStartVer();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// App启动次数
    /// </summary>
    public  int  GetStartNum(){
        GameFramework.CLog.Log($"调用代理层getStartNum:");
        
        int  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetStartNum();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetStartNum();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取首次运行时间
    /// </summary>
    public  long  GetFirstInstallTime(){
        GameFramework.CLog.Log($"调用代理层getFirstInstallTime:");
        
        long  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetFirstInstallTime();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetFirstInstallTime();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取屏幕宽
    /// </summary>
    public  int  GetScreenWidth(){
        GameFramework.CLog.Log($"调用代理层getScreenWidth:");
        
        int  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetScreenWidth();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetScreenWidth();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取屏幕高
    /// </summary>
    public  int  GetScreenHeight(){
        GameFramework.CLog.Log($"调用代理层getScreenHeight:");
        
        int  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetScreenHeight();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetScreenHeight();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 返回是个int值，按照上面注释的顺序 和数字一一对应
    /// </summary>
    public  int  GetSystemLanguage(){
        GameFramework.CLog.Log($"调用代理层getSystemLanguage:");
        
        int  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetSystemLanguage();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetSystemLanguage();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取设备deviceid
    /// </summary>
    public  string GetDeviceId(){
        GameFramework.CLog.Log($"调用代理层getDeviceId:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetDeviceId();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetDeviceId();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取UUID
    /// </summary>
    public  string GetUUID(){
        GameFramework.CLog.Log($"调用代理层getUUID:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetUUID();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetUUID();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否wifi环境
    /// </summary>
    public  bool IsWifi(){
        GameFramework.CLog.Log($"调用代理层isWifi:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.IsWifi();
        #elif UNITY_IOS
        Result = AppInfoIOS.IsWifi();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否越狱/root
    /// </summary>
    public  bool IsRootSystem(){
        GameFramework.CLog.Log($"调用代理层isRootSystem:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.IsRootSystem();
        #elif UNITY_IOS
        Result = AppInfoIOS.IsRootSystem();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取设备地区
    /// </summary>
    public  string GetOsCountryCode(){
        GameFramework.CLog.Log($"调用代理层getOsCountryCode:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetOsCountryCode();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetOsCountryCode();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取App发布地区 0：国内，1：国外, 2:国际版
    /// </summary>
    public  int  GetPublishCountryCode(){
        GameFramework.CLog.Log($"调用代理层getPublishCountryCode:");
        
        int  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetPublishCountryCode();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetPublishCountryCode();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取设备信息
    /// </summary>
    public  string GetPhoneInfo(){
        GameFramework.CLog.Log($"调用代理层getPhoneInfo:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetPhoneInfo();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetPhoneInfo();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取设备Mac地址
    /// </summary>
    public  string GetLocalMacAddress(){
        GameFramework.CLog.Log($"调用代理层getLocalMacAddress:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetLocalMacAddress();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetLocalMacAddress();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取设备IDFA
    /// </summary>
    public  string GetIDFA(){
        GameFramework.CLog.Log($"调用代理层getIDFA:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        
        #elif UNITY_IOS
        Result = AppInfoIOS.GetIDFA();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取设备AndroidID
    /// </summary>
    public  string GetAndroidId(){
        GameFramework.CLog.Log($"调用代理层getAndroidId:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetAndroidId();
        #elif UNITY_IOS
        
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取设备IMEI (Android)
    /// </summary>
    public  string GetIMEI(){
        GameFramework.CLog.Log($"调用代理层getIMEI:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetIMEI();
        #elif UNITY_IOS
        
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取设备IMSI (Android)
    /// </summary>
    public  string GetIMSI(){
        GameFramework.CLog.Log($"调用代理层getIMSI:");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetIMSI();
        #elif UNITY_IOS
        
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取App打包时间
    /// </summary>
    public  long  GetAppBuildTime(){
        GameFramework.CLog.Log($"调用代理层getAppBuildTime:");
        
        long  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetAppBuildTime();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetAppBuildTime();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 跳转到设置界面
    /// </summary>
    public  void ShowPreferences(){
        GameFramework.CLog.Log($"调用代理层showPreferences:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AppInfoAndroid.ShowPreferences();
        #elif UNITY_IOS 
        AppInfoIOS.ShowPreferences();
        #endif

    }


    /// <summary>
    /// 获取SD卡的可用大小 单位M
    /// </summary>
    public  int  GetSDAvailableSizeStatic(){
        GameFramework.CLog.Log($"调用代理层getSDAvailableSizeStatic:");
        
        int  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetSDAvailableSizeStatic();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetSDAvailableSizeStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取Android审核参数 0:关闭审核(审核通过)  1：正在审核
    /// </summary>
    public  int  GetDesignModeStatic(){
        GameFramework.CLog.Log($"调用代理层getDesignModeStatic:");
        
        int  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetDesignModeStatic();
        #elif UNITY_IOS
        Result = AppInfoIOS.GetDesignModeStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 是否显示多玩法
    /// </summary>
    public  bool ShowMoreGameStatic(){
        GameFramework.CLog.Log($"调用代理层showMoreGameStatic:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.ShowMoreGameStatic();
        #elif UNITY_IOS
        Result = AppInfoIOS.ShowMoreGameStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 获取在线参数
    /// </summary>
    public  string GetOnlineConfigParams(string param){
        GameFramework.CLog.Log($"调用代理层getOnlineConfigParams:param[{param}]");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetOnlineConfigParams(param);
        #elif UNITY_IOS
        Result = AppInfoIOS.GetOnlineConfigParams(param);
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 按版本号获取在线参数
    /// </summary>
    public  string GetOnlineConfigParamsByAppVer(string param){
        GameFramework.CLog.Log($"调用代理层getOnlineConfigParamsByAppVer:param[{param}]");
        
        string Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AppInfoAndroid.GetOnlineConfigParamsByAppVer(param);
        #elif UNITY_IOS
        Result = AppInfoIOS.GetOnlineConfigParamsByAppVer(param);
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


}
}

