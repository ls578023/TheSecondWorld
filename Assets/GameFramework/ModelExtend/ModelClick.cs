using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameFramework
{
    public class ModelClick : MonoBehaviour
    {
        public UnityEvent unityEvent;
        public bool needCheck = true;
        private void OnMouseDown()
        {

            if (Utils.IsClickOnUI() && NeedCheck())
                return;
            unityEvent?.Invoke();
        }

        protected virtual bool NeedCheck()
        {
            return needCheck;

        }
    }
}
