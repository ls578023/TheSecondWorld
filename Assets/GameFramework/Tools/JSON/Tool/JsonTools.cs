using System;
using System.Reflection;
namespace GameFramework.Tools
{
    public class JsonTools
    {
        static public string GetDataType(Type type)
        {
            if (type == typeof(int) || type == typeof(float) || type == typeof(double) || type == typeof(Single) || type == typeof(long))
            {
                return DataType.NUMBER;
            }
            else if (type == typeof(string))
            {
                return DataType.STRING;
            }
            else if (type == typeof(bool))
            {
                return DataType.BOOL;
            }
            else if (type.IsGenericType && type.GetGenericArguments().Length>=2)
            {
                return DataType.DICTIONARY;
            }
            else if (type.IsGenericType)
            {
                return DataType.ARRAY;
            }
            else if (type.BaseType == typeof(Array))
            {
                return DataType.ARRAY;
            }
            else if(type.IsClass)
            {
                return DataType.OBJECT;
            }
            else
            {
                FieldInfo[] fields = type.GetFields();
                if(fields!=null&&fields.Length>0)
                {
                    return DataType.OBJECT;
                }
                else
                {
                    return DataType.OTHER;
                }
            }
        }
    }
}
