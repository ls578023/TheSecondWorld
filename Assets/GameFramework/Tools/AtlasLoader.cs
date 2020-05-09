
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using GameFramework;

public class AtlasLoader : MonoBehaviour
{
    void OnEnable()
    {
        SpriteAtlasManager.atlasRequested += RequestAtlas;
    }

    void OnDisable()
    {
        SpriteAtlasManager.atlasRequested -= RequestAtlas;
    }

    async void RequestAtlas(string tag, System.Action<SpriteAtlas> callback)
    {
      
        SpriteAtlas objs = await GameFramework.GameFrameEntry.GetModule<AssetbundleModule>().LoadSpriteAtlas(tag);
        callback(objs);
        
    }   
}