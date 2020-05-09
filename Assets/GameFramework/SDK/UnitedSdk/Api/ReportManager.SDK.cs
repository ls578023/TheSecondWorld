//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK
{
public partial class ReportManager
{
    
    /// <summary>
    /// 事件统计
    /// </summary>
    public  void OnEvent(string event_id,string label){
        GameFramework.CLog.Log($"调用代理层onEvent:event_id[{event_id}]label[{label}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        ReportAndroid.OnEvent(event_id,label);
        #elif UNITY_IOS 
        ReportIOS.OnEvent(event_id,label);
        #endif

    }


    /// <summary>
    /// 事件统计
    /// </summary>
    public  void OnEvent(string event_id,string label,int n){
        GameFramework.CLog.Log($"调用代理层onEvent:event_id[{event_id}]label[{label}]n[{n}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        ReportAndroid.OnEvent(event_id,label,n);
        #elif UNITY_IOS 
        ReportIOS.OnEvent(event_id,label,n);
        #endif

    }


    /// <summary>
    /// 时长统计 时长单位毫秒
    /// </summary>
    public  void OnEventDuration(string event_id,int ms){
        GameFramework.CLog.Log($"调用代理层onEventDuration:event_id[{event_id}]ms[{ms}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        ReportAndroid.OnEventDuration(event_id,ms);
        #elif UNITY_IOS 
        ReportIOS.OnEventDuration(event_id,ms);
        #endif

    }


    /// <summary>
    /// 时长统计 事件ID 事件Label 时长单位毫秒
    /// </summary>
    public  void OnEventDuration(string event_id,string label,int ms){
        GameFramework.CLog.Log($"调用代理层onEventDuration:event_id[{event_id}]label[{label}]ms[{ms}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        ReportAndroid.OnEventDuration(event_id,label,ms);
        #elif UNITY_IOS 
        ReportIOS.OnEventDuration(event_id,label,ms);
        #endif

    }


    /// <summary>
    ///  只上报一次事件
    /// </summary>
    public  void OnEventOnlyOnce(string event_id,string label){
        GameFramework.CLog.Log($"调用代理层onEventOnlyOnce:event_id[{event_id}]label[{label}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        ReportAndroid.OnEventOnlyOnce(event_id,label);
        #elif UNITY_IOS 
        ReportIOS.OnEventOnlyOnce(event_id,label);
        #endif

    }


    /// <summary>
    ///  只上报一次事件
    /// </summary>
    public  void OnEventOnlyOnce(string event_id,string label,int n){
        GameFramework.CLog.Log($"调用代理层onEventOnlyOnce:event_id[{event_id}]label[{label}]n[{n}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        ReportAndroid.OnEventOnlyOnce(event_id,label,n);
        #elif UNITY_IOS 
        ReportIOS.OnEventOnlyOnce(event_id,label,n);
        #endif

    }


}
}

