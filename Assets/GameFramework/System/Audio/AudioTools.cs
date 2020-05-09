using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public static class AudioTools
    {

        //随机一个id<不重复>
        public static int RandomId(int min, int max, List<int> arise_lst)
        {
            return Utils.RandomId(min, max, arise_lst);
        }

        public static AudioSource CreateAudioSource(GameObject root, string name)
        {
            if (root != null)
            {
                AudioSource audioSource = null;
                var go = Utils.NewGameObject(root, name);
                audioSource = go.AddComponent<AudioSource>();
                return audioSource;
            }
            return null;
        }

        public static void DestroyAudioSource(AudioSource audioSource)
        {
            if (audioSource != null)
            {
                GlobalUnityEngineAPI.Destroy(audioSource.gameObject);
            }
        }

        public static async Task<AudioClip> LoadAudioClip(string resPath)
        {
            AudioClip ac = null;
            //ac =Resources.Load<AudioClip>(resPath);
            ac = await LoadHelper.LoadSound(resPath);
            return ac;
        }
    }
}
