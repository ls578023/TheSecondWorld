
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;


namespace GameFramework.Tools
{

    public class Json
    {
        static private Json _instance;

        public static Json Instance
        {
            get { 
                if(_instance==null)
                {
                    _instance = new Json();
                }
                return Json._instance; 
            }

        }
        public Json()
        {

        }

        /// <summary>
        /// 把对象转成字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public string ToJson(object obj)
        {
            return Instance.GetField(obj);
        }
        /// <summary>
        /// 把对象转成字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public string ToJson(JsonData obj)
        {
            return Instance.GetFieldByJd(obj);
        }

        /// <summary>
        /// 把字符串转成对象
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public JsonData ToObject(string str)
        {
            Instance.InitData();
            str = TrimStr(str);
            //return Instance.GetJsonData(str);
            try
            {
                JsonData jd = Instance.CreateJsonData(str);
                return jd;
            }
            catch(Exception e)
            {
                return null;
            }

        }

        static private string TrimStr(string str)
        {
            str = str.Replace("\r ", "");
            str = str.Replace("\n ", "");
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");
            return str;
        }

        /// <summary>
        /// 根据类型把字符串转成类的实例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        static public T ToObject<T>(string str) where T : class
        {
            JsonData jd = ToObject(str);
            if(jd!=null)
            {
                Type type = typeof(T);
                return (T)GetT(type, jd);
            }
            else
            {
                return null;
            }

        }

        static public object GetT(Type type, JsonData jd)
        {
                string realType = JsonTools.GetDataType(type);
                if(realType==DataType.OBJECT)
                {
                    return GetObjT(type, jd);
                }
                else if(realType==DataType.ARRAY)
                {
                    if (type.IsGenericType)
                    {
                        Type listType = type.GetMethod("Find").ReturnType;
                        object orgList = Activator.CreateInstance(type);
                        
                        for(int j = 0;j<jd.Count;j++)
                        {
                            object lo = GetT(listType, jd.Get(j));
                            if(lo!=null)
                            {
                                MethodInfo m = orgList.GetType().GetMethod("Add");
                                m.Invoke(orgList,new object[]{lo});
                            }
                        }

                        return orgList;

                    }
                    else if (type.BaseType == typeof(Array))
                    {
                        MethodInfo[] ms = type.GetMethods();
                        Type listType = type.GetElementType();
                        object orgList = Array.CreateInstance(listType,jd.Count);
                        MethodInfo sv = type.GetMethod("SetValue", new Type[2] { typeof(object), typeof(int) });
                        for(int j = 0;j<jd.Count;j++)
                        {
                            object lo = GetT(listType, jd.Get(j));
                            if (lo != null)
                            {
                                sv.Invoke(orgList, new object[] { lo, j });
                            }
                        }

                        return orgList;
                    }
                }
                else if(realType==DataType.BOOL)
                {
                    bool bv = jd.ToString().ToLower() == "true" ? true : false;
                    return bv;
                }
                else if(realType == DataType.STRING)
                {
                    return jd.ToString();
                }
                else if(realType == DataType.NUMBER)
                {
                    string sv = jd.ToString();
                    if(type==typeof(int))
                    {
                        return int.Parse(sv);
                    }
                    else if (type == typeof(float))
                    {
                        double dsv = Convert.ToDouble(sv);
                        float fsv = Convert.ToSingle(dsv);
                        return fsv;
                    }
                    else if (type == typeof(double))
                    {
                        return double.Parse(sv);
                    }
                    else if (type == typeof(Single))
                    {
                       return Single.Parse(sv);
                    }
                    else if (type == typeof(long))
                    {
                       return  long.Parse(sv);
                    }
                }
                else if (realType == DataType.DICTIONARY)
                {
                    object orgList = Activator.CreateInstance(type);
                    string str = type.FullName;
                    string regex = @"(?<=Dictionary`2\[).+(?=\])";
                    string match = StringTools.GetFirstMatch(str, regex);
                    regex = @"\[[^\[\]]*(((?'Open'\[)[^\[\]]*)+((?'-Open'\])[^\[\]]*)+)*(?(Open)(?!))\]";
                    string[] matchs = StringTools.GetAllMatchs(match, regex);
                    List<Type> keyTypes = new List<Type>();
                    for (int j = 0; j < matchs.Length; j++)
                    {
                        if (matchs[j].IndexOf("Dictionary") < 0)
                        {
                            regex = @"(?<=\[)[^,]+(?=,)";
                            match = StringTools.GetFirstMatch(matchs[j], regex);
                            keyTypes.Add(Type.GetType(match, true, true));
                        }
                        else
                        {
                            regex = @"(?<=Dictionary`2\[).+(?=\])";
                            match = StringTools.GetFirstMatch(matchs[j], regex);
                            regex = @"(?<=\[)[^,]+(?=,)";
                            string[] subMatchs = StringTools.GetAllMatchs(match, regex);
                            if (subMatchs.Length >= 2)
                            {
                                Type t1 = Type.GetType(subMatchs[0]);
                                Type t2 = Type.GetType(subMatchs[1]);
                                IDictionary subDict = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(t1, t2));
                                keyTypes.Add(subDict.GetType());
                            }

                        }
                    }


                    Type type1 = keyTypes[0];
                    Type type2 = keyTypes[1];
                    MethodInfo add = type.GetMethod("Add", new Type[2] { type1, type2 });
                    MethodInfo m = orgList.GetType().GetMethod("Add");
                    Dictionary<object, JsonData> dict = jd.GetObjDict();
                    if (dict != null && dict.Count > 0)
                    {
                        foreach (KeyValuePair<object, JsonData> item in dict)
                        {
                            object lo = GetT(type2, item.Value);
                            Type keyType = item.Key.GetType();
                            object keyObj;
                            if (keyType != type1)
                            {
                                if (type1 == typeof(int))
                                {
                                    keyObj = int.Parse(item.Key.ToString());
                                }
                                else if (type1 == typeof(float))
                                {
                                    keyObj = float.Parse(item.Key.ToString());
                                }
                                else if (type1 == typeof(double))
                                {
                                    keyObj = double.Parse(item.Key.ToString());
                                }
                                else if (type1 == typeof(string))
                                {
                                    keyObj = item.Key.ToString();
                                }
                                else
                                {
                                    keyObj = item.Key.ToString();
                                }
                            }
                            else
                            {
                                keyObj = item.Key;
                            }
                            if (lo != null)
                            {
                                m.Invoke(orgList, new object[] { keyObj, lo });
                            }
                        }
                        return orgList;
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;

        }

        static public object GetObjT(Type type, JsonData jd)
        {
            FieldInfo[] fields = type.GetFields();
            if (fields.Length == 0)
            {
                return null;
            }
            object objT = (object)Activator.CreateInstance(type);

