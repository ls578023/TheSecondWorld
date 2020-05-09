using System;
using System.Text;
using UnityEngine;

namespace GameFramework
{
    public class CLog
    {
        private static bool isShowLog = true;
        public static void SetIsShowLog(bool isShow)
        {
            isShowLog = isShow;
        }

        /// <summary>
        /// 普通打印输出
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(params object[] msg)
        {
            if (isShowLog)
                Debug.Log(ObjectsToString(msg));
        }
        /// <summary>
        /// 输出警告信息
        /// </summary>
        public static void Warning(params object[] msg)
        {
            if (isShowLog)
                Debug.LogWarning(ObjectsToString(msg));
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        public static void Error(params object[] msg)
        {
            Debug.LogError(ObjectsToString(msg));
        }
        /// <summary>
        /// 系统输出信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Sys(params object[] msg)
        {
            Debug.Log("<color=#FF8000>[SYS:]" + ObjectsToString(msg) + "</color>");
        }
        /// <summary>
        /// 特殊信息输出信息
        /// </summary>
        /// <param name="msg"></param>
        public static void LogProcedure(params object[] msg)
        {
            Debug.Log("<color=#F1EB0C>[Procedure:]" + ObjectsToString(msg) + "</color>");
        }

        public static string ObjectsToString(object[] logs,string sTitle=null)
        {
            StringBuilder sb = new System.Text.StringBuilder();
            if(sTitle!=null)
                sb.Append(sTitle);
            for (int i = 0; i < logs.Length; ++i)
            {
                if (logs[i] == null)
                    sb.Append("null");
                else
                    sb.Append(logs[i].ToString());
                if (i < logs.Length - 1)
                    sb.Append(", ");
            }
            return sb.ToString();
        }
    }
}