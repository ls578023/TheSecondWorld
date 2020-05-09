using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{

    public class UserDataPrefs
    {

        private static readonly Type fixationFloat = typeof(float);
        private static readonly Type fixationInt = typeof(int);
        private static readonly Type fixationStr = typeof(string);

        private static Dictionary<string, UserVariable> m_UserDataDic;

        private static int m_TickTimeId;

        private static bool IsInit;

        public static void OnInit()
        {
            if (IsInit)
            {
                CLog.Log("UserDataPrefs已经初始化");
                return;
            }
            CLog.Log("UserDataPrefs初始化");
            IsInit = true;
            m_UserDataDic = new Dictionary<string, UserVariable>();
            ReadData();
            //AddSaveListener();
        }

        public static void DeleteAll()
        {
            if (m_UserDataDic != null)
                m_UserDataDic.Clear();
        }

        public static void DeleteKey(string key)
        {
            if (m_UserDataDic != null && m_UserDataDic.ContainsKey(key))
            {
                m_UserDataDic.Remove(key);
            }
        }

        public static float GetFloat(string key, float defaultValue)
        {
            float value = defaultValue;
            object obj = GetVariable(key, fixationFloat);
            if (obj != null)
                value = (float)obj;
            return value;
        }

        public static float GetFloat(string key)
        {
            float value = GetFloat(key, 0);
            return value;
        }

        public static int GetInt(string key, int defaultValue)
        {
            int value = defaultValue;
            object obj = GetVariable(key, fixationInt);
            if (obj != null)
                value = (int)obj;
            return value;
        }

        public static int GetInt(string key)
        {
            int value = GetInt(key, 0);
            return value;
        }

        public static string GetString(string key, string defaultValue)
        {
            string value = defaultValue;
            object obj = GetVariable(key, fixationStr);
            if (obj != null)
                value = (string)obj;
            return value;
        }

        public static string GetString(string key)
        {
            string value = GetString(key, string.Empty);
            return value;
        }

        public static bool HasKey(string key)
        {
            bool hasKey = m_UserDataDic != null && m_UserDataDic.ContainsKey(key);
            return hasKey;
        }

        public static void SetFloat(string key, float value)
        {
            SetVariable(key, value);
        }

        public static void SetInt(string key, int value)
        {
            SetVariable(key, value);
        }

        public static void SetString(string key, string value)
        {
            SetVariable(key, value);
        }



        /// <summary>
        ///5分钟写一次文件，游戏退出时写一次文件
        /// </summary>
        public static void OnSave()
        {
            if (m_UserDataDic != null)
            {
                string savePath = GamePath.Instance.UserDataPath + GameConst.UserDataFileName;
                UserData userData = new UserData();
                userData.UserDataList = null;
                if (m_UserDataDic != null)
                    userData.UserDataList = new List<UserVariable>(m_UserDataDic.Values);
                string content = Tools.Json.ToJson(userData);
                Debug.Log("保存文件:" + content);
                Tools.FileManager.Instance.SaveFile(savePath, content, true);
            }
        }

        private static void ReadData()
        {
            string savePath = GamePath.Instance.UserDataPath + GameConst.UserDataFileName;
            bool isFileExists = Tools.FileManager.Instance.IsFileExists(savePath);
            UserData userData = null;
            if (isFileExists)
            {
                string content = Tools.FileManager.Instance.ReadFileText(savePath, true);
                userData = Tools.Json.ToObject<UserData>(content);
            }
            if (userData != null && userData.UserDataList != null && m_UserDataDic != null)
            {
                for (int i = 0; i < userData.UserDataList.Count; i++)
                {
                    UserVariable userVariable = userData.UserDataList[i];
                    if (userVariable != null && !string.IsNullOrEmpty(userVariable.Key))
                        m_UserDataDic[userVariable.Key] = userVariable;
                }
            }
        }

        private static object GetVariable(string key, Type varType)
        {
            object var = null;
            if (m_UserDataDic != null && m_UserDataDic.ContainsKey(key))
            {
                UserVariable userVar = m_UserDataDic[key];
                if (varType == typeof(float))
                {
                    var = userVar.FloatVar;
                }
                else if (varType == typeof(int))
                {
                    var = userVar.IntVar;
                }
                else if (varType == typeof(string))
                {
                    var = userVar.StrVar;
                }
            }
            return var;
        }

        private static void SetVariable(string key, object var)
        {
            if (m_UserDataDic != null && !string.IsNullOrEmpty(key) && var != null)
            {
                UserVariable userVar = null;
                if (m_UserDataDic.ContainsKey(key))
                {
                    userVar = m_UserDataDic[key];
                }
                if (var.GetType() == typeof(float))
                {
                    if (userVar == null)
                        userVar = UserVariable.BuildVariable(key, (float)var);
                    else
                        userVar.FloatVar = (float)var;
                }
                else if (var.GetType() == typeof(int))
                {
                    if (userVar == null)
                        userVar = UserVariable.BuildVariable(key, (int)var);
                    else
                        userVar.IntVar = (int)var;
                }
                else if (var.GetType() == typeof(string))
                {
                    if (userVar == null)
                        userVar = UserVariable.BuildVariable(key, (string)var);
                    else
                        userVar.StrVar = (string)var;
                }
                if (userVar != null)
                    m_UserDataDic[key] = userVar;
            }
        }

        private static void AddSaveListener()
        {
            if (m_TickTimeId >= 0)
            {
                GameFrameEntry.GetModule<TimeModule>().RemoveTime(m_TickTimeId);
            }
            m_TickTimeId = GameFrameEntry.GetModule<TimeModule>().SetEveryMinute(CheckUseByTime, 5);
        }

        private static void CheckUseByTime(int id, int time)
        {
            if (m_TickTimeId < 0)
            {
                return;
            }
            if (m_TickTimeId != id)
            {
                return;
            }
            OnSave();
        }
    }

}