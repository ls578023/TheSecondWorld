using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{


    [AttributeUsage(AttributeTargets.Class)]
    public class ModelRegister : Attribute
    {
        private string gameName;
        public ModelRegister(string GameName)
        {
            this.gameName = GameName;
        }

        public string GameName { get => gameName; }
    }

    /// <summary>
    /// 数据管理父类
    /// </summary>
    public abstract class DataManagerBase
    {
        public abstract void OnInit();
        public abstract void OnSave();
        public abstract void OnRelease();

        protected virtual void SaveLocalData(string str)
        {
            string savePath = GamePath.Instance.MineGameDataPath + this.GetType().Name+".txt";
#if UNITY_EDITOR
            Tools.FileManager.Instance.SaveFile(savePath, str, false);
#else
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);
            byte[] newBs = Tools.XXTEAHelper.Encrypt(byteArray, GameConst.encryptKey);
            Tools.FileManager.Instance.SaveBytes(savePath, newBs);
#endif
        }
        protected virtual string ReadLocalData()
        {
            string savePath = GamePath.Instance.MineGameDataPath + this.GetType().Name + ".txt";
            bool isFileExists = Tools.FileManager.Instance.IsFileExists(savePath);
            string content = "";
            if (isFileExists)
            {
#if UNITY_EDITOR
                content = Tools.FileManager.Instance.ReadFileText(savePath, false);
#else
                byte[] byteArray = Tools.FileManager.Instance.ReadFileBytes(savePath);
                byte[] newBs = Tools.XXTEAHelper.Decrypt(byteArray, GameConst.encryptKey);
                content = System.Text.Encoding.Default.GetString(newBs);
#endif
            }
            return content;
        }
    }
}
 
