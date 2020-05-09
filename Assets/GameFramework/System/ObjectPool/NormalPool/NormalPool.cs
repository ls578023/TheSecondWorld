using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.ObjectPool
{

    /// <summary>
    /// 普通对象池。
    /// </summary>
    public sealed class NormalPool
    {
        private IObjectPool<NormalObject> m_InstancePool;
        private UnityEngine.Transform m_UnspawnParent;
        private string m_PoolName;

        public void CreateObjectPool(string poolName, int capacity, float lifeTime, UnityEngine.Transform poolParent)
        {
            IObjectPoolManager objectPoolManager = GameFrameEntry.GetModule<ObjectPoolManager>();
            if (objectPoolManager != null)
            {
                m_PoolName = poolName;
                m_UnspawnParent = poolParent;
                m_InstancePool = objectPoolManager.CreateSingleSpawnObjectPool<NormalObject>(poolName);
                m_InstancePool.AutoReleaseInterval = lifeTime;
                m_InstancePool.Capacity = capacity;
                m_InstancePool.ExpireTime = lifeTime;
                m_InstancePool.Priority = 0;
            }
        }


        public void DestroyObjectPool()
        {
            IObjectPoolManager objectPoolManager = GameFrameEntry.GetModule<ObjectPoolManager>();
            if (objectPoolManager != null)
                objectPoolManager.DestroyObjectPool<NormalObject>(m_PoolName);
            if (m_UnspawnParent != null)
                GlobalUnityEngineAPI.Destroy(m_UnspawnParent.gameObject);
        }

        /// <summary>
        /// Spawn 对象 
        /// </summary>
        /// <param name="objAssetName">资源名称 带路径名 </param>
        /// <returns></returns>
        public async Task<UnityEngine.GameObject> Spawn(string objAssetName)
        {
            UnityEngine.GameObject target = null;
            NormalObject normalObject = m_InstancePool.Spawn(objAssetName);
            if (normalObject == null)
            {
                //UnityEngine.GameObject objAsset = ResourceManager.Instance.LoadAssetDataPath<UnityEngine.GameObject>(objAssetName);
                UnityEngine.GameObject objAsset = await GameFrameEntry.GetModule<AssetbundleModule>().LoadPrefab(objAssetName);
                if (objAsset != null)
                {
                    normalObject = new NormalObject(objAssetName, null, objAsset, m_UnspawnParent);
                    m_InstancePool.Register(normalObject, true);
                }
            }
            if (normalObject != null)
            {
                target = normalObject.Target as UnityEngine.GameObject;
            }
            return target;
        }

        public void UnSpawn(UnityEngine.Object obj)
        {
            m_InstancePool.Unspawn(obj);
        }
    }
}
