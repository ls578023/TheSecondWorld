using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class AudioManager
    {
        private static AudioManager _Instance = null;
        private AudioListener m_AudioListener = null;
        private GameObject m_AudioRoot = null;
        private const int randomMin = 1;
        private const int randomMax = 10000;

        private Dictionary<int, AudioCompositeBase> m_NodeDic = new Dictionary<int, AudioCompositeBase>();

        Dictionary<int, AudioData> dicAudioDatas;
        /// <summary>
        /// 安卓平台下 音频ID -- 文件路径
        /// </summary>
        private Dictionary<int, string> m_AndroidAudioDics = new Dictionary<int, string>();
        public static AudioManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new AudioManager();
                }
                return _Instance;
            }
        }
        /// <summary>
        /// 初始化音频管理器
        /// </summary>
        /// <param name="audioDatas"></param>
        public void InitAudioManager(List<AudioData> audioDatas) 
        {
            dicAudioDatas = new Dictionary<int, AudioData>();
            foreach (var item in audioDatas)
            {
                dicAudioDatas.Add(item.id, item);
            }
#if UNITY_ANDROID
            AndroidNativeAudio.makePool();
            for (int i = 0; i < audioDatas.Count; i++)
            {
                string resPath = audioDatas[i].ResPath;
                //只加载Audio/Once目录下的音效
                if (resPath.IndexOf("Audio/Once") != -1)
                {
                    string AudioFileName = resPath.Substring(resPath.LastIndexOf("/") + 1);
                    string path = /*Application.streamingAssetsPath + "/" +*/ AppSetting.PlatformName + "/" + AppSetting.MineGameName + "/" + "audio/once/" + AudioFileName;
                    CLog.Log("SDK加载音频路径：" + path);
                    DBTSDK.SDKToolManager.Instance.PreloadEffect(path);
                    //int fileId = AndroidNativeAudio.load(AppSetting.PlatformName + "/" + AppSetting.MineGameName + "/" + "audio/once/" + AudioFileName);
                    m_AndroidAudioDics[audioDatas[i].id] = path;
                }
            }
#endif
        }

        public string GetAndroidAudioID(int AudioId)
        {
            string path = "";
            if (m_AndroidAudioDics.ContainsKey(AudioId))
                path = m_AndroidAudioDics[AudioId];
            return path;
        }

        public AudioData GetAudioData(int id) 
        {
            AudioData audioData;
            dicAudioDatas.TryGetValue(id, out audioData);
            return audioData;
        }

        private void AddNode(int id, AudioCompositeBase audioCompositeBase)
        {
            if (audioCompositeBase != null && m_NodeDic != null)
                m_NodeDic[id] = audioCompositeBase;
        }

        private void RemoveNode(int id)
        {
            if (m_NodeDic != null && m_NodeDic.ContainsKey(id))
                m_NodeDic.Remove(id);
        }

        private void OnCreate()
        {
            CreateAudioRoot();
            MoveAudioListen(AudioMode.A2D);
        }

        private void CreateAudioRoot()
        {
            if (this.m_AudioRoot == null)
            {
                this.m_AudioRoot = Utils.NewGameObject(null, "AudioRoot");
                UnityEngine.Object.DontDestroyOnLoad(this.m_AudioRoot);
            }
        }

        private void DestroyAudioRoot()
        {
            if (this.m_AudioRoot != null)
                GlobalUnityEngineAPI.Destroy(this.m_AudioRoot);
            this.m_AudioRoot = null;
        }

        private void CreateListener()
        {
            if (this.m_AudioListener == null)
            {
                GameObject audioListenerObj = Utils.NewGameObject(null, "AudioListener");
                m_AudioListener = audioListenerObj.AddComponent<AudioListener>();
            }
        }

        private void DestroyListener()
        {
            if (this.m_AudioListener != null)
            {
                GlobalUnityEngineAPI.Destroy(m_AudioListener.gameObject);
                this.m_AudioListener = null;
            }
        }


        // 移动audio监听
        public void MoveAudioListen(AudioMode audioMode)
        {
            Camera uiCamera = GameFrameEntry.GetModule<UIModule>().UICamera;
            CreateListener();
            if (audioMode == AudioMode.A2D)
            {
                if (uiCamera != null)
                    m_AudioListener.transform.SetParent(uiCamera.transform);
            }
        }


        //创建一个音效复合节点
        public AudioCompositeBase CreateNode(AudioCompositeType stype, GameObject root = null)
        {
            //如果根节点为空则创建
            if (this.m_AudioRoot == null)
            {
                //this.Destroy();
                this.OnCreate();
            }
            AudioCompositeBase node = null;
            List<int> idList = null;
            if (this.m_NodeDic != null && this.m_NodeDic.Count > 0)
            {
                idList = new List<int>(this.m_NodeDic.Keys);
            }
            int node_id = AudioTools.RandomId(randomMin, randomMax, idList);
            if (root == null)
                root = this.m_AudioRoot;

            if (stype == AudioCompositeType.Parallel)
            {
                string name = "parallel_" + node_id;
                node = new AudioParallel(root, node_id, name);
            }
            else if (stype == AudioCompositeType.Sequence)
            {
                string name = "sequence_" + node_id;
                node = new AudioSequence(root, node_id, name);
            }
            else if (stype == AudioCompositeType.Single)
            {
                string name = "single_" + node_id;
                node = new AudioSingle(root, node_id, name);
            }
            this.AddNode(node_id, node);
            return node;
        }

        //删除一个音效复合节点
        public void DestroyNode(int id)
        {
            if (m_NodeDic != null && m_NodeDic.ContainsKey(id))
            {
                var node = m_NodeDic[id];
                if (node != null)
                    node.Stop();
                this.RemoveNode(id);
            }
        }

        //清空node
        public void ClearNode()
        {
            if (m_NodeDic != null && m_NodeDic != null)
            {
                var v = m_NodeDic.GetEnumerator();
                while (v.MoveNext())
                {
                    var node = v.Current.Value;
                    node.Stop();
                }
                m_NodeDic.Clear();
            }
            //清空音频资源集合
            dicAudioDatas?.Clear();
        }
        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="voiceType">音效类型</param>
        /// <param name="value"></param>
        public void SetMusicGameVolume(VoiceType voiceType, float value)
        {
            switch (voiceType)
            {
                case VoiceType.Music:
                    GlobalData.MusicGameVolume = value;
                    break;
                case VoiceType.Sound:
                    GlobalData.SoundGameVolume = value;
                    break;
            }
            MsgDispatcher.SendMessage(GlobalEventType.GlobalVolumeChange, voiceType, value);
        }
        /// <summary>
        /// 同时设置音乐和音效 声音
        /// </summary>
        /// <param name="value"></param>
        public void SetGameVolume(float value)
        {
            SetMusicGameVolume(VoiceType.Music, value);
            SetMusicGameVolume(VoiceType.Sound, value);
        }

        // 销毁所有音效，删除音效节点
        public void Destroy()
        {
#if UNITY_ANDROID
            DBTSDK.SDKToolManager.Instance.End();
            //foreach (var item in m_AndroidAudioDics.Values)
            //{
            //    AndroidNativeAudio.unload(item);
            //}

            //AndroidNativeAudio.releasePool();
            //m_AndroidAudioDics.Clear();
#endif
            this.ClearNode();
            this.DestroyAudioRoot();
            this.DestroyListener();
        }

    }
}
