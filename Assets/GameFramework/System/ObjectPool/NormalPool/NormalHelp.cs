using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFramework.ObjectPool
{
    internal static class NormalHelp
    {
        public static UnityEngine.Object InstantiateUIForm(UnityEngine.Object uiFormAsset)
        {
            return GlobalUnityEngineAPI.Instantiate((UnityEngine.Object)uiFormAsset);
        }


        public static void ReleaseUIForm(object uiFormAsset, object uiFormInstance)
        {
            GlobalUnityEngineAPI.Destroy((UnityEngine.Object)uiFormInstance);
            uiFormAsset = null;
        }
    }
}
