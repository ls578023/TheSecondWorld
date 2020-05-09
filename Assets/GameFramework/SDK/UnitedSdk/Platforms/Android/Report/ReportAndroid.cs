//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK{
 public class ReportAndroid{
  
/// <summary>
/// 事件统计
 /// </summary>
public static void OnEvent(string event_id,string label)
    {
        GameFramework.CLog.Log($"调用安卓onEvent: event_id[{event_id}]label[{label}]");
        AndroidClass.BaseActivityHelper.CallStatic("onEvent",event_id,label);
    }
  
/// <summary>
/// 事件统计
 /// </summary>
public static void OnEvent(string event_id,string label,int n)
    {
        GameFramework.CLog.Log($"调用安卓onEvent: event_id[{event_id}]label[{label}]n[{n}]");
        AndroidClass.BaseActivityHelper.CallStatic("onEvent",event_id,label,n);
    }
  
/// <summary>
/// 时长统计 时长单位毫秒
 /// </summary>
public static void OnEventDuration(string event_id,int ms)
    {
        GameFramework.CLog.Log($"调用安卓onEventDuration: event_id[{event_id}]ms[{ms}]");
        AndroidClass.BaseActivityHelper.CallStatic("onEventDuration",event_id,ms);
    }
  
/// <summary>
/// 时长统计 事件ID 事件Label 时长单位毫秒
 /// </summary>
public static void OnEventDuration(string event_id,string label,int ms)
    {
        GameFramework.CLog.Log($"调用安卓onEventDuration: event_id[{event_id}]label[{label}]ms[{ms}]");
        AndroidClass.BaseActivityHelper.CallStatic("onEventDuration",event_id,label,ms);
    }
  
/// <summary>
///  只上报一次事件
 /// </summary>
public static void OnEventOnlyOnce(string event_id,string label)
    {
        GameFramework.CLog.Log($"调用安卓onEventOnlyOnce: event_id[{event_id}]label[{label}]");
        AndroidClass.BaseActivityHelper.CallStatic("onEventOnlyOnce",event_id,label);
    }
  
/// <summary>
///  只上报一次事件
 /// </summary>
public static void OnEventOnlyOnce(string event_id,string label,int n)
    {
        GameFramework.CLog.Log($"调用安卓onEventOnlyOnce: event_id[{event_id}]label[{label}]n[{n}]");
        AndroidClass.BaseActivityHelper.CallStatic("onEventOnlyOnce",event_id,label,n);
    }

}
}

