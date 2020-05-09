using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class BaseItem : UIObject
    {
        /// <summary>
        /// UI路径名,自动获取,跟据UI命名空间(如果不符合自己重写此方法)
        /// UI预制路径和UI类名保持一致
        /// 获取结果为:Login/LoginUI
        /// </summary>
        public virtual string UIPath {
            get {
                Type type = this.GetType();
                return type.Namespace.Substring(type.Namespace.LastIndexOf(".") + 1) + "/" + type.Name;
            }
        }

        /// <summary>
        /// 在已有的GameObject上添加脚本
        /// 注:GameObject上要有UIOutlet脚本
        /// </summary>
        private void Instantiate(GameObject go) 
        {
            initGameObject(go);
        }

        /// <summary>
        /// 实例化Item
        /// </summary>
        /// <param name="prefab">预制体,会重新实例一个</param>
        /// <param name="parent">父对象</param>
        /// <param name="parent">是否实例一个新的预制体 </param>
        public void Instantiate(GameObject prefab, Transform parent)
        {
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            go.transform.SetParent(parent,false);
            initGameObject(go);
        }

        /// <summary>
        /// 实例化Item
        /// </summary>
        /// <param name="prefab">预制体,会重新实例一个</param>
        /// <param name="parent">父对象</param>
        public void Instantiate(UIOutlet uiOut, Transform parent)
        {
            Instantiate(uiOut.gameObject, parent);
        }
        
        /// <summary>
        /// 实例化Item,跟据类名自创建GameObject
        /// 这是一个异步实例 由于2018.3的新预制件流程 预设可以同时包含独立的预设，此方法废除
        /// </summary>
        //public async Task Instantiate(Transform parent = null)
        //{
        //    //异步初始化完成自动刷新界面
        //    GameObject go = await GameFrame.Mgr.Assetbundle.LoadPrefab("ui/"+UIPath);
        //    if (m_isDispose)
        //    {
        //        GameObject.DestroyImmediate(go);
        //        return;
        //    }
        //    if(parent!=null)
        //        go.transform.SetParent(parent,false);
        //    initGameObject(go);
        //}
        /// <summary>刷新界面</summary>
        public virtual void Refresh()
        {
        }

        public override void Dispose()
        {
            GameObject.Destroy(gameObject);
            base.Dispose();
        }
    }
}