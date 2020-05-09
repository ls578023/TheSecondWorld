using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 发布版本开关
    /// </summary>
    public class ReleaseVerSwitch : MonoBehaviour
    {
        private void Awake()
        {
        }
        private void Start()
        {
            this.gameObject.SetActive(!GlobalData.IsReleaseVersion);
        }
    }
}
