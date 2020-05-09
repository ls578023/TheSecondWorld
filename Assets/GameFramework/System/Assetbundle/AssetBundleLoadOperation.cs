﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace AssetBundles
{
    public abstract class AssetBundleLoadOperation : IEnumerator
    {
        public object Current
        {
            get
            {
                return null;
            }
        }
        public bool MoveNext()
        {
            return !IsDone();
        }

        public void Reset()
        {
        }

        abstract public bool Update();

        abstract public bool IsDone();
        //cjh add 
        abstract public float Progress();
    }

    public abstract class AssetBundleLoadAssetOperation : AssetBundleLoadOperation
    {
        public abstract T GetAsset<T>() where T : UnityEngine.Object;
    }

    public abstract class AssetBundleLoadBundleOperation : AssetBundleLoadOperation
    {
        public abstract LoadedAssetBundle GetAssetBundle();
    }


    #region LoadLevel
#if UNITY_EDITOR
    public class AssetBundleLoadLevelSimulationOperation : AssetBundleLoadOperation
    {
        AsyncOperation m_Operation = null;


        public AssetBundleLoadLevelSimulationOperation(string assetBundleName, string levelName, bool isAdditive)
        {
            string[] levelPaths = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetBundleName, levelName);
            if (levelPaths.Length == 0)
            {
                ///@TODO: The error needs to differentiate that an asset bundle name doesn't exist
                //        from that there right scene does not exist in the asset bundle...

                Debug.LogError("There is no scene with name \"" + levelName + "\" in " + assetBundleName);
                return;
            }
            //为了解决加载不能完全显示等效问题 改用
            //LoadSceneParameters sceneParameters = new LoadSceneParameters();
            //sceneParameters.loadSceneMode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            try
            {
                m_Operation = SceneManager.LoadSceneAsync(levelPaths[0], isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            }
            catch (System.Exception e)
            {
                Debug.LogError("请先添加场景");
            }
            //UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(levelPaths[0], sceneParameters);
            //if (isAdditive)
            //    m_Operation = UnityEditor.EditorApplication.LoadLevelAdditiveAsyncInPlayMode(levelPaths[0]);
            //else
            //    m_Operation = UnityEditor.EditorApplication.LoadLevelAsyncInPlayMode(levelPaths[0]);
        }

        public override bool Update()
        {
            return false;
        }

        public override bool IsDone()
        {
            return m_Operation == null && m_Operation.isDone&& m_Operation.allowSceneActivation;
        }

        public override float Progress()
        {
            return m_Operation == null ? 0 : m_Operation.progress;
        }
    }
#endif


    public class AssetBundleLoadLevelOperation : AssetBundleLoadOperation
    {
        protected string m_AssetBundleName;
        protected string m_LevelName;
        protected bool m_IsAdditive;
        protected string m_DownloadingError;
        protected AsyncOperation m_Request;

        public AssetBundleLoadLevelOperation(string assetbundleName, string levelName, bool isAdditive)
        {
            m_AssetBundleName = assetbundleName;
            m_LevelName = levelName;
            m_IsAdditive = isAdditive;
        }

        public override bool Update()
        {
            if (m_Request != null)
                return false;

            LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle(m_AssetBundleName, out m_DownloadingError);
            if (bundle != null)
            {
                //if (m_IsAdditive)                   
                //    m_Request = Application.LoadLevelAdditiveAsync (m_LevelName);
                //else
                //    m_Request = Application.LoadLevelAsync(m_LevelName);
                //cjh edit
                m_Request = SceneManager.LoadSceneAsync(m_LevelName, m_IsAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
                return false;
            }
            else
                return true;
        }

        public override bool IsDone()
        {
            // Return if meeting downloading error.
            // m_DownloadingError might come from the dependency downloading.
            if (m_Request == null && m_DownloadingError != null)
            {
                Debug.LogError(m_DownloadingError);
                return true;
            }
            return m_Request != null && m_Request.isDone&&m_Request.allowSceneActivation;
        }

        public override float Progress()
        {
            return m_Request == null ? 0 : m_Request.progress;
        }
    }
   
    #endregion


    #region LoadAsset
    public class AssetBundleLoadAssetOperationSimulation : AssetBundleLoadAssetOperation
    {
        Object m_SimulatedObject;

        public AssetBundleLoadAssetOperationSimulation(Object simulatedObject)
        {
            m_SimulatedObject = simulatedObject;
        }

        public override T GetAsset<T>()
        {
            return m_SimulatedObject as T;
        }

        public override bool Update()
        {
            return false;
        }

        public override bool IsDone()
        {
            return true;
        }
        public override float Progress()
        {
            return 1;
        }
    }
    public class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation
    {
        protected string m_AssetBundleName;
        protected string m_AssetName;
        protected string m_DownloadingError;
        protected System.Type m_Type;
        protected AssetBundleRequest m_Request = null;

        public AssetBundleLoadAssetOperationFull(string bundleName, string assetName, System.Type type)
        {
            m_AssetBundleName = bundleName;
            m_AssetName = assetName;
            m_Type = type;
        }

        public override T GetAsset<T>()
        {
            if (m_Request != null && m_Request.isDone)
                return m_Request.asset as T;
            else
                return null;
        }

        // Returns true if more Update calls are required.
        public override bool Update()
        {
            if (m_Request != null)
                return false;

            LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle(m_AssetBundleName, out m_DownloadingError);
            if (bundle != null)
            {
                ///@TODO: When asset bundle download fails this throws an exception...
                m_Request = bundle.m_AssetBundle.LoadAssetAsync(m_AssetName, m_Type);
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool IsDone()
        {
            // Return if meeting downloading error.
            // m_DownloadingError might come from the dependency downloading.
            if (m_Request == null && m_DownloadingError != null)
            {
                Debug.LogError(m_DownloadingError);
                return true;
            }

            return m_Request != null && m_Request.isDone;
        }

        public override float Progress()
        {
            return m_Request == null ? 0 : m_Request.progress;
        }
    }

    public class AssetBundleLoadManifestOperation : AssetBundleLoadAssetOperationFull
    {
        public AssetBundleLoadManifestOperation(string bundleName, string assetName, System.Type type)
            : base(bundleName, assetName, type)
        {
        }

        public override bool Update()
        {
            base.Update();

            if (m_Request != null && m_Request.isDone)
            {
                AssetBundleManager.AssetBundleManifestObject = GetAsset<AssetBundleManifest>();
                return false;
            }
            else
                return true;
        }
    }
    #endregion

    #region LoadAssetBundle
    public class AssetBundleLoadOperationSimulation : AssetBundleLoadBundleOperation
    {
        Object m_SimulatedObject;

        public AssetBundleLoadOperationSimulation(Object simulatedObject)
        {
            m_SimulatedObject = simulatedObject;
        }

        public override LoadedAssetBundle GetAssetBundle()
        {
            return null;
        }

        public override bool Update()
        {
            return false;
        }

        public override bool IsDone()
        {
            return true;
        }
        public override float Progress()
        {
            return 1;
        }
    }
    public class AssetBundleLoadOperationFull : AssetBundleLoadBundleOperation
    {
        protected string m_AssetBundleName;
        protected string m_DownloadingError;
        protected WWW www;
        protected LoadedAssetBundle bundle;
        public AssetBundleLoadOperationFull(string bundleName)
        {
            m_AssetBundleName = bundleName;
            www = AssetBundleManager.GetAssetBundleWWW(bundleName);
        }

        public override LoadedAssetBundle GetAssetBundle()
        {
            return bundle;
        }

        // Returns true if more Update calls are required.
        public override bool Update()
        {           
            bundle = AssetBundleManager.GetLoadedAssetBundle(m_AssetBundleName, out m_DownloadingError);
            if (bundle != null)
                return false;
            else
                return true;
        }

        public override bool IsDone()
        {
            // Return if meeting downloading error.
            // m_DownloadingError might come from the dependency downloading.
            if (bundle != null)
                return true;

            if (www != null && m_DownloadingError != null)
            {
                Debug.LogError(m_DownloadingError);
                return true;
            }
            return www != null && www.isDone;
        }

        public override float Progress()
        {
            if (bundle != null)
                return 1;
            return www == null ? 0 : www.progress;
        }
    }
    #endregion

}