            for (int i = 0; i < fields.Length; i++)
            {
                if (!fields[i].IsPublic)
                {
                    continue;
                }
                if (fields[i].IsStatic)
                {
                    continue;
                }
                string fieldName = fields[i].Name;
                JsonData subJd = jd.Get(fieldName);
                if (subJd == null)
                {
                    continue;
                }
                Type subType = fields[i].FieldType;
                string realType = JsonTools.GetDataType(subType);
                if (realType == DataType.OBJECT)
                {
                    object subObj = GetT(subType, subJd);
                    fields[i].SetValue(objT, subObj);
                }
                else if (realType == DataType.ARRAY)
                {
                    if (subType.IsGenericType)
                    {
                        Type listType = subType.GetMethod("Find").ReturnType;
                        object orgList = Activator.CreateInstance(subType);

                        for (int j = 0; j < subJd.Count; j++)
                        {
                            object lo = GetT(listType, subJd.Get(j));
                            if (lo != null)
                            {
                                MethodInfo m = orgList.GetType().GetMethod("Add");
                                m.Invoke(orgList, new object[] { lo });
                            }
                        }

                        fields[i].SetValue(objT, orgList);

                    }
                    else if (subType.BaseType == typeof(Array))
                    {
                        MethodInfo[] ms = subType.GetMethods();
                        Type listType = subType.GetElementType();
                        object orgList = Array.CreateInstance(listType, subJd.Count);
                        MethodInfo sv = subType.GetMethod("SetValue", new Type[2] { typeof(object), typeof(int) });
                        for (int j = 0; j < subJd.Count; j++)
                        {
                            object lo = GetT(listType, subJd.Get(j));
                            if (lo != null)
                            {
                                sv.Invoke(orgList, new object[] { lo, j });
                            }
                        }

                        fields[i].SetValue(objT, orgList);
                    }
                }
                else if (realType == DataType.BOOL)
                {
                    bool bv = subJd.ToString().ToLower() == "true" ? true : false;
                    fields[i].SetValue(objT, bv);
                }
                else if (realType == DataType.STRING)
                {
                    fields[i].SetValue(objT, subJd.ToString());
                }
                else if (realType == DataType.NUMBER)
                {
                    string sv = subJd.ToString();
                    if (subType == typeof(int))
                    {
                        fields[i].SetValue(objT, int.Parse(sv));
                    }
                    else if (subType == typeof(float))
                    {
                        double dsv = Convert.ToDouble(sv);
                        float fsv = Convert.ToSingle(dsv);
                        fields[i].SetValue(objT, fsv);
                    }
                    else if (subType == typeof(double))
                    {
                        fields[i].SetValue(objT, double.Parse(sv));
                    }
                    else if (subType == typeof(Single))
                    {
                        fields[i].SetValue(objT, Single.Parse(sv));
                    }
                    else if (subType == typeof(long))
                    {
                        fields[i].SetValue(objT, long.Parse(sv));
                    }
                }
                else if(realType == DataType.DICTIONARY)
                {
                    //Type listType = subType.GetMethod("Find").ReturnType;
                    object orgList = Activator.CreateInstance(subType);
                    string str = subType.FullName;
                    string regex = @"(?<=Dictionary`2\[).+(?=\])";
                    string match = StringTools.GetFirstMatch(str, regex);
                    regex = @"\[[^\[\]]*(((?'Open'\[)[^\[\]]*)+((?'-Open'\])[^\[\]]*)+)*(?(Open)(?!))\]";
                    string[] matchs = StringTools.GetAllMatchs(match, regex);
                    List<Type> keyTypes = new List<Type>();
                    for (int j = 0; j < matchs.Length; j++)
                    {
                        if (matchs[j].IndexOf("Dictionary") < 0)
                        {
                            regex = @"(?<=\[)[^,]+(?=,)";
                            match = StringTools.GetFirstMatch(matchs[j], regex);
                            keyTypes.Add(Type.GetType(match,true,true));
                        }
                        else
                        {
                            regex = @"(?<=Dictionary`2\[).+(?=\])";
                            match = StringTools.GetFirstMatch(matchs[j], regex);
                            regex = @"(?<=\[)[^,]+(?=,)";
                            string[] subMatchs = StringTools.GetAllMatchs(match,regex);
                            if(subMatchs.Length>=2)
                            {
                                Type t1 = Type.GetType(subMatchs[0]);
                                Type t2 = Type.GetType(subMatchs[1]);
                                IDictionary subDict = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(t1, t2));
                                keyTypes.Add(subDict.GetType());
                            }
                            
                        }
                    }


                    Type type1 = keyTypes[0];
                    Type type2 = keyTypes[1];
                        MethodInfo add = subType.GetMethod("Add", new Type[2] { type1,type2 });
                        MethodInfo m = orgList.GetType().GetMethod("Add");
                        Dictionary<object, JsonData> dict = subJd.GetObjDict();
                        if (dict != null && dict.Count > 0)
                        {
                            foreach(KeyValuePair<object,JsonData>item in dict)
                            {
                                object lo = GetT(type2, item.Value);
                                Type keyType = item.Key.GetType();
                                object keyObj;
                                if(keyType!=type1)
                                {
                                    if(type1==typeof(int))
                                    {
                                        keyObj = int.Parse(item.Key.ToString());
                                    }
                                    else if(type1 == typeof(float))
                                    {
                                        keyObj = float.Parse(item.Key.ToString());
                                    }
                                    else if(type1 == typeof(double))
                                    {
                                        keyObj = double.Parse(item.Key.ToString());
                                    }
                                    else if (type1 == typeof(string))
                                    {
                                        keyObj = item.Key.ToString();
                                    }
                                    else
                                    {
                                        keyObj = item.Key.ToString();
                                    }
                                }
                                else
                                {
                                    keyObj = item.Key;
                                }
                                if (lo != null)
                                {
                                    m.Invoke(orgList, new object[] { keyObj, lo });
                                }
                            }
                            fields[i].SetValue(objT, orgList);
                        
                    }
                    //for (int j = 0; j < subJd.Count; j++)
                    //{
                    //    object lo = GetT(listType, subJd.Get(j));
                    //    if (lo != null)
                    //    {
                    //        MethodInfo m = orgList.GetType().GetMethod("Add");
                    //        m.Invoke(orgList, new object[] { lo });
                    //    }
                    //}

                    //fields[i].SetValue(objT, orgList);

                }

            }
            return objT;
        }

        private string GetFieldByJd(JsonData jd)
        {
            string str = "";

            string dataType = jd.Type;
            if (dataType == DataType.NUMBER)
            {
                str = jd.ToString();
            }
            else if (dataType == DataType.STRING)
            {
                str += "\"";
                str += jd.ToString();
                str += "\"";
            }
            else if (dataType == DataType.BOOL)
            {
                str = jd.ToString().ToLower();
            }
            else if (dataType == DataType.ARRAY)
            {
                str = GetArrayStrByJd(jd);
            }
            else if (dataType == DataType.OBJECT)
            {
                str += "{";

                List<string> keys = jd.GetKeys();
                if(keys==null)
                {
                    return "";
                }
                for (int i = 0; i < keys.Count; i++)
                {
                    str += "\"";
                    str += keys[i];
                    str += "\":";
                    JsonData value = jd.Get(keys[i]);
                    if (value != null)
                    {
                        str += GetFieldByJd(value);
                    }
                    else
                    {
                        str += "null";
                    }
                    if (i < keys.Count - 1)
                    {
                        str += ",";
                    }

                }
                str += "}";
            }
            return str;
        }

        private string GetArrayStrByJd(JsonData jd)
        {
            string str = "";
            str += "[";
            str += GetListStrByJd(jd);

            str += "]";
            return str;
        }

        private string GetListStrByJd(JsonData jd)
        {
            string str = "";

            List<JsonData> list = jd.GetList();
            if(list==null)
            {
                return "";
            }
            for (int i = 0; i < list.Count; i++)
            {
                str += GetFieldByJd(list[i]);
                if (i < list.Count - 1)
                {
                    str += ",";
                }
            }
            return str;
        }




        private string GetField(object obj)
        {
            string str = "";
            Type type = obj.GetType();
            string dataType = JsonTools.GetDataType(type);
            if(dataType==DataType.NUMBER)
            {
                str = obj.ToString();
            }
            else if(dataType==DataType.STRING)
            {
                str += "\"";
                str += obj.ToString();
                str += "\"";
            }
            else if(dataType == DataType.BOOL)
            {
                str = obj.ToString().ToLower();
            }
            else if(dataType == DataType.DICTIONARY)
            {
                str = GetDictStr(obj);
            }
            else if( dataType == DataType.ARRAY)
            {
                str = GetArrayString(obj);
            }
            else if(dataType==DataType.OBJECT)
            {
                str += "{";

                FieldInfo[] fields = type.GetFields();
                for (int i = 0; i < fields.Length; i++)
                {
                    if(!fields[i].IsPublic)
                    {
                        continue;
                    }
                    if (fields[i].IsStatic)
                    {
                        continue;
                    }
                    str += "\"";
                    str += fields[i].Name;
                    str += "\":";
                    object value = fields[i].GetValue(obj);
                    if(value!=null)
                    {
                        str += GetField(value);
                    }
                    else
                    {
                        str += "null";
                    }
                    if(i<fields.Length-1)
                    {
                        str+=",";
                    }
                    
                }
                str += "}";
            }
            else
            {
                FieldInfo[] fields = type.GetFields();
                if(fields!=null&&fields.Length>0)
                {
                    str += "{";
                    for (int i = 0; i < fields.Length; i++)
                    {
                        str += "\"";
                        str += fields[i].Name;
                        str += "\":";
                        object value = fields[i].GetValue(obj);
                        if (value != null)
                        {
                            str += GetField(value);
                        }
                        else
                        {
                            str += "null";
                        }
                        if (i < fields.Length - 1)
                        {
                            str += ",";
                        }

                    }
                    str += "}";

                }
            }
            return str;

        }

        private string GetDictStr(object objs)
        {
            string str = "";
            str += "{";
            //Type[] ts = objs.GetType().GetGenericArguments();
            //Dictionary<object,object> en = objs as Dictionary<object,object>;
            //int ind = 0;
            //int len = en.Count;
            //foreach (KeyValuePair<object,object> obj in en)
            //{
            //    object k = obj.Key;
            //    object v = obj.Value;
            //    str += GetField(k);
            //    str += ":";
            //    str += GetField(v);
            //    if (ind<len-1)
            //    {
            //        str += ",";
            //    }
            //}
            IEnumerable en = objs as IEnumerable;
            int ind = 0;
            int len = 0;
            foreach (object obj in en)
            {
                len++;
            }
            foreach (object obj in en)
            {
                PropertyInfo[] fields = obj.GetType().GetProperties();
                object k = null;
                object v = null;
                for(int i = 0;i<fields.Length;i++)
                {
                    string n = fields[i].Name;
                    object subObj = fields[i].GetValue(obj, null);
                    if(n.ToLower()=="key")
                    {
                        k = subObj;
                    }
                    else if(n.ToLower() == "value")
                    {
                        v = subObj;
                    }
                }
                if (k!=null && v!=null)
                {
                    str += GetField(k);
                    str += ":";
                    str += GetField(v);
                    if (ind < len - 1)
                    {
                        str += ",";
                    }
                }
                ind++;

            }
            str += "}";
            return str;
        }

        private string GetArrayString(object obj)
        {
            string str = "";
            str += "[";
            Type type = obj.GetType();
            if(type.IsGenericType)
            {
                str += GetListString(obj);
            }
            if (type.BaseType == typeof(Array))
            {
                str += GetArrString(obj);
            }
            str += "]";
            return str;
        }

        private string GetListString(object objs)
        {
            string str = "";
            IEnumerable en = objs as IEnumerable;
            List<object> list = new List<object>();
            foreach (object obj in en)
            {
                list.Add(obj);
            }
            for (int i = 0; i < list.Count; i++)
            {
                str += GetField(list[i]);
                if (i < list.Count - 1)
                {
                    str += ",";
                }
            }
            return str;
        }

        private string GetArrString(object obj)
        {
            string str = "";
            Array orgarr = obj as Array;
            object[] arr = new object[orgarr.Length];
            orgarr.CopyTo(arr, 0);
            //object[] arr = new object[orgarr.Length];
            //orgarr.CopyTo(arr, 0);

            //object[] arr = new object[orgarr.Length];
            
            for (int i = 0; i < arr.Length; i++)
            {
                str += GetField(arr[i]);
                if (i < arr.Length - 1)
                {
                    str += ",";
                }
            }
            return str;
        }



        private string objConStr = @"\w*(?'Open'\{)[^\(^\}]+(?'-Open'\})(?(Open)(?!))";

        private string arrConStr = @"\w*(?'Open'\[)[^\(^\]]+(?'-Open'\])(?(Open)(?!))";

        private string orgStr;
        private int curPos = 0;
        private JsonData CreateJsonData(string str)
        {
            return new JsonHelper().GetJsonData(str);

        }



        private JsonData GetJsonData(string str)
        {
            str = RemoveLayer(str);
            JsonData jd = new JsonData();
            List<string> list = GetMatch(str, objConStr);
            bool isFinal = false;
            if(list==null)
            {
                List<string> arrList = GetMatch(str, arrConStr);
                if(arrList==null)
                {
                    isFinal = true;
                }
                else
                {
                    for (int i = 0; i < arrList.Count; i++)
                    {
                        string subStr = GetArr(arrList[i]);
                        str = str.Replace(arrList[i], subStr);
                    }
                }
            }
            else
            {
                for(int i = 0;i<list.Count;i++)
                {
                    string subStr = GetJd(list[i]);
                    str = str.Replace(list[i], subStr);
                }
            }
            if(!isFinal)
            {
                return GetJsonData(str);
            }
            else
            {
                return GetFinalJd(str);
            }

        }

        private JsonData GetFinalJd(string str)
        {
            string regexStr = "\"[^\"]+\":[^,]+";
            List<string> list = GetMatch(str,regexStr);
            if(list==null)
            {
                return null;
            }
            else
            {
                JsonData jd = new JsonData();
                jd.SetType(DataType.OBJECT);
                for(int i = 0;i<list.Count;i++)
                {
                    string[] subList = list[i].Split(':');
                    string key = GetKey(subList[0]);

                    JsonData subJd = GetValue(subList[1]);

                    jd.SetValue(key, subJd);
                }
                return jd;
            }
        }

        private string GetKey(string str)
        {
            string regexStr = "(?<=\")[^\"]+(?=\")";
            Match m = Regex.Match(str, regexStr);
            string x = m.ToString();
            if(string.IsNullOrEmpty(x))
            {
                return str;
            }
            else
            {
                return x;
            }
        }

        private JsonData GetValue(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                return null;
            }
            if(str.ToLower()=="null")
            {
                return null;
            }
            List<int> list = GetObjInd(str);
            if(list==null)
            {
                JsonData jd = new JsonData();
                string value = GetKey(str);
                jd.SetValue(value);
                return  jd;
            }
            else
            {
                if(list.Count==0)
                {
                    return null;
                }
                else
                {
                    int ind = list[0];
                    if(jsonDataDict.ContainsKey(ind))
                    {
                        return jsonDataDict[ind];
                    }
                    else
                    {
                        return null;
                    }
                }
            }

        }


        private string RemoveLayer(string str)
        {
            string regexStr = "(?<={).+(?=})";
            Match m = Regex.Match(str, regexStr);
            string x = m.ToString();
            if(string.IsNullOrEmpty(x))
            {
                return str;
            }
            else
            {
                return x;
            }
        }

        private List<string> GetMatch(string str,string regexStr)
        {
            MatchCollection mc;
                mc = Regex.Matches(str, regexStr); 
            if(mc==null)
            {
                return null;
            }
            if(mc.Count==0)
            {
                return null;
            }
            List<string> strs = new List<string>();
            for(int i = 0;i<mc.Count;i++)
            {
                strs.Add(mc[i].ToString());
            }
            return strs;
        }

        private int objCount = 0;

        private Dictionary<int, JsonData> jsonDataDict = new Dictionary<int, JsonData>();

        private void InitData()
        {
            objCount = 0;
            jsonDataDict = new Dictionary<int, JsonData>();
        }

        private string GetJd(string str)
        {
            objCount++;
            JsonData jd = GetJsonData(str);
            jsonDataDict.Add(objCount, jd);
            return "@"+objCount;
        }

        private string GetArr(string str)
        {
            objCount++;
            List<JsonData> list = new List<JsonData>();
            List<int> inds = GetObjInd(str);
            for (int i = 0; i < inds.Count;i++ )
            {
                if(jsonDataDict.ContainsKey(inds[i]))
                {
                    list.Add(jsonDataDict[inds[i]]);
                }
            }
            JsonData jd = new JsonData();
            jd.SetValue(list);
            jsonDataDict.Add(objCount, jd);
            return "@" + objCount;
        }

        private List<int> GetObjInd(string str)
        {
            string regexStr = "(?<=@)[0-9]+";
            List<string> strList = GetMatch(str, regexStr);
            if(strList!=null)
            {
                List<int> list = new List<int>();
                for(int i = 0;i<strList.Count;i++)
                {
                    list.Add(int.Parse(strList[i]));
                }
                return list;
            }
            else
            {
                return null;
            }
        }

    }
}
