using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 数据池 存储各个模块的数据管理对象
    /// </summary>
    public class DataManagerPool
    {
        /// <summary>
        /// 数据管理集合
        /// </summary>
        private Dictionary<Type, DataManagerBase> m_DataManagerDic;

        private static DataManagerPool m_Instance;

        public static DataManagerPool Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new DataManagerPool();
                return m_Instance;
            }
        }

        public void OnInit()
        {
            m_DataManagerDic = new Dictionary<Type, DataManagerBase>();
            RegisterModel();
            CLog.Log("初始化DataManagerPool完成");
        }
        private void RegisterModel()
        {

            Assembly assembly;
            assembly = Assembly.Load("Assembly-CSharp");
            Type[] types = assembly.GetExportedTypes();
            foreach (Type t in types)
            {
                ModelRegister modelRegister = t.GetCustomAttribute<ModelRegister>();
                if (modelRegister != null)
                {
                    if (modelRegister.GameName == AppSetting.MineGameName)
                    {
                        DataManagerBase mb = (DataManagerBase)Activator.CreateInstance(t);
                        mb.OnInit();
                        m_DataManagerDic[t] = mb;
                    }
                }

            }
        }
        /// <summary>
        /// 存储数据
        /// </summary>
        public void SaveData()
        {
            foreach (var item in m_DataManagerDic.Values)
            {
                item.OnSave();
            }
        }
        public T GetModel<T>() where T : DataManagerBase
        {
            T t = default(T);
            Type tp = typeof(T);
            if (m_DataManagerDic != null && m_DataManagerDic.ContainsKey(tp))
                t = m_DataManagerDic[tp] as T;
            else
            {
                CLog.Error($"未找到[{tp.FullName}]数据管理器");
            }
            return t;
        }
        /// <summary>
        /// 释放数据
        /// </summary>
        public  void OnRelease()
        {
            SaveData();
            foreach (var item in m_DataManagerDic.Values)
            {
                item.OnRelease();
            }
            m_DataManagerDic.Clear();
        }
    }
}
