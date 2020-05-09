using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEditor.U2D;
using UnityEngine.UI;
using System.Collections;

namespace GameFramework
{
    public class UIExtendMenu
    {
        [MenuItem("GameObject/★UI扩展★/★创建★/创建UI", false, 10)]
        static void CreateUIObject(MenuCommand menuCommadn)
        {
            GameObject go     = new GameObject("New UI");
            GameObject parent = GameObject.Find("UICanvas");
            if (parent == null)
                parent = menuCommadn.context as GameObject;
            GameObjectUtility.SetParentAndAlign(go, parent);
            RectTransform rect = go.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            go.AddComponent<SpriteAtlasList>();
            go.AddComponent<UIOutlet>();
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建UI Item", false, 10)]
        static void CreateUIObjectItem(MenuCommand menuCommadn)
        {
            GameObject go     = new GameObject("New Item");
            GameObject parent = GameObject.Find("UICanvas");
            if (parent == null)
                parent = menuCommadn.context as GameObject;

            GameObjectUtility.SetParentAndAlign(go, parent);
            RectTransform rect = go.AddComponent<RectTransform>();
            go.AddComponent<UIOutlet>();
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建UI ItemPlulic", false, 10)]
        static void CreateUIObjectItemPlulic(MenuCommand menuCommadn)
        {
            GameObject go     = new GameObject("New ItemP");
            GameObject parent = GameObject.Find("UICanvas");
            if (parent == null)
                parent = menuCommadn.context as GameObject;

            GameObjectUtility.SetParentAndAlign(go, parent);
            RectTransform rect = go.AddComponent<RectTransform>();
            go.AddComponent<UIOutlet>();
            Selection.activeObject = go;
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建多语言 Text", false, 10)]
        static void CreateTextLang(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject go = new GameObject("New Lang Text");
                GameObjectUtility.SetParentAndAlign(go, parent);
                go.AddComponent<UILangText>();
                Text          txt  = go.AddComponent<Text>();
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(200, 22);
                txt.alignment  = TextAnchor.MiddleLeft;
                txt.fontSize   = 22;
                Color outColor = Color.white;
                ColorUtility.TryParseHtmlString("#FFFFFF", out outColor);
                txt.color                = outColor;
                txt.text                 = "New Lang Text";
                txt.resizeTextForBestFit = true;
                txt.supportRichText      = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Resources/Font/SIMHEI.TTF");
                txt.font = font;
            }
            else
            {
                ToolsHelper.Log("只能在UI下创建LangText");
            }
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建 Text", false, 10)]
        static void CreateText(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject go = new GameObject("New Text");
                GameObjectUtility.SetParentAndAlign(go, parent);
                Text          txt  = go.AddComponent<Text>();
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(200, 22);
                txt.alignment  = TextAnchor.MiddleLeft;
                txt.fontSize   = 22;
                txt.text       = "New Text";
                Color outColor = Color.white;
                ColorUtility.TryParseHtmlString("#FFFFFF", out outColor);
                txt.color                = outColor;
                txt.resizeTextForBestFit = true;
                txt.supportRichText      = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Resources/Font/SIMHEI.TTF");
                txt.font = font;
            }
            else
            {
                ToolsHelper.Log("只能在UI下创建Text");
            }
        }

        [MenuItem("GameObject/★UI扩展★/★创建★/创建多语言 Button", false, 10)]
        static void CreateButtonLang(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject goBtn = new GameObject("New Button");
                GameObjectUtility.SetParentAndAlign(goBtn, parent);
                Image image = goBtn.AddComponent<Image>();
                image.sprite =
                    AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameRes/ArtRes/UIAtlas/PublicButton/Btn1_BG.png");
                image.SetNativeSize();

                GameObject BtnIcon = new GameObject("Icon");
                BtnIcon.transform.SetParent(goBtn.transform, false);
                Image IconImg = BtnIcon.AddComponent<Image>();
                IconImg.sprite =
                    AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameRes/ArtRes/UIAtlas/PublicButton/Btn1_1.png");
                RectTransform IconImgrect = IconImg.GetComponent<RectTransform>();
                IconImgrect.anchorMin = Vector2.zero;
                IconImgrect.anchorMax = Vector2.one;
                IconImgrect.offsetMin = Vector2.zero;
                IconImgrect.offsetMax = Vector2.zero;
                IconImgrect.sizeDelta = new Vector2(-16, -16);


                Button btn = goBtn.AddComponent<Button>();
                btn.image = IconImg;

                GameObject goTxt = new GameObject("Text");
                GameObjectUtility.SetParentAndAlign(goTxt, goBtn);
                goTxt.AddComponent<UILangText>();
                Text  txt   = goTxt.AddComponent<Text>();
                Color color = Color.black;
                ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                txt.color = color;
                RectTransform rect = goTxt.GetComponent<RectTransform>();

                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                rect.sizeDelta = new Vector2(-40, -34);
                txt.fontSize   = 24;
                txt.alignment  = TextAnchor.MiddleCenter;
                txt.text       = "Lang Button";
                //txt.resizeTextForBestFit = true;
                //txt.supportRichText = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Resources/Font/SIMHEI.TTF");
                txt.font = font;
                Shadow shadow = goTxt.AddComponent<Shadow>();
                shadow.effectColor    = new Color(0, 0, 0, 1);
                shadow.effectDistance = new Vector2(1, -3);

            }
            else
            {
                ToolsHelper.Log("只能在UI下创建LangText");
            }
        }


        [MenuItem("GameObject/★UI扩展★/★创建★/创建视频 Button", false, 10)]
        static void CreateVideoButtonLang(MenuCommand menuCommadn)
        {
            GameObject parent = menuCommadn.context as GameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>() != null)
            {
                GameObject goBtn = new GameObject("New Button");
                GameObjectUtility.SetParentAndAlign(goBtn, parent);
                Image image = goBtn.AddComponent<Image>();
                image.sprite =
                    AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameRes/ArtRes/UIAtlas/PublicButton/Btn1_BG.png");
                image.SetNativeSize();

                GameObject BtnIcon = new GameObject("Icon");
                BtnIcon.transform.SetParent(goBtn.transform, false);
                Image IconImg = BtnIcon.AddComponent<Image>();
                IconImg.sprite =
                    AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameRes/ArtRes/UIAtlas/PublicButton/Btn1_1.png");
                RectTransform IconImgrect = IconImg.GetComponent<RectTransform>();
                IconImgrect.anchorMin = Vector2.zero;
                IconImgrect.anchorMax = Vector2.one;
                IconImgrect.offsetMin = Vector2.zero;
                IconImgrect.offsetMax = Vector2.zero;
                IconImgrect.sizeDelta = new Vector2(-16, -16);


                DBTVideoBtn btn = goBtn.AddComponent<DBTVideoBtn>();
                btn.image = IconImg;

                GameObject goTxt = new GameObject("Text");
                GameObjectUtility.SetParentAndAlign(goTxt, goBtn);
                goTxt.AddComponent<UILangText>();
                Text txt = goTxt.AddComponent<Text>();
                Color color = Color.black;
                ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                txt.color = color;
                RectTransform rect = goTxt.GetComponent<RectTransform>();

                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
                rect.sizeDelta = new Vector2(-40, -34);
                txt.fontSize = 24;
                txt.alignment = TextAnchor.MiddleCenter;
                txt.text = "Lang Button";
                //txt.resizeTextForBestFit = true;
                //txt.supportRichText = true;
                Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Resources/Font/SIMHEI.TTF");
                txt.font = font;
                Shadow shadow = goTxt.AddComponent<Shadow>();
                shadow.effectColor = new Color(0, 0, 0, 1);
                shadow.effectDistance = new Vector2(1, -3);

            }
            else
            {
                ToolsHelper.Log("只能在UI下创建LangText");
            }
        }

