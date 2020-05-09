using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Tools;

namespace TheSecondWorld.TaskSystem
{
    [ModelRegister("TheSecondWorld")]
    public class TaskModel : DataManagerBase
    {
        TaskDic data;
        public override void OnInit()
        {
            string content = ReadLocalData();
            data = Json.ToObject<TaskDic>(content);
            if (data == null)
            {
                data = new TaskDic();
                data.dataDic = new Dictionary<int, TaskData>();
                data.vec = Vector3.zero;
            }
            foreach (var item in ConfigMgr.Instance.dicTask.Values)
            {
                if (!data.dataDic.ContainsKey(item.id))
                {
                    TaskData tmp = new TaskData();
                    tmp.taskID = item.id;
                    tmp.taskDes = item.des;
                    data.dataDic.Add(tmp.taskID, tmp);
                }
            }

        }

        public void TaskFinish(int id)
        {
            if(data.dataDic.ContainsKey(id))
                data.dataDic[id].isFinish = true;
        }

        public TaskData GetTask(int id)
        {
            if (data.dataDic.ContainsKey(id))
                return data.dataDic[id];
            return null;
        }

        public override void OnSave()
        {
            if (data != null)
            {
                string str = Json.ToJson(data);
                SaveLocalData(str);
            }
        }

        public Vector3 GetRotation()
        {
            return data.vec;
        }

        public void SetRotation(Vector3 arg)
        {
            data.vec = arg;
        }


        public override void OnRelease()
        {

        }
    }

    public class TaskData
    {
        public int taskID;
        public string taskDes;
        public bool isFinish;
    }

    public class TaskDic
    {
        public Dictionary<int, TaskData> dataDic;
        public Vector3 vec;
    }

}
