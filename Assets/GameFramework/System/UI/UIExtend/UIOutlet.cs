using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{

    public class UIOutlet : MonoBehaviour
    {
        [System.Serializable]
        public class OutletInfo
        {
            public string Name;

            public string ComponentType;

            public UnityEngine.Object Object;

        }
        public List<OutletInfo> OutletInfos = new List<OutletInfo>();
    }
}
