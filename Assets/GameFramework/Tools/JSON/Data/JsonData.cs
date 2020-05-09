using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFramework.Tools
{
    public class JsonData
    {
        private string _type = "";

        public string Type
        {
            get { 
                return _type; 
            }

        }
        private int _count = 0;

        public int Count
        {
            get { return _count; }
        }
        private object content;
        private Dictionary<object, JsonData> contentDict;
        private List<string> keys = new List<string>();

        public JsonData()
        {
            contentDict = new Dictionary<object, JsonData>();
        }

        public void SetType(string t)
        {
            _type = t;
        }

        public void SetValue(object obj)
        {
            content = obj;
            _type = GetTypeByObj(obj);
        }

        public void SetValue(JsonData[] objs)
        {

            _type = DataType.ARRAY;
            _count = 0;
            if(objs!=null&&objs.Length>0)
            {
                _count = objs.Length;
                for(int i = 0;i<objs.Length;i++)
                {
                    //contents.Add(objs[i]);
                    SetValue(i, objs[i]);
                }
            }            
        }

        public void SetValue(List<JsonData> objs)
        {
            _count = 0;
            _type = DataType.ARRAY;
            if(objs!=null&&objs.Count>0)
            {
                _count = objs.Count;
                for (int i = 0; i < objs.Count; i++)
                {
                    SetValue(i,objs[i]);
                }
            }


        }

        public void SetValue(object key,JsonData value)
        {
            if(contentDict.ContainsKey(key))
            {
                contentDict[key] = value;
            }
            else
            {
                contentDict.Add(key, value);
            }
            string ks = key.ToString();
            if(keys.IndexOf(ks)<0)
            {
                keys.Add(ks);
            }
        }



        /// <summary>
        /// 判断传入内容的类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string GetTypeByObj(object obj)
        {
            Type type = obj.GetType();

            return JsonTools.GetDataType(type);
        }

        /// <summary>
        /// 取某个key的值，返回jsonData
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonData Get(object id)
        {
            if(contentDict.ContainsKey(id))
            {
                return contentDict[id];
            }
            else
            {
                return null;
            }
        }

        public JsonData this[object id]
        {
            get{
                if (contentDict.ContainsKey(id))
                {
                    return contentDict[id];
                }
                else
                {
                    return null;
                }
            }
            set
            {

                if (contentDict.ContainsKey(id))
                {
                    contentDict[id] = value;
                }
                else
                {
                    contentDict.Add(id, value);
                }
            }   
        }

        /// <summary>
        /// 设置Jsondata某个key的值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        private void Set(object id,JsonData obj)
        {
            if(contentDict.ContainsKey(id))
            {
                contentDict[id] = obj;
            }
            else
            {
                contentDict.Add(id, obj);
            }
        }

        public object ToValue()
        {
            if(_type == DataType.ARRAY)
            {
                return GetList();
            }
            else if(_type == DataType.BOOL)
            {
                return ToBool();
            }
            else if(_type == DataType.NUMBER)
            {
                return ToFloat();
            }
            else if(_type == DataType.STRING)
            {
                return ToString();
            }
            else
            {
                return ToString();
            }
        }

        /// <summary>
        /// 把值转成string字符串
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            if(content!=null)
            {
                return content.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 把值转成整型
        /// </summary>
        /// <returns></returns>
        public int ToInt()
        {
            if (content != null)
            {
                return int.Parse(content.ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 把值转成浮点数值
        /// </summary>
        /// <returns></returns>
        public float ToFloat()
        {
            if (content != null)
            {
                return float.Parse(content.ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 把值转成布尔
        /// </summary>
        /// <returns></returns>
        public bool ToBool()
        {
            if (content != null)
            {
                string b = content.ToString().ToLower();
                if(b=="true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取该JsonData的所有key
        /// </summary>
        /// <returns></returns>
        public List<string> GetKeys()
        {
            if(_type!=DataType.OBJECT)
            {
                return null;
            }
            return keys;
        }

        public Dictionary<object,JsonData> GetObjDict()
        {
            if(contentDict== null)
            {
                return null;
            }
            else
            {
                return contentDict;
            }
        }

        /// <summary>
        /// 获取该JsonData的数组值
        /// </summary>
        /// <returns></returns>
        public List<JsonData> GetList()
        {
            if(_type!=DataType.ARRAY)
            {
                return null;
            }
            List<JsonData> list = new List<JsonData>();
            if(content!=null)
            {
                Array arr = content as Array;
                if(arr!=null)
                {
                    foreach(System.Object ob in arr)
                    {
                        JsonData js = new JsonData();
                        js.SetValue(ob);
                        list.Add(js);
                    }
                    return list;
                }
            }
            for(int i = 0;i<_count;i++)
            {
                if(contentDict.ContainsKey(i))
                {
                    list.Add(contentDict[i]);
                }      
            }
            return list;
        }


    }
}
