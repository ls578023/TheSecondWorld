using System;
using System.Collections.Generic;

namespace GameFramework
{
    public class SceneAudioManager
    {
        private static SceneAudioManager _Instance = null;
        private AudioCompositeBase m_ParallelNode = null;
        private bool m_IsInit = false;
        private Dictionary<AudioCompositeType, Action<UnityEngine.GameObject, int[]>> m_NodeDic = new Dictionary<AudioCompositeType, Action<UnityEngine.GameObject, int[]>>();

        public static SceneAudioManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SceneAudioManager();
                }
                return _Instance;
            }
        }

        private void OnInit()
        {
            this.RegisterNodeDic();
            AudioManager.Instance.MoveAudioListen(AudioMode.A3D);
            this.m_IsInit = true;
        }

        private void RegisterNodeDic()
        {
            m_NodeDic.Clear();
            m_NodeDic.Add(AudioCompositeType.Parallel, PlayParallel);
        }

        public void Play(AudioCompositeType ptype, UnityEngine.GameObject root, params int[] audioId)
        {
            if (!m_IsInit)
                this.OnInit();
            if (m_NodeDic != null)
            {
                Action<UnityEngine.GameObject, int[]> func = null;
                m_NodeDic.TryGetValue(ptype, out func);
                if (func != null)
                    func(root, audioId);
            }
        }

        public void PauseAll(bool isPause)
        {
            if (this.m_ParallelNode != null)
            {
                this.m_ParallelNode.Pause(isPause);
            }
        }

        public void PauseParallerNode(bool isPause)
        {
            if (this.m_ParallelNode != null)
            {
                this.m_ParallelNode.Pause(isPause);
            }
        }

        public void StopAll()
        {
            if (this.m_ParallelNode != null)
            {
                this.m_ParallelNode.Stop();
                this.m_ParallelNode = null;
            }
        }

        private void PlayParallel(UnityEngine.GameObject root, params int[] audioId)
        {
            if (this.m_ParallelNode == null)
            {
                var node = AudioManager.Instance.CreateNode(AudioCompositeType.Parallel);
                this.m_ParallelNode = node;
            }
            if (this.m_ParallelNode != null)
                this.m_ParallelNode.Play(root, audioId);
        }

        private void DestroyNode()
        {
            if (this.m_ParallelNode != null)
            {
                int nodeId = this.m_ParallelNode.GetNodeId();
                AudioManager.Instance.DestroyNode(nodeId);
            }
        }

        public void Destroy()
        {
            this.DestroyNode();
        }
    }
}