using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading.Tasks;

namespace GameFramework
{
    public abstract class AudioActionBase
    { 
        /// <summary>
        /// updateId
        /// </summary>
        protected int s_UpdateId = 0;

        /// <summary>
        /// 音效id
        /// </summary>
        protected int s_AudioId = 0;

        /// <summary>
        /// 音效唯一id
        /// </summary>
        protected int s_AudioGuid = 0;

        /// <summary>
        /// 音量
        /// </summary>
        protected float s_Vol = 1;

        protected VoiceType s_VoiceType;

        /// <summary>
        /// 挂载节点
        /// </summary>
        protected GameObject s_AudioRoot = null;

        /// <summary>
        /// 音效资源
        /// </summary>
        protected AudioSource s_audioSource = null;

        /// <summary>
        /// 开始播放回调
        /// </summary>
        protected Action<int> s_StartCall = null;

        /// <summary>
        /// 结束播放回调
        /// </summary>
        protected Action<int> s_EndCall = null;

        public VoiceType S_VoiceType { get => s_VoiceType;  }

        protected AudioActionBase(GameObject root, int audioId, Action<int> startCall, Action<int> endCall)
        {
            this.s_AudioRoot = root;
            this.s_AudioId = audioId;
            this.s_AudioGuid = GetHashCode();
            this.s_StartCall = startCall;
            this.s_EndCall = endCall;
            this.OnInit();
        }

        private void OnInit()
        {
            this.OnAwake();
            s_UpdateId = UpdateManager.AddHandle(Update);
            AddVolumeListener();
        }

        private void AddVolumeListener()
        {
            RemoveVolumeListener();
            MsgDispatcher.AddEventListener(GlobalEventType.GlobalVolumeChange, RefreshVolume);
        }

        private void RemoveVolumeListener()
        {
            MsgDispatcher.RemoveEventListener(GlobalEventType.GlobalVolumeChange, RefreshVolume);
        }

        private void RefreshVolume(params object[] parms)
        {
            if (parms != null && parms.Length > 1)
            {
                VoiceType voiceType = (VoiceType)parms[0];
                float v = (float)parms[1];
                RefreshAudioVolume(voiceType, v);
            }
        }

        protected void OnStartCall()
        {
            if (this.s_StartCall != null)
                this.s_StartCall(this.s_AudioGuid);
        }

        protected void OnEndCall()
        {
            if (this.s_EndCall != null)
                this.s_EndCall(this.s_AudioGuid);
        }

        protected float GetSettingVolume()
        {
            float vol = 1;
            if (VoiceType.Music == s_VoiceType)
            {
                vol = GlobalData.MusicGameVolume;
            }
            else if (VoiceType.Sound == s_VoiceType)
            {
                vol = GlobalData.SoundGameVolume;
            }
            return vol;
        }

        private AudioData GetAudioConfig(int id)
        {
            return AudioManager.Instance.GetAudioData(id);
        }

        private void Update()
        {
            this.OnUpdate();
        }

        public async void Play()
        {
            var audio_id = this.s_AudioId;
            var root = this.s_AudioRoot;
            var audioConfig = GetAudioConfig(audio_id);
            if (audioConfig != null && root != null)
            {
                this.s_Vol = (float)(audioConfig.Vol <= 0 ? 1 : audioConfig.Vol);
                this.s_VoiceType = audioConfig.VoiceType;
                this.s_audioSource = await this.OnPlay(audioConfig);
                if (this.s_audioSource != null)
                    OnStartCall();
            }
        }

        public void Pause(bool isPause)
        {
            if (this.s_audioSource != null)
            {
                if (isPause)
                    this.s_audioSource.Pause();
                else
                    this.s_audioSource.UnPause();
                this.OnPause(isPause);
            }
        }

        public bool IsPlaying()
        {
            bool isPlaying = false;
            if (this.s_audioSource != null)
            {
                isPlaying = this.s_audioSource.isPlaying;
            }
            return isPlaying;
        }

        public void Mute(bool isMute)
        {
            if (this.s_audioSource != null)
            {
                this.s_audioSource.mute = isMute;
            }
        }

        public void Stop()
        {
            this.OnStop();
        }

        public void Dispose()
        {
            if (s_UpdateId != 0)
            {
                UpdateManager.RemoveHandleById(this.s_UpdateId);
                s_UpdateId = 0;
            }
            RemoveVolumeListener();
            this.OnDispose();
        }

        public int GetAudioId()
        {
            return s_AudioId;
        }

        public int GetAudioGuid()
        {
            return s_AudioGuid;
        }

        public AudioSource GetAudioSource()
        {
            return s_audioSource;
        }

        protected virtual void RefreshAudioVolume(VoiceType voiceType, float vol)
        {
            if (this.s_VoiceType == voiceType)
            {
                if (s_audioSource != null)
                    s_audioSource.volume = vol * s_Vol;
            }
        }

        // 以下方法由子类重写
        protected abstract void OnAwake();

        protected abstract Task<AudioSource> OnPlay(AudioData audioConfig);

        protected abstract void OnPause(bool isPause);

        protected abstract void OnStop();

        protected abstract void OnUpdate();

        protected abstract void OnDispose();
    }
}