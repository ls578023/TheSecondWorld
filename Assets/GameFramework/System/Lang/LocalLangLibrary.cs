using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class LocalLangLibrary : MonoBehaviour
    {
        /// <summary>
        /// 默认包含中文
        /// </summary>
        [EnumFlags]
        public  LocalELangLibrary localLibrary= LocalELangLibrary.ZH_CN;

   
        private void Update()
        {
            Debug.Log(localLibrary.HasFlag(LocalELangLibrary.EN));
        }
    }
    
}
