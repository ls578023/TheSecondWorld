using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;
using GameFramework;
using static EventListener;

    public delegate void BaseDataArgDelegate(object argl, BaseEventData eventData);

    public delegate void PointerDataArgDelegate(object arg, PointerEventData eventData);

    public delegate void AxisDataArgDelegate(object arg, AxisEventData eventData);

    public static class UIExtension
    {
        /// <summary> GameObject 点击事件</summary>
        public static void AddClick(this GameObject go, Action action)
        {
            EventListener.Get(go).onClick = (data) => { action(); };
        }

        /// <summary> GameObject 点击事件,带1参数</summary>
        public static void AddClick<T1>(this GameObject go, Action<T1> action, T1 arg1)
        {
            EventListener.Get(go).onClick = (data) => { action(arg1); };
        }

        /// <summary> GameObject 点击事件,带2参数</summary>
        public static void AddClick<T1, T2>(this GameObject go, Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            EventListener.Get(go).onClick = (data) => { action(arg1, arg2); };
        }

        /// <summary> GameObject 点击事件,带返回参数,点击点数据</summary>
        public static void AddClick(this GameObject go, PointerDataArgDelegate action, object arg = null)
        {
            EventListener.Get(go).onClick = (data) => { action(arg, data); };
        }

      
        //=========================================================

        /// <summary> 控件点击事件</summary>
        public static void AddClick<T>(this T go, Action action) where T : Component
        {
            EventListener.Get(go).onClick = (data) => { action(); };
        }

        /// <summary> 控件点击事件</summary>
        public static void AddClick<T, T1>(this T go, Action<T1> action, T1 arg1) where T : Component
        {
            EventListener.Get(go).onClick = (data) => { action(arg1); };
        }

        /// <summary> 控件点击事件</summary>
        public static void AddClick<T, T1, T2>(this T go, Action<T1, T2> action, T1 arg1, T2 arg2) where T : Component
        {
            EventListener.Get(go).onClick = (data) => { action(arg1, arg2); };
        }

        /// <summary> 控件点击事件</summary>
        public static void AddClick<T>(this T go, PointerDataArgDelegate action, object arg = null) where T : Component
        {
            EventListener.Get(go).onClick = (data) => { action(arg, data); };
        }

        /// <summary> 按钮增加点击事件(滑动窗口中使用，不然不能拖动)</summary>
        public static void AddClick(this Button btn, Action action)
        {
            btn.onClick.AddListener(() => { action(); });
        }

        /// <summary>下拉框改变事件</summary>
        public static void AddChange(this Dropdown drop, UnityAction<int> action)
        {
            drop.onValueChanged.AddListener(action);
        }

        /// <summary>Toggle改变事件</summary>
        public static void AddChange(this Toggle toogle, UnityAction<bool, Toggle> action)
        {
            toogle.onValueChanged.AddListener((bool value) => action(value, toogle));
        }

        /// <summary>Scrol改变事件</summary>
        public static void AddChange(this ScrollRect scrollRect, UnityAction<Vector2> action)
        {
            scrollRect.onValueChanged.AddListener(action);
        }
        /// <summary>Slider改变事件</summary>
        public static void AddChange(this Slider slider, UnityAction<float> action)
        {
            slider.onValueChanged.AddListener(action);
        }
        /// <summary>input改变事件</summary>
        public static void AddChange(this InputField input, UnityAction<string> action)
        {
            input.onValueChanged.AddListener(action);
        }

        /// <summary>控件拖拽结束事件</summary>
        public static void AddDragEnd(this GameObject go, Action action)
        {
            DragEventListener.Get(go).onEndDrag = (data) => { action(); };
        }

        //================================================================================================================
        /// <summary> GameObject 进入事件</summary>
        public static void AddEnter(this GameObject go, Action action)
        {
            EventListener.Get(go).onEnter = (data) => { action(); };
        }
    /// <summary> GameObject 移出事件</summary>
    public static void AddDown(this GameObject go, Action action)
    {
        EventListener.Get(go).onDown = (data) => { action(); };
    }
    /// <summary> GameObject 移出事件</summary>
    public static void AddUp(this GameObject go, Action action)
    {
        EventListener.Get(go).onUp = (data) => { action(); };
    }

    /// <summary> GameObject 移出事件</summary>
    public static void AddExit(this GameObject go, Action action)
        {
            EventListener.Get(go).onExit = (data) => { action(); };
        }

        /// <summary> 进入事件</summary>
        public static void AddEnter<T>(this T go, Action action) where T : Component
        {
            EventListener.Get(go).onEnter = (data) => { action(); };
        }

        /// <summary> 移出事件</summary>
        public static void AddExit<T>(this T go, Action action) where T : Component
        {
            EventListener.Get(go).onExit = (data) => { action(); };
        }

        #region Image扩展

        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="uiAtlas">UIAtlas</param>
        public static async void SetSprite(this Image img, string spriteName, string uiAtlas = UIAtlas.PublickIcon, bool autoSetSize = false)
        {
            if (img == null) return;
            SpriteAtlas atlas = await GameFrameEntry.GetModule<AssetbundleModule>().LoadSpriteAtlas(uiAtlas);
            if (img == null) return;
            img.sprite = atlas.GetSprite(spriteName);
            if (img.sprite == null && uiAtlas == UIAtlas.PublickIcon) //使用默认头像
                img.sprite = atlas.GetSprite("Default");
            if (autoSetSize)
            {
                img.SetNativeSize();
            }
        }

    /// <summary>
    /// 设置图片 基于Textures Textures不用指定 文件后缀不用指定 目前只支持png
    /// </summary>
    /// <param name="img">图片路径 基于Textures目录 </param>
    public static async void SetTexturesSprite(this Image img, string assetBundleName, bool autoSetSize = false)
    {
        if (img == null) return;
        Sprite sp = await LoadHelper.LoadSprite(assetBundleName);
        if (sp == null)
        {
            CLog.Error($"图片加载失败[{assetBundleName}]");
            return;
        }
        img.sprite = sp;
        if (autoSetSize)
        {
            img.SetNativeSize();
        }
    }

    /// <summary>
    /// 加载贴图
    /// </summary>
    /// <param name="img"></param>
    /// <param name="imgName"></param>
    /// <param name="imgName">IsAutoShow</param>
    public static async void SetTextures(this RawImage img, string imgName)
        {
            if (img == null) return;
            Texture tex = await LoadHelper.LoadTexture(imgName);
            if (img == null) return;
            img.texture = tex;
        }

        public static async Task SetTexturesAwait(this RawImage img, string imgName)
        {
            if (img == null) return;
            Texture tex = await LoadHelper.LoadTexture(imgName);
            if (img == null) return;
            img.texture = tex;
        }

        /// <summary>
        /// 设置Graphic透明度
        /// </summary>
        /// <param name="img"></param>
        /// <param name="alpha"></param>
        public static void SetAlpha<T>(this T img, float alpha) where T : MaskableGraphic
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }

        #endregion

        #region  SpriteRenderer扩展
        #endregion
        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="uiAtlas">UIAtlas</param>
        public static async void SetSpriteImage(this SpriteRenderer img, string spriteName, string uiAtlas = UIAtlas.PublickBg, bool autoSetSize = false)
        {
            if (img == null) return;
            SpriteAtlas atlas = await GameFrameEntry.GetModule<AssetbundleModule>().LoadSpriteAtlas(uiAtlas);
            if (img == null) return;
            img.sprite = atlas.GetSprite(spriteName);
            if (img.sprite == null && uiAtlas == UIAtlas.PublickBg) //使用默认头像
                img.sprite = atlas.GetSprite("Default");
        }
        #region 透明区域不可点击

        public static void AlphaUnClick(this Button btn)
        {
            btn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.3f;
        }

        #endregion

        #region 变灰

        private static Material grayMaterial;
        private static Material UIMarkGgrayMaterial;

        /// <summary>
        /// 设置图片变灰
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isGray">是否变灰,false/还原</param>
        public static void SetGray(this Image img, bool isGray = true)
        {
            if (isGray)
            {
                if (grayMaterial == null)
                    grayMaterial = Resources.Load<Material>("Materials/UIGray");
                img.material = grayMaterial;
            }
            else
                img.material = null;
        }
    public static void SetGray(this GameObject obj,bool isGray = true) 
    {
        MaskableGraphic[] maskableGraphics = obj.GetComponentsInChildren<MaskableGraphic>();
        if(isGray)
        {
            if (grayMaterial == null)
                grayMaterial = Resources.Load<Material>("Materials/UIGray");
        }
        for (int i = 0; i < maskableGraphics.Length; i++)
        {
            Outline outline = maskableGraphics[i].GetComponent<Outline>();

            if (outline != null)
            {
                outline.enabled = !isGray;
            }
            Shadow shadow = maskableGraphics[i].GetComponent<Shadow>();

            if (shadow != null)
            {
                shadow.enabled = !isGray;
            }
            if (isGray)
            {
                maskableGraphics[i].material = grayMaterial;
            }
            else
            {
                maskableGraphics[i].material = null;
            }

        }
    }

    /// <summary>
    /// 设置图片变灰-UIMark中使用
    /// </summary>
    /// <param name="img"></param>
    /// <param name="isGray">是否变灰,false/还原</param>
    public static void SetGrayByUIMark(this Image img, bool isGray = true)
    {
        if (isGray)
        {
            if (UIMarkGgrayMaterial == null)
                UIMarkGgrayMaterial = Resources.Load<Material>("Materials/UIMarkGray");
            img.material = UIMarkGgrayMaterial;
        }
        else
            img.material = null;
    }
    /// <summary>
    /// 设置所有图片变灰-UIMark中使用
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="isGray"></param>
    public static void SetGrayByUIMark(this GameObject obj, bool isGray = true)
    {
        MaskableGraphic[] imageArray = obj.GetComponentsInChildren<MaskableGraphic>();
        for (int i = 0; i < imageArray.Length; i++)
        {
            if (isGray)
            {
                if (UIMarkGgrayMaterial == null)
                    UIMarkGgrayMaterial = Resources.Load<Material>("Materials/UIMarkGray");
                imageArray[i].material = UIMarkGgrayMaterial;
            }
            else
                imageArray[i].material = null;
        }
    }


    public static void SetGray(this Button btn, bool isGray = true)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.SetGray(isGray);
            Image[] imgChild = btn.GetComponentsInChildren<Image>();
            foreach (var variable in imgChild)
            {
                variable.SetGray(isGray);
            }
        }

        public static void SetGrayButton(this Button btn, bool isGray = true)
        {
            //Image img = btn.image;
            //if (isGray)
            //{
            //    img.SetSprite("Btn1_2", UIAtlas.PublicButton);
            //    btn.GetComponent<Image>().SetSprite("Btn1_BG2", UIAtlas.PublicButton);
            //}
            //else
            //{
            //    btn.GetComponent<Image>().SetSprite("Btn1_BG", UIAtlas.PublicButton);
            //}
        }
        #endregion

        #region 高亮

        private static Material lightMaterial;

        /// <summary>
        /// 设置图片高亮
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isLight">是否高亮,false/还原</param>
        public static void SetLight(this Image img, bool isLight = true)
        {
            img.SetLight(isLight,1.5f);
        }

        public static void SetLight(this Button btn, bool isLight = true)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.SetLight(isLight);
            Image[] imgChild = btn.GetComponentsInChildren<Image>();
            foreach (var variable in imgChild)
            {
                variable.SetLight(isLight);
            }
        }
        /// <summary>
        /// 设置图片白色高亮
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isLight">是否高亮,false/还原</param>
        public static void SetLight(this Image img, bool isLight = true, float Bright = 1.5f)
        {
            if (isLight)
            {
                if (lightMaterial == null)
                    lightMaterial = Resources.Load<Material>("Materials/UILight");
                img.material = lightMaterial;
                img.material.SetFloat("_Bright", Bright);
            }
            else
                img.material = null;
        }

        #endregion

        #region 闪烁

        private static Material flashMaterial;

        /// <summary>
        /// 设置图片闪烁
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isflash">是否闪烁,false/还原</param>
        public static void Setflash(this Image img, bool isflash = true)
        {
            if (isflash)
            {
                if (flashMaterial == null)
                    flashMaterial = Resources.Load<Material>("Materials/UIFlash");
                img.material = flashMaterial;
            }
            else
                img.material = null;
        }

        public static void Setflash(this Button btn, bool isflash = true)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.Setflash(isflash);
            Image[] imgChild = btn.GetComponentsInChildren<Image>();
            foreach (var variable in imgChild)
            {
                variable.Setflash(isflash);
            }
        }

        #endregion

        /// <summary>
        /// 关闭所有效果
        /// </summary>
        /// <param name="img"></param>
        public static void CloseAllLight(this Image img)
        {
            img.material = null;
        }

        #region UI相关释放

        /// <summary>
        /// 释放UI上的Item列表
        /// </summary>
        public static void Dispose<T>(this List<T> list) where T : BaseItem
        {
            if (list != null)
                list.ForEach((item) => { item.Dispose(); });
            list.Clear();
        }

        #endregion

        #region 获取子对象

        /// <summary>
        /// 获取GameObject子对像，不包含自己
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static List<Transform> GetChildrenTransform(this GameObject tran)
        {
            List<Transform> list = new List<Transform>();
            foreach (Transform child in tran.transform)
                list.Add(child);
            return list;
        }

    #endregion

    static Font defultFont;
        #region Text
    public static void SetDefultFont(this Text txt)
    {
        if (defultFont == null)
            defultFont = Resources.Load<Font>("Font/SIMHEI");
        if (txt.font == null)
            txt.font = defultFont;
    }
    #endregion

    #region RectTransform扩展
    /// <summary>
    /// 获取RectTransform4个点的屏幕坐标，只争对中心点在中心的RectRectTransform
    /// </summary>
    public static Vector2[] GetRect4ScreenPointPoint(this RectTransform rectTransform,Camera UICamera) 
    {
        float width = rectTransform.rect.width / 2.0f;
        float height = rectTransform.rect.height / 2.0f;
        Vector2[] v2s = new Vector2[4];//左上  左下  右上 右下

        v2s[0] = new Vector2(-width, height);
        v2s[1] = new Vector2(-width, -height);
        v2s[2] = new Vector2(width, height);
        v2s[3] = new Vector2(width, -height);

        for (int i = 0; i < v2s.Length; i++)
        {
            v2s[i] = rectTransform.TransformPoint(v2s[i]);
            v2s[i] = UICamera.WorldToScreenPoint(v2s[i]);
        }
        return v2s;
    }
    #endregion

}
