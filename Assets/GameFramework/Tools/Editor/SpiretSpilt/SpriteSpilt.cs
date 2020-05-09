using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace GameFramework
{
    public class SpriteSpilt
    {
        /// <summary>
        /// 图片切成9块
        /// </summary>
        public static void CreateSpriteSpolt(Texture2D image)
        {
            #region 复制图片 切9个小块
            //图片保存路径
            //Texture2D image = Selection.activeObject as Texture2D;
            //获取被复制的文件
            string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));
            string path = rootPath + "/" + image.name + ".jpg";//图片路径名称

            TextureImporter rootTexture = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口
            rootTexture.textureType = TextureImporterType.Sprite;

            AssetDatabase.ImportAsset(path, ImportAssetOptions.Default);

            string ImgName = image.name;
            //Debug.Log(image.width);
            //Debug.Log(image.height);

            string SpriteSaveFilePath = AppSetting.BundleArtResDir + AppSetting.UIAtlasDir + ImgName + "/";
            //创建图片文件夹
            ToolsHelper.CreateDir(SpriteSaveFilePath, true);
            //生成的文件
            string fileName = ImgName + ".jpg";
            string fileNameSpile = ImgName + "_Spilt.jpg";
            //string FileNamePath = System.IO.Path.Combine(Path.GetDirectoryName(SpriteSaveFilePath), fileName);
            string fileNameSpilePath = System.IO.Path.Combine(Path.GetDirectoryName(SpriteSaveFilePath), fileNameSpile);
            //复制2份文件导目标文件夹
            //AssetDatabase.CopyAsset(path, FileNamePath);
            AssetDatabase.CopyAsset(path, fileNameSpilePath);
            //第一张图片设置成UI界面引用的图
            //TextureImporter texImp = AssetImporter.GetAtPath(FileNamePath) as TextureImporter;//获取图片入口
            //texImp.textureType = TextureImporterType.Sprite;
            //texImp.spriteImportMode = SpriteImportMode.Single;
            //第2张图切9个小块
            TextureImporter texImp = AssetImporter.GetAtPath(fileNameSpilePath) as TextureImporter;//获取图片入口


            //获取图片的宽高
            int width = image.width;
            int height = image.height;
            //单位框高
            int tempWidth = width / 3;
            int tempHeight = height / 3;

            List<SpriteMetaData> spriteMetaDatas = new List<SpriteMetaData>();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    SpriteMetaData metaData = new SpriteMetaData();
                    metaData.name = image.name + "_" + (y * 3 + x);
                    metaData.pivot = Vector2.one * 0.5f;
                    metaData.border = Vector4.zero;

                    Rect rect = new Rect();
                    rect.x = x * tempWidth;
                    rect.y = (2 - y) * tempHeight;
                    rect.width = tempWidth;
                    rect.height = tempHeight;
                    metaData.rect = rect;
                    spriteMetaDatas.Add(metaData);
                }
            }

            texImp.textureType = TextureImporterType.Sprite;
            texImp.spriteImportMode = SpriteImportMode.Multiple;
            texImp.spritesheet = spriteMetaDatas.ToArray();
            #endregion

            #region 创建一个图集与上一部创建的图相关联

            string AtlasSaveFilePath = AppSetting.BundleResDir + AppSetting.UIAtlasDir + ImgName + ".spriteatlas";
            if (File.Exists(AtlasSaveFilePath))
            {
                AssetDatabase.DeleteAsset(AtlasSaveFilePath);
            }
            SpriteAtlas spriteAtlas = new SpriteAtlas();
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(AppSetting.BundleArtResDir + AppSetting.UIAtlasDir + ImgName, typeof(UnityEngine.Object));
            Debug.Log(obj);
            spriteAtlas.Add(new[] { obj });
            AssetDatabase.CreateAsset(spriteAtlas, AtlasSaveFilePath);
            #endregion

            //AssetDatabase.ImportAsset(FileNamePath, ImportAssetOptions.Default);
            AssetDatabase.ImportAsset(fileNameSpilePath, ImportAssetOptions.Default);
        }

        public static void CreateSpriteSpolt2(Texture2D image,string Fileformat="jpg")
        {
            #region 复制图片 切9个小块
            //图片保存路径
            //Texture2D image = Selection.activeObject as Texture2D;
            //获取被复制的文件
            string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));
            string path = rootPath + "/" + image.name + $".{Fileformat}";//图片路径名称
            Debug.Log(path);
            TextureImporter rootTexture = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口
            rootTexture.textureType = TextureImporterType.Sprite;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.Default);

            //string fileNameSpile = "/"+image.name + "_Spilt.jpg";
            //string fileNameSpilePath = rootPath+ fileNameSpile;
            //Debug.Log(fileNameSpilePath);
            //AssetDatabase.CopyAsset(path, fileNameSpilePath);

            //TextureImporter targetTexture = AssetImporter.GetAtPath(fileNameSpilePath) as TextureImporter;//获取图片入口
            string ImgName = image.name;
            #region 切割9块
            //获取图片的宽高
            int width = image.width;
            int height = image.height;
            //单位框高
            int tempWidth = width / 3;
            int tempHeight = height / 3;

            List<SpriteMetaData> spriteMetaDatas = new List<SpriteMetaData>();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    SpriteMetaData metaData = new SpriteMetaData();
                    metaData.name = image.name + "_" + (y * 3 + x);
                    metaData.pivot = Vector2.one * 0.5f;
                    metaData.border = Vector4.zero;

                    Rect rect = new Rect();
                    rect.x = x * tempWidth;
                    rect.y = (2 - y) * tempHeight;
                    rect.width = tempWidth;
                    rect.height = tempHeight;
                    metaData.rect = rect;
                    spriteMetaDatas.Add(metaData);
                }
            }
            #endregion
            rootTexture.spriteImportMode = SpriteImportMode.Multiple;
            rootTexture.spritesheet = spriteMetaDatas.ToArray();

            #endregion
            //AssetDatabase.ImportAsset(FileNamePath, ImportAssetOptions.Default);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.Default);
        }
    }
}
