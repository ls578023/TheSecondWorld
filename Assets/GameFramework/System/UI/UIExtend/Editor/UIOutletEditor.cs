using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using GameFramework;

[InitializeOnLoad]
[CustomEditor(typeof(UIOutlet))]
public class UIOutletEditor : Editor
{

    static Dictionary<GameObject, string[]> _outletObjects = new Dictionary<GameObject, string[]>();

    static UIOutletEditor()
    {      
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        //UIWindowAssetEditor.CustomInspectorGUIAfter += (UIWindowAsset target) =>
        //{
        //    if (target.gameObject.GetComponent<UILuaOutlet>() == null)
        //    {
        //        if (GUILayout.Button("Add UILuaOutlet"))
        //        {
        //            target.gameObject.AddComponent<UILuaOutlet>();
        //        }
        //    }
        //};
    }

    private static void HierarchyItemCB(int instanceid, Rect selectionrect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceid) as GameObject;
        if (obj != null)
        {
            if (_outletObjects.ContainsKey(obj))
            {
                Rect r = new Rect(selectionrect);
                r.x = r.x+ GetStringWidth(obj.name)+25;
                r.y += 2;
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.green;
                //GUI.Label(r, string.Format("{0} [{1}]", _outletObjects[obj][0], _outletObjects[obj][1]), style);
                GUI.Label(r, string.Format("[{0}]", _outletObjects[obj][1]), style);
            }
        }
    }
    private static float GetStringWidth(string str)
    {
        Font font = GUI.skin.font;        
        font.RequestCharactersInTexture(str, font.fontSize, FontStyle.Normal);
        CharacterInfo characterInfo;
        float width = 0f;
        for (int i = 0; i < str.Length; i++)
        {
            font.GetCharacterInfo(str[i], out characterInfo, font.fontSize);            
            width += characterInfo.advance;
        }
        return width;

    }
   

    GUIStyle GreenFont;
    GUIStyle RedFont;

    private HashSet<string> _cachedPropertyNames = new HashSet<string>();

    void OnEnable()
    {
        GreenFont = new GUIStyle();
        GreenFont.fontStyle = FontStyle.Bold;
        GreenFont.fontSize = 11;
        GreenFont.normal.textColor = Color.green;
        RedFont = new GUIStyle();
        RedFont.fontStyle = FontStyle.Bold;
        RedFont.fontSize = 11;
        RedFont.normal.textColor = Color.red;
    }

    public override void OnInspectorGUI()
    {
        _cachedPropertyNames.Clear();

        EditorGUI.BeginChangeCheck();

        UIOutlet outlet = target as UIOutlet;

        if (outlet.OutletInfos == null || outlet.OutletInfos.Count == 0)
        {
            if (GUILayout.Button("Add New Outlet"))
            {
                if (outlet.OutletInfos == null)
                    outlet.OutletInfos = new List<UIOutlet.OutletInfo>();
                else
                {
                    outlet.OutletInfos.Clear();
                    _outletObjects.Clear();
                }
                Undo.RecordObject(target, "Add OutletInfo");
                outlet.OutletInfos.Add(new UIOutlet.OutletInfo());
            }

        }
        else
        {

            // outlet ui edit

            for (var j = outlet.OutletInfos.Count - 1; j >= 0; j--)
            {
                var currentTypeIndex = -1;
                UIOutlet.OutletInfo outletInfo = outlet.OutletInfos[j];
                string[] typesOptions = new string[0];

                var isValid = outletInfo.Object != null && !_cachedPropertyNames.Contains(outletInfo.Name);

                _cachedPropertyNames.Add(outletInfo.Name);

                if (outletInfo.Object != null)
                {
                    if (outletInfo.Object is GameObject)
                    {
                       
                        var gameObj = outletInfo.Object as GameObject;
                        var components = gameObj.GetComponents<Component>();

                        if (components.Length == 1)
                            currentTypeIndex = 0;
                        else
                            currentTypeIndex = components.Length;// 设置默认类型,默认选中最后一个
                        string objTypeName = "";

                        typesOptions = new string[components.Length+1];
                        typesOptions[0] = gameObj.GetType().FullName;
                        if (typesOptions[0] == outletInfo.ComponentType)
                        {
                            currentTypeIndex = 0;
                            objTypeName = gameObj.GetType().Name;
                        }

                        for (var i = 1; i <= components.Length; i++)
                        {
                            var com = components[i - 1];
                            var typeName = typesOptions[i] = com.GetType().FullName;
                            if (typeName == outletInfo.ComponentType)
                            {
                                currentTypeIndex = i;
                                objTypeName = com.GetType().Name;
                            }
                        }
                        _outletObjects[gameObj] = new string[] { outletInfo.Name, objTypeName };

                        if(string.IsNullOrEmpty(outletInfo.Name))
                            outletInfo.Name = gameObj.name;
                    }

                }


                EditorGUILayout.Separator();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(string.Format("Property: {0}", outletInfo.Name), isValid ? GreenFont : RedFont);
                EditorGUILayout.Space();
                if (GUILayout.Button("+"))
                {
                    Undo.RecordObject(target, "Insert OutletInfo");
                    outlet.OutletInfos.Insert(j, new UIOutlet.OutletInfo());
                }
                if (GUILayout.Button("-"))
                {
                    Undo.RecordObject(target, "Remove OutletInfo");
                    if (outlet.OutletInfos[j].Object != null)
                    {
                        _outletObjects.Remove(outlet.OutletInfos[j].Object as GameObject);
                    }
                    outlet.OutletInfos.RemoveAt(j);
                }
                if (GUILayout.Button("↑", GUILayout.MaxWidth(20))&&j < outlet.OutletInfos.Count-1)
                {
                    UIOutlet.OutletInfo curr = outlet.OutletInfos[j];
                    outlet.OutletInfos[j] = outlet.OutletInfos[j + 1];
                    outlet.OutletInfos[j + 1] = curr;
                }
                if (GUILayout.Button("↓", GUILayout.MaxWidth(20))&&j>0)
                {
                    UIOutlet.OutletInfo curr = outlet.OutletInfos[j];
                    outlet.OutletInfos[j] = outlet.OutletInfos[j - 1];
                    outlet.OutletInfos[j - 1] = curr;
                }
                GUILayout.Space(20);

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                ///outletInfo.Name = EditorGUILayout.TextField("Name:", outletInfo.Name);
                if (outletInfo.Object != null)
                {
                    outletInfo.Name = outletInfo.Object.name;
                    outletInfo.Object = EditorGUILayout.ObjectField("", outletInfo.Object, typeof(UnityEngine.Object), true);
                }
                else
                {
                    outletInfo.Name = "Select Object";
                    outletInfo.Object = EditorGUILayout.ObjectField("", outletInfo.Object, typeof(UnityEngine.Object), true);
                }

                if (currentTypeIndex >= 0)
                {
                    var typeIndex = EditorGUILayout.Popup("", currentTypeIndex, typesOptions);
                    outletInfo.ComponentType = typesOptions[typeIndex].ToString();

                }
                EditorGUILayout.EndHorizontal();
            }
        }
        //base.OnInspectorGUI ();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "GUI Change Check");
        }
    }


}
