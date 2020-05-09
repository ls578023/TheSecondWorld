using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GameFramework
{

    public class MsgDispatcher
    {

        private static MsgDispatcher _instance;
        private static MsgDispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MsgDispatcher();
                }
                return _instance;
            }
        }


        public delegate void EventHandler(object[] parms);
        //public EventHandler Event;
        private Dictionary<string, EventHandler> events = new Dictionary<string, EventHandler>();
        private int delayUpdateId = -1;

        private void AddEvent(string type, EventHandler eventHandle)
        {
            if (events.ContainsKey(type))
            {
                events[type] += eventHandle;
            }
            else
            {
                events.Add(type, eventHandle);
            }
        }

        private void RemoveEvent(string type, EventHandler eventHandle)
        {
            if (events.ContainsKey(type))
            {
                events[type] -= eventHandle;
                if (events[type] == null)
                {
                    events.Remove(type);
                }
            }
        }

        private void ClearAll()
        {
            _instance = new MsgDispatcher();
        }

        /// <summary>
        /// 检查是否有对某个事件的监听
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasEventListener(string id)
        {
            if (events.ContainsKey(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Execute(string type, object[] parms)
        {
            if (events.ContainsKey(type))
            {
                if (parms == null)
                {
                    events[type](null);
                }
                else
                {
                    events[type](parms);
                }
            }
        }

        private void ExecuteGetKey(string type, object[] parms)
        {
            if (events.ContainsKey(type))
            {
                object[] newParams = new object[parms.Length + 1];
                for (int i = 0; i < parms.Length; i++)
                {
                    newParams[i] = parms[i];
                }
                newParams[newParams.Length - 1] = type;
                events[type](newParams);
            }
        }

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parms"></param>
        public static void SendMessage(string type, params object[] parms)
        {
            Instance.Execute(type, parms);
        }

        /// <summary>
        /// 分发事件
        /// </summary>
        /// <param name="type"></param>
        public static void SendMessage(string type)
        {
            Instance.Execute(type, null);
        }
        /// <summary>
        /// 分发事件，获得事件名称
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parms"></param>
        public static void SendMessageGetKey(string type, params object[] parms)
        {
            Instance.ExecuteGetKey(type, parms);
        }






        /// <summary>
        /// 注册事件类型和监听方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="eventHandle"></param>
        public static void AddEventListener(string type, EventHandler eventHandle)
        {
            Instance.AddEvent(type, eventHandle);
        }

        /// <summary>
        /// 删除事件和对应的监听方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="eventHandle"></param>
        public static void RemoveEventListener(string type, EventHandler eventHandle)
        {
            Instance.RemoveEvent(type, eventHandle);
        }

        public static void ClearEvent()
        {
            Instance?.ClearAll();
        }

    }
}
