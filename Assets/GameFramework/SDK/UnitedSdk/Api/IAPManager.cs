using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBTSDK
{

    public class IAPManager : MonoBehaviour
    {
        public static IAPManager Instance;

        private System.Action<string, string> m_OnPurchaseFailed;
        private System.Action<string,string> m_OnPurchaseSucceed;


        public event System.Action<string, string> OnPurchaseSucceedCall
        {
            add
            {
                m_OnPurchaseSucceed += value;
            }
            remove
            {
                m_OnPurchaseSucceed -= value;
            }
        }

        public event System.Action<string, string> OnPurchaseFailedCall
        {
            add
            {
                m_OnPurchaseFailed += value;
            }
            remove
            {
                m_OnPurchaseFailed -= value;
            }
        }


        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="prodId">商品ID</param>
        /// <param name="prodName">商品名称</param>
        /// <param name="body">商品描述信息</param>
        /// <param name="cpOrderNo">CP端订单号</param>
        /// <param name="amount">金额 元 小数点两位</param>
        public  void BuyProduct(string prodId, string prodName, string body, string cpOrderNo, float amount)
        {
#if UNITY_IOS
            IAPIOS.BuyProduct(prodId, prodName, body, cpOrderNo, amount);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            IAPAndroid.BuyProduct(prodId, prodName, body, cpOrderNo, amount);
#endif
        }

        /// <summary>
        /// 通知购买结果 客户端商品数据添加成功后发送此消息给服务器
        /// </summary>
        /// <param name="dbtOrderNo"></param>
        /// <param name="cpResultStatus">0-未发货 1-发货成功 2-发货失败</param>
        /// <param name="cpResultMsg"></param>
        public  void NotifyProductResult(string dbtOrderNo, string cpResultStatus, string cpResultMsg)
        {
#if UNITY_IOS
            IAPIOS.NotifyProductResult(dbtOrderNo, cpResultStatus, cpResultMsg);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            IAPAndroid.NotifyProductResult(dbtOrderNo, cpResultStatus, cpResultMsg);
#endif
        }

        #region 回调方法

        const string codeStr = "code";
        const string dbtOrderNoStr = "dbtOrderNo";
        const string prodIdStr = "prodId";
        const string errorCodeStr = "errorCode";
        const string errorMsgStr = "errorMsg";
        /* 视频广告播放完成回调安卓 */
        public void onPayCompleteCallback(string data)
        {
            DataParse(Util.DataParse(data));

        }
        /* 视频广告播放完成回调IOS */
        public void onPayCompleteCallbackIOS(string data)
        {
            DataParse(Util.DataParse(data));
        }

        /// <summary>
        /// 解析第三方传入的数据
        /// </summary>
        /// <param name="dicDic"></param>
        void DataParse(Dictionary<string, object> dicDic)
        {
            string code = dicDic.ContainsKey(codeStr) ? dicDic[codeStr].ToString() : "";
            string dbtOrderNo = dicDic.ContainsKey(dbtOrderNoStr) ? dicDic[dbtOrderNoStr].ToString() : "";
            string errorCode = dicDic.ContainsKey(errorCodeStr) ? dicDic[errorCodeStr].ToString() : "";
            string errorMsg = dicDic.ContainsKey(errorMsgStr) ? dicDic[errorMsgStr].ToString() : "";
            string prodId = dicDic.ContainsKey(prodIdStr) ? dicDic[prodIdStr].ToString() : "";

            Debug.Log("购买回调Code: " + code);

            if (code.Length == 0)
            {
                Debug.Log("callback data invalid");
                return;
            }
            if (code.Equals("0"))
            {
                //发送购买成功消息
                m_OnPurchaseSucceed?.Invoke(dbtOrderNo, prodId);
            }
            else if (code.Equals("-1"))
            {
                //发送购买失败消息
                m_OnPurchaseFailed?.Invoke(errorMsg,prodId);
            }
        }
        #endregion


        void Start()
        {

        }
    }
}
