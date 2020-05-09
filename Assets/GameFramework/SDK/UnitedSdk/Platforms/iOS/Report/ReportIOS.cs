//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace DBTSDK{
 public class ReportIOS{
#if UNITY_IOS

     [DllImport("__Internal")]
    internal static extern void onEvent(string event_id,string label);


     [DllImport("__Internal")]
    internal static extern void onEvent(string event_id,string label,int n);


     [DllImport("__Internal")]
    internal static extern void onEventDuration(string event_id,int ms);


     [DllImport("__Internal")]
    internal static extern void onEventDuration(string event_id,string label,int ms);


     [DllImport("__Internal")]
    internal static extern void onEventOnlyOnce(string event_id,string label);


     [DllImport("__Internal")]
    internal static extern void onEventOnlyOnce(string event_id,string label,int n);




/// <summary>
/// 事件统计
 /// </summary>
public static void OnEvent(string event_id,string label){
        GameFramework.CLog.Log($"调用IOSonEvent: event_id[{event_id}]label[{label}]");
        onEvent(event_id,label);
}


/// <summary>
/// 事件统计
 /// </summary>
public static void OnEvent(string event_id,string label,int n){
        GameFramework.CLog.Log($"调用IOSonEvent: event_id[{event_id}]label[{label}]n[{n}]");
        onEvent(event_id,label,n);
}


/// <summary>
/// 时长统计 时长单位毫秒
 /// </summary>
public static void OnEventDuration(string event_id,int ms){
        GameFramework.CLog.Log($"调用IOSonEventDuration: event_id[{event_id}]ms[{ms}]");
        onEventDuration(event_id,ms);
}


/// <summary>
/// 时长统计 事件ID 事件Label 时长单位毫秒
 /// </summary>
public static void OnEventDuration(string event_id,string label,int ms){
        GameFramework.CLog.Log($"调用IOSonEventDuration: event_id[{event_id}]label[{label}]ms[{ms}]");
        onEventDuration(event_id,label,ms);
}


/// <summary>
///  只上报一次事件
 /// </summary>
public static void OnEventOnlyOnce(string event_id,string label){
        GameFramework.CLog.Log($"调用IOSonEventOnlyOnce: event_id[{event_id}]label[{label}]");
        onEventOnlyOnce(event_id,label);
}


/// <summary>
///  只上报一次事件
 /// </summary>
public static void OnEventOnlyOnce(string event_id,string label,int n){
        GameFramework.CLog.Log($"调用IOSonEventOnlyOnce: event_id[{event_id}]label[{label}]n[{n}]");
        onEventOnlyOnce(event_id,label,n);
}


#endif
}
}