        [MenuItem("GameObject/★UI扩展★/★生成★/生成Item脚本", false, 21)]
        static void CreateItemScript(MenuCommand menuCommadn)
        {
            GameObject target = menuCommadn.context as GameObject;
            if (target != null && (target.name.EndsWith("Item")))
            {
                UIOutlet uiObj = target.GetComponent<UIOutlet>();
                if (uiObj != null)
                {
                    UIScriptExport.ExportItemScript(uiObj);
                    ToolsHelper.Log("生成成功!!!");
                    AssetDatabase.Refresh();
                    return;
                }
            }

            if (target != null && (target.name.EndsWith("ItemP")))
            {
                ToolsHelper.Log("对于ItemP类型的Item，请在对应预设文件夹生成Item");
                return;
            }

            ToolsHelper.Log("请选择有效果的Item对象!!!,Item包含UIOutlet脚本，并且以Item命名结尾");
        }

        [MenuItem("GameObject/★UI扩展★/★生成★/生成 UI && Item 脚本", false, 21)]
        static void CreateUIScript(MenuCommand menuCommadn)
        {
            GameObject target = menuCommadn.context as GameObject;
            if (target != null && target.transform.parent.name == "UICanvas" && target.name.EndsWith("UI"))
            {
                if (target.name.StartsWith("New UI"))
                {
                    ToolsHelper.Log("请修改UI名称!!!");
                    return;
                }

                UIOutlet uiObj = target.GetComponent<UIOutlet>();
                if (uiObj != null)
                {
                    UIScriptExport.ExportUIScript(uiObj);
                    ToolsHelper.Log("生成成功!!!");
                    AssetDatabase.Refresh();
                    return;
                }
            }

            ToolsHelper.Log("请选择有效果的UI对象!!!");
        }
        //[MenuItem("GameObject/★UI扩展★/生成公有Item 脚本", false, 22)]
        //static void CreateItemPublicScript(MenuCommand menuCommadn)
        //{
        //    GameObject target = menuCommadn.context as GameObject;
        //    if (target != null && target.name.EndsWith("ItemP"))
        //    {
        //        UIOutlet uiObj = target.GetComponent<UIOutlet>();
        //        if (uiObj != null)
        //        {
        //            UIScriptExport.ExportPublicItemScript(uiObj);
        //            ToolsHelper.Log("生成成功!!!");
        //            return;
        //        }
        //    }
        //    ToolsHelper.Log("请选择有效果的ItemP对象!!!");
        //}

