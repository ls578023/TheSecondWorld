
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    internal class EffectModule : GameFrameModule
    {
        internal override int Priority => ModulePriority.EffectModule;

        private Dictionary<int, BaseEffect> dicEffects = new Dictionary<int, BaseEffect>();

        private Dictionary<int, EffectData> dicEffectDatas;

        internal override async Task Initialize()
        {
            dicEffectDatas = new Dictionary<int, EffectData>();
            //await new WaitForEndOfFrame();

            CLog.Log("初始化EffectModule完成");
            return;
        }

        internal override void Start()
        {

        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }
        /// <summary>
        /// 设置特性数据 进入到单个游戏时调用
        /// </summary>
        /// <param name="datas"></param>
        public void SetEffectData(Dictionary<int,EffectData> datas) 
        {
            dicEffectDatas = datas;
        }

        public EffectData GetEffectDataById(int id)
        {
            EffectData data;
            if(!dicEffectDatas.TryGetValue(id,out data))
            {
                CLog.Error($"未找到ID[{id}]为的特效");
            }
            return data;
        }
        internal override void Shutdown()
        {
            foreach (var item in dicEffects.Values)
            {
                item.Dispose();
            }
            dicEffects.Clear();
            dicEffectDatas.Clear();
        }

        /// <summary>
        /// 创建特效 指定pos
        /// </summary>
        /// <param name="templId">特效配置Id</param>
        /// <param name="offset">显示位置</param>
        /// <param name="parent">父对象，没有设置统一放特效跟节点下</param>
        /// <param name="effectOrder">显示层级排序 -1自动排</param>
        /// <returns></returns>
        int ShowEffectWithId(int templId, Vector3 pos,Vector3 rot,Vector3 targetPos,float flyTime, Transform parent = null, int effectSortingLayer = -1, int effectOrder = -1)
        {
            BaseEffect eff = CreateEffect<BaseEffect>(templId, pos, rot, targetPos,flyTime, parent, effectSortingLayer, effectOrder);
            dicEffects.Add(eff.UID, eff);
            eff.onComplete = (effect) =>
            {
                if (dicEffects.ContainsKey(eff.UID))
                {
                    dicEffects.Remove(effect.UID);
                }
            };
            return eff.UID;
        }
        /// <summary>
        /// 对外接口 默认播放
        /// </summary>
        /// <param name="templId"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public int Show(int templId, Transform parent = null)
        {
            return ShowEffectWithId(templId, Vector3.zero,Vector3.zero,Vector3.zero,0, parent);
        }
        /// <summary>
        /// 对外接口 指定坐标
        /// </summary>
        /// <param name="templId"></param>
        /// <param name="pos"></param>
        /// <param name="parent"></param>
        /// <param name="effectOrder"></param>
        /// <returns></returns>
        public int Show(int templId, Vector3 pos, Transform parent = null,int effectSortingLayer = -1, int effectOrder = -1)
        {
            return ShowEffectWithId(templId, pos, Vector3.zero,Vector3.zero, 0,parent, effectSortingLayer, effectOrder);
        }
        /// <summary>
        /// 对外接口 指定坐标和旋转
        /// </summary>
        /// <param name="templId"></param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="parent"></param>
        /// <param name="effectOrder"></param>
        /// <returns></returns>
        public int Show(int templId, Vector3 pos, Vector3 rot, Transform parent = null, int effectSortingLayer = -1, int effectOrder = -1)
        {
            return ShowEffectWithId(templId, pos, rot, Vector3.zero,0, parent, effectSortingLayer, effectOrder);
        }
        /// <summary>
        /// 对外接口 指定坐标、旋转、目标位置
        /// </summary>
        /// <param name="templId"></param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="parent"></param>
        /// <param name="effectOrder"></param>
        /// <returns></returns>
        public int Show(int templId, Vector3 pos, Vector3 rot, Vector3 targetPos,float flyTime=1.3f, Transform parent = null, int effectSortingLayer = -1, int effectOrder = -1)
        {
            return ShowEffectWithId(templId, pos, rot, targetPos, flyTime, parent, effectSortingLayer, effectOrder);
        }

        public BaseEffect Get(int uid)
        {
            BaseEffect eff;
            dicEffects.TryGetValue(uid, out eff);
            return eff;
        }

        /// <summary>
        /// 关闭特效
        /// </summary>
        /// <param name="uid"></param>
        public void Close(int uid)
        {
            BaseEffect eff;
            if (dicEffects.TryGetValue(uid, out eff))
            {
                dicEffects.Remove(uid);
                eff.Stop(); //停止特效
            }
        }
        /// <summary>
        /// 关闭所有特效
        /// </summary>
        public void CloseAll() 
        {
            foreach (var item in dicEffects.Values)
            {
                item.Stop(); //停止特效
            }
            dicEffects.Clear();
        }
        /// <summary>
        /// 特效时间
        /// </summary>
        /// <param name="uid"></param>
        public double GetEffectDuration(int uid)
        {
            BaseEffect eff;
            if (dicEffects.TryGetValue(uid, out eff))
            {
                return eff.Config.duration;
            }
            return 0;
        }

        /// <summary>
        /// 创建特效,不会进行统一管理
        /// </summary>
        /// <param name="templId"></param>
        /// <param name="pos"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public T CreateEffect<T>(int templId, Transform parent = null, int effectOrder = -1) where T : BaseEffect
        {
            return CreateEffect<T>(templId, Vector3.zero, Vector3.zero, Vector3.zero,0, parent, effectOrder);
        }

        public T CreateEffect<T>(int templId, Vector3 pos, Vector3 rot,Vector3 targetPos, float flyTime, Transform parent = null,int effectSortingLayer=-1, int effectOrder = -1) where T : BaseEffect
        {
            EffectData config;
            config = GetEffectDataById(templId);
            if (pos == null)
                pos = Vector3.zero;
            BaseEffect effect = null;
            switch ((EffectType)config.type)
            {
                case EffectType.Normal:
                    if (parent == null) //加到UI根节点的特效都显示在顶层,指定了父节点的自动设置
                    {
                        parent = UIEffectRoot;
                    }
                    effect = new UIEffect(config, pos, rot, parent);
                    break;

                case EffectType.Fly:
                    if (parent == null) //加到UI根节点的特效都显示在顶层,指定了父节点的自动设置
                    {
                        parent = UIEffectRoot;
                    }
                    effect = new FlyEffect(config, pos, rot,targetPos, flyTime,parent);
                    break;

                case EffectType.Line:
                    Vector3 StartPos = parent.position;
                    parent = LineEffectRoot;
                    effect = new LineEffect(config, pos, rot, StartPos, targetPos, parent);
                    break;
                default:
                    if (parent == null) parent = WorldEffectRoot;
                    effect = new BaseEffect(config, pos, rot,parent);
                    break;
            }

            //如果需要手动输入层级
            if (effectOrder != -1)
            {
                string SLayer = "Top";
                if (effectSortingLayer != -1)
                    SLayer = SortingLayer.IDToName(effectSortingLayer);
                effect.SetOrder(SLayer, effectOrder);
            }
            return (T)effect;
        }

        private Transform _worldEffectRoot;
        private Transform _uiEffectRoot;
        private RectTransform _LineEffectRoot;
        /// <summary>特效世界位置跟节点</summary>
        private Transform WorldEffectRoot {
            get {
                if (_worldEffectRoot == null)
                {
                    _worldEffectRoot = new GameObject("__WorldEffectRoot").transform;
                    GameObject.DontDestroyOnLoad(_worldEffectRoot);
                }
                return _worldEffectRoot;
            }
        }
        /// <summary>特效UI位置跟节点</summary>
        private Transform UIEffectRoot {
            get {
                if (_uiEffectRoot == null)
                {
                    _uiEffectRoot = new GameObject("_UIEffectRoot").transform;
                    _uiEffectRoot.SetParent(GameFrameEntry.GetModule<UIModule>().canvas.transform, false);
                }
                return _uiEffectRoot;
            }
        }

        /// <summary>连线特效父节点</summary>
        private RectTransform LineEffectRoot
        {
            get
            {
                if (_LineEffectRoot == null)
                {
                    GameObject obj = new GameObject("_LineEffectRoot");
                    obj.transform.SetParent(GameFrameEntry.GetModule<UIModule>().UIRoot.transform, false);
                    _LineEffectRoot = obj.AddComponent<RectTransform>();
                    Canvas canvas = _LineEffectRoot.gameObject.AddComponent<Canvas>();
                    canvas.overrideSorting = true;
                    canvas.sortingOrder = 100;
                    canvas.sortingLayerID = SortingLayer.NameToID("Top");
                }
                return _LineEffectRoot;
            }
        }
        /// <summary>
        /// 世界坐标转换成UI对象本地坐标
        /// </summary>
        /// <param name="WorldPos"></param>
        /// <returns></returns>
        public Vector2 WorldToRectLocalPoint(Vector3 WorldPos)
        {
            Vector2 ScrePos = RectTransformUtility.WorldToScreenPoint(GameFrameEntry.GetModule<UIModule>().UICamera, WorldPos);
            Vector2 v2 = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_LineEffectRoot, ScrePos, GameFrameEntry.GetModule<UIModule>().UICamera, out v2);
            return v2;
        }



    }
}
