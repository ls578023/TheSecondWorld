using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameFramework;


public class UpdateManager : MonoBehaviour
{
    static private UpdateManager _instance;
    public static UpdateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj;
                obj = GameObject.Find("UpdateManager");
                if (obj == null)
                {
                    obj = new GameObject();
                    obj.name = "UpdateManager";
                }
                _instance = obj.GetComponent<UpdateManager>();
                if (_instance == null)
                {
                    _instance = obj.AddComponent<UpdateManager>();
                }
                DontDestroyOnLoad(obj);
            }
            return UpdateManager._instance;
        }
    }

    public delegate void UpdateHandle();

    private Dictionary<int, UpdateHandle> _indexDict;

    public Dictionary<int, UpdateHandle> IndexDict
    {
        get
        {
            if (_indexDict == null)
            {
                _indexDict = new Dictionary<int, UpdateHandle>();
            }
            return _indexDict;
        }
        set { _indexDict = value; }
    }

    public bool isPause = false;


    private int handleIndex = 0;

    private List<UpdateHandle> _handleList;

    public List<UpdateHandle> HandleList
    {
        get
        {
            if (_handleList == null)
            {
                _handleList = new List<UpdateHandle>();
            }
            return _handleList;
        }
    }

    private List<UpdateHandle> _forceHandleList;

    public List<UpdateHandle> ForceHandleList
    {
        get
        {
            if (_forceHandleList == null)
            {
                _forceHandleList = new List<UpdateHandle>();
            }
            return _forceHandleList;
        }
        set { _forceHandleList = value; }
    }


    List<UpdateHandle> fixedUpdateHandle;
    public List<UpdateHandle> FixedUpdateHandle
    {
        get
        {
            if (fixedUpdateHandle == null)
            {
                fixedUpdateHandle = new List<UpdateHandle>();
            }
            return fixedUpdateHandle;
        }
    }

    private List<UpdateHandle> usedHandles;
    private List<int> delIndexs = new List<int>();
    private List<UpdateHandle> delHandles = new List<UpdateHandle>();
    private Dictionary<int, UpdateData> updateDatas = new Dictionary<int, UpdateData>();

    private int _timePreFrame = 16;
    public int TimePreFrame
    {
        get { return _timePreFrame; }
        set { _timePreFrame = value; }
    }
    private List<UpdateHandle> useList;
    // Use this for initialization
    void Start()
    {

    }

    private void FixedUpdate()
    {

        if (isPause)
        {
            return;
        }

        int len = FixedUpdateHandle.Count;
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                FixedUpdateHandle[i]?.Invoke();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause)
        {
            return;
        }

        CheckDel();
        int len = ForceHandleList.Count;
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                ForceHandleList[i]();
            }
        }

        len = HandleList.Count;
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                HandleList[i]();
            }
        }
       

    }

    void OnApplicationPause(bool pauseStatus)
    {
    }

    private void CheckDel()
    {
        if (delIndexs.Count > 0)
        {
            for (int i = 0; i < delIndexs.Count; i++)
            {
                RemoveHandleFormListById(delIndexs[i]);
            }
        }
        delIndexs = new List<int>();

        if (delHandles.Count > 0)
        {
            for (int i = 0; i < delHandles.Count; i++)
            {
                RemoveHandleFormList(delHandles[i]);
            }
        }
        delHandles = new List<UpdateHandle>();
    }


    private void RestoreList()
    {
        int len = useList.Count;
        HandleList.RemoveRange(0, len);
        for (int i = 0; i < useList.Count; i++)
        {
            HandleList.Add(useList[i]);
        }
    }

    /// <summary>
    /// 添加处理
    /// </summary>
    /// <param name="handle"></param>
    private int AddHandle2List(UpdateHandle handle)
    {
        int id = GetHandleId();
        HandleList.Add(handle);
        IndexDict.Add(id, handle);
        return id;
    }

    private int AddHandle2ForceList(UpdateHandle handle)
    {
        int id = GetHandleId();
        ForceHandleList.Add(handle);
        IndexDict.Add(id, handle);
        return id;
    }

    private void AddUpdateData2Dict(int id, string st, string hand)
    {
        UpdateData d = new UpdateData();
        d.actionName = st;
        d.handle = hand;
        if (updateDatas.ContainsKey(id))
        {
            CLog.Error("重复添加：" + id);
        }
        else
        {
            updateDatas.Add(id, d);
        }
    }

    private int GetHandleId()
    {
        int id = handleIndex + 1;
        int errorCount = 0;
        while (IndexDict.ContainsKey(id) && errorCount < int.MaxValue - 1)
        {
            id++;
        }
        handleIndex = id;
        if (handleIndex >= int.MaxValue - 1)
        {
            handleIndex = 0;
        }
        return id;
    }

    private void Add2DelHandles(UpdateHandle handle)
    {
        if (delHandles.IndexOf(handle) < 0)
        {
            delHandles.Add(handle);
        }

    }

    private void Add2DelIndexs(int ind)
    {
        if (delIndexs.IndexOf(ind) < 0)
        {
            delIndexs.Add(ind);
        }

    }

    /// <summary>
    /// 删除处理
    /// </summary>
    /// <param name="handle"></param>
    private void RemoveHandleFormList(UpdateHandle handle)
    {
        int ind = HandleList.IndexOf(handle);
        if (ind >= 0)
        {
            HandleList.RemoveAt(ind);
        }
        else
        {
            ind = ForceHandleList.IndexOf(handle);
            if (ind >= 0)
            {
                ForceHandleList.RemoveAt(ind);
            }
        }
        ind = FixedUpdateHandle.IndexOf(handle);
        if (ind > 0)
        {
            ForceHandleList.RemoveAt(ind);
        }
        //else
        //{
        //    GameLogger.LogError("找不到已经注册的handle删除");
        //}
        if (IndexDict.ContainsValue(handle))
        {
            int id = -1;
            foreach (KeyValuePair<int, UpdateHandle> item in IndexDict)
            {
                if (item.Value == handle)
                {
                    id = item.Key;
                    break;
                }
            }
            if (id >= 0)
            {
                IndexDict.Remove(id);
            }
        }
    }

    private void RemoveHandleFormListById(int id)
    {
        //GameFramework.Util.Log("-------RemoveHandleFormListById--------" + id);
        if (IndexDict.ContainsKey(id))
        {
            RemoveHandleFormList(IndexDict[id]);
            if (updateDatas.ContainsKey(id))
            {
                updateDatas.Remove(id);
            }
        }
    }

    /// <summary>
    /// 添加FixedUpdate
    /// </summary>
    /// <param name="handle"></param>
    private int AddFixedUpdate(UpdateHandle handle)
    {
        int id = GetHandleId();
        FixedUpdateHandle.Add(handle);
        IndexDict.Add(id, handle);
        return id;
    }

    static public int AddHandle(UpdateHandle handle)
    {
        return Instance.AddHandle2List(handle);
        //return handleIndex;
    }
    static public int AddFixedHandle(UpdateHandle handle)
    {
        return Instance.AddFixedUpdate(handle);
    }
    static public int AddForceHandle(UpdateHandle handle)
    {
        return Instance.AddHandle2ForceList(handle);
    }

    static public void RemoveHandle(UpdateHandle handle)
    {
        //Instance.RemoveHandleFormList(handle);
        Instance.Add2DelHandles(handle);
    }

    static public void RemoveHandleById(int id)
    {
        //Instance.RemoveHandleFormListById(id);
        Instance.Add2DelIndexs(id);
    }

    static public void AddUpdateData(int id, string an, string hand)
    {
        Instance.AddUpdateData2Dict(id, an, hand);
    }

}

public class UpdateData
{
    public string actionName;
    public string handle;
}
