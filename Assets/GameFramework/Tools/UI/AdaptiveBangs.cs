using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class AdaptiveBangs : MonoBehaviour
    {
        public bool IsNeedAdaptiveBangs=false;
        public static float currScreenRote;
        // Start is called before the first frame update
        void Start()
        {
            if (!IsNeedAdaptiveBangs)
                return;
            AdaptiveIphone(this.gameObject);
        }
        /// <summary>
        /// 需要适配的View
        /// </summary>
        /// <param name="gameObject"></param>
        public static void AdaptiveIphone(GameObject gameObject)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            int OffsetHeight = AdaptiveIphoneInit();
            Debug.Log("刘海屏适配高度:"+ OffsetHeight);
            AppSetting.BangsPixel = OffsetHeight;
            AppSetting.IsBangs = AppSetting.BangsPixel != 0;
            if (AppSetting.IsPortrait)
                rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, rectTransform.offsetMax .y- OffsetHeight);
            else
                rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x+ OffsetHeight, rectTransform.offsetMin.y);
        }

        /// <summary>
        /// 获取是否是刘海屏，并给高赋值
        /// </summary>
        /// <returns></returns>
        public static int AdaptiveIphoneInit()
        {

            int height = 0;
#if UNITY_ANDROID
            if (CaleScreenRote() > 2.0f) height = 85;
#elif UNITY_IOS
            if (CaleScreenRote() > 2.0f) height = 88;
#endif
            return height;
        }

        /// <summary>
        /// 判断是否是刘海屏
        /// </summary>
        /// <returns></returns>
        private static float CaleScreenRote()
        {
            float rote = 0;

            if (AppSetting.IsPortrait)//竖版
            {
                rote = (float)Screen.height / Screen.width;
            }
            else//横板
            {
                rote = (float)Screen.width / Screen.height;
            }
            Debug.Log("宽高比：" + rote);
            currScreenRote = rote;
            return rote;
        }
    }
}
