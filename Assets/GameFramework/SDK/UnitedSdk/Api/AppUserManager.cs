using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DBTSDK
{
    public partial class AppUserManager : MonoBehaviour
    {
        public static AppUserManager Instance;
        /// <summary>
        /// 防沉迷预设
        /// </summary>
        GameObject AntiAddictionPrefab;
        /// <summary>
        /// 防沉迷文本
        /// </summary>
        RectTransform AntiAddictionTxt;
        /// <summary>
        /// 防沉迷移动动画
        /// </summary>
        Tweener AntiAddictionTxtTweener;
        /// <summary>
        /// 防沉迷是否已经显示了
        /// </summary>
        bool IsShowAntiAddiction =false;
        private void Awake()
        {
            Instance = this;
            LoadAntiAddiction();
        }

        /// <summary>
        /// 显示评价 添加审核开关判断
        /// </summary>
        public void ShowEvaluate_M()
        {
            if (!SDKOpenFunction.DesignMode.IsOpen())
                return;

#if VersionPackage
            return;
#else
            ShowEvaluate();
#endif
        }
        #region 防沉迷
        void LoadAntiAddiction()
        {
            AntiAddictionPrefab = Instantiate(Resources.Load<GameObject>("AntiAddiction"));
            AntiAddictionTxt = AntiAddictionPrefab.GetComponentInChildren<UnityEngine.UI.Text>().rectTransform;
            AntiAddictionTxtTweener = AntiAddictionTxt.DOAnchorPos(Vector2.one * 100, 20).
                SetAutoKill(false).SetEase(Ease.Linear).Pause().SetLoops(-1, LoopType.Restart);//
            AntiAddictionPrefab.SetActive(false);
            AntiAddictionPrefab.transform.SetParent(this.transform);
        }

        /// <summary>
        /// 显示防沉迷提示 ContentCorlor带#号：#FFFFFF
        /// </summary>
        /// <param name="IsTop">是否在顶部</param>
        /// <param name="OffsetY">偏移量</param>
        /// <param name="txtContent"></param>
        public async void ShowAntiAddiction(bool IsTop, float OffsetY, string ContentCorlor="", string txtContent = "")
        {
#if !UNITY_EDITOR
            //如果APP的当前语言不是简体中文，则不显示防沉迷
            if (GameFrameEntry.GetModule<LangModule>().LangType!= ELangType.ZH_CN)
            {
                return;
            }
#endif

            if (IsShowAntiAddiction)
            {
                CLog.Log("已经显示过防沉迷提示了");
                return;
            }
            IsShowAntiAddiction = true;
            AntiAddictionPrefab.transform.SetParent(GameFrameEntry.GetModule<UIModule>()._GetUINode(EUINode.UIMessage.ToString()));
            RectTransform rect = AntiAddictionPrefab.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            float x = GameFrameEntry.GetModule<UIModule>().UIRoot.rect.width;
            float y = GameFrameEntry.GetModule<UIModule>().UIRoot.rect.height;
            y = y - 50 + OffsetY;
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
            rect.anchoredPosition = new Vector2(0, IsTop ? 0 : -y);
            //string content = GameFrameEntry.GetModule<LangModule>().Get("GameFramework.AntiAddictionText");
            //if (!string.IsNullOrEmpty(content))
            UnityEngine.UI.Text AText = AntiAddictionTxt.GetComponent<UnityEngine.UI.Text>();
            Color TextColor = Color.white;
            if (!string.IsNullOrEmpty(ContentCorlor))
            {
                ColorUtility.TryParseHtmlString(ContentCorlor, out TextColor);
            }
            AText.text = GameConst.GameFrameWorkLang.Get(GameConst.GameFrameWorkLang.GameFrameWorkLangKey.AntiAddictionTips);
            AText.color = TextColor;
            AntiAddictionPrefab.SetActive(true);
            await new WaitForEndOfFrame();
            AntiAddictionTxtTweener.ChangeEndValue(new Vector2(-AntiAddictionTxt.sizeDelta.x - x, 0)).Restart();
        }
        bool IsCloseAntiAddiction = false;
        /// <summary>
        /// 关闭防成谜提示
        /// </summary>
        public void CloseAntiAddiction()
        {
            if (!IsShowAntiAddiction || IsCloseAntiAddiction)
                return;
            AntiAddictionPrefab?.SetActive(false);
            AntiAddictionTxtTweener?.Kill();
            IsCloseAntiAddiction = true;
        }
        #endregion

        #region 分享

        /// <summary>
        /// 是否显示分享按钮，用于部分市场在打包脚本中控制变量
        /// </summary>
        public bool IsShowShare_Override()
        {

#if VersionPackage
            return false;
#else
            return IsShowShare();
#endif
        }

        /// <summary>
        /// 分享-系统分享 分享调用这个方法不要调用SDK里面的方法
        /// </summary>
        public void ShowShare(string DefaultTitle,string Url)
        {
            string title = AppInfoManager.Instance.GetOnlineConfigParams("EnShare");
            string url = GetShareUrl();
            if (string.IsNullOrEmpty(title))
                title = DefaultTitle;
            if (string.IsNullOrEmpty(url))
                url = Url;
            ShareText(title + url, title + url, url);
        }


        #endregion


        
        /// <summary>
        /// 是否显示登陆入口
        /// </summary>
        public bool ShowLogin_Override()
        {
#if VersionPackage
            return false;
#else
            return ShowLogin();
#endif

        }
    }
}
