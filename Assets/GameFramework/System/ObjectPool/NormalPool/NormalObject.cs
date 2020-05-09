using GameFramework.ObjectPool;
using UnityEngine;

namespace GameFramework.ObjectPool
{
    /// <summary>
    /// 普通实例对象。
    /// </summary>
    internal sealed class NormalObject : ObjectBase
    {
        private readonly object m_ObjAsset;

        private Transform m_TargetParent;
        private Transform m_UnspawnParent;

        public NormalObject(string name, object objAsset, object objInstance, Transform unspawnParent)
            : base(name, objInstance)
        {
            if (objAsset == null)
            {
                //throw new GameFrameworkException("object asset is invalid.");
            }

            m_ObjAsset = objAsset;
            m_UnspawnParent = unspawnParent;
        }


        protected internal override void OnSpawn()
        {
            UnityEngine.GameObject go = base.Target as UnityEngine.GameObject;
            if (go != null)
            {
                if (m_TargetParent != null)
                {
                    go.transform.SetParent(m_TargetParent, false);
                }
                go.SetActive(true);
            }
        }


        protected internal override void OnUnspawn()
        {
            UnityEngine.GameObject go = base.Target as UnityEngine.GameObject;

            if (go != null)
            {
                m_TargetParent = go.transform.parent;
                go.SetActive(false);
                go.transform.SetParent(m_UnspawnParent, false);
            }
        }

        protected internal override void Release()
        {
            NormalHelp.ReleaseUIForm(m_ObjAsset, Target);
        }
    }

}
