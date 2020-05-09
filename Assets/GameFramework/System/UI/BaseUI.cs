using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    public class BaseUI : UIObject
    {
        /// <summary>
        /// UI所属节点类型
        /// </summary>
        public EUINode UINode = EUINode.UIWindow;

        /// <summary>
        /// 打开UI传进来的参数
        /// </summary>
        protected object[] _args;

        /// <summary>
        /// 打开UI的效果
        /// </summary>
        protected EUIAnim OpenAnim = EUIAnim.None;

        /// <summary>
        /// 关闭UI的效果
        /// </summary>
        protected EUIAnim CloseAnim = EUIAnim.None;

        /// <summary>
        /// 是否启用加载等待动画(默认开启)
        /// </summary>
        protected bool EnableLoading = false;

        protected bool   ShowTop = true;
        public    Action CloseAction;

        /// <summary>
        /// UI路径名,自动获取,跟据UI命名空间(如果不符合自己重写此方法)
        /// UI预制路径和UI类名保持一致
        /// 获取结果为:Login/LoginUI
        /// </summary>
        public virtual string UIPath
        {
            get
            {
                Type type = this.GetType();
                return type.Namespace.Substring(type.Namespace.LastIndexOf(".") + 1) + "/" + type.Name;
            }
        }

        /// <summary>刷新界面</summary>
        public virtual void Refresh()
        {
        }

        /// <summary>打开UI</summary>
        public async void _openUI(params object[] args)
        {
            _args = args;
            showLoading();
            GameObject obj = await GameFrameEntry.GetModule<UIModule>().LoadUI(UIPath, UINode.ToString());
            if (m_isDispose) //UI已经销毁掉了
            {
                GameObject.DestroyImmediate(obj);
                return;
            }

            if (obj == null) return;
            openTopUI();
            initGameObject(obj);
            addCanvas();
            closeLoading();
            Refresh();
            //GuideTrigger.OpenUI(this);
            if(OpenAnim== EUIAnim.Customize)
                await OpenAnimCustomize();
            else
                await GameFrameEntry.GetModule<UIModule>().UIAnim(obj, OpenAnim);
            OpenLater();
            MsgDispatcher.SendMessage(GlobalEventType.OpenUI, this);
        }
        /// <summary>
        /// 自定义打开动画-异步
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OpenAnimCustomize()
        {

        }
        /// <summary>
        /// 自定义关闭动画-异步
        /// </summary>
        /// <returns></returns>
        protected virtual async Task CloseAnimCustomize()
        {

        }

        /// <summary>打开界面之后执行，在执行动画完成后</summary>
        protected virtual void OpenLater()
        {
        }

        protected virtual void showLoading()
        {
            if (EnableLoading)
                GameFrameEntry.GetModule<UIModule>().Loading.SetVisible(true);
        }

        protected virtual void closeLoading()
        {
            if (EnableLoading)
                GameFrameEntry.GetModule<UIModule>().Loading.SetVisible(false);
        }

        protected virtual void openTopUI()
        {
            if (ShowTop && UINode == EUINode.UIWindow)
            {
                //TopUI top = Mgr.UI.GetUI<TopUI>();
                //if (top != null)
                //{
                //    top.SetData(this);
                //}
                //else
                //{
                //    Mgr.UI.Show<TopUI>(this);
                //}
            }
        }

        void addCanvas()
        {
            if (UINode == EUINode.UIWindow)
            {
                Canvas canvas = transform.GetComponent<Canvas>();
                if (canvas == null)
                    canvas = transform.gameObject.AddComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingOrder = 10 + (GameFrameEntry.GetModule<UIModule>().GetWindowNum() - 1) * 3;
                canvas.sortingLayerID = SortingLayer.NameToID("UIWindow");
                if (transform.GetComponent<GraphicRaycaster>() == null)
                    gameObject.AddComponent<GraphicRaycaster>();
            }
           
        }

        /// <summary>
        /// 等待界面加载完成
        /// </summary>
        public virtual async Task Await()
        {
            await new WaitUntil(() => { return gameObject != null || m_isDispose; });
        }

        /// <summary>关闭当前UI</summary>
        public virtual void CloseSelf()
        {
            //if (ShowTop && UINode == EUINode.UIWindow)
            //{
            //    TopUI  top = Mgr.UI.GetUI<TopUI>();
            //    BaseUI ui  = top?.GetFirst();
            //    if (ui != null && ui == this)
            //    {
            //        top.DisposeFirst();
            //    }
            //}

            CloseAction?.Invoke();

            GameFrameEntry.GetModule<UIModule>().CloseForName(GetType().Name);
        }

        /// <summary>关闭UI</summary>
        public async void _closeUI()
        {
            CloseBefore();
            if (CloseAnim == EUIAnim.Customize)
                await CloseAnimCustomize();
            else
                await GameFrameEntry.GetModule<UIModule>().UIAnim(gameObject, CloseAnim);
            m_isDispose = true;
            await new WaitForEndOfFrame();
            Dispose();
            GameObject.DestroyImmediate(gameObject);
            await new WaitForEndOfFrame();
            MsgDispatcher.SendMessage(GlobalEventType.CloseUI, this);
        }

        /// <summary>关闭之前执行，在执行动画前面</summary>
        protected virtual void CloseBefore()
        {
        }

        /// <summary>
        /// 获得指定数据
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>数据</returns>
        public T GetArg<T>(int index)
        {
            return _args != null && _args.Length > index ? (T) _args[index] : default(T);
        }

        /// <summary>
        /// 获得第一个指定类型的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>数据</returns>
        public T GetArg<T>()
        {
            T value = default(T);
            foreach (object v in _args)
            {
                if (v is T)
                    return (T) v;
            }

            return value;
        }

        public override void Dispose()
        {
            base.Dispose();
            _args = null;
        }
    }
}