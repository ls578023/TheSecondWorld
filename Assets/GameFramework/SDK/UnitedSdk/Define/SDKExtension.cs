using DBTSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    public static class SDKExtension 
    {
        /// <summary>
        /// 功能是否开放
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public static bool IsOpen(this SDKOpenFunction function)
        {
            return DBTSDKManager.Instance.GetSDKOpenFunctionState(function);
        }
        /// <summary>
        /// 功能与按钮的绑定
        /// </summary>
        /// <param name="function"></param>
        /// <param name="Btns"></param>
        public static void BindFunctionBtn(this SDKOpenFunction function,params Button[] Btns)
        {
            DBTSDKManager.Instance.BindFunctionBtn(function, Btns);
        }
        /// <summary>
        /// 功能与按钮的绑定,带参数的
        /// 视频按钮：参数传入视频按钮标识为 VideFlag，不填默认是VideFlag.Flag1 看视频送道具
        /// </summary>
        /// <param name="function"></param>
        /// <param name="Btns"></param>
        public static void BindFunctionBtn(this SDKOpenFunction function, Button Btn, params object[] Args)
        {
            Button[] btns = { Btn };
            DBTSDKManager.Instance.BindFunctionBtn(function, btns, Args);
        }
    }
}
