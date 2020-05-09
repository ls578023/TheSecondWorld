using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBTSDK
{

    public class IAPAndroid
    {

        static AndroidJavaObject javaClass;

        public static AndroidJavaObject JavaClass
        {
            get
            {
                if (javaClass == null)
                    javaClass = new AndroidJavaObject(SdkDefine.AdsClassName);
                return javaClass;
            }
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="prodId">商品ID</param>
        /// <param name="prodName">商品名称</param>
        /// <param name="body">商品描述信息</param>
        /// <param name="cpOrderNo">CP端订单号</param>
        /// <param name="amount">金额 元 小数点两位</param>
        public static void BuyProduct(string prodId,string prodName,string body,string cpOrderNo,float amount)
        {
            JavaClass.CallStatic("buyProduct", prodId, prodName, body, cpOrderNo, amount);
        }

        /// <summary>
        /// 通知购买结果
        /// </summary>
        /// <param name="dbtOrderNo"></param>
        /// <param name="cpResultStatus"></param>
        /// <param name="cpResultMsg"></param>
        public static void NotifyProductResult(string dbtOrderNo, string cpResultStatus, string cpResultMsg)
        {
            JavaClass.CallStatic("notifyProductResult", dbtOrderNo, cpResultStatus, cpResultMsg);
        }
    }
}
