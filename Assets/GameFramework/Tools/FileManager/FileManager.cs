using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameFramework.Tools
{

    public class FileManager
    {
        private static FileManager _instance;
        public const string SAVE_PATH = "savePath.txt";

        public static FileManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FileManager();
                }
                return FileManager._instance;
            }

        }
        public FileManager()
        {
            //System.Text.Encoding.GetEncoding("gb2312");
        }

        public long GetFileSize(string path)
        {
            if (IsFileExists(path) == false)
            {
                return 0;
            }
            FileInfo info = new FileInfo(path);
            return info.Length;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public void SaveFile(string path, string content, bool needUtf8 = false)
        {
            CheckFileSavePath(path);
            if (needUtf8)
            {
                UTF8Encoding code = new UTF8Encoding(false);
                File.WriteAllText(path, content, code);
            }
            else
            {
                File.WriteAllText(path, content, Encoding.Default);
                
            }
        }

        public void SaveFileByCode(string path, string content, Encoding code)
        {
            CheckFileSavePath(path);
            File.WriteAllText(path, content, code);
        }

        public void SaveLine(string path, string content)
        {
            CheckFileSavePath(path);
            StreamWriter f = new StreamWriter(path, true);
            f.WriteLine(content);
            f.Close();
        }

        /// <summary>
        /// 写入所有行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        public void SaveAllLines(string path, string[] lines)
        {
            CheckFileSavePath(path);
            File.WriteAllLines(path, lines);
        }
        /// <summary>
        /// 保存bytes
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        public void SaveBytes(string path, byte[] bytes)
        {
            CheckFileSavePath(path);
            File.WriteAllBytes(path, bytes);
        }

        public void DelFolder(string path)
        {
            if (!IsDirectoryExists(path))
            {
                return;
            }
            Directory.Delete(path, true);
        }

        public void DelDir(string target_dir)
        {
            if (IsDirectoryExists(target_dir))
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DelDir(dir);
                }

                Directory.Delete(target_dir, false);
            }
        }

        /// <summary>
        /// 移除空文件夹。
        /// </summary>
        /// <param name="directoryName">要处理的文件夹名称。</param>
        /// <returns>是否移除空文件夹成功。</returns>
        public bool RemoveEmptyDirectory(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                throw new System.Exception("Directory name is invalid.");
            }

            try
            {
                if (!Directory.Exists(directoryName))
                {
                    return false;
                }

                // 不使用 SearchOption.AllDirectories，以便于在可能产生异常的环境下删除尽可能多的目录
                string[] subDirectoryNames = Directory.GetDirectories(directoryName, "*");
                int subDirectoryCount = subDirectoryNames.Length;
                foreach (string subDirectoryName in subDirectoryNames)
                {
                    if (RemoveEmptyDirectory(subDirectoryName))
                    {
                        subDirectoryCount--;
                    }
                }

                if (subDirectoryCount > 0)
                {
                    return false;
                }

                if (Directory.GetFiles(directoryName, "*").Length > 0)
                {
                    return false;
                }

                Directory.Delete(directoryName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DelFile(string path)
        {
            if (!IsFileExists(path))
            {
                return;
            }
            File.Delete(path);
        }

        /// <summary>
        /// 获取所有行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] ReadAllLines(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            return File.ReadAllLines(path);
        }

        /// <summary>
        /// 读取文件的字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadFileText(string path, bool isUTF8 = false)
        {
            if (!File.Exists(path))
            {
                return "";
            }
            string str = "";
            if (isUTF8)
            {
                str = File.ReadAllText(path, Encoding.UTF8);
            }
            else
            {
                str = File.ReadAllText(path, Encoding.Default);
            }

            return str;
        }

        /// <summary>
        /// 读取bytes
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] ReadFileBytes(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            byte[] str = File.ReadAllBytes(path);
            return str;
        }

        /// <summary>
        /// 检查某文件夹路径是否存在，如不存在，创建
        /// </summary>
        /// <param name="path"></param>
        public void CheckDirection(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 单纯检查某个文件夹路径是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsDirectoryExists(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查某个文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsFileExists(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取某目录下所有指定类型的文件的路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="exName"></param>
        /// <returns></returns>
        public List<string> GetAllFiles(string path, string exName)
        {
            if (!IsDirectoryExists(path))
            {
                return null;
            }
            bool needCheckExName = true;
            exName = exName.ToLower();
            if (string.IsNullOrEmpty(exName))
            {
                needCheckExName = false;
            }
            List<string> names = new List<string>();
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            string ex;
            for (int i = 0; i < files.Length; i++)
            {
                if (needCheckExName == true)
                {
                    ex = FilePathHelper.GetExName(files[i].FullName);
                    ex = ex.ToLower();
                    if (ex != exName)
                    {
                        continue;
                    }
                }
                names.Add(StringTools.ChangePathFormat(files[i].FullName));
            }
            DirectoryInfo[] dirs = root.GetDirectories();
            if (dirs.Length > 0)
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    List<string> subNames = GetAllFiles(dirs[i].FullName, exName);
                    if (subNames.Count > 0)
                    {
                        for (int j = 0; j < subNames.Count; j++)
                        {
                            names.Add(StringTools.ChangePathFormat(subNames[j]));
                        }
                    }
                }
            }

            return names;

        }


        /// <summary>
        /// 获取某目录下所有除了指定类型外的文件的路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="exName"></param>
        /// <returns></returns>
        public List<string> GetAllFilesExcept(string path, string exName)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }
            List<string> names = new List<string>();
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            string ex;
            for (int i = 0; i < files.Length; i++)
            {
                ex = FilePathHelper.GetExName(files[i].FullName);
                if (ex == exName)
                {
                    continue;
                }
                names.Add(StringTools.ChangePathFormat(files[i].FullName));
            }
            DirectoryInfo[] dirs = root.GetDirectories();
            if (dirs.Length > 0)
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    List<string> subNames = GetAllFilesExcept(dirs[i].FullName, exName);
                    if (subNames.Count > 0)
                    {
                        for (int j = 0; j < subNames.Count; j++)
                        {
                            names.Add(StringTools.ChangePathFormat(subNames[j]));
                        }
                    }
                }
            }

            return names;

        }

        /// <summary>
        /// 获取某目录下所有除了指定类型外的文件的路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="exName"></param>
        /// <returns></returns>
        public List<string> GetAllFilesIncludeList(string path, List<string> exName)
        {
            List<string> names = new List<string>();
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            for (int i = 0; i < exName.Count; i++)
            {
                exName[i] = exName[i].ToLower();
            }
            string ex;
            for (int i = 0; i < files.Length; i++)
            {
                ex = FilePathHelper.GetExName(files[i].FullName);
                ex = ex.ToLower();
                if (exName.IndexOf(ex) < 0)
                {
                    continue;
                }
                names.Add(StringTools.ChangePathFormat(files[i].FullName));
            }
            DirectoryInfo[] dirs = root.GetDirectories();
            if (dirs.Length > 0)
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    List<string> subNames = GetAllFilesIncludeList(dirs[i].FullName, exName);
                    if (subNames.Count > 0)
                    {
                        for (int j = 0; j < subNames.Count; j++)
                        {
                            names.Add(StringTools.ChangePathFormat(subNames[j]));
                        }
                    }
                }
            }

            return names;

        }

        /// <summary>
        /// 获取某目录下所有除了指定类型外的文件的路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="exName"></param>
        /// <returns></returns>
        public List<string> GetAllFilesExceptList(string path, List<string> exName)
        {
            List<string> names = new List<string>();
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            for (int i = 0; i < exName.Count; i++)
            {
                exName[i] = exName[i].ToLower();
            }
            string ex;
            for (int i = 0; i < files.Length; i++)
            {
                ex = FilePathHelper.GetExName(files[i].FullName);
                ex = ex.ToLower();
                if (exName.IndexOf(ex) >= 0)
                {
                    continue;
                }
                names.Add(StringTools.ChangePathFormat(files[i].FullName));
            }
            DirectoryInfo[] dirs = root.GetDirectories();
            if (dirs.Length > 0)
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    List<string> subNames = GetAllFilesExceptList(dirs[i].FullName, exName);
                    if (subNames.Count > 0)
                    {
                        for (int j = 0; j < subNames.Count; j++)
                        {
                            names.Add(StringTools.ChangePathFormat(subNames[j]));
                        }
                    }
                }
            }

            return names;

        }

        /// <summary>
        /// 获取指定路径下第一层的子文件夹路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetSubFolders(string path)
        {
            if (!IsDirectoryExists(path))
            {
                return null;
            }
            DirectoryInfo root = new DirectoryInfo(path);

            DirectoryInfo[] dirs = root.GetDirectories();
            List<string> folders = new List<string>();
            if (dirs.Length > 0)
            {
                for (int i = 0; i < dirs.Length; i++)
                {
                    folders.Add(StringTools.ChangePathFormat(dirs[i].FullName));
                }
            }

            return folders;

        }

        /// <summary>
        /// 获取指定路径下一层的指定格式的文件的路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="exName"></param>
        /// <returns></returns>
        public List<string> GetSubFiles(string path, string exName)
        {
            List<string> names = new List<string>();
            if (IsDirectoryExists(path) == false)
            {
                return names;
            }

            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            string ex;
            for (int i = 0; i < files.Length; i++)
            {
                ex = FilePathHelper.GetExName(files[i].FullName);
                if (ex != exName)
                {
                    continue;
                }
                names.Add(StringTools.ChangePathFormat(files[i].FullName));
            }
            return names;
        }

        /// <summary>
        /// 获取指定路径下一层的除指定格式以外的文件的路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="exName"></param>
        /// <returns></returns>
        public List<string> GetSubFilesExcept(string path, string exName)
        {
            List<string> names = new List<string>();
            if (IsDirectoryExists(path) == false)
            {
                return names;
            }

            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            string ex;
            for (int i = 0; i < files.Length; i++)
            {
                ex = FilePathHelper.GetExName(files[i].FullName);
                if (ex == exName)
                {
                    continue;
                }
                names.Add(StringTools.ChangePathFormat(files[i].FullName));
            }

            return names;
        }

        public void CheckFileSavePath(string path)
        {
            string realPath = path;
            int ind = path.LastIndexOf("/");
            if (ind >= 0)
            {
                realPath = path.Substring(0, ind);
            }
            else
            {
                ind = path.LastIndexOf("\\");
                if (ind >= 0)
                {
                    realPath = path.Substring(0, ind);
                }
            }
            bool isHaveFile = IsFileExists(realPath);
            if (isHaveFile)
            {
                DelFile(realPath);
            }
            CheckDirection(realPath);
        }

        public void CopyFile(string path, string tarPath)
        {
            if (!IsFileExists(path))
            {
                return;
            }
            CheckFileSavePath(tarPath);
            File.Copy(path, tarPath, true);
        }

        public void MoveFolder(string orgPath, string tarPath)
        {
            DirectoryInfo folder = new DirectoryInfo(orgPath);
            if (folder.Exists)
            {
                CheckDirection(tarPath);
                Directory.Delete(tarPath);
                folder.MoveTo(tarPath);
            }
        }

        public void MoveFile(string orgPath, string tarPath)
        {
            if (File.Exists(orgPath) == false)
            {
                return;
            }
            DelFile(tarPath);
            File.Move(orgPath, tarPath);
        }

    }
}
