using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace GameFramework
{
    [InitializeOnLoad]
    [CustomEditor(typeof(ModelOutlet))]
    public class ModelOutletEditor : Editor
    {

        static Dictionary<Transform, string[]> _outletObjects = new Dictionary<Transform, string[]>();



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

            ModelOutlet outlet = target as ModelOutlet;

            if (outlet.OutletInfos == null || outlet.OutletInfos.Count == 0)
            {
                if (GUILayout.Button("Add New Outlet"))
                {
                    if (outlet.OutletInfos == null)
                        outlet.OutletInfos = new List<ModelOutlet.ModelOutletInfo>();
                    else
                    {
                        outlet.OutletInfos.Clear();
                        _outletObjects.Clear();
                    }
                    Undo.RecordObject(target, "Add OutletInfo");
                    outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo());
                }

            }
            else
            {

                // outlet ui edit

                for (var j = outlet.OutletInfos.Count - 1; j >= 0; j--)
                {
                    ModelOutlet.ModelOutletInfo outletInfo = outlet.OutletInfos[j];
                    string[] typesOptions = new string[0];

                    var isValid = outletInfo.transform != null && !_cachedPropertyNames.Contains(outletInfo.NodeName);

                    _cachedPropertyNames.Add(outletInfo.NodeName);


                    EditorGUILayout.Separator();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(string.Format("Property: {0}", outletInfo.NodeName), isValid ? GreenFont : RedFont);
                    EditorGUILayout.Space();
                    if (GUILayout.Button("+"))
                    {
                        Undo.RecordObject(target, "Insert OutletInfo");
                        outlet.OutletInfos.Insert(j, new ModelOutlet.ModelOutletInfo());
                    }
                    if (GUILayout.Button("-"))
                    {
                        Undo.RecordObject(target, "Remove OutletInfo");
                        if (outlet.OutletInfos[j].transform != null)
                        {
                            _outletObjects.Remove(outlet.OutletInfos[j].transform);
                        }
                        outlet.OutletInfos.RemoveAt(j);
                    }
                    if (GUILayout.Button("↑", GUILayout.MaxWidth(20)) && j < outlet.OutletInfos.Count - 1)
                    {
                        ModelOutlet.ModelOutletInfo curr = outlet.OutletInfos[j];
                        outlet.OutletInfos[j] = outlet.OutletInfos[j + 1];
                        outlet.OutletInfos[j + 1] = curr;
                    }
                    if (GUILayout.Button("↓", GUILayout.MaxWidth(20)) && j > 0)
                    {
                        ModelOutlet.ModelOutletInfo curr = outlet.OutletInfos[j];
                        outlet.OutletInfos[j] = outlet.OutletInfos[j - 1];
                        outlet.OutletInfos[j - 1] = curr;
                    }
                    GUILayout.Space(20);

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    outletInfo.NodeName = EditorGUILayout.TextField("", outletInfo.NodeName);
                    if (outletInfo.transform != null)
                    {
                        if (outletInfo.NodeName == string.Empty)
                            outletInfo.NodeName = outletInfo.transform.name;
                        outletInfo.transform = EditorGUILayout.ObjectField("", outletInfo.transform, typeof(UnityEngine.Transform), true) as Transform;
                    }
                    else
                    {
                        outletInfo.transform = EditorGUILayout.ObjectField("", outletInfo.transform, typeof(UnityEngine.Transform), true) as Transform;
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

}