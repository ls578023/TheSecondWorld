using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBTSDK
{
    public class DBTSDKManager : MonoBehaviour
    {
        private const string adsObjName = "DBTSDKManager";
        private GameObject m_SdkGameObject;

        private static DBTSDKManager m_Instance;

        public static DBTSDKManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    GameObject obj = GameObject.Find(adsObjName);
                    if (obj == null)
                    {
                        obj = new GameObject(adsObjName);
                        UnityEngine.Object.DontDestroyOnLoad(obj);
                    }
                    m_Instance = obj.AddComponent<DBTSDKManager>();
                    m_Instance.m_SdkGameObject = obj;
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// 返回界面用户选着结果
        /// </summary>
        private System.Action<string> onBackPressed;


        public event System.Action<string> BackPressed
        {
            add
            {
                onBackPressed += value;
            }
            remove
            {
                onBackPressed -= value;
            }
        }

        public bool IsInstance;
        public static void InitSDK() 
        {
            CloseAdsTips = false;
            if (Instance.IsInstance)
            {
                ReportManager.Instance.InitReportManager();
                return;
            }
            Instance.InitDBTSDK();
            Instance.IsInstance = true;
        }
        public void InitDBTSDK() 
        {
            //初始化广告
            AddSdkMonoComponent<AdsManager>();
            //初始化内购
            AddSdkMonoComponent<IAPManager>();
            //初始化上报
            AddSdkMonoComponent<ReportManager>();
            //初始化APP的基本属
            AddSdkMonoComponent<AppInfoManager>();
            //初始化用户模块
            AddSdkMonoComponent<AppUserManager>();
            //初始化SDK提供的工具
            AddSdkMonoComponent<SDKToolManager>();
            StartCoroutine("InitSDKOpenFunction");
        }
        /// <summary>
        /// 添加脚本到DBTSDKManager对象下 方便第三方交互
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void AddSdkMonoComponent<T>()where T: Component
        {
            if (m_SdkGameObject == null)
                Debug.LogError("DBTSDKManager对象未找到");
            m_SdkGameObject.GetOrAddComponent<T>();
        }

        #region SDK功能开放模块

        Dictionary<SDKOpenFunction, bool> dicFunctionState=new Dictionary<SDKOpenFunction, bool>();
        Dictionary<SDKOpenFunction, List<Button>> dicFunctionBtns = new Dictionary<SDKOpenFunction, List<Button>>();
        Dictionary<SDKOpenFunction, UnityEngine.Events.UnityAction> dicFunctionAction = new Dictionary<SDKOpenFunction, UnityEngine.Events.UnityAction>();

        IEnumerator InitSDKOpenFunction() 
        {
            yield return new WaitForEndOfFrame();
            #region 绑定回调方法
            dicFunctionAction.Add(SDKOpenFunction.AdsVideo, AdsVideoFunction);
            dicFunctionAction.Add(SDKOpenFunction.ShowShare, ShowShareFunction);
            dicFunctionAction.Add(SDKOpenFunction.ShowLogin, ShowLoginFunction);
            dicFunctionAction.Add(SDKOpenFunction.ShowFeedback, FeedbackFunction);
            dicFunctionAction.Add(SDKOpenFunction.GDPRUser, GDPRUserFunction);
            dicFunctionAction.Add(SDKOpenFunction.ShowPolicy, ShowPolicyFunction);
            dicFunctionAction.Add(SDKOpenFunction.ShowUserProtocol, ShowUserProtocolFunction);
            dicFunctionAction.Add(SDKOpenFunction.ShowGameBanHao, ShowGameBanHaoFunction);
            #endregion
            #region 检查功能是否开放
#if UNITY_EDITOR

            dicFunctionState.Add(SDKOpenFunction.InstallVersion, true);
            dicFunctionState.Add(SDKOpenFunction.FirstStartVer, true);
            dicFunctionState.Add(SDKOpenFunction.IsWifi, true);
            dicFunctionState.Add(SDKOpenFunction.isRootSystem, true);
            dicFunctionState.Add(SDKOpenFunction.ShowMoreGameStatic, true);
            dicFunctionState.Add(SDKOpenFunction.AdsVideo, true);
            dicFunctionState.Add(SDKOpenFunction.ShowShare, true);
            dicFunctionState.Add(SDKOpenFunction.ShowLogin, true);
            dicFunctionState.Add(SDKOpenFunction.ShowFeedback, true);
            dicFunctionState.Add(SDKOpenFunction.GDPRUser, true);
            dicFunctionState.Add(SDKOpenFunction.ShowPolicy, true);
            dicFunctionState.Add(SDKOpenFunction.ShowUserProtocol, true);
            dicFunctionState.Add(SDKOpenFunction.ShowRealName, true);
            dicFunctionState.Add(SDKOpenFunction.ShowContactInfo, true);
            dicFunctionState.Add(SDKOpenFunction.VideoIsReady, true);
            dicFunctionState.Add(SDKOpenFunction.ShowEvaluate, true);
            dicFunctionState.Add(SDKOpenFunction.DesignMode, true);
            dicFunctionState.Add(SDKOpenFunction.ShowGameBanHao, true);

#else

            dicFunctionState.Add(SDKOpenFunction.InstallVersion, AppInfoManager.Instance.IsInstallVersion());
            dicFunctionState.Add(SDKOpenFunction.FirstStartVer, AppInfoManager.Instance.IsFirstStartVer());
            dicFunctionState.Add(SDKOpenFunction.IsWifi, AppInfoManager.Instance.IsWifi());
            dicFunctionState.Add(SDKOpenFunction.isRootSystem, AppInfoManager.Instance.IsRootSystem());
            dicFunctionState.Add(SDKOpenFunction.ShowMoreGameStatic, AppInfoManager.Instance.ShowMoreGameStatic());
            dicFunctionState.Add(SDKOpenFunction.AdsVideo, AdsManager.Instance.IsCanShowVideo());
            dicFunctionState.Add(SDKOpenFunction.ShowShare, AppUserManager.Instance.IsShowShare_Override());
            dicFunctionState.Add(SDKOpenFunction.ShowLogin, AppUserManager.Instance.ShowLogin_Override());
            dicFunctionState.Add(SDKOpenFunction.ShowFeedback, AppUserManager.Instance.IsShowFeedback());
            dicFunctionState.Add(SDKOpenFunction.GDPRUser, AppUserManager.Instance.IsRequestLocationInEeaOrUnknownStatic());
            dicFunctionState.Add(SDKOpenFunction.ShowPolicy, SDKToolManager.Instance.IsShowPolicy());
            dicFunctionState.Add(SDKOpenFunction.ShowUserProtocol, SDKToolManager.Instance.IsShowPolicy());
            dicFunctionState.Add(SDKOpenFunction.ShowRealName, SDKToolManager.Instance.IsShowRealNameRegistration());
            dicFunctionState.Add(SDKOpenFunction.ShowContactInfo, SDKToolManager.Instance.IsShowContactInformation());
            dicFunctionState.Add(SDKOpenFunction.VideoIsReady, false);
            dicFunctionState.Add(SDKOpenFunction.ShowEvaluate, AppUserManager.Instance.IsShowEvaluate());
            dicFunctionState.Add(SDKOpenFunction.DesignMode, AppInfoManager.Instance.GetDesignModeStatic()==0);
            dicFunctionState.Add(SDKOpenFunction.ShowGameBanHao, SDKToolManager.Instance.GetGameBanHaoType()==0);
#endif
            #endregion

        }

        #region 按钮视频特殊处理方法
        /// <summary>
        /// 业务层调用 让SDK层检查视频是否准备好
        /// </summary>
        public void CheckVideoIsReady() 
        {
            if (!SDKOpenFunction.AdsVideo.IsOpen())
                return;
            ChangeVideoBtnState(AdsManager.Instance.isVideoReady());
        }
        /// <summary>
        /// SDK内部调用 其他模块不要调用
        /// </summary>
        /// <param name="IsReady"></param>
        public void ChangeVideoBtnState(bool IsReady) 
        {
            if (!SDKOpenFunction.AdsVideo.IsOpen())
                return;
            SetSDKFunctionState(SDKOpenFunction.VideoIsReady, IsReady);
            CheckNull(SDKOpenFunction.AdsVideo);
            List<Button> btns = new List<Button>();
            if(dicFunctionBtns.TryGetValue(SDKOpenFunction.AdsVideo, out btns))
            {
                foreach (var item in btns)
                {
                    if (item is DBTVideoBtn)
                    {
                        ((DBTVideoBtn)item).SetButtonState(IsReady ? DBTVideoBtn.BtnState.Active : DBTVideoBtn.BtnState.ActiveNotClick);
                    }
                    else
                        SetButtonGay(item, !IsReady);
                }
            }
        }
        /// <summary>
        /// 按钮置灰
        /// </summary>
        void SetButtonGay(Button btn,bool i) 
        {
            btn.gameObject.SetGray(i);
            //Image[] imgs = btn.GetComponentsInChildren<Image>();
            //foreach (var item in imgs)
            //{
            //    item.SetGray(i);
            //}
        }
        #endregion

        /// <summary>
        /// 绑定按钮
        /// </summary>
        /// <param name="sDKOpenFunction"></param>
        /// <param name="buttons"></param>
        public void BindFunctionBtn(SDKOpenFunction sDKOpenFunction,Button[] buttons,params object[] Args)
        {
            List<Button> btns;
            if(!dicFunctionBtns.TryGetValue(sDKOpenFunction,out btns))
            {
                btns = new List<Button>();
                dicFunctionBtns.Add(sDKOpenFunction, btns);
            }
            foreach (var item in buttons)
            {
                //如果此功能系统自带了点击功能，则绑定功能
                UnityEngine.Events.UnityAction callBack;
                if (dicFunctionAction.TryGetValue(sDKOpenFunction, out callBack))
                {
                    if(item is DBTVideoBtn)
                    {
                        ((DBTVideoBtn)item).TSonClick.AddListener(callBack);
                    }
                    else
                        item.onClick.AddListener(callBack);
                }
                btns.Add(item);

                item.gameObject.SetActive(sDKOpenFunction.IsOpen());
                ////视频按钮的开放做特殊处理
                //if (sDKOpenFunction== SDKOpenFunction.AdsVideo)
                //{
                //    VideFlag videFlag= VideFlag.Flag1;
                //    //对于视频按钮的显示，除了SDKOpenFunction.AdsVideo方法外 还需要审核状态进行控制，此功能只存在与IOS
                //    if (Args != null && Args.Length > 0)
                //        videFlag = (VideFlag)Args[0];

                //    item.gameObject.SetActive(sDKOpenFunction.IsOpen());
                //    //if (videFlag== VideFlag.Flag1)
                //    //{
                //    //    //视频送道具按钮特殊处理
                //    //    //item.gameObject.SetActive(sDKOpenFunction.IsOpen() && SDKOpenFunction.DesignMode.IsOpen());
                //    //}
                //    //else
                //    //{
                //    //    item.gameObject.SetActive(sDKOpenFunction.IsOpen());
                //    //}
                //}
                //else
                //{
                //}
            }
            CheckNull(sDKOpenFunction);
            //ReshsdkFunctionBtnState(sDKOpenFunction);
        }
        /// <summary>
        /// 刷新按钮的状态
        /// </summary>
        /// <param name="sDKOpenFunction"></param>
        void ReshsdkFunctionBtnState(SDKOpenFunction sDKOpenFunction)
        {
            List<Button> btns;
            if (dicFunctionBtns.TryGetValue(sDKOpenFunction, out btns))
            {
                foreach (var item in btns)
                {
                    if(null!= item)
                    {
                        item.gameObject.SetActive(sDKOpenFunction.IsOpen());
                    }
                }
            }
        }
        /// <summary>
        /// 检查按钮是否被摧毁了
        /// </summary>
        /// <param name="btns"></param>
        void CheckNull(SDKOpenFunction sDKOpenFunction)
        {
            List<Button> btns;
            if (dicFunctionBtns.TryGetValue(sDKOpenFunction, out btns))
            {
                int count = btns.Count-1;
                for (int i = count; i >= 0; i--)
                {
                    if (null == btns[i])
                        btns.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 获取功能的状态
        /// </summary>
        /// <param name="sDKOpenFunction"></param>
        /// <returns></returns>
        public bool GetSDKOpenFunctionState(SDKOpenFunction sDKOpenFunction) 
        {
            bool result = false;
            dicFunctionState.TryGetValue(sDKOpenFunction, out result);
            return result;
        }
        /// <summary>
        /// 设置功能状态
        /// </summary>
        /// <param name="function"></param>
        /// <param name="state"></param>
        void SetSDKFunctionState(SDKOpenFunction function,bool state) 
        {
            if (dicFunctionState.ContainsKey(function))
            {
                dicFunctionState[function] = state;
            }
            else
            {
                Debug.LogError($"SDK功能开放中不存在Key[{function}]");
            }
        }

        #region 功能开放时，触发的按钮事件函数


        public static bool CloseAdsTips = false;
        /// <summary>广告视频 </summary>
        void AdsVideoFunction() 
        {
            if (CloseAdsTips)
                return;
            if (!SDKOpenFunction.VideoIsReady.IsOpen())
            {
                SDKToolManager.Instance.ShowToast(GameConst.GameFrameWorkLang.Get(GameConst.GameFrameWorkLang.GameFrameWorkLangKey.VideoNoReady));
            }
        }
        /// <summary>分享 </summary>
        void ShowShareFunction() 
        {
            //string title = AppInfoManager.Instance.GetOnlineConfigParams("EnShare");
            //string url = AppInfoManager.Instance.GetOnlineConfigParams("ShareUrl");
            //AppUserManager.Instance.ShareAppBySys(title, title, url);
        }
        /// <summary>登陆 </summary>
        void ShowLoginFunction() 
        {
            AppUserManager.Instance.LoginComStatic(1);
        }
        /// <summary>反馈按钮</summary>
        void FeedbackFunction() 
        {
            AppUserManager.Instance.ShowFeedback();
        }
        /// <summary>欧盟用户 </summary>
        void GDPRUserFunction() 
        {
            AppUserManager.Instance.ShowGDPRDialogStatic();
        }
        /// <summary>隐私政策和用户协议 </summary>
        void ShowPolicyFunction() 
        {
            SDKToolManager.Instance.GotoPrivacyPolicyStatic();
        }
        /// <summary>用户协议 </summary>
        void ShowUserProtocolFunction()
        {
            SDKToolManager.Instance.GotoTermsServiceStatic();
        }
        /// <summary>
        /// 显示版号
        /// </summary>
        void ShowGameBanHaoFunction() 
        {
            SDKToolManager.Instance.ShowGameBanHao();
        }
        #endregion

        #endregion

        #region Back键第三方回调
        static string VideoCallBackCode = "code";
        /* 视频广告播放完成回调安卓 */
        public void onBackPressedCallback(string data)
        {
            //Debug.Log("get Android onBackPressedCallbackIOS data: " + data);

            //Dictionary<string, object> dicDic = DCGJSON.Json.Deserialize(data) as Dictionary<string, object>;

            DataParse(Util.DataParse(data));

        }
        /* 视频广告播放完成回调IOS */
        public void onBackPressedCallbackIOS(string data)
        {
            //Debug.Log("get IOS onBackPressedCallbackIOS data: " + data);
            //Dictionary<string, object> dicDic = VXJSON.Json.Deserialize(data) as Dictionary<string, object>;

            DataParse(Util.DataParse(data));
        }

        /// <summary>
        /// 解析第三方传入的数据
        /// </summary>
        /// <param name="dicDic"></param>
        void DataParse(Dictionary<string, object> dicDic)
        {
            Debug.Log("解析的PressedCallbackdic为:" + dicDic);
            string code = dicDic.ContainsKey(VideoCallBackCode) ? dicDic[VideoCallBackCode].ToString() : "";

            Debug.Log("PressedCallback Code: " + code);

            if (code.Length == 0)
            {
                Debug.Log("callback data invalid");
                return;
            }
            onBackPressed?.Invoke(code);
        }
        #endregion


        private void Update()
        {
            if (!IsInstance)
                return;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SDKToolManager.Instance.ShowExitDialog();
            }
        }
        public void Shutdown() 
        {
            GameObject.Destroy(m_SdkGameObject);
            Instance.IsInstance = false;
            m_Instance = null;
        }
    }
}
