using UnityEditor;
using UnityEngine;

namespace GameFramework
{


    [InitializeOnLoad]
    internal class UIOutletEditorInitializer
    {
        static UIOutletEditorInitializer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        }

        private static void HierarchyItemCB(int instanceid, Rect selectionrect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceid) as GameObject;
            if (obj != null)
            {
                UIOutlet uiLua = obj.GetComponent<UIOutlet>();
                if (uiLua != null)
                {
                    Rect r = new Rect(selectionrect);
                    r.x = r.width - 50;
                    r.y += 2;
                    var style = new GUIStyle();
                    style.normal.textColor = Color.yellow;
                    string skinId = "";
                    GUI.Label(r, string.Format("Infos:{0}", uiLua.OutletInfos.Count + skinId), style);
                }
            }
        }
    }

}