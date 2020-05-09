using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework
{
    public class UIAudioManager
    {
        private static UIAudioManager _Instance = null;
        private AudioCompositeBase m_ParallelNode = null;
        private AudioCompositeBase m_SequenceNode = null;
        private AudioCompositeBase m_SingleNode = null;
        private bool m_IsInit = false;
        private Dictionary<AudioCompositeType, Action<int[]>> m_NodeDic = new Dictionary<AudioCompositeType, Action<int[]>>();


        public static UIAudioManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UIAudioManager();
                }
                return _Instance;
            }
        }



        private void OnInit()
        {
            this.RegisterNodeDic();
            this.m_IsInit = true;
        }

        private void RegisterNodeDic()
        {
            m_NodeDic.Clear();
            m_NodeDic.Add(AudioCompositeType.Parallel, PlayParallel);
            m_NodeDic.Add(AudioCompositeType.Sequence, PlaySequence);
            m_NodeDic.Add(AudioCompositeType.Single, PlaySingle);
        }

        public void Play(AudioCompositeType ptype, params int[] audioId)
        {
            if (!m_IsInit)
                this.OnInit();
            if (m_NodeDic != null)
            {
                Action<int[]> func = null;
                m_NodeDic.TryGetValue(ptype, out func);
                if (func != null)
                    func(audioId);
            }
        }

        public void PauseAll(bool isPause)
        {
            if (this.m_ParallelNode != null)
            {
                this.m_ParallelNode.Pause(isPause);
            }
            if (this.m_SequenceNode != null)
            {
                this.m_SequenceNode.Pause(isPause);
            }
            if (this.m_SingleNode != null)
            {
                this.m_SingleNode.Pause(isPause);
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
            if (this.m_SequenceNode != null)
            {
                this.m_SequenceNode.Stop();
                this.m_SequenceNode = null;
            }
            if (this.m_SingleNode != null)
            {
                this.m_SingleNode.Stop();
                this.m_SingleNode = null;
            }
        }
        /// <summary>
        /// 停止Parallel类型的音效 0-停止所有 1-停止音乐（背景） 2-停止音效
        /// </summary>
        /// <param name="StopType"></param>
        public void StopParallelNode(int StopType)
        {
            if (this.m_ParallelNode != null)
            {
                this.m_ParallelNode.Stop(StopType);
            }
        }

        public void StopSingleNode(bool state)
        {
            if (this.m_SingleNode != null)
            {
                if (state) this.m_SingleNode.Stop();
                else m_NodeDic[AudioCompositeType.Single] = PlaySingle;
            }
        }

        private void PlayParallel(params int[] audioId)
        {
            if (this.m_ParallelNode == null)
            {
                var node = AudioManager.Instance.CreateNode(AudioCompositeType.Parallel);
                this.m_ParallelNode = node;
            }
            if (this.m_ParallelNode != null)
                this.m_ParallelNode.Play(audioId);
        }

        private void PlaySequence(params int[] audioId)
        {
            if (this.m_SequenceNode == null)
            {
                var node = AudioManager.Instance.CreateNode(AudioCompositeType.Sequence);
                this.m_SequenceNode = node;
            }
            if (this.m_SequenceNode != null)
                this.m_SequenceNode.Play(audioId);
        }

        private void PlaySingle(params int[] audioId)
        {
            if (this.m_SingleNode == null)
            {
                var node = AudioManager.Instance.CreateNode(AudioCompositeType.Single);
                this.m_SingleNode = node;
            }
            if (this.m_SingleNode != null)
                this.m_SingleNode.Play(audioId);
        }

        private void DestroyNode()
        {
            if (this.m_ParallelNode != null)
            {
                int nodeId = this.m_ParallelNode.GetNodeId();
                AudioManager.Instance.DestroyNode(nodeId);
            }

            if (this.m_SequenceNode != null)
            {
                int nodeId = this.m_SequenceNode.GetNodeId();
                AudioManager.Instance.DestroyNode(nodeId);
            }

            if (this.m_SingleNode != null)
            {
                int nodeId = this.m_SingleNode.GetNodeId();
                AudioManager.Instance.DestroyNode(nodeId);
            }
        }

        public void Destroy()
        {
            this.DestroyNode();
        }
    }
}