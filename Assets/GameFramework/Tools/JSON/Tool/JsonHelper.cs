using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFramework.Tools
{
    public class JsonHelper
    {
        private string orgStr;
        private int len;
        private int curPos;
        private string token;
        private JsonData curJd;
        private JsonData parentJd;
        public JsonData GetJsonData(string str)
        {
            curPos = 0;
            orgStr = str;
            len = str.Length;
            token = ReadUnSpace();
            if(token=="{")
            {
                return GetObject();
            }
            else if(token == "[")
            {
                return GetArray();
            }

            return null;
        }

        private JsonData GetObject()
        {
            JsonData jd = new JsonData();
            jd.SetType(DataType.OBJECT);
            while (token == " ")
            {
                token = Read();
            }
            while(token!="}"&&curPos<len&&!string.IsNullOrEmpty(token))
            {
                if(token!=",")
                {
                    string key = GetKey();
                    key = key.Trim();
                    if (string.IsNullOrEmpty(key))
                    {
                        break;
                    }

                    JsonData v = GetValue();
                    jd.SetValue(key, v);
                }
                token = ReadUnSpace();
            }

            return jd;

        }

        private JsonData GetArray()
        {
            List<JsonData> list = new List<JsonData>();
            JsonData jd = new JsonData();
            token = ReadUnSpace();
            while (token != "]" && !string.IsNullOrEmpty(token))
            {
                if(token=="{")
                {
                    list.Add(GetObject());
                }
                else if(token=="[")
                {
                    list.Add(GetArray());
                }
                else if(token!=",")
                {
                    JsonData arrJd = GetFinalValue();
                    if(arrJd!=null)
                    {
                        list.Add(arrJd);
                    }                    
                }
                token = ReadUnSpace();
            }
            jd.SetValue(list);
            return jd;
        }

        private string GetKey()
        {
            string k = "";
            while (token != ":"&& token!="}"&& !string.IsNullOrEmpty(token))
            {
                if(token!="\""&&token!="{")
                {
                    k += token;                    
                }
                token = Read();
            }
            return k;

        }

        private JsonData GetValue()
        {
            token = ReadUnSpace();
            if (token == "{")
            {
                return GetObject();
            }
            else if (token == "[")
            {
                return GetArray();
            }
            else
            {
                return GetFinalValue();
            }
        }

        private JsonData GetFinalValue()
        {
            JsonData jd = new JsonData();
            string k = "";
            string t = token;
            //string addStr= GetString();

            if (t == "\"")
            {
                string addStr = GetString();
                jd.SetValue(addStr);
            }
            else if (t.ToLower() == "t" || t.ToLower() == "f")
            {
                string addStr = GetBool();
                k += t;
                k += addStr;
                bool b = k.ToLower() == "true" ? true : false;
                jd.SetValue(b);
            }
            else if (t.ToLower() == "n")
            {
                string addStr = GetNull();
                k += t;
                k += addStr;
                return null;
            }
            else
            {
                k += t;
                string addStr = GetNum();
                k += addStr;
                if (k.ToLower() == "null")
                {
                    return null;
                }
                if (k == "}" || k == "]")
                {
                    curPos -= 1;
                    return null;
                }
                jd.SetValue(double.Parse(k.Trim()));
            }

            return jd;
        }

        private string GetString()
        {
            string k = "";
            token = Read();
            while (token != "\"" &&  !string.IsNullOrEmpty(token))
            {
                k += token;
                token = Read();
            }
            if (token == "}" || token == "]")
            {
                curPos -= 1;
            }
            return k;
        }

        private string GetBool()
        {
            string k = "";
            token = Read();
            while (token != "\"" && token != "," && token != "}" && token != "]" && !string.IsNullOrEmpty(token))
            {
                k += token;
                token = Read();
            }
            if (token == "}" || token == "]")
            {
                curPos -= 1;
            }
            return k;
        }

        private string GetNum()
        {
            string k = "";
            token = Read();
            while (token != "\"" && token != "," && token != "}" && token != "]" && !string.IsNullOrEmpty(token))
            {
                k += token;
                token = Read();
            }
            if (token == "}" || token == "]")
            {
                curPos -= 1;
            }
            return k;
        }

        private string GetNull()
        {
            string k = "";
            token = Read();
            while (token != "\"" && token != "," && token != "}" && token != "]" && !string.IsNullOrEmpty(token))
            {
                k += token;
                token = Read();
            }
            if (token == "}" || token == "]")
            {
                curPos -= 1;
            }
            return k;
        }

        private string Read()
        {
            if(curPos>=len)
            {
                return "";
            }
            string s = orgStr.Substring(curPos, 1);
            curPos++;
            while(s=="\n"||s=="\r")
            {
                s = orgStr.Substring(curPos, 1);
                curPos++;
            }
            
            return s;
        }

        private string ReadUnSpace()
        {
            if (curPos >= len)
            {
                return "";
            }
            string s = orgStr.Substring(curPos, 1);
            curPos++;
            while (s == "\n" || s == "\r"||s==" ")
            {
                s = orgStr.Substring(curPos, 1);
                curPos++;
            }

            return s;
        }

    }


}
