using System;
using System.Collections.Generic;


namespace GameFramework
{
    public class UserVariable
    {
        public string Key;

        public string StrVar;

        public int IntVar;

        public float FloatVar;

        public static UserVariable BuildVariable(string key, string strVar)
        {
            UserVariable userVariable = new UserVariable();
            userVariable.Key = key;
            userVariable.StrVar = strVar;
            return userVariable;
        }


        public static UserVariable BuildVariable(string key, int intVar)
        {
            UserVariable userVariable = new UserVariable();
            userVariable.Key = key;
            userVariable.IntVar = intVar;
            return userVariable;
        }


        public static UserVariable BuildVariable(string key, float floatVar)
        {
            UserVariable userVariable = new UserVariable();
            userVariable.Key = key;
            userVariable.FloatVar = floatVar;
            return userVariable;
        }

        public static UserVariable BuildVariable(string key, string strVar = "", int intVar = 0, float floatVar = 0f)
        {
            UserVariable userVariable = new UserVariable();
            userVariable.Key = key;
            userVariable.StrVar = strVar;
            userVariable.IntVar = intVar;
            userVariable.FloatVar = floatVar;
            return userVariable;
        }
    }

    public class UserData
    {
        public List<UserVariable> UserDataList;
    }
}

