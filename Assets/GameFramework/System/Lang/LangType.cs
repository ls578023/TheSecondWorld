using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{

    public enum ELangType
    {
        ZH_CN=0,   //简体中文
        ZH_TW,  //繁体中文
        EN,         //英文
        JA,         //日语
        KO,        //韩语 
    }
    [Flags]
    /// <summary>
    /// 本地语言库
    /// </summary>
    public enum LocalELangLibrary
    {
        ZH_CN=1<<0,   //简体中文
        ZH_TW = 1 << 1,  //繁体中文
        EN = 1 << 2,         //英文
        JA = 1 << 3,         //日语
        KO = 1 << 4,        //韩语 
    }

    public class EnumFlags : PropertyAttribute { }

}
