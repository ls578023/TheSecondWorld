using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBTSDK
{
    public  partial class AppInfoManager : MonoBehaviour
    {
        public static AppInfoManager Instance;
        private void Awake()
        {
            Instance = this;
        }
    }
}
