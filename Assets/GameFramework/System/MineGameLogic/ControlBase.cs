using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class ControlBase<T> where T: IControl, new()
    {
        protected static T instance;
        /// <summary>
        /// 实例
        /// </summary>
        public static T I
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
}