        //[MenuItem("Assets/★UI扩展★/文件夹--拼图九宫格", false, 21)]
        //static void Jigasw9X9forDirectory()
        //{
        //    UnityEngine.Object @object = Selection.activeObject;
        //    string path = AssetDatabase.GetAssetPath(@object);

        //    DirectoryInfo dir = new DirectoryInfo(path);
        //    List<string> fileNmame = new List<string>();
        //    ToolsHelper.GetFile(path, fileNmame);
        //    int lenght = fileNmame.Count;
        //    //去除。meta文件
        //    for (int i = lenght - 1; i >= 0; i--)
        //    {
        //        if (fileNmame[i].EndsWith(".meta"))
        //        {
        //            fileNmame.RemoveAt(i);
        //        }
        //    }
        //    foreach (string item in fileNmame)
        //    {
        //        string pathAsset = item.Substring(item.IndexOf("Assets"));
        //        Debug.Log("pathAsset:" + pathAsset);
        //        Texture2D objects = AssetDatabase.LoadAssetAtPath<Texture2D>(pathAsset);
        //        SpriteSpilt.CreateSpriteSpolt2(objects);
        //        ToolsHelper.Log($"切割图片[{objects}]完成");
        //    }
        //    //UnityEngine.Object[] objects = AssetDatabase.LoadAllAssetsAtPath(path);
        //    //Debug.Log(objects.Length);
        //    //foreach (var item in objects)
        //    //{
        //    //    Debug.Log(item);
        //    //    ToolsHelper.Log($"切割图片[{item}]完成");
        //    //}
        //    AssetDatabase.Refresh();
        //    ToolsHelper.Log("切割图片完成");

        //}

        //[MenuItem("Assets/★UI扩展★/文件--拼图九宫格.Png", false, 22)]
        //static void Jigasw9X9forText2d()
        //{
        //    UnityEngine.Object[] @object = Selection.objects;
        //    foreach (var item in @object)
        //    {
        //        Texture2D objects = (Texture2D)item;
        //        SpriteSpilt.CreateSpriteSpolt2(objects, "png");
        //        ToolsHelper.Log($"切割图片[{objects}]完成");
        //    }
        //    AssetDatabase.Refresh();
        //    ToolsHelper.Log("切割图片完成");

        //}
        //[MenuItem("Assets/★UI扩展★/文件--拼图九宫格.jpg", false, 23)]
        //static void Jigasw9X9forText2dJPG()
        //{
        //    UnityEngine.Object[] @object = Selection.objects;
        //    foreach (var item in @object)
        //    {
        //        Texture2D objects = (Texture2D)item;
        //        SpriteSpilt.CreateSpriteSpolt2(objects, "jpg");
        //        ToolsHelper.Log($"切割图片[{objects}]完成");
        //    }
        //    AssetDatabase.Refresh();
        //    ToolsHelper.Log("切割图片完成");

        //}


        [MenuItem("GameObject/★UI扩展★/★增加组件★/BulidVisualArea", false, 11)]
        static void CreateBulidVisualArea()
        {
            var goRoot = Selection.activeGameObject;
            if (goRoot == null)
                return;

            var button = goRoot.GetComponent<Button>();

            if (button == null)
            {
                Debug.Log("Selecting Object is not a button!");
                return;
            }

            var           polygon = new GameObject("BulidVisualArea");
            RectTransform rect    = polygon.AddComponent<RectTransform>();
            polygon.transform.SetParent(goRoot.transform, false);
            rect.pivot     = Vector2.zero;
            rect.sizeDelta = new Vector2(100, 100);
            Image image = polygon.AddComponent<Image>();
            image.raycastTarget = false;
            Color outColor = new Color32(255, 233, 8, 89);
            image.color = outColor;
        }
        
        [MenuItem("GameObject/★添加描边阴影★", false, -100)]
        static void CreateUIFont(MenuCommand menuCommadn)
        {
            GameObject goRoot = (GameObject)menuCommadn.context;
            if (goRoot == null)
                return;
                
            if (goRoot.GetComponent<Text>() == null)
            {
                Debug.Log("Selecting Object is not a text!");
                return;
            }

            Outline[] outline = goRoot.GetComponents<Outline>();
            for (int i = 0; i < outline.Length; i++)
            {
                GameObject.DestroyImmediate(outline[i]);
            }
            Shadow[] shadow = goRoot.GetComponents<Shadow>();
            for (int i = 0; i < shadow.Length; i++)
            {
                GameObject.DestroyImmediate(shadow[i]);
            }

            goRoot.AddComponent<Outline>().effectDistance = new Vector2(2, -2);
            goRoot.AddComponent<Shadow>().effectDistance = new Vector2(0, -2);
        }
    }
}