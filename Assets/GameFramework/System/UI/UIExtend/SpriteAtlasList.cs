using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.U2D;
using Unity.Collections;

namespace GameFramework
{

    /// <summary>
    /// UI使用的图集信息
    /// </summary>
    public class SpriteAtlasList : MonoBehaviour
    {
        [ReadOnly] [SerializeField] public SpriteAtlas[] AtlasList;
    }
}
