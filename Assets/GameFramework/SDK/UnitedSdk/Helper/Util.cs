using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBTSDK
{
    public class Util 
    {
        public static Dictionary<string, object> DataParse(string data)
        {
            Debug.Log("get callback data: " + data);
            Dictionary<string, object> dicDic = new Dictionary<string, object>();
#if UNITY_IOS
            dicDic = VXJSON.Json.Deserialize(data) as Dictionary<string, object>;
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            dicDic = DCGJSON.Json.Deserialize(data) as Dictionary<string, object>;
#endif
            return dicDic;
        }
    }
}
