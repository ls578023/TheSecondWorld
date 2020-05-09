
//using GameFrameworkTest;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameFramework
{
    /// <summary>
    /// UI脚本导出
    /// </summary>
    public class UIScriptExport
    {
        /// <summary>导出UI</summary>
        /// <param name="ui"></param>
        public static void ExportUIScript(UIOutlet ui)
        {
            string modName = SceneManager.GetActiveScene().name;
            string uiName = ui.gameObject.name;
            CreateUI(modName, uiName, ui);
            CreateUIView(modName, uiName, ui);
            //如果UI中含有Item,导出Item
            foreach(UIOutlet.OutletInfo info in ui.OutletInfos)
            {
                if (info.Name.EndsWith("Item"))
                {
                    UIOutlet itemOut = (info.Object as GameObject).GetComponent<UIOutlet>();
                    if (itemOut != null)
                    {
                        ExportItemScript(itemOut);
                    }
                }
            }
        }

        /// <summary>导出私有Item</summary>
        public static void ExportItemScript(UIOutlet ui)
        {
            string modName = SceneManager.GetActiveScene().name;
            string uiName = ui.gameObject.name.Replace("prefab", ""); ;
            CreateItem(modName, uiName, ui);
            CreateItemView(modName, uiName, ui);
        }


        /// <summary>导出公有Item</summary>
        public static void ExportPublicItemScript(UIOutlet ui)
        {
            string modName = "PublicItem";
            string uiName = ui.gameObject.name.Replace("prefab", "");
            uiName = uiName.Substring(0, uiName.Length - 1);
            CreateItem(modName, uiName, ui);
            CreateItemView(modName, uiName, ui);
        }

        #region 创建UI       
        private static void CreateUI(string modName, string uiName, UIOutlet ui)
        {
            string saveFilePath = $"{ExportScriptDir}{modName}/UI/{uiName}.cs";
            StringBuilder eventAddStrs = new StringBuilder();
            StringBuilder eventStrs = new StringBuilder();
            UIOutlet.OutletInfo info;
            string objType;
            for (int i = ui.OutletInfos.Count; --i >= 0;)
            {
                info = ui.OutletInfos[i];
                if (info == null) continue;
                objType = getTypeName(info.ComponentType);
                if (info.Object.name.StartsWith("btn")) //自动生成按钮事件
                {
                    eventAddStrs.AppendLine($"            {info.Object.name}.AddClick({info.Object.name}_Click);   //");
                    eventStrs.AppendLine($@"        /// <summary></summary>
        void {info.Object.name}_Click()
        {{
        }}");
                }
            }

            string fieldStr = $@"using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using GameFramework;

namespace {AppSetting.MineGameName}.UI.{modName}
{{
    public partial class {uiName} : BaseUI
    {{
        /// <summary>XXXX界面</summary>
        public {uiName}()
        {{
            UINode = EUINode.UIWindow;     //UI节点
            OpenAnim = EUIAnim.None;      //UI打开效果
            CloseAnim = EUIAnim.None;   //UI关闭效果 
        }}
        
        /// <summary>添加事件监听</summary>
        override protected void Awake()
        {{ 
{eventAddStrs}
        }}
        
         /// <summary>刷新</summary>
        public override void Refresh()
        {{
        }}

{eventStrs}
        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {{
        }}
    }}
}}
";
            ToolsHelper.SaveFile(saveFilePath, fieldStr, false);
        }
        #endregion

        #region 创建UIView 
        private static void CreateUIView(string modName, string uiName, UIOutlet ui)
        {
            string saveFilePath = $"{ExportScriptDir}{modName}/UI/View/{uiName}View.cs";

            StringBuilder fieldStrs = new StringBuilder();
            StringBuilder getStrs = new StringBuilder();
            UIOutlet.OutletInfo info;
            string objType;
            for (int i = ui.OutletInfos.Count; --i >= 0;)
            {
                info = ui.OutletInfos[i];
                if (info == null) continue;
                objType = getTypeName(info.ComponentType);
                fieldStrs.AppendLine($"        private {objType} {info.Name};");
                if(objType=="GameObject")
                    getStrs.AppendLine($@"            {info.Name} = Get(""{info.Name}"");");
                else
                    getStrs.AppendLine($@"            {info.Name} = Get<{objType}>(""{info.Name}"");");
            }

            string fieldStr = $@"//工具生成不要修改

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GameFramework;

namespace {AppSetting.MineGameName}.UI.{modName}
{{
    public partial class {uiName} : BaseUI
    {{
{fieldStrs} 
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {{
{getStrs}
        }}
    }}
}}
";
            ToolsHelper.SaveFile(saveFilePath, fieldStr, true);
        }
        #endregion

        #region 创建Item       
        private static void CreateItem(string modName, string uiName, UIOutlet ui)
        {
            string saveFilePath = $"{ExportScriptDir}{modName}/UI/Item/{uiName}.cs";
            StringBuilder eventAddStrs = new StringBuilder();
            StringBuilder eventStrs = new StringBuilder();
            UIOutlet.OutletInfo info;
            string objType;
            for (int i = ui.OutletInfos.Count; --i >= 0;)
            {
                info = ui.OutletInfos[i];
                if (info == null) continue;
                objType = getTypeName(info.ComponentType);
                if (info.Object.name.StartsWith("btn")) //自动生成按钮事件
                {
                    eventAddStrs.AppendLine($"            {info.Object.name}.AddClick({info.Object.name}_Click);   //");
                    eventStrs.AppendLine($@"        /// <summary></summary>
        void {info.Object.name}_Click()
        {{
        }}");
                }
            }

            string selClick = "             gameObject.AddClick(self_Click);     //当前对象点击事件";
            if (ui.GetComponent<Button>() != null)
                selClick = "             gameObject.GetComponent<Button>().AddClick(self_Click);  //当前对象点击事件";
            string fieldStr = $@"using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using GameFramework;

namespace {AppSetting.MineGameName}.UI.{modName}
{{
    public partial class {uiName} : BaseItem
    {{
        public Action<{uiName}> onClick; //Item点击事件

        /// <summary>添加事件监听</summary>
        override protected void Awake()
        {{
{selClick}             {eventAddStrs}
        }}
        /// <summary>设置数据<param name=""data""></param>
        /*public void SetData({uiName}Data data)
        {{
            Data = data;
            txtServerName.text = data.ServerName;
        }}*/
        /// <summary>刷新Item</summary>
        public override void Refresh()
        {{
        }}

        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {{
            onClick?.Invoke(this);
        }}
{ eventStrs}
        /// <summary>释放Item引用</summary>
        public override void Dispose()
        {{
            onClick = null;
        }}
    }}
}}
";
            ToolsHelper.SaveFile(saveFilePath, fieldStr, false);
        }
        #endregion

        #region 创建UIView 
        private static void CreateItemView(string modName, string uiName, UIOutlet ui)
        {
            string saveFilePath = $"{ExportScriptDir}{modName}/UI/Item/View/{uiName}View.cs";

            StringBuilder fieldStrs = new StringBuilder();
            StringBuilder getStrs = new StringBuilder();
            StringBuilder skinStrsRect = new StringBuilder();
            StringBuilder skinStrs = new StringBuilder();
            UIOutlet.OutletInfo info;
            string objType;
            for (int i = ui.OutletInfos.Count; --i >= 0;)
            {
                info = ui.OutletInfos[i];
                if (info == null) continue;
                objType = getTypeName(info.ComponentType);
                fieldStrs.AppendLine($"        private {objType} {info.Name};");
                if (objType == "GameObject")
                    getStrs.AppendLine($@"            {info.Name} = Get(""{info.Name}"");");
                else
                    getStrs.AppendLine($@"            {info.Name} = Get<{objType}>(""{info.Name}"");");
            }

            string fieldStr = $@"//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GameFramework;

namespace {AppSetting.MineGameName}.UI.{modName}
{{
    public partial class {uiName} : BaseItem
    {{
{fieldStrs} 
 
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {{
{getStrs}
        }}
        /// <summary>初始化皮肤设置</summary>
        protected override void InitializeSkin()
        {{
{skinStrs}
            
        }}
    }}
}}
";
            ToolsHelper.SaveFile(saveFilePath, fieldStr, true);
        }
        #endregion

        private static string getTypeName(string fullName)
        {
            return fullName.Substring(fullName.LastIndexOf(".") + 1);
        }
        private static string ExportScriptDir
        {
            //get { return Path.GetFullPath("../" + AppSetting.HotFixName + "/Module/").Replace("\\", "/"); }
            get { return AppSetting.DataPath + AppSetting.BusinessModuleDir; }
        }

        #region 导出SDK脚本

//        public static void ExportSDKScript()
//        {
//            /// <summary> 类说明</summary>
//            Dictionary<object, GameFrameworkTest.ClassInfoConfig> dicClassInfo = new Dictionary<object, GameFrameworkTest.ClassInfoConfig>();
//            /// <summary> APP基本信息</summary>
//            Dictionary<object, GameFrameworkTest.FunctionInfoConfig> dicFunctionInfo = new Dictionary<object, GameFrameworkTest.FunctionInfoConfig>();
//            #region 读取表数据
//            UnityEngine.Object configObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("Assets/GameRes/BundleRes/Data/Config/ClassInfoConfig.txt");
//            if (configObj != null)
//            {
//                string strconfig = configObj.ToString();
//                List<GameFrameworkTest.ClassInfoConfig> list = JsonMapper.ToObject<List<GameFrameworkTest.ClassInfoConfig>>(strconfig);
//                for (int i = 0; i < list.Count; i++)
//                {
//                    if (dicClassInfo.ContainsKey(list[i].UniqueID))
//                        CLog.Error($"表[类说明]中有相同键({list[i].UniqueID})");
//                    else
//                        dicClassInfo.Add(list[i].UniqueID, list[i]);
//                }
//            }
//            configObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("Assets/GameRes/BundleRes/Data/Config/FunctionInfoConfig.txt");
//            if (configObj != null)
//            {
//                string strconfig = configObj.ToString();
//                List<GameFrameworkTest.FunctionInfoConfig> list = JsonMapper.ToObject<List<GameFrameworkTest.FunctionInfoConfig>>(strconfig);
//                for (int i = 0; i < list.Count; i++)
//                {
//                    if (dicFunctionInfo.ContainsKey(list[i].UniqueID))
//                        CLog.Error($"表[类说明]中有相同键({list[i].UniqueID})");
//                    else
//                        dicFunctionInfo.Add(list[i].UniqueID, list[i]);
//                }
//            }
//            //Debug.Log(dicClassInfo.Count);
//            //Debug.Log(dicFunctionInfo.Count);
//            #endregion

//            Dictionary<string, bool> AndroidClassName = new Dictionary<string, bool>();
//            foreach (var item in dicClassInfo.Values)
//            {
//                List<GameFrameworkTest.FunctionInfoConfig> functionList = new List<GameFrameworkTest.FunctionInfoConfig>();
//                foreach (var FunctionItem in dicFunctionInfo.Values)
//                {
//                    if (FunctionItem.id > item.id * 1000 && FunctionItem.id < (item.id + 1) * 1000)
//                        functionList.Add(FunctionItem);
//                }
//                //Debug.Log("函数个数："+functionList.Count);
//                //AdsManager
//                string saveFilePath = $"{AppSetting.DataPath}GameFramework/SDK/UnitedSdk/Api/{item.className}Manager.SDK.cs";
//                //Android接口
//                string saveAndroidFilePath = $"{AppSetting.DataPath}GameFramework/SDK/UnitedSdk/Platforms/Android/{item.className}/{item.className}Android.cs";
//                //IOS接口
//                string saveIOSFilePath = $"{AppSetting.DataPath}GameFramework/SDK/UnitedSdk/Platforms/iOS/{item.className}/{item.className}IOS.cs";
//                //安卓对象类
//                string saveAndroidJavaClassFilePath = $"{AppSetting.DataPath}GameFramework/SDK/UnitedSdk/Define/AndroidClass.cs";

//                StringBuilder AndroidFunctionStrs = new StringBuilder();
//                #region 先生成安卓类
//                foreach (var function in functionList)
//                {
//                    if (function.platform != 0 && function.platform != 1)
//                        continue;
//                    string xc = "";
//                    string sc = "";
//                    string printxc = "";
//                    if (function.InputArgsName != null && function.InputArgsName.Length > 0)
//                    {
//                        for (int i = 0; i < function.InputArgsName.Length; i++)
//                        {
//                            xc += function.InputArgsType[i] + " " + function.InputArgsName[i] + ((i == function.InputArgsName.Length - 1) ? "" : ",");
//                            sc += function.InputArgsName[i] + ((i == function.InputArgsName.Length - 1) ? "" : ",");
//                            printxc += function.InputArgsName[i] + $"[{{{function.InputArgsName[i]}}}]";
//                        }
//                    }
//                    if (!string.IsNullOrEmpty(sc))
//                    {
//                        sc = "," + sc;
//                    }
//                    if (!AndroidClassName.ContainsKey(function.javaClassName))
//                    {
//                        AndroidClassName.Add(function.javaClassName, true);
//                    }
//                    string JavaClass = function.javaClassName.Substring(function.javaClassName.LastIndexOf(".") + 1);
//                    string FHZ = $@"AndroidClass.{JavaClass}.CallStatic(""{function.functionName}""{sc});";
//                    if (!function.retureType.Contains("void"))
//                    {
//                        FHZ = $@"{function.retureType} Reuslt = AndroidClass.{JavaClass}.CallStatic<{function.retureType}>(""{function.functionName}""{sc});
//        Debug.Log($""返回值为：[{{Reuslt}}]"");
//        return Reuslt;";
//                    }

//                    AndroidFunctionStrs.AppendLine($@"  
///// <summary>
///// {function.ResPath}
// /// </summary>
//public static {function.retureType} {FistCharToUpper(function.functionName)}({xc})
//    {{
//        Debug.Log($""调用安卓{function.functionName}: {printxc}"");
//        {FHZ}
//    }}");
//                    //Debug.Log("AndroidFunctionStrs" + AndroidFunctionStrs);
//                }
//                string AndroidfieldStr = $@"//工具生成不要修改
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace DBTSDK{{
// public class {item.className}Android{{
//{AndroidFunctionStrs}
//}}
//}}
//";
//                ToolsHelper.SaveFile(saveAndroidFilePath, AndroidfieldStr, true);
//                #endregion

//                #region 生成IOS类
//                StringBuilder DLLImport = new StringBuilder();
//                StringBuilder IOSFunction = new StringBuilder();
//                foreach (var function in functionList)
//                {
//                    if (function.platform != 0 && function.platform != 2)
//                        continue;
//                    string xc = "";
//                    string sc = "";
//                    string printxc = "";
//                    if (function.InputArgsName != null && function.InputArgsName.Length > 0)
//                    {
//                        for (int i = 0; i < function.InputArgsName.Length; i++)
//                        {
//                            xc += function.InputArgsType[i] + " " + function.InputArgsName[i] + ((i == function.InputArgsName.Length - 1) ? "" : ",");
//                            sc += function.InputArgsName[i] + ((i == function.InputArgsName.Length - 1) ? "" : ",");
//                            printxc += function.InputArgsName[i] + $"[{{{function.InputArgsName[i]}}}]";
//                        }
//                    }

//                    DLLImport.AppendLine($@"
//     [DllImport(""__Internal"")]
//    internal static extern {function.retureType} {function.functionName}({xc});
//");
//                    string FHZ = $@"{function.functionName}({sc});";
//                    if (!function.retureType.Contains("void"))
//                    {
//                        FHZ = $@"{function.retureType} Reuslt ={function.functionName}({sc});
//        Debug.Log($""返回值为：[{{Reuslt}}]"");
//        return Reuslt;";
//                    }
//                    IOSFunction.AppendLine($@"
///// <summary>
///// {function.ResPath}
// /// </summary>
//public static {function.retureType} {FistCharToUpper(function.functionName)}({xc}){{
//        Debug.Log($""调用IOS{function.functionName}: {printxc}"");
//        {FHZ}
//}}
//");
//                }
//                string IOSfieldStr = $@"//工具生成不要修改
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using UnityEngine;
//namespace DBTSDK{{
// public class {item.className}IOS{{
//{DLLImport}

//{IOSFunction}
//}}
//}}
//";
//                ToolsHelper.SaveFile(saveIOSFilePath, IOSfieldStr, true);
//                #endregion

//                #region 生成代理脚本

//                StringBuilder ManagerFuncion = new StringBuilder();
//                foreach (var function in functionList)
//                {
//                    string xc = "";
//                    string sc = "";
//                    string printxc = "";
//                    if (function.InputArgsName != null && function.InputArgsName.Length > 0)
//                    {
//                        for (int i = 0; i < function.InputArgsName.Length; i++)
//                        {
//                            xc += function.InputArgsType[i] + " " + function.InputArgsName[i] + ((i == function.InputArgsName.Length - 1) ? "" : ",");
//                            sc += function.InputArgsName[i] + ((i == function.InputArgsName.Length - 1) ? "" : ",");
//                            printxc += function.InputArgsName[i] + $"[{{{function.InputArgsName[i]}}}]";
//                        }
//                    }

//                    string ANDROID = "";
//                    string IOS = "";
//                    if (function.platform == 0 || function.platform == 1)
//                        ANDROID = $"{item.className}Android.{FistCharToUpper(function.functionName)}({sc});";

//                    if (function.platform == 0 || function.platform == 2)
//                        IOS = $"{item.className}IOS.{FistCharToUpper(function.functionName)}({sc});";


//                    string FHZ = $@"
//        #if UNITY_ANDROID && !UNITY_EDITOR
//        {ANDROID}
//        #elif UNITY_IOS && !UNITY_EDITOR
//        {IOS}
//        #endif
//";
//                    if (!function.retureType.Contains("void"))
//                    {

//                        if (function.platform == 0 || function.platform == 1)
//                            ANDROID = $"Result = {item.className}Android.{FistCharToUpper(function.functionName)}({sc});";

//                        if (function.platform == 0 || function.platform == 2)
//                            IOS = $"Result = {item.className}IOS.{FistCharToUpper(function.functionName)}({sc});";


//                        FHZ = $@"
//        {function.retureType} Result=default;
//        #if UNITY_ANDROID && !UNITY_EDITOR
//        {ANDROID}
//        #elif UNITY_IOS  && !UNITY_EDITOR
//        {IOS}
//        #endif
//        Debug.Log($""结果为:[{{ Result}}]"");
//        return Result;
//";
//                    }
//                    ManagerFuncion.AppendLine($@"
//    /// <summary>
//    /// {function.ResPath}
//    /// </summary>
//    public  {function.retureType} {FistCharToUpper(function.functionName)}({xc}){{
//        Debug.Log($""调用代理层{function.functionName}:{printxc}"");
//        {FHZ}
//    }}
//");
//                }
//                string ManagerStr = $@"//工具生成不要修改
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace DBTSDK
//{{
//public partial class {item.className}Manager
//{{
//    {ManagerFuncion}
//}}
//}}
//";
//                ToolsHelper.SaveFile(saveFilePath, ManagerStr, true);

//                #endregion

//                #region 生成安卓java类

//                StringBuilder AClassNameAbs = new StringBuilder();
//                foreach (var AClassName in AndroidClassName.Keys)
//                {
//                    string ArrName = AClassName.Substring(AClassName.LastIndexOf(".") + 1);
//                    AClassNameAbs.AppendLine($@"
//        static AndroidJavaObject {ArrName}_javaClass;
//        public static AndroidJavaObject {ArrName} {{ get 
//            {{
//                if({ArrName}_javaClass==null)
//                    {ArrName}_javaClass = new AndroidJavaObject(""{AClassName}"");
//                return {ArrName}_javaClass;
//            }} 
//        }}   
//");
//                }
//                string AndroidJavaStr = $@"//工具生成不要修改
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace DBTSDK
//{{
//public  class AndroidClass
//{{
//    {AClassNameAbs}
//}}
//}}
//";
//                ToolsHelper.SaveFile(saveAndroidJavaClassFilePath, AndroidJavaStr, true);
//                #endregion
//                AssetDatabase.Refresh();
//            }



//        }
        #endregion

        static string FistCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToUpper() + input.Substring(1);
            return str;
        }
    }

}
