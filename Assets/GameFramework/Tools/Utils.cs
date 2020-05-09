using AssetBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFramework
{
    public class Utils
    {
        public static void ResetShader(UnityEngine.Object obj)
        {
#if UNITY_EDITOR
            if (AssetBundleManager.SimulateAssetBundleInEditor)
                return;
            List<Material> listMat = new List<Material>();
            if (obj == null)
            {
                Renderer[] rends = Transform.FindObjectsOfType<Renderer>();
                if (null != rends)
                {
                    foreach (Renderer item in rends)
                    {
                        Material[] materialsArr = item.sharedMaterials;
                        foreach (Material m in materialsArr)
                            listMat.Add(m);
                    }
                }
            }
            else if (obj is Material)
            {
                Material m = obj as Material;
                listMat.Add(m);
            }
            else if (obj is GameObject)
            {
                GameObject go = obj as GameObject;
                Renderer[] rends = go.GetComponentsInChildren<Renderer>();
                if (null != rends)
                {
                    foreach (Renderer item in rends)
                    {
                        Material[] materialsArr = item.sharedMaterials;
                        foreach (Material m in materialsArr)
                            listMat.Add(m);
                    }
                }
            }
            for (int i = 0; i < listMat.Count; i++)
            {
                Material m = listMat[i];
                if (null == m)
                    continue;
                var shaderName = m.shader.name;
                var newShader = Shader.Find(shaderName);
                if (newShader != null)
                    m.shader = newShader;
            }
#endif
        }
        public static void LogForMat(string form, params string[] strs)
        {
            string strR = string.Format(form, strs);
            CLog.Log(strR);
        }

        private static List<string> luaPaths = new List<string>();
        public static int Int(object o)
        {
            return Convert.ToInt32(o);
        }

        public static float Float(object o)
        {
            return (float)Math.Round(Convert.ToSingle(o), 2);
        }

        public static long Long(object o)
        {
            return Convert.ToInt64(o);
        }

        public static int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static string Uid(string uid)
        {
            int position = uid.LastIndexOf('_');
            return uid.Remove(0, position + 1);
        }
        /// <summary>
        /// 查找子对象
        /// </summary>
        public static GameObject Child(Transform tf, string subnode)
        {
            Transform tran = tf.Find(subnode);
            if (tran == null)
                return null;
            return tran.gameObject;
        }

        public static GameObject NewGameObject(GameObject root, string name)
        {
            GameObject go = new GameObject();
            if (!string.IsNullOrEmpty(name))
                go.name = name;
            SetParent(root, go);
            return go;
        }

        /// <summary>
        /// 取平级对象
        /// </summary>
        public static GameObject Peer(GameObject go, string subnode)
        {
            return Peer(go.transform, subnode);
        }

        /// <summary>
        /// 取平级对象
        /// </summary>
        public static GameObject Peer(Transform go, string subnode)
        {
            Transform tran = go.parent.Find(subnode);
            if (tran == null)
                return null;
            return tran.gameObject;
        }
        /// <summary>
        /// 清除所有子节点
        /// </summary>
        public static void ClearChild(Transform go)
        {
            if (go == null) return;
            for (int i = go.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(go.GetChild(i).gameObject);
            }
        }

        public static GameObject AddChildByGUI(GameObject parent, GameObject prefab)
        {
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            if (go != null && parent != null)
            {
                SetParent(parent, go);
            }
            return go;
        }

        public static void SetParent(GameObject root, GameObject go, bool need_init = true)
        {
            if (root != null && go != null)
            {
                go.transform.SetParent(root.transform, false);
                if (need_init)
                {
                    Transform t = go.transform;
                    t.localPosition = UnityEngine.Vector3.zero;
                    t.localRotation = UnityEngine.Quaternion.identity;
                    t.localScale = UnityEngine.Vector3.one;
                    go.layer = root.layer;
                }
            }
        }

        public static T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            T comp = go.GetComponent<T>();
            if (comp == null)
            {
                Transform t = go.transform.parent;

                while (t != null && comp == null)
                {
                    comp = t.gameObject.GetComponent<T>();
                    t = t.parent;
                }
            }
            return comp;
        }

        public static int RandomId(int min, int max, List<int> ariseList)
        {
            int id = UnityEngine.Random.Range(min, max);
            if (ariseList != null)
            {
                for (int i = 0; i < ariseList.Count; i++)
                {
                    if (ariseList[i] == id)
                    {
                        RandomId(min, max, ariseList);
                    }
                }
            }
            return id;
        }

        public static bool IsClickOnUI()
        {
#if !UNITY_EDITOR
            if (Input.touches!=null && Input.touches.Length > 0)
            {
                if (ClickIsOverUI.Instance.IsPointerOverUIObject(Input.GetTouch(0).position))
                    return true;
                else
                    return false;
            }
#else
            if (EventSystem.current.IsPointerOverGameObject())
                return true;
#endif
            return false;
        }

    }
}