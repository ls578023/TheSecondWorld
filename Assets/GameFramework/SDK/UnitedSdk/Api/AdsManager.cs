
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameFramework;

namespace DBTSDK
{
    public partial class AdsManager : MonoBehaviour
    {
        public string GameName="SDKTest";
        private System.Action<string, bool> videoPlayAction;

        public static AdsManager Instance;
        private void Awake()
        {
            Instance = this;
            //GameName = GameFramework.AppSetting.MineGameName;
        }


        #region Banner广告
        public void ShowBanner(int Pos=1,bool filterHighMemorySDK=true)
        {
            ShowBannerStatic(Pos, filterHighMemorySDK);
        }
        public void HideBanner()
        {
            HideBannerStatic();
        }
        #endregion

        #region 插屏广告
        public void ShowInterst()
        {
            ShowInterstitialStatic(GameName);
        }
        #endregion

        #region 激励视频
        /// <summary>
        /// 是否能够播放视频
        /// </summary>
        public bool IsCanShowVideo()
        {
#if UNITY_EDITOR || VersionPackage
            return true;
#else
            return IsShowVideoStatic();
#endif
        }

        public bool isVideoReady() 
        {

#if UNITY_EDITOR || VersionPackage
            return true;
#else
                
            if(!DBTSDK.SDKOpenFunction.DesignMode.IsOpen())
                return false;
            return IsVideoReadyStatic();
#endif
        }

        float ShowVideoInterval;
        WaitForSeconds wait2 = new WaitForSeconds(0.2f);
        public async void ShowVide(VideFlag videFlag,System.Action<string, bool> CallBack) 
        {
            //延迟0.2s 等待按钮声音播放完成
            await wait2;
            if (Time.time < ShowVideoInterval)
            {
                return;
            }
            ShowVideoInterval = Time.time + 1;
            videoPlayAction = null;
            videoPlayAction += CallBack;
            int Flag = (int)videFlag;


#if UNITY_EDITOR || VersionPackage
            CallBack?.Invoke(Flag.ToString(), true);
#else
 if (GameFramework.GlobalData.IsReleaseVersion)
            {
                GameFramework.GlobalData.IsLookVideo = true;
                ShowVideoStatic(Flag);
            }
            else
            {
                if (GameFramework.GlobalData.JumpVideo)
                {
                    CallBack?.Invoke(Flag.ToString(), true);
                }
                else
                {
                    GameFramework.GlobalData.IsLookVideo = true;
                    ShowVideoStatic(Flag);
                }
            }
#endif

        }
#endregion


        #region 回调
        public void afterVideo(string data)
        {
            GameFramework.GlobalData.IsLookVideo = false;
            DataParesAfterVide(Util.DataParse(data));

        }
        /// <summary>
        /// 激励视频回调
        /// </summary>
        /// <param name="dicDic"></param>
        void DataParesAfterVide(Dictionary<string, object> dicDic) 
        {
            string videoFlag = dicDic.ContainsKey("videoFlag") ? dicDic["videoFlag"].ToString() : "";
            string result = dicDic.ContainsKey("result") ? dicDic["result"].ToString() : "";
            Debug.Log($"解析视频回调：videoFlag[{videoFlag}] result[{result}] ");
            if (result.Equals("0"))
            {
                ReportManager.Instance.ReportVideo(videoFlag);
            }
            videoPlayAction?.Invoke(videoFlag, result.Equals("0") ? true : false);
            
        }


        public void setVideoButtonStatus(string data) 
        {
            DataParesSetVideoButtonStatus(Util.DataParse(data));
        }
        void DataParesSetVideoButtonStatus(Dictionary<string, object> dicDic)
        {
            string status = dicDic.ContainsKey("status") ? dicDic["status"].ToString() : "";
            Debug.Log($"收到插件视频按钮状态：status[{status}]");
            if(DBTSDKManager.Instance!=null && DBTSDKManager.Instance.IsInstance)
            {
                bool ActiveVideo = status.Equals("2") ? true : false;

                if (!DBTSDK.SDKOpenFunction.DesignMode.IsOpen())
                    ActiveVideo = false;
#if  !VersionPackage
                DBTSDKManager.Instance.ChangeVideoBtnState(ActiveVideo);
                GameFramework.MsgDispatcher.SendMessage(GameFramework.GlobalEventType.VideoButtonStatus, status);
#endif
            }
        }

        #endregion

