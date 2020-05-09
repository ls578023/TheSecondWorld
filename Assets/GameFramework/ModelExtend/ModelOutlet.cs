using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
    public class ModelOutlet : MonoBehaviour
    {
        [System.Serializable]
        public class ModelOutletInfo
        {
            public string NodeName;

            public Transform transform;

        }

        public List<ModelOutletInfo> OutletInfos = new List<ModelOutletInfo>();

        private Dictionary<string, Transform> m_dicOutletInfo;
        public Dictionary<string, Transform> dicOutletInfo
        {
            get
            {
                if (m_dicOutletInfo == null)
                {
                    m_dicOutletInfo = new Dictionary<string, Transform>();
                    foreach (ModelOutletInfo info in OutletInfos)
                        m_dicOutletInfo.Add(info.NodeName, info.transform);
                }
                return m_dicOutletInfo;
            }
        }

        /// <summary>
        /// 获取节点上的对象(在ModelOutlet中定义的)
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="name">节点名</param>
        public T GetComponent<T>(string name) where T : Component
        {
            Transform tran;
            if (dicOutletInfo.TryGetValue(name, out tran))
                return tran.GetComponent<T>();
            return default(T);
        }

        /// <summary>
        /// 获取节点对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Transform GetNode(string name)
        {
            Transform tran;
            if (dicOutletInfo.TryGetValue(name, out tran))
                return tran;
            return null;
        }
    }
}
