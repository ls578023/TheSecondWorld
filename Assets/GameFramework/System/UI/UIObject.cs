using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameFramework
{
    public class UIObject
    {
        protected Dictionary<string, GameObject> objectList = new Dictionary<string, GameObject>();

        public GameObject gameObject;
        public Transform transform;
        public RectTransform rectTransform;
        protected bool m_isVisible = true;
        protected bool m_isActive = true;
        protected bool isInstance = false; //是否初始化完成

        protected bool m_isDispose = false; //是否已经销毁掉了

        /// <summary>
        /// 皮肤Id
        /// </summary>
        protected int useSkinId=0;

        protected void initGameObject(GameObject obj)
        {
            gameObject = obj;
            transform = obj.transform;
            Normalize();
            rectTransform = obj.GetComponent<RectTransform>();
            UIOutlet uiInfo = obj.GetComponent<UIOutlet>();

            foreach (UIOutlet.OutletInfo item in uiInfo.OutletInfos)
            {
                objectList.Add(item.Name, item.Object as GameObject);
            }
            gameObject.SetActive(m_isActive);
            gameObject.SetVisible(m_isVisible);
            isInstance = true;
            InitializeComponent();
            Initialize();
            InitializeSkin();
            //SetTextFont();
            Awake();
        }
        void SetTextFont() 
        {
            Text[] texts = gameObject.GetComponentsInChildren<Text>(true);
            foreach (var item in texts)
            {
                if (item.font == null)
                {
                    item.SetDefultFont();
                    //ToolsHelper.Log("FileName:"+ file+"Text:" + item.name+"Font:"+ item.font, false);
                }
            }
        }
        /// <summary>
        /// 标准化位置和缩放，如果不需要子类覆盖
        /// </summary>
        protected virtual void Normalize()
        {
            transform.localScale = Vector3.one;
        }
        /// <summary>初始化组件(代码生成器生成)</summary>
        protected virtual void InitializeComponent()
        {
        }
        /// <summary>皮肤初始化(代码生成器生成)</summary>
        protected virtual void InitializeSkin()
        {
        }
        /// <summary>初始化组件(自定义)</summary>
        protected virtual void Initialize()
        {
        }
        protected virtual void Awake()
        {
        }

        public virtual void SetActive(bool value)
        {
            if (gameObject != null)
                gameObject.SetActive(value);
            m_isActive = value;
        }
        public bool IsActive => m_isActive;

        public void SetVisible(bool value)
        {
            if (gameObject != null)
                gameObject.SetVisible(value);
            m_isVisible = value;
        }
        public bool IsVisible => m_isVisible;

    /// <summary>
    /// 获取控件引用对象(在UIOutlet中定义的)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">控件名</param>
    /// <returns></returns>
    public T Get<T>(string name) where T : Component
        {
            GameObject obj = Get(name);
            if (obj == null)
                return null;
            return obj.GetComponent<T>();
        }
        /// <summary>
        /// 获取引用对象(在UIOutlet中定义的)
        /// </summary>
        public GameObject Get(string name)
        {
            GameObject obj;
            if (!objectList.TryGetValue(name, out obj))
            {
                CLog.Error($"未找到GameObject对象,请在UIOutlet中设置:{name}");
            }
            return obj;
        }

        /// <summary>
        /// 获取控件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">路径</param>
        /// <param name="parent">父对象</param>
        /// <returns></returns>
        public T GetComponen<T>(string path, Transform parent=null) where T : Component
        {
            if (parent == null)
                parent = transform;
            Transform tran = parent.Find(path);
            if (tran == null)
            {
                string str = $"父对象[{parent.name}],子路径[{path}]下未找到[{typeof(T).Name}]类型的对象";
                CLog.Error(str);
                return null;
            }
            return parent.Find(path).GetComponent<T>();
        }
        public T GetComponen<T>(string path, Component parent) where T : Component
        {
            return GetComponen<T>(path, parent.transform);
        }
        public T GetComponen<T>(string path, GameObject parent) where T : Component
        {
            return GetComponen<T>(path, parent.transform);
        }
        /// <summary>
        /// 隐藏所有控件
        /// </summary>
        protected void HideAllObject()
        {
            foreach (var item in objectList.Values)
            {
                item.SetVisible(false);
            }
        }
        protected void ShowObject(params GameObject[] obj)
        {
            foreach (var item in obj)
            {
                item.SetVisible(true);
            }
        }
        public virtual void Dispose()
        {
            m_isDispose = true;
            objectList = null;
            gameObject = null;
            transform = null;
        }
    }
}