        #region 信息流
        /// <summary>
        /// 信息流底部框界面上的颜色  [r, g, b], 0~255
        /// </summary>
        /// <param name="titleColor">底部框中标题/描述的字体颜色 [r, g, b], 0~255</param>
        /// <param name="actionBackgroundColor">底部框中按钮背景颜色</param>
        /// <param name="actionColor">底部框中按钮字体颜色</param>
        public void SetInformationFlowUIColor(int titleColorR, int titleColorG, int titleColorB,
            int actionBackgroundColorR, int actionBackgroundColorG, int actionBackgroundColorB,
            int actionColorR, int actionColorG, int actionColorB) 
        {
            TitleColor[0] = titleColorR;
            TitleColor[1] = titleColorG;
            TitleColor[2] = titleColorB;

            ActionBackgroundColor[0] = actionBackgroundColorR;
            ActionBackgroundColor[1] = actionBackgroundColorG;
            ActionBackgroundColor[2] = actionBackgroundColorB;

            ActionColor[0] = actionColorR;
            ActionColor[1] = actionColorG;
            ActionColor[2] = actionColorB;

        }
        int[] TitleColor = { 0, 0, 0 };
        int[] ActionBackgroundColor = { 0, 0, 0 };
        int[] ActionColor = { 0, 0, 0 };
        int InformationFlowscene = 2;
        int InformationFlowID = 99;
        /// <summary>
        /// 
        /// 显示信息流，传入信息流根节点
        /// </summary>
        /// <param name="InformationFlowRoot">信息流根节点</param>
        /// <returns>如果播放成功则返回信息流广告ID，如果不成功则返回-1</returns>
        public int ShowInformationFlow(RectTransform InformationFlowRoot) 
        {
            //找到PictureRect和 FooterRect，注意：不要改变  InformationFlowRoot、PictureRect、FooterRect的中心点
            Image PictureImage = InformationFlowRoot.Find("PictureRect")?.GetComponent<Image>();
            Image FooterImage = InformationFlowRoot.Find("FooterRect")?.GetComponent<Image>();
            Text afterAd = PictureImage.GetComponentInChildren<Text>();
            afterAd.enabled = false;
            //获取信息流状态
            int State= GetAdsStatusStatic(InformationFlowscene, InformationFlowID);
            CLog.Log("信息流状态=====" + State);
            switch (State)
            {
                case 0://不展示广告
                    PictureImage.enabled = true;
                    FooterImage.enabled = false;
                    break;
                case 1://只有主图
                    PictureImage.enabled = false;
                    FooterImage.enabled = false;
                    break;
                case 2://主图+底框
                    PictureImage.enabled = false;
                    FooterImage.enabled = true;
                    break;
                case 3://只有底框
                    PictureImage.enabled = false;
                    FooterImage.enabled = true;
                    afterAd.enabled = true;
                    break;
            }
            if (State != 0)
            {
                Camera UICamera = GameFrameEntry.GetModule<UIModule>().UICamera;//测试工程下 由于框架没有启动
                //Camera UICamera = Camera.main;
                float ScreenHeight = Screen.height;
                //InformationFlowRoot的矩阵
                Vector2[] InformationFlowRootScreenPoint = InformationFlowRoot.GetRect4ScreenPointPoint(UICamera);
                //由于平台是以手机左上为圆点  所以倒转Y轴
                int[] InforRect = CalculationPlatformRect(InformationFlowRootScreenPoint);
                
                //
                //Picture 大图矩阵
                Vector2[] PictureScreenPoint = PictureImage.rectTransform.GetRect4ScreenPointPoint(UICamera);
                int[] PictureRect = CalculationPlatformRect(PictureScreenPoint);
                PictureRect[0] = PictureRect[0] - InforRect[0];
                PictureRect[1] = PictureRect[1] - InforRect[1];

                //FooterImage 底框矩阵
                Vector2[] FooterScreenPoint = FooterImage.rectTransform.GetRect4ScreenPointPoint(UICamera);
                int[] FooterRect = CalculationPlatformRect(FooterScreenPoint);
                FooterRect[0] = FooterRect[0] - InforRect[0];
                FooterRect[1] = FooterRect[1] - InforRect[1];

                return ShowAdsStatic(InformationFlowscene, InformationFlowID,
                    InforRect, PictureRect, FooterRect,
                    TitleColor, ActionBackgroundColor, ActionColor);
            }
            return -1;
        }
        /// <summary>
        /// 计算平台所需的参数 Points矩形的4个点
        /// </summary>
        /// <returns></returns>
        int[] CalculationPlatformRect(Vector2[] Points) 
        {
            float ScreenHeight = Screen.height;
            float left = Points[0].x;
            float top = ScreenHeight-Points[0].y;
            float width = Points[2].x - Points[0].x;
            float height = Points[0].y - Points[1].y;
            int[] Result = { (int)left , (int)top, (int)width, (int)height};
            return Result;
        }

        public void RemoveInformationFlow(int id)
        {
            if (id < 0)
                return;
            RemoveAdsWidgetStatic(id);
        }
        #endregion

        void Start()
        {

        }

    }
}

