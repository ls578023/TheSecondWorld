using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

namespace TheSecondWorld.TaskSystem
{
    public class TaskCtrl : ControlBase<TaskCtrl>, IControl
    {
        TaskModel data;
        public bool ActiveUpdate { get; set; }
        public void OnInit() 
        {
            data = DataManagerPool.Instance.GetModel<TaskModel>();
            AddListener();
        }

        void AddListener()
        {
            MsgDispatcher.AddEventListener(GlobalEventType.TaskFinish, TaskFinish);
        }

        void RemoveListener()
        {
            MsgDispatcher.RemoveEventListener(GlobalEventType.TaskFinish, TaskFinish);
        }

        void TaskFinish(object[] arg)
        {
            if (arg != null && arg.Length > 0)
                data.TaskFinish((int)arg[0]);
        }

        public TaskData GetTask(int id)
        {
            return data.GetTask(id);
        }

        public Vector3 GetRotation()
        {
            return data.GetRotation();
        }

        public void SetRotation(Vector3 arg)
        {
            data.SetRotation(arg);
        }




        public void Updata(float elapseSeconds, float realElapseSeconds)
        {

        }

        public void OnDispose()
        {
            RemoveListener();
        }
    }
}
