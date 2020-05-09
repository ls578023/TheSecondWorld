using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameFramework
{
    internal partial class UIModule : GameFrameModule
    {

        public const float UIScaleInTime = 0.5f;
        internal override int Priority => ModulePriority.UIModule;
        public GameObject canvas;
        public RectTransform UIRoot;
        public Camera UICamera;
        private Dictionary<string, Transform> _uiNodeList = new Dictionary<string, Transform>();

        private WaitForEndOfFrame wait = new WaitForEndOfFrame();


        internal override async Task Initialize()
        {
            canvas = GameObject.Find("Canvas");
            canvas.transform.position = new Vector3(-100, 0, 0);
            GameObject CameraObj = GameObject.Find("UICamera");
            if (CameraObj == null)
                Debug.LogError("UI相机未找到");
            UICamera = CameraObj?.GetComponent<Camera>();
            UnityEngine.GameObject.DontDestroyOnLoad(canvas);

            GameObject obj = await GameFrameEntry.GetModule<AssetbundleModule>().LoadPrefab("UI/UIRoot", "UIRoot");
            UIRoot = obj.GetComponent<RectTransform>();
            UIRoot.transform.SetParent(canvas.transform);

            UIRoot.localScale = Vector3.one;
            UIRoot.anchorMin = Vector2.zero;
            UIRoot.anchorMax = Vector2.one;
            UIRoot.offsetMin = Vector2.zero;
            UIRoot.offsetMax = Vector2.zero;
            //是竖版的情况，并且处于平板上时，适配平板
            if (AppSetting.IsPortrait && canvas.GetComponent<CanvasScaler>().matchWidthOrHeight == 1)
            {
                float CurrWidth = canvas.GetComponent<RectTransform>().sizeDelta.x;
                //按照高控制宽
                if (CurrWidth > 720)
                {
                    float offsetX = (CurrWidth - 720) / 2.0f;
                    //宽被拉到了 把宽缩小到720的正常分辨率中
                    UIRoot.offsetMax = new Vector2(-offsetX, 0);
                    UIRoot.offsetMin = new Vector2(offsetX, 0);
                }
            }
            //else
            //{
            //    float CurrHeight = canvas.GetComponent<RectTransform>().sizeDelta.y;
            //    //按宽控制高
            //    if (CurrHeight > 1280)
            //    {
            //        float offsetY = (CurrHeight - 1280) / 2.0f;
            //        //高被拉了 把宽缩小到720的正常分辨率中
            //        //UIRoot.offsetMax = new Vector2(-offsetX, 0);
            //        UIRoot.offsetMin = new Vector2(UIRoot.offsetMin.x, offsetY);
            //    }
            //}

            CLog.Log("初始化UIModule完成");
        }

        internal override void Start()
        {
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        internal override void Shutdown()
        {
            foreach (var item in _uiList)
            {
                item.Value.Dispose();
            }
            _uiList.Clear();
            UnityEngine.GameObject.Destroy(canvas);
        }
       
        /// <summary>
        /// 刷新语言
        /// </summary>
        public void RefreshLang()
        {
            UILangText[] list = canvas.GetComponentsInChildren<UILangText>();
            for (int i = list.Length; --i >= 0;)
                list[i].Refresh();
        }

        /// <summary>
        /// 加载UI
        /// </summary>
        /// <param name="uiPath">相对于UI目录下的路径如:Login/Login</param>
        public async Task<GameObject> LoadUI(string uiPath, string uiNode)
        {
            string uiName = uiPath.Substring(uiPath.LastIndexOf("/") + 1);
            GameObject go = await GameFrameEntry.GetModule<AssetbundleModule>().LoadPrefab("UI/" + uiPath, uiName);
            await wait;
            go.transform.SetParent(_GetUINode(uiNode));
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            return go;
        }
        /// <summary>
        /// 获取UI节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Transform _GetUINode(string node)
        {
            Transform tran;
            if (!_uiNodeList.TryGetValue(node, out tran))
            {
                tran = UIRoot.transform.Find(node);
                if (tran == null)
                {
                    CLog.Error("未找到UI节点:" + node);
                    tran = canvas.transform;
                }
                _uiNodeList.Add(node, tran);
            }
            return tran;
        }


        private WaitForSeconds waitAnim5 = new WaitForSeconds(0.5f);
        private WaitForSeconds waitAnim3 = new WaitForSeconds(0.3f);

        /// <summary>
        /// 界面集合
        /// </summary>
        private Dictionary<string, BaseUI> _uiList = new Dictionary<string, BaseUI>();
        private Canvas m_canvas;
        private GameObject m_loading;
        public Dictionary<string, BaseUI> UIList => _uiList;


        public Canvas Canvas
        {
            get
            {
                if (m_canvas == null)
                    m_canvas = canvas.GetComponent<Canvas>();
                return m_canvas;
            }
        }
        public GameObject Loading
        {
            get
            {
                if (m_loading == null)
                {
                    m_loading = UIRoot.transform.Find("Loading").gameObject;
                    //Mgr.Effect.Show(3, m_loading.transform);
                }

                return m_loading;
            }
        }

        /// <summary>
        /// 对外接口 打开UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public T Show<T>(params object[] args) where T : BaseUI, new()
        {
            Type type = typeof(T);
            string name = type.Name;
            T ui = null;
            if (_uiList.ContainsKey(name))
                ui = (T)_uiList[name];
            try
            {
                if (ui == null)
                {
                    ui = new T();
                    _uiList.Add(name, ui);
                    ui._openUI(args);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + ex.StackTrace);
            }
            return ui;
        }

        /// <summary>
        /// 跟据UI名打开UI
        /// </summary>
        /// <param name="uiName">模块名.UI名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public BaseUI Open(string uiName, params object[] args)
        {
            Type type = Type.GetType("GameFrame." + uiName);
            BaseUI ui = null;
            string name = type.Name;
            if (_uiList.ContainsKey(name))
                ui = _uiList[name];
            try
            {
                if (ui == null)
                {
                    ui = Activator.CreateInstance(type) as BaseUI;
                    _uiList.Add(name, ui);
                    ui._openUI(args);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message + ex.StackTrace);
            }

            return ui;
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Close<T>() where T : BaseUI
        {
            string name = typeof(T).Name;
            CloseForName(name);
        }
        /// <summary>
        /// 关闭全部UI
        /// </summary>
        public void CloseAll()
        {
            for (int i = _uiList.Count; i-- > 0;)
                _uiList.Values.ElementAt(i)._closeUI();
            _uiList.Clear();
        }

        /// <summary>
        /// 跟据UI名字关闭UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void CloseForName(string name)
        {
            if (_uiList.ContainsKey(name))
            {
                BaseUI ui = _uiList[name];
                _uiList.Remove(name);
                ui._closeUI();
            }
        }

        /// <summary>
        /// 刷新UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Refresh<T>() where T : BaseUI
        {
            T ui = GetUI<T>();
            if (ui != null)
                ui.Refresh();
        }

        /// <summary>
        /// 设置UI隐藏和显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetActive<T>(bool isActive) where T : BaseUI
        {
            T ui = GetUI<T>();
            if (ui != null)
                ui.SetActive(isActive);
        }

        /// <summary>
        /// 获取UI,UI没打开时返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUI<T>() where T : BaseUI
        {
            string name = typeof(T).Name;
            if (_uiList.ContainsKey(name))
                return (T)_uiList[name];
            return null;
        }

        /// <summary>
        /// 跟据UI名字获取UI
        /// </summary>
        /// <param name="name"></param>
        public BaseUI GetUI(string name)
        {
            if (_uiList.ContainsKey(name))
            {
                BaseUI ui = _uiList[name];
                return ui;
            }

            return null;
        }

        /// <summary>
        /// 获取当前在Window下的界面个数
        /// </summary>
        /// <returns></returns>
        public int GetWindowNum()
        {
            int num = 0;
            foreach (var variable in _uiList.Values)
            {
                if (variable.UINode == EUINode.UIWindow && null!=variable.gameObject)
                {
                    num++;
                }
            }

            return num;
        }

        /// <summary>
        /// 渐现菜单
        /// </summary>
        /// <param name="targetGO">菜单游戏对象</param>
        public async Task UIAnim(GameObject target, EUIAnim anim)
        {
            float time = UIScaleInTime;
            switch (anim)
            {
                case EUIAnim.FadeOut:
                    time = 0.35f;
                    break;
                case EUIAnim.ScaleOut:
                    time = 0.2f;
                    break;
            }

            await UIUtils.ObjectAnim(target, anim, time);
        }

    }
}
