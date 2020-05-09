using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBTSDK
{
    public partial class SDKToolManager : MonoBehaviour
    {
        public static SDKToolManager Instance;
        public bool IsDebugVer;
        private void Awake()
        {
            Instance = this;
#if UNITY_EDITOR || JumpSDK
            IsDebugVer = true;
#else
            IsDebugVer = DBTSDK.SDKToolManager.Instance.IsDebugVersion();
#endif

        }
    }
}
