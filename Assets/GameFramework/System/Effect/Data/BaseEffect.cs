
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class BaseEffect
    {
        private static readonly object obj = new object();
        static int _identityUID = 0;

        public int UID = 0;
        public EffectData Config;
        protected Transform m_parent = null;
        protected Vector3 m_pos;
        protected Vector3 m_rot;
        public GameObject gameObject;
        protected bool m_isActive = true;
        protected bool m_IsLoad= true;
        protected bool m_isDispose = false;
        public Action<BaseEffect> onComplete;  //播放完成事件

        public BaseEffect(Transform parent)
        {
            m_parent = parent;
        }

        public BaseEffect(EffectData config, Vector3 pos, Vector3 rot,Transform parent)
        {
            lock (obj)
                UID = ++_identityUID;
            Config = config;
            m_pos = pos;
            m_rot = rot;
            m_parent = parent;
            Start();
        }

        public void SetActive(bool value)
        {
            if (gameObject != null)
                gameObject.SetActive(value);
            m_isActive = value;
        }

        /// <summary>
        /// 开始播放特效
        /// </summary>
        protected virtual async void Start()
        {
            Load();
            await Await();
            Play();
        }
        protected virtual async void Play()
        {
            if(null== gameObject)
            {
                Stop();
                onComplete?.Invoke(this);
                return;
            }
            gameObject.SetActive(m_isActive);
            if (Config.duration > 0) //有持续时间的自动停止
            {
                await new WaitForSeconds((float)Config.duration);
                if (m_isDispose)
                    return;
                Stop();
                onComplete?.Invoke(this);
            }
        }

        /// <summary>
        /// 停止播放特效
        /// </summary>
        public void Stop()
        {
            Dispose();
        }

        /// <summary>
        /// 设置LocalPostion
        /// </summary>
        /// <param name="pos"></param>
        public void SetPosition(Vector3 pos)
        {
            m_pos = pos;
            if(gameObject != null)
                gameObject.transform.localPosition = m_pos;
        }
        /// <summary>
        /// 设置Postion
        /// </summary>
        /// <param name="pos"></param>
        public async void SetWorldPosition(Vector3 pos)
        {
            await new WaitUntil(()=> { return gameObject != null; });
            gameObject.transform.position = pos;
        }

        /// <summary>加载特效</summary>
        async void Load()
        {
            m_IsLoad = false;
            gameObject = await GameFrameEntry.GetModule<AssetbundleModule>().LoadPrefab("Effect/" + getEffectFolder()+ Config.res);           
            gameObject.transform.SetParent(m_parent, false);
            gameObject.transform.localPosition = m_pos;
            gameObject.transform.localEulerAngles = m_rot;
            SetComponent();
            m_IsLoad = true;
        }


        /// <summary>
        /// 等待载完成
        /// </summary>
        public virtual async Task Await()
        {
            await new WaitUntil(() => { return m_IsLoad || m_isDispose; });
        }

        protected string getEffectFolder()
        {
            switch ((EffectType)Config.type)
            {
                case EffectType.Normal:
                    return "UI/";
                case EffectType.Fly:
                    return "Fly/";
                case EffectType.Line:
                    return "Line/";
                case EffectType.Scene:
                    return "Scene/";
            }
            return string.Empty;
        }

        private int m_order = -1;
        private string m_orderlayer = "";
        private bool isNeedOrder = false;
        protected virtual void SetComponent()
        {
            if (gameObject != null)
            {
                if (m_order == -1)
                {
                    Canvas canvas = gameObject.GetComponentInParent<Canvas>();
                    if (canvas != null)
                    {
                        m_orderlayer = canvas.sortingLayerName;
                        m_order = canvas.sortingOrder + 1;
                    }
                    SetOrder(m_orderlayer, m_order);
                }
                else if (isNeedOrder)
                    SetOrder(m_orderlayer, m_order);
            }
        }
        /// <summary>
        /// 设置UI特效排序
        /// </summary>
        /// <param name="order"></param>
        public void SetOrder(string orderlayer, int order)
        {
            m_order = order;
            m_orderlayer = orderlayer;
            if (gameObject != null)
            {
                Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer render in renders)
                {
                    render.sortingLayerName = m_orderlayer;
                    render.sortingOrder = m_order;
                }

                Canvas[] canvas = gameObject.GetComponentsInChildren<Canvas>();
                foreach (Canvas canv in canvas)
                {
                    canv.sortingLayerName = m_orderlayer;
                    canv.sortingOrder = m_order + canv.sortingOrder;
                }

                isNeedOrder = false;
            }
            else
                isNeedOrder = true;
        }

        public virtual void Dispose()
        {
            m_isDispose = true;
            if (null!=gameObject)
                GameObject.Destroy(gameObject);
        }

    }
}
