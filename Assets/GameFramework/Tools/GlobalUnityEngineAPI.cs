using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 此类只封装Unity相关API，不涉及任何common方法
    /// 方便以后更新版本修改过时API
    /// </summary>
    public static class GlobalUnityEngineAPI
    {

        public static void Destroy(UnityEngine.Object obj)
        {
            if (obj != null)
                UnityEngine.Object.Destroy(obj);
        }

        public static void Destroy(UnityEngine.Object obj, float t)
        {
            if (obj != null)
                UnityEngine.Object.Destroy(obj, t);
        }

        public static UnityEngine.Object Instantiate(UnityEngine.Object original)
        {
            UnityEngine.Object goInstance = UnityEngine.Object.Instantiate(original);
            return goInstance;

        }

        public static T Instantiate<T>(T original) where T : UnityEngine.Object
        {
            T goInstance = UnityEngine.Object.Instantiate<T>(original);
            return goInstance;
        }

        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : UnityEngine.Object
        {
            T goInstance = UnityEngine.Object.Instantiate<T>(original, position, rotation);
            return goInstance;
        }
    }
}

