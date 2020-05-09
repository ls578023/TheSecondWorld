 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GameFramework
{

    public class InputManager : MonoBehaviour
    {
        #region 单例
        static private InputManager _instance;

        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = GameObject.Find("InputManager");
                    if (obj == null)
                    {
                        obj = new GameObject();
                        obj.name = "InputManager";
                    }
                    _instance = obj.GetComponent<InputManager>();
                    if (_instance == null)
                    {
                        _instance = obj.AddComponent<InputManager>();
                    }

                    DontDestroyOnLoad(obj);
                }
                return InputManager._instance;
            }
        }
        #endregion
        #region 变量
        private float _mouseX = 0;
        /// <summary>
        /// 鼠标x位置
        /// </summary>
        public float MouseX
        {
            get { return _mouseX; }
        }
        private float _mouseY = 0;
        /// <summary>
        /// 鼠标Y位置
        /// </summary>
        public float MouseY
        {
            get { return _mouseY; }
        }

        private KeyCode curKey = KeyCode.None;
        private List<KeyCode> curKeys;

        private Vector3 mouseBeginPos;

        private int _mulTouchLen = 0;

        public int MulTouchLen
        {
            get { return _mulTouchLen; }
        }
        private List<MulTouchData> mulDatas;

        private string touchDebugStr = "";

        #region 触摸类事件委托
        public delegate void OnTouchDebugHandle(string str);
        public delegate void OnTouchHandle(float x, float y);
        public delegate void OnMulTouchHandle(int num, List<MulTouchData> datas);
        public delegate void OnMulTouchEndHandle();
        public delegate void OnScaleHandle(float delta);
        #endregion
        #region 键盘类事件委托
        public delegate void OnKeyHandle(KeyCode key);
        public delegate void OnKeyHoldHandle(List<KeyCode> keys);
        #endregion

        #region 触摸类事件处理
        private OnTouchHandle touchBeginHandle;
        private OnTouchHandle touchHoldHandle;
        private OnTouchHandle touchMoveHandle;
        private OnTouchHandle touchEndHandle;
        private OnTouchHandle clickHandle;

        private OnTouchDebugHandle touchDebugHandle;

        private OnMulTouchHandle mulTouchBeginHandle;
        private OnMulTouchHandle mulTouchHoldHandle;
        private OnMulTouchHandle mulTouchMoveHandle;
        private OnMulTouchEndHandle mulTouchEndHandle;
        private OnScaleHandle scaleHandle;
        #endregion

        #region 键盘类事件处理
        private OnKeyHandle keyUpHandle;
        private OnKeyHoldHandle keyHoldHandle;
        private OnKeyHandle keyDownHandle;
        #endregion
        #endregion

        #region Unity自身方法
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckTouch();
            AndroidBack();
        }

        void AndroidBack()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameFramework.MsgDispatcher.SendMessage(GameFramework.GlobalEventType.AndroidBackKey);
            }
        }

        void OnGUI()
        {
            CheckKey();
        }
        #endregion

        #region 鼠标或者手指触摸类检测
        private void CheckTouch()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                GetTouchInput();
            }
            else
            {
                GetMouseInput();
            }

        }

        private void GetTouchInput()
        {
            Touch[] touchs = Input.touches;
            int touchCount = Input.touchCount;
            if (touchCount == 1)
            {
                _mulTouchLen = 1;
                Touch touch = Input.touches[0];
                _mouseX = touch.position.x;
                _mouseY = touch.position.y;
                if (touch.phase == TouchPhase.Began)
                {
                    mouseBeginPos = touch.position;
                    OnTouchBegin();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    OnTouchEnd();
                    float dis = Vector2.Distance(mouseBeginPos, touch.position);
                    if (dis < 1)
                    {
                        OnClick();
                    }
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    OnTouchMove();
                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    OnTouchHold();
                }
            }
            else if (touchCount > 1)
            {
                _mulTouchLen = 0;
                List<MulTouchData> lastMulDatas = mulDatas;
                mulDatas = new List<MulTouchData>();
                int state = 0;
                for (int i = 0; i < touchCount; i++)
                {
                    Touch touch = touchs[i];
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        continue;
                    }
                    _mulTouchLen++;
                    if (touch.phase == TouchPhase.Moved)
                    {
                        state = 1;
                    }
                    MulTouchData d = new MulTouchData();
                    d.id = i;
                    d.x = touch.position.x;
                    d.y = touch.position.y;
                    mulDatas.Add(d);
                }
                if (_mulTouchLen > 1)
                {
                    if (state == 0)
                    {
                        OnMulTouchHold();
                    }
                    else if (state == 1)
                    {
                        OnMulTouchMove();
                        if (scaleHandle != null)
                        {
                            if (lastMulDatas.Count > 1)
                            {
                                scaleValue = Vector2.Distance(new Vector2(mulDatas[0].x, mulDatas[0].y), new Vector2(mulDatas[1].x, mulDatas[1].y)) - Vector2.Distance(new Vector2(lastMulDatas[0].x, lastMulDatas[0].y), new Vector2(lastMulDatas[1].x, lastMulDatas[1].y));
                                scaleValue = scaleValue / 50;
                                OnScale();
                            }
                        }
                    }
                }
            }
            else
            {
                if (_mulTouchLen > 0)
                {
                    _mulTouchLen = 0;
                    OnMulTouchEnd();
                }


            }
            touchDebugStr = "len:" + touchCount + " enable:" + _mulTouchLen + "\r\n";
            for (int i = 0; i < touchCount; i++)
            {
                touchDebugStr += touchs[i].phase.ToString();
                touchDebugStr += "\r\n";
            }
            OnTouchDebug();

        }

        private float _lastX = 0;
        private float _lastY = 0;

        private float scaleValue = 0;

        private void GetMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseX = Input.mousePosition.x;
                _mouseY = Input.mousePosition.y;
                _lastX = _mouseX;
                _lastY = _mouseY;
                mouseBeginPos = Input.mousePosition;
                OnTouchBegin();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _mouseX = Input.mousePosition.x;
                _mouseY = Input.mousePosition.y;
                float dis = Vector2.Distance(mouseBeginPos, Input.mousePosition);
                if (dis < 1)
                {
                    OnClick();
                }
                _lastX = _mouseX;
                _lastY = _mouseY;
                OnTouchEnd();
            }
            else if (Input.GetMouseButton(0))
            {

                _lastX = _mouseX;
                _lastY = _mouseY;
                _mouseX = Input.mousePosition.x;
                _mouseY = Input.mousePosition.y;
                if (_lastX == _mouseX && _lastY == _mouseY)
                {
                    OnTouchHold();
                }
                else
                {
                    OnTouchMove();
                }
            }
            float wheelValue = Input.GetAxis("Mouse ScrollWheel");
            if (wheelValue != 0)
            {
                scaleValue = wheelValue;
                OnScale();

            }
        }
        #endregion

        #region 键盘类输入检测
        private List<KeyCode> keyUpList;
        private void CheckKey()
        {
            if (curKeys == null)
            {
                curKeys = new List<KeyCode>();
            }
            Event e = Event.current;
            if (e == null)
            {
                return;
            }
            if (e.isKey)
            {
                KeyCode code = e.keyCode;
                if (code != KeyCode.None)
                {
                    curKey = code;
                    if (e.type == EventType.KeyDown)
                    {
                        int ind = curKeys.IndexOf(curKey);
                        if (ind < 0)
                        {
                            curKeys.Add(curKey);
                            OnKeyDown();
                        }
                    }
                    else if (e.type == EventType.KeyUp)
                    {
                        OnKeyUp();
                        int ind = curKeys.IndexOf(curKey);
                        if (ind >= 0)
                        {
                            curKeys.RemoveAt(ind);
                        }
                    }

                }
                if (curKeys.Count > 0)
                {
                    OnKeyHold();
                }

            }

        }
        #endregion

        #region 鼠标（触摸）事件
        #region 私有方法
        private void AddTouchBeginHandle(OnTouchHandle handle)
        {
            touchBeginHandle += handle;
        }

        private void AddTouchMoveHandle(OnTouchHandle handle)
        {
            touchMoveHandle += handle;
        }

        private void AddTouchHoldHandle(OnTouchHandle handle)
        {
            touchHoldHandle += handle;
        }

        private void AddTouchEndHandle(OnTouchHandle handle)
        {
            touchEndHandle += handle;
        }

        private void RemoveTouchBeginHandle(OnTouchHandle handle)
        {
            touchBeginHandle -= handle;
        }

        private void RemoveTouchHoldHandle(OnTouchHandle handle)
        {
            touchHoldHandle -= handle;
        }

        private void RemoveTouchMoveHandle(OnTouchHandle handle)
        {
            touchMoveHandle -= handle;
        }

        private void RemoveTouchEndHandle(OnTouchHandle handle)
        {
            touchEndHandle -= handle;
        }

        private void AddClickHandle(OnTouchHandle handle)
        {
            clickHandle += handle;
        }

        private void RemoveClickHandle(OnTouchHandle handle)
        {
            clickHandle -= handle;
        }
        #endregion
        #region 静态方法
        /// <summary>
        /// 添加鼠标开始事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddTouchBeginListener(OnTouchHandle handle)
        {
            Instance.AddTouchBeginHandle(handle);
        }
        /// <summary>
        /// 添加鼠标移动事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddTouchMoveListener(OnTouchHandle handle)
        {
            Instance.AddTouchMoveHandle(handle);
        }

        /// <summary>
        /// 添加鼠标点住事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddTouchHoldListener(OnTouchHandle handle)
        {
            Instance.AddTouchHoldHandle(handle);
        }
        /// <summary>
        /// 添加鼠标离开事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddTouchEndListener(OnTouchHandle handle)
        {
            Instance.AddTouchEndHandle(handle);
        }
        /// <summary>
        /// 移除鼠标开始事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveTouchBeginListener(OnTouchHandle handle)
        {
            Instance.RemoveTouchBeginHandle(handle);
        }
        /// <summary>
        /// 移除鼠标点住事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveTouchHoldListener(OnTouchHandle handle)
        {
            Instance.RemoveTouchHoldHandle(handle);
        }
        /// <summary>
        /// 移除鼠标移动事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveTouchMoveListener(OnTouchHandle handle)
        {
            Instance.RemoveTouchMoveHandle(handle);
        }
        /// <summary>
        /// 移除鼠标结束事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveTouchEndListener(OnTouchHandle handle)
        {
            Instance.RemoveTouchEndHandle(handle);
        }

        static public void AddClickListener(OnTouchHandle handle)
        {
            Instance.AddClickHandle(handle);
        }

        static public void RemoveClickListener(OnTouchHandle handle)
        {
            Instance.RemoveClickHandle(handle);
        }
        #endregion
        #endregion
        #region 多点触摸事件
        #region 私有方法
        private void AddMulTouchBeginHandle(OnMulTouchHandle handle)
        {
            mulTouchBeginHandle += handle;
        }
        private void AddMulTouchHoldHandle(OnMulTouchHandle handle)
        {
            mulTouchHoldHandle += handle;
        }
        private void AddMulTouchMoveHandle(OnMulTouchHandle handle)
        {
            mulTouchMoveHandle += handle;
        }
        private void AddMulTouchEndHandle(OnMulTouchEndHandle handle)
        {
            mulTouchEndHandle += handle;
        }

        private void AddScaleHandle(OnScaleHandle handle)
        {
            scaleHandle += handle;
        }

        private void RemoveMulTouchBeginHandle(OnMulTouchHandle handle)
        {
            mulTouchBeginHandle -= handle;
        }
        private void RemoveMulTouchHoldHandle(OnMulTouchHandle handle)
        {
            mulTouchHoldHandle -= handle;
        }
        private void RemoveMulTouchMoveHandle(OnMulTouchHandle handle)
        {
            mulTouchMoveHandle -= handle;
        }
        private void RemoveMulTouchEndHandle(OnMulTouchEndHandle handle)
        {
            mulTouchEndHandle -= handle;
        }
        private void RemoveScaleHandle(OnScaleHandle handle)
        {
            scaleHandle -= handle;
        }

        private void AddTouchDebug(OnTouchDebugHandle handle)
        {
            touchDebugHandle += handle;
        }

        private void RemoveTouchDebug(OnTouchDebugHandle handle)
        {
            touchDebugHandle -= handle;
        }
        #endregion
        #region 静态方法
        /// <summary>
        /// 增加多点触摸开始事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddMulTouchBeginListener(OnMulTouchHandle handle)
        {
            Instance.AddMulTouchBeginHandle(handle);
        }
        /// <summary>
        /// 增加多点触摸按住事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddMulTouchHoldListener(OnMulTouchHandle handle)
        {
            Instance.AddMulTouchHoldHandle(handle);
        }
        /// <summary>
        /// 增加多点触摸移动事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddMulTouchMoveListener(OnMulTouchHandle handle)
        {
            Instance.AddMulTouchMoveHandle(handle);
        }
        /// <summary>
        /// 增加多点触摸结束事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddMulTouchEndListener(OnMulTouchEndHandle handle)
        {
            Instance.AddMulTouchEndHandle(handle);
        }

        static public void AddScaleListener(OnScaleHandle handle)
        {
            Instance.AddScaleHandle(handle);
        }

        /// <summary>
        /// 移除多点触摸开始事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveMulTouchBeginListener(OnMulTouchHandle handle)
        {
            Instance.RemoveMulTouchBeginHandle(handle);
        }
        /// <summary>
        /// 移除多点触摸按住事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveMulTouchHoldListener(OnMulTouchHandle handle)
        {
            Instance.RemoveMulTouchHoldHandle(handle);
        }
        /// <summary>
        /// 移除多点触摸移动事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveMulTouchMoveListener(OnMulTouchHandle handle)
        {
            Instance.RemoveMulTouchMoveHandle(handle);
        }
        /// <summary>
        /// 移除多点触摸结束事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveMulTouchEndListener(OnMulTouchEndHandle handle)
        {
            Instance.RemoveMulTouchEndHandle(handle);
        }

        static public void RemoveScaleListener(OnScaleHandle handle)
        {
            Instance.RemoveScaleHandle(handle);
        }

        static public void AddTouchDebugListener(OnTouchDebugHandle handle)
        {
            Instance.AddTouchDebug(handle);
        }

        static public void RemoveTouchDebugListener(OnTouchDebugHandle handle)
        {
            Instance.RemoveTouchDebug(handle);
        }
        #endregion
        #endregion

        #region 键盘按钮事件
        #region 私有方法
        private void AddKeyUpHandle(OnKeyHandle handle)
        {
            keyUpHandle += handle;
        }

        private void AddKeyHoldHandle(OnKeyHoldHandle handle)
        {
            keyHoldHandle += handle;
        }

        private void AddKeyDownHandle(OnKeyHandle handle)
        {
            keyDownHandle += handle;
        }

        private void RemoveKeyUpHandle(OnKeyHandle handle)
        {
            keyUpHandle -= handle;
        }

        private void RemoveKeyHoldHandle(OnKeyHoldHandle handle)
        {
            keyHoldHandle -= handle;
        }

        private void RemoveKeyDownHandle(OnKeyHandle handle)
        {
            keyDownHandle -= handle;
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 添加按键弹起事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddKeyUpListener(OnKeyHandle handle)
        {
            Instance.AddKeyUpHandle(handle);
        }
        /// <summary>
        /// 添加按键按住事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddKeyHoldListener(OnKeyHoldHandle handle)
        {
            Instance.AddKeyHoldHandle(handle);
        }
        /// <summary>
        /// 添加按键按下事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void AddKeyDownListener(OnKeyHandle handle)
        {
            Instance.AddKeyDownHandle(handle);
        }

        /// <summary>
        /// 移除按键弹起事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveKeyUpListener(OnKeyHandle handle)
        {
            Instance.RemoveKeyUpHandle(handle);
        }
        /// <summary>
        /// 移除按键按住事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveKeyHoldListener(OnKeyHoldHandle handle)
        {
            Instance.RemoveKeyHoldHandle(handle);
        }
        /// <summary>
        /// 移除按键按下事件监听
        /// </summary>
        /// <param name="handle"></param>
        static public void RemoveKeyDownListener(OnKeyHandle handle)
        {
            Instance.RemoveKeyDownHandle(handle);
        }
        #endregion
        #endregion

        #region 实际调用
        private void OnTouchBegin()
        {
            if (touchBeginHandle != null)
            {
                touchBeginHandle.Invoke(_mouseX, _mouseY);
            }
        }

        private void OnTouchHold()
        {
            if (touchHoldHandle != null)
            {
                touchHoldHandle.Invoke(_mouseX, _mouseY);
            }
        }

        private void OnTouchMove()
        {
            if (touchMoveHandle != null)
            {
                touchMoveHandle.Invoke(_mouseX, _mouseY);
            }
        }

        private void OnTouchEnd()
        {
            if (touchEndHandle != null)
            {
                touchEndHandle.Invoke(_mouseX, _mouseY);
            }
        }

        private void OnScale()
        {
            if (scaleHandle != null && scaleValue != 0)
            {
                scaleHandle.Invoke(scaleValue);
            }
        }

        private void OnKeyUp()
        {
            if (keyUpHandle != null)
            {
                keyUpHandle.Invoke(curKey);
            }
        }

        private void OnKeyDown()
        {
            if (keyDownHandle != null)
            {
                keyDownHandle.Invoke(curKey);
            }
        }

        private void OnKeyHold()
        {
            if (keyHoldHandle != null)
            {
                if (curKeys != null && curKeys.Count > 0)
                {
                    keyHoldHandle.Invoke(curKeys);
                }
            }
        }

        private void OnMulTouchBegin()
        {
            if (mulTouchBeginHandle != null)
            {
                if (mulDatas != null)
                {
                    mulTouchBeginHandle.Invoke(_mulTouchLen, mulDatas);
                }

            }
        }

        private void OnMulTouchHold()
        {
            if (mulTouchHoldHandle != null)
            {
                if (mulDatas != null)
                {
                    mulTouchHoldHandle.Invoke(_mulTouchLen, mulDatas);
                }

            }
        }

        private void OnMulTouchMove()
        {
            if (mulTouchMoveHandle != null)
            {
                if (mulDatas != null)
                {
                    mulTouchMoveHandle.Invoke(_mulTouchLen, mulDatas);
                }

            }
        }

        private void OnMulTouchEnd()
        {
            if (mulTouchEndHandle != null)
            {

                mulTouchEndHandle.Invoke();


            }
        }

        private void OnTouchDebug()
        {
            if (touchDebugHandle != null)
            {
                touchDebugHandle.Invoke(touchDebugStr);
            }
        }

        private void OnClick()
        {
            if (clickHandle != null)
            {
                clickHandle.Invoke(_mouseX, _mouseY);
            }
        }

        #endregion


    }
    #region 数据类
    public class MulTouchData
    {
        public int id;
        public float x;
        public float y;
    }
    #endregion
}