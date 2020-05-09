
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{

    internal class TimeModule : GameFrameModule
    {

        internal override int Priority => ModulePriority.TimeModule;

        #region 变量
        static private System.DateTime now;
        static private System.DateTime startTime;
        static private bool isInit = false;
        static private double timeDifference = 0;

        static private System.DateTime defaultTime = new System.DateTime(1970, 1, 1, 8, 0, 0);

        public delegate void CompleteHandler(int id);
        public delegate void EveryHandle(int id, int time);

        private double lastTime;
        private double lastMillisecond;
        private double lastMinute;
        private int timeId;

        private double unityRealTime;

        private Dictionary<int, CompleteHandler> completeHandles;
        private Dictionary<int, double> completeTimes;
        private Dictionary<int, EveryHandleData> everySecondHandles;
        private Dictionary<int, EveryHandleData> everyMillisecondHandles;
        private Dictionary<int, EveryHandleData> everyMinuteHandles;
        #endregion

        #region 循环倒计时
        /// <summary>
        /// 循环计时器数据
        /// </summary>
        private Dictionary<int, LoopTimerData> loopTimers;

        #endregion

        internal override async Task Initialize()
        {
            startTime = defaultTime;
            completeHandles = new Dictionary<int, CompleteHandler>();
            everySecondHandles = new Dictionary<int, EveryHandleData>();
            everyMinuteHandles = new Dictionary<int, EveryHandleData>();
            everyMillisecondHandles = new Dictionary<int, EveryHandleData>();
            completeTimes = new Dictionary<int, double>();
            loopTimers = new Dictionary<int, LoopTimerData>();

            dicUnityTimer = new Dictionary<int, UnityTimerDate>();
            dicEverySecondTimer = new Dictionary<int, UnityTimerDate>();
            dicEveryMinuteTimer = new Dictionary<int, UnityTimerDate>();

            timeId = 0;
            lastTime = 0;
            lastMinute = 0;
            CLog.Log("初始化TimeModule完成");
            return;

        }

        internal override void Start()
        {
            MsgDispatcher.AddEventListener(GlobalEventType.GamePause, GamePause);
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            now = System.DateTime.Now;
            CheckTime(now, Time.realtimeSinceStartup);
            CheckLoopTimer(elapseSeconds);
            CheckUnityTimer();
        }

        void GamePause(object[] Args)
        {
            bool IsGamePause = (bool)Args[0];
            if (IsGamePause)
            {
                foreach (var item in loopTimers.Values)
                {
                    item.GamePause();
                }
            }
        }

        /// <summary>
        ///清空倒计时
        /// </summary>
        void ClearTimer()
        {
            completeHandles.Clear();
            everySecondHandles.Clear();
            everyMinuteHandles.Clear();
            everyMillisecondHandles.Clear();
            completeTimes.Clear();
            loopTimers.Clear();
            dicUnityTimer.Clear();
            dicEverySecondTimer.Clear();
            dicEveryMinuteTimer.Clear();
            timeId = 0;
            lastTime = 0;
            lastMinute = 0;
        }

        #region 循环计时

        int AddLoopTimer(int LoopSteepTime, bool UseGamePause, bool AutoPlay, Action<int> CompleteCallBack)
        {
            int id = GetTimeId();
            LoopTimerData loopTimerData = new LoopTimerData(CompleteCallBack);
            loopTimerData.id = id;
            loopTimerData.LoopSteepTime = LoopSteepTime;
            loopTimerData.UseGamePause = UseGamePause;
            loopTimers.Add(id, loopTimerData);
            if (AutoPlay)
            {
                loopTimerData.Pause(false);
            }
            return id;
        }

        void CheckLoopTimer(float elapseSeconds)
        {
            foreach (var item in loopTimers.Values)
            {
                item.AddTimes(elapseSeconds);
            }

            foreach (var item in loopTimers.Values)
            {
                item.Check();
            }
        }

        /// <summary>
        /// 增加一个循环计时器 不带暂停回调
        /// </summary>
        /// <param name="LoopSteepTime"></param>
        /// <param name="LoopTimeCompleteCallBack"></param>
        /// <returns></returns>
        public int AddLoopTimer(int LoopSteepTime, Action<int> LoopTimeCompleteCallBack, bool AutoPaly = true)
        {
            return AddLoopTimer(LoopSteepTime, false, AutoPaly, LoopTimeCompleteCallBack);
        }
        /// <summary>
        /// 增加一个循环计时器 带暂停回调
        /// </summary>
        /// <param name="LoopSteepTime"></param>
        /// <param name="LoopTimeCompleteCallBack"></param>
        /// <param name="UseGamePause"></param>
        /// <param name="GamePauseActiveCallBack"></param>
        /// <returns></returns>
        public int AddLoopTimer(int LoopSteepTime, bool UseGamePause, Action<int> CompleteCallBack, bool AutoPaly = true)
        {
            return AddLoopTimer(LoopSteepTime, UseGamePause, AutoPaly, CompleteCallBack);
        }
        /// <summary>
        /// 开始循环计时器
        /// </summary>
        /// <param name="Id"></param>
        public void PlayLoopTimer(int Id)
        {
            LoopTimerData data;
            if (!loopTimers.TryGetValue(Id, out data))
            {
                CLog.Error($"PlayLoopTimer错误，未找到对应ID[{Id}]的数据类");
                return;
            }
            data.Pause(false);

        }
        /// <summary>
        /// 暂停循环计时器
        /// </summary>
        /// <param name="Id"></param>
        public void PauseLoopTimer(int Id)
        {
            LoopTimerData data;
            if (!loopTimers.TryGetValue(Id, out data))
            {
                CLog.Error($"PauseLoopTimer，未找到对应ID[{Id}]的数据类");
                return;
            }
            data.Pause(true);
        }

        public void ResetLoopTimer(int Id)
        {
            LoopTimerData data;
            if (!loopTimers.TryGetValue(Id, out data))
            {
                CLog.Error($"ResetLoopTimer，未找到对应ID[{Id}]的数据类");
                return;
            }
            data.ResetTimer();
        }

        /// <summary>
        /// 移除循环计时器
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveLoopTimer(int Id)
        {
            if (loopTimers.ContainsKey(Id))
            {
                loopTimers[Id].Destroy();
                loopTimers.Remove(Id);
                return;
            }
            CLog.Warning($"RemoveLoopTimer，未找到对应ID[{Id}]的数据类");
        }

        #endregion

        #region  真实时间倒计时
        /// <summary>
        /// 通过dateTime获得时间戳 ms
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private double GetTime(System.DateTime t)
        {
            double ts = (t - startTime).TotalMilliseconds + timeDifference;//.TotalSeconds;

            return ts;
        }
        /// <summary>
        /// 获得当前时间的时间戳 ms
        /// </summary>
        /// <returns></returns>
        public double GetNowTime()
        {
            now = System.DateTime.Now;
            double ts = (now - startTime).TotalMilliseconds + timeDifference;

            return ts;
        }

        /// <summary>
        /// 获得当前时间的时间戳 ms
        /// </summary>
        /// <returns></returns>
        public int GetNowTimeInt()
        {
            now = System.DateTime.Now;
            double ts = (now - startTime).TotalSeconds;

            return (int)ts;
        }

        /// <summary>
        /// 返回unity真实运行时间，单位ms
        /// </summary>
        /// <returns></returns>
        public double RealtimeSinceStartup()
        {
            double realtime = unityRealTime * 1000;
            return realtime;
        }


        /// <summary>
        /// 设置服务器时间
        /// </summary>
        /// <param name="t"></param>
        public void SetServerTime(double t)
        {
            if (!isInit)
            {
                lastTime = 0;
                isInit = true;
            }
            System.DateTime nowTime = System.DateTime.Now;
            double ts = (nowTime - startTime).TotalMilliseconds;
            timeDifference = t * 1000 - ts;
        }
        /// <summary>
        /// 获取目前时间一天后的时间
        /// </summary>
        /// <returns></returns>
        public static int GetToDayZeroTime()
        {
            System.DateTime zero = System.DateTime.Now.Date.AddHours(24);
            int ts = (int)(zero - startTime).TotalSeconds;
            return ts;
        }
        /// <summary>
        /// 获取目前时间两天后的时间
        /// </summary>
        /// <returns></returns>
        public static int GetNextDayTwoTime()
        {
            System.DateTime zero = System.DateTime.Now.Date.AddHours(48);
            int ts = (int)(zero - startTime).TotalSeconds;
            return ts;
        }

        private int GetTimeId()
        {
            timeId++;
            return timeId;
        }
        /// <summary>
        /// 增加deadLine时间处理
        /// </summary>
        /// <param name="eHandle"></param>
        /// <param name="cHandle"></param>
        /// <returns></returns>
        private int AddDeadLine(double deadLine, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            //Timer.CreateTimer();
            if (cHandle == null)
            {
                return -1;
            }
            double nt = GetNowTime();
            int tId = GetTimeId();
            completeHandles.Add(tId, cHandle);
            completeTimes.Add(tId, deadLine);
            if (eHandle != null)
            {
                everySecondHandles.Add(tId, new EveryHandleData(eHandle));
            }
            return tId;
        }

        private int AddDeadLineStr(string deadLineStr, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            //Timer.CreateTimer();
            if (cHandle == null)
            {
                return -1;
            }
            double deadLine = double.Parse(deadLineStr) * 1000;
            double nt = GetNowTime();
            int tId = GetTimeId();
            completeHandles.Add(tId, cHandle);
            completeTimes.Add(tId, deadLine);
            if (eHandle != null)
            {
                everySecondHandles.Add(tId, new EveryHandleData(eHandle));
            }
            return tId;
        }

        /// <summary>
        /// 增加倒计时处理
        /// </summary>
        /// <param name="sec">单位秒</param>
        /// <param name="eHandle">每秒种处理</param>
        /// <param name="cHandle">完成处理</param>
        /// <returns></returns>
        private int AddCoundDown(int sec, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            //Timer.CreateTimer();
            if (cHandle == null)
            {
                return -1;
            }
            int tId = GetTimeId();
            completeHandles.Add(tId, cHandle);
            double deadLine = GetNowTime() + sec * 1000;
            completeTimes.Add(tId, deadLine);
            if (eHandle != null)
            {
                everySecondHandles.Add(tId, new EveryHandleData(eHandle));
            }
            return tId;
        }

        /// <summary>
        /// 增加倒计时处理2
        /// </summary>
        /// <param name="sec">单位毫秒</param>
        /// <param name="eHandle">每毫秒处理</param>
        /// <param name="cHandle">完成处理</param>
        /// <returns></returns>
        private int AddCoundDown2(int sec, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            //Timer.CreateTimer();
            if (cHandle == null)
            {
                return -1;
            }
            int tId = GetTimeId();
            completeHandles.Add(tId, cHandle);
            double deadLine = GetNowTime() + sec;
            completeTimes.Add(tId, deadLine);
            if (eHandle != null)
            {
                everyMillisecondHandles.Add(tId, new EveryHandleData(eHandle));
            }
            return tId;
        }

        /// <summary>
        /// 增加每秒处理
        /// </summary>
        /// <param name="eHandle"></param>
        /// <returns></returns>
        private int AddSecond(EveryHandle eHandle, int t)
        {
            //Timer.CreateTimer();
            if (eHandle == null)
            {
                return -1;
            }
            int tId = GetTimeId();
            everySecondHandles.Add(tId, new EveryHandleData(eHandle, t));
            return tId;
        }

        /// <summary>
        /// 增加每毫秒处理
        /// </summary>
        /// <param name="eHandle"></param>
        /// <returns></returns>
        private int AddMillisecond(EveryHandle eHandle, int t)
        {
            //Timer.CreateTimer();
            if (eHandle == null)
            {
                return -1;
            }
            int tId = GetTimeId();
            everyMillisecondHandles.Add(tId, new EveryHandleData(eHandle, t));
            return tId;
        }
        /// <summary>
        /// 增加每分钟处理
        /// </summary>
        /// <param name="eHandle"></param>
        /// <returns></returns>
        private int AddMinute(EveryHandle eHandle, int t)
        {
            //Timer.CreateTimer();
            if (eHandle == null)
            {
                return -1;
            }
            int tId = GetTimeId();
            if (everyMinuteHandles.Count == 0)
            {
                lastMinute = GetNowTime() / 1000;
            }
            everyMinuteHandles.Add(tId, new EveryHandleData(eHandle, t));
            return tId;
        }
        /// <summary>
        /// 删除时间事件
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id)
        {
            if (completeHandles.ContainsKey(id))
            {
                completeHandles.Remove(id);
            }
            if (completeTimes.ContainsKey(id))
            {
                completeTimes.Remove(id);
            }
            if (everySecondHandles.ContainsKey(id))
            {
                everySecondHandles.Remove(id);
            }
            if (everyMinuteHandles.ContainsKey(id))
            {
                everyMinuteHandles.Remove(id);
            }
            if (everyMillisecondHandles.ContainsKey(id))
            {
                everyMillisecondHandles.Remove(id);
            }
            int count = completeHandles.Count + everySecondHandles.Count + everyMinuteHandles.Count;



        }

        private void Check(System.DateTime t)
        {
            double curTime = GetTime(t);
            CheckComplete(curTime);
            CheckSecond(curTime);
            CheckMillisecond(curTime);
            CheckMinute(curTime);
        }
        private List<int> removeList;
        private void CheckComplete(double t)
        {
            if (completeHandles.Count <= 0)
            {
                return;
            }
            removeList = new List<int>();
            List<int> keys = new List<int>();
            List<CompleteHandler> handles = new List<CompleteHandler>();
            foreach (KeyValuePair<int, CompleteHandler> item in completeHandles)
            {
                keys.Add(item.Key);
                handles.Add(item.Value);
            }

            for (int i = 0; i < handles.Count; i++)
            {
                if (handles[i] != null)
                {
                    if (completeTimes.ContainsKey(keys[i]) == true)
                    {
                        double dTime = completeTimes[keys[i]];
                        if (t >= dTime)
                        {
                            handles[i](keys[i]);
                            removeList.Add(keys[i]);
                        }
                    }

                }

            }

            if (removeList.Count > 0)
            {
                for (int i = 0; i < removeList.Count; i++)
                {
                    Remove(removeList[i]);
                }
            }
        }

        private void CheckSecond(double t)
        {
            if (t - lastTime >= 1000)
            {
                if (everySecondHandles.Count > 0)
                {
                    List<int> keys = new List<int>();
                    List<EveryHandleData> handles = new List<EveryHandleData>();
                    foreach (KeyValuePair<int, EveryHandleData> item in everySecondHandles)
                    {
                        keys.Add(item.Key);
                        handles.Add(item.Value);
                    }
                    for (int i = 0; i < handles.Count; i++)
                    {
                        if (handles[i] != null)
                        {
                            int vt = 0;
                            if (completeTimes.ContainsKey(keys[i]))
                            {
                                vt = (int)(completeTimes[keys[i]] - t) / 1000;
                            }
                            handles[i].Call(keys[i], vt, (float)(t - lastTime) / 1000);
                        }
                    }
                }
                lastTime = t;
            }
        }

        private void CheckMillisecond(double t)
        {
            if (t > lastMillisecond)
            {
                if (everyMillisecondHandles.Count > 0)
                {
                    List<int> keys = new List<int>();
                    List<EveryHandleData> handles = new List<EveryHandleData>();
                    foreach (KeyValuePair<int, EveryHandleData> item in everyMillisecondHandles)
                    {
                        keys.Add(item.Key);
                        handles.Add(item.Value);
                    }
                    for (int i = 0; i < handles.Count; i++)
                    {
                        if (handles[i] != null)
                        {
                            int vt = 0;
                            if (completeTimes.ContainsKey(keys[i]))
                            {
                                vt = (int)(completeTimes[keys[i]] - t);
                            }
                            handles[i].Call(keys[i], vt, (float)(t - lastMillisecond));
                        }
                    }

                }
                lastMillisecond = t;
            }
        }

        private void CheckMinute(double t)
        {
            if (t - lastMinute >= 60000)
            {
                if (everyMinuteHandles.Count > 0)
                {
                    List<int> keys = new List<int>();
                    List<EveryHandleData> handles = new List<EveryHandleData>();
                    foreach (KeyValuePair<int, EveryHandleData> item in everyMinuteHandles)
                    {
                        keys.Add(item.Key);
                        handles.Add(item.Value);
                    }
                    for (int i = 0; i < handles.Count; i++)
                    {
                        if (handles[i] != null)
                        {
                            int vt = 0;
                            if (completeTimes.ContainsKey(keys[i]))
                            {
                                vt = (int)(completeTimes[keys[i]] - t) / 60000;
                            }
                            handles[i].Call(keys[i], vt, (float)(t - lastMinute) / 60000);
                        }
                    }

                    lastMinute = t;
                }

            }
        }



        /// <summary>
        /// 传入时间处理
        /// </summary>
        /// <param name="t"></param>
        private void CheckTime(System.DateTime t, float unityRealTime)
        {
            this.unityRealTime = unityRealTime;
            Check(t);
        }
        /// <summary>
        /// 设置deadLine时间处理
        /// </summary>
        /// <param name="deadLine">最终时间</param>
        /// <param name="cHandle">完成处理</param>
        /// <param name="eHandle">每秒处理</param>
        /// <returns></returns>
        public int SetDeadLine(int deadLine, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            return AddDeadLine(deadLine * 1000, cHandle, eHandle);
        }


        public int SetDeadLineStr(string deadLine, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            return AddDeadLineStr(deadLine, cHandle, eHandle);
        }

        public int SetDeadLineMs(double deadLine, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            return AddDeadLine(deadLine, cHandle, eHandle);
        }


        /// <summary>
        /// 设置倒计时处理,传入秒
        /// </summary>
        /// <param name="sec">倒计时秒数</param>
        /// <param name="cHandle">完成处理</param>
        /// <param name="eHandle">每秒处理</param>
        /// <returns></returns>
        public int SetCountDown(int sec, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            return AddCoundDown(sec, cHandle, eHandle);
        }

        /// <summary>
        /// 设置倒计时处理2,传入毫秒
        /// </summary>
        /// <param name="sec">倒计时毫秒数</param>
        /// <param name="cHandle">完成处理</param>
        /// <param name="eHandle">每毫秒处理</param>
        /// <returns></returns>
        public int SetCountDownByMillisecond(int sec, CompleteHandler cHandle, EveryHandle eHandle = null)
        {
            return AddCoundDown2(sec, cHandle, eHandle);
        }

        /// <summary>
        /// 设置每毫秒秒处理
        /// </summary>
        /// <param name="eHandle">每秒处理</param>
        /// <returns></returns>
        public int SetEveryMilliSecond(EveryHandle eHandle, int t = 1)
        {
            return AddMillisecond(eHandle, t);//.AddSecond(eHandle, t);
        }

        /// <summary>
        /// 设置每秒处理
        /// </summary>
        /// <param name="eHandle">每秒处理</param>
        /// <returns></returns>
        public int SetEverySecond(EveryHandle eHandle, int t = 1)
        {
            return AddSecond(eHandle, t);
        }
        /// <summary>
        /// 设置每分钟
        /// </summary>
        /// <param name="eHandle">每分钟处理</param>
        /// <returns></returns>
        public int SetEveryMinute(EveryHandle eHandle, int t = 1)
        {
            return AddMinute(eHandle, t);
        }
        /// <summary>
        /// 删除时间事件
        /// </summary>
        /// <param name="id"></param>
        public void RemoveTime(int id)
        {
            Remove(id);
        }

        static public void SetStartTime(int year, int month, int day)
        {
            startTime = new System.DateTime(year, month, day);
        }

        #endregion

        #region 逻辑倒计时
        Dictionary<int, UnityTimerDate> dicUnityTimer;
        Dictionary<int, UnityTimerDate> dicEverySecondTimer;
        Dictionary<int, UnityTimerDate> dicEveryMinuteTimer;
        /// <summary>
        /// 逻辑倒计时-S
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="UseTimeScale"></param>
        /// <param name="CompleteActive"></param>
        /// <param name="EverySecond"></param>
        public int AddUnityTimer(float duration, bool UseTimeScale, Action CompleteActive, Action<float> EveryUpdate = null, Action EverySecond=null) 
        {
            int id = GetTimeId();
            UnityTimerDate unityTimerDate = new UnityTimerDate(id, duration, UseTimeScale, false, CompleteActive, EveryUpdate);
            dicUnityTimer.Add(id, unityTimerDate);
            if (EverySecond != null)
            {
                dicEverySecondTimer.Add(id, new UnityTimerDate(id, 1, UseTimeScale, true, EverySecond));
            }
            return id;
        }
        /// <summary>
        /// 每秒倒计时
        /// </summary>
        /// <param name="UseTimeScale"></param>
        /// <param name="EverySecond"></param>
        public int AddUnityEverySecondTimer(bool UseTimeScale,Action EverySecond)
        {
            int id = GetTimeId();
            dicEverySecondTimer.Add(id, new UnityTimerDate(id, 1, UseTimeScale, true, EverySecond));
            return id;
        }
        //每分钟倒计时
        public int AddUnityEveryMinuteTimer(bool UseTimeScale, Action EverySecond)
        {
            int id = GetTimeId();
            dicEveryMinuteTimer.Add(id, new UnityTimerDate(id, 60, UseTimeScale, true, EverySecond));
            return id;
        }
        List<int> CompleteUnityTimer = new List<int>();
        void CheckUnityTimer() 
        {
            foreach (var item in CompleteUnityTimer)
            {
                RemoveUnityTimer(item);
            }
            CompleteUnityTimer.Clear();

            foreach (var item in dicEverySecondTimer.Values)
            {
                item.AddTimer();
            }
            foreach (var item in dicUnityTimer.Values)
            {
                item.AddTimer();
                if (item.isComplete)
                    CompleteUnityTimer.Add(item.id);
            }
            foreach (var item in dicEveryMinuteTimer.Values)
            {
                item.AddTimer();
            }
        }

        public void RemoveUnityTimer(int id)
        {
            if (dicUnityTimer.ContainsKey(id))
                dicUnityTimer.Remove(id);

            if (dicEverySecondTimer.ContainsKey(id))
                dicEverySecondTimer.Remove(id);

            if (dicEveryMinuteTimer.ContainsKey(id))
                dicEveryMinuteTimer.Remove(id);
        }

        #endregion
        internal override void Shutdown()
        {

            ClearTimer();
        }

    }
    /// <summary>
    /// 循环计时器
    /// </summary>
    internal class LoopTimerData
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public int id;
        /// <summary>
        /// 循环间隔
        /// </summary>
        public int LoopSteepTime;
        /// <summary>
        /// 是否使用游戏暂停计时功能  游戏暂停会调用游戏暂停回调 为flase则不会触发
        /// </summary>
        public bool UseGamePause;
        /// <summary>
        /// 循环时间到了回调
        /// </summary>
        private Action<int> CompleteActive;
        /// <summary>
        /// 计时器
        /// </summary>
        private float Timer;
        /// <summary>
        /// 暂停计时器
        /// </summary>
        private bool IsPause = false;

        public LoopTimerData(Action<int> LoopTimeCompleteCallBack)
        {
            CompleteActive += LoopTimeCompleteCallBack;
        }
        /// <summary>
        /// 增加时间
        /// </summary>
        /// <param name="deltaTime"></param>
        public void AddTimes(float deltaTime)
        {
            if (IsPause)
                return;
            Timer += deltaTime;
        }

        public void Pause(bool i)
        {
            IsPause = i;
        }

        /// <summary>
        /// 检查是否倒计时完成
        /// </summary>
        public void Check()
        {
            if (Timer >= LoopSteepTime)
            {
                InitTimer();
                CompleteActive?.Invoke(LoopSteepTime);
            }
        }
        /// <summary>
        /// 初始化倒计时
        /// </summary>
        void InitTimer() {
            Timer = 0;
        }
        /// <summary>
        /// 重置计时器
        /// </summary>
        public void ResetTimer()
        {
            Timer = 0;
        }

        /// <summary>
        /// 游戏暂停回调
        /// </summary>
        public void GamePause()
        {
            if (!UseGamePause)
                return;
            CompleteActive?.Invoke((int)Timer);
            InitTimer();
        }

        public void Destroy()
        {
            CompleteActive = null;
        }
    }
    /// <summary>
    /// U3D倒计时数据类
    /// </summary>
    internal class UnityTimerDate
    {
        public int id;
        /// <summary>
        /// 倒计时持续时间
        /// </summary>
        float duration;
        /// <summary>
        /// 计时器
        /// </summary>
        float Timer;
        /// <summary>
        /// 是否受游戏速度影响 true受游戏速度影响 false 不受
        /// </summary>
        bool UseTimeScale;
        /// <summary>
        /// 是否循环
        /// </summary>
        bool isLoop;
        /// <summary>
        /// 完成计时
        /// </summary>
        public bool isComplete;
        /// <summary>
        /// 完成计时回调
        /// </summary>
        Action CompleteActive;
        /// <summary>
        /// 每帧回调
        /// </summary>
        Action<float> UpdateActive;
        public UnityTimerDate(int id, float duration,bool UseTimeScale,bool isLoop, Action CompleteActive, Action<float> UpdateActive=null) 
        {
            this.id = id;
            this.duration = duration;
            this.UseTimeScale = UseTimeScale;
            this.isLoop = isLoop;
            this.CompleteActive = CompleteActive;
            this.UpdateActive = UpdateActive;
        }
        public void AddTimer() 
        {
            if (isComplete)
                return;
            if (UseTimeScale)
                Timer += Time.deltaTime;
            else
                Timer += Time.unscaledDeltaTime;
            //Debug.Log("duration=" + duration + "  Timer=" + Timer);
            UpdateActive?.Invoke(Timer);
            if (Timer >= duration)
                Complete();
        }
        void Complete() 
        {
            CompleteActive?.Invoke();
            if (isLoop)
            {
                Timer -= duration;
            }
            else
            {
                isComplete = true;
            }
        }
    }
    internal class EveryHandleData
    {
        public TimeModule.EveryHandle handle;
        public int maxTime = 0;
        public float curTime = 0;
        public EveryHandleData(TimeModule.EveryHandle h, int t = 1)
        {
            handle = h;
            maxTime = t;
            curTime = 0;
        }

        public void Call(int id, int time, float addTime = 1)
        {
            curTime += addTime;
            if (curTime >= maxTime)
            {
                handle(id, time);
                curTime = 0;
            }
        }
    }
}

