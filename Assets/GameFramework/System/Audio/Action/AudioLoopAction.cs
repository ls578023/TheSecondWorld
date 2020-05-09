using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class AudioLoopAction : AudioActionBase
    {
        public AudioLoopAction(GameObject root, int audioId, Action<int> startCall, Action<int> endCall) : base(root, audioId, startCall, endCall)
        {
        }

        protected override void OnAwake()
        {
        }

        protected override void OnDispose()
        {
            AudioTools.DestroyAudioSource(base.s_audioSource);
            base.s_audioSource = null;
        }

        protected override void OnPause(bool isPause)
        {
        }

        protected override async Task<AudioSource> OnPlay(AudioData audioConfig)
        {
            if (audioConfig != null)
            {
                var path = audioConfig.ResPath;
                var start_time = audioConfig.StartTime;
                var end_time = audioConfig.EndTime;
                var atype = (AudioMode)audioConfig.Atype;
                var vol = audioConfig.Vol <= 0 ? 1 : audioConfig.Vol;
                var vol_mul = GetSettingVolume();
                var name = "loop_" + this.GetAudioGuid();

                AudioClip clip = await AudioTools.LoadAudioClip(path);
                if (clip != null)
                {
                    AudioSource source = AudioTools.CreateAudioSource(base.s_AudioRoot, name);
                    source.playOnAwake = false;
                    source.spatialBlend = atype == AudioMode.A2D ? 0 : 1;
                    if (atype == AudioMode.A3D)
                    {
                        //source.minDistance = audioConfig.MinDistance;
                        source.dopplerLevel = 0;
                    }
                    source.clip = clip;
                    source.volume = vol * vol_mul;
                    source.time = start_time;
                    source.loop = true;
                    source.Play();
                    return source;
                }
            }
            else
            {
                base.Stop();
            }
            return null;
        }

        protected override void OnStop()
        {
            if (this.s_audioSource != null)
            {
                this.s_audioSource.Stop();
            }
            base.OnEndCall();
            base.Dispose();
        }

        protected override void OnUpdate()
        {
        }
    }
}