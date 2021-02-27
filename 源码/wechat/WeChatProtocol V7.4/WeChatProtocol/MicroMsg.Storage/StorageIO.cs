namespace MicroMsg.Storage
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.InteropServices;

    public class StorageIO
    {
        private const string accountDir = "/account";
        public const string attachmentDir = "/attachment";
        private const string bannerDir = "/banner";
        private const string configDir = "/config";
        public const string ConversationBgDir = "/Conversation_bg";
        public const string customImageDir = "/custom_img";
        private const string dataBaseDir = "/db";
        private const string globalConfigDir = "config";
        private const string globalDataDir = "data";
        private const string headImageDir = "/head_img";
        private const string headImageQQ = "/head_img_qq";
        private const string headImageTemp = "/head_img_tmp";
        private const string headImgHD = "/head_img_hd";
        public const string imageDir = "/image";
        private const string massDataDir = "/mass_data";
        private const string pluginDir = "/plugin";
        private const string rootDir = "storage";
        private const string shakeDir = "/shake";
        public const string snsDir = "/sns";
        private const string TAG = "StorageIO";
        public const string tempDir = "/temp";
        public const string thumbImage = "/thumbnail";
        public const string thumbVideo = "/thumbVideo";
        private static string[] userDataDirList = new string[] { 
            "/db", "/head_img", "/head_img_tmp", "/head_img_hd", "/thumbnail", "/image", "/voice", "/video", "/account", "/shake", "/mass_data", "/config", "/plugin", "/banner", "/thumbVideo", "/Conversation_bg", 
            "/sns", "/temp", "/custom_img", "/attachment"
         };
        private static string userPath;
        public const string videoDir = "/video";
        public const string voiceDir = "/voice";

        public static void appendToFile(string fileName, byte[] data)
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, file))
                    {
                        stream.Seek(0L, SeekOrigin.End);
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
            catch (Exception exception)
            {
                Log.e("storage", "writeToFile file fail" + exception);
            }
        }

        private static int countDirsNumber(IsolatedStorageFile isf, string strPath)
        {
            int length = isf.GetDirectoryNames(strPath + "/*").Length;
            foreach (string str in isf.GetDirectoryNames(strPath + "/*"))
            {
                length += countDirsNumber(isf, strPath + "/" + str);
            }
            return length;
        }

        private static int countFilesNumber(IsolatedStorageFile isf, string strPath)
        {
            int length = isf.GetFileNames(strPath + "/*").Length;
            foreach (string str in isf.GetDirectoryNames(strPath + "/*"))
            {
                length += countFilesNumber(isf, strPath + "/" + str);
            }
            return length;
        }

        public static bool createDir(string strPath)
        {
            if (!string.IsNullOrEmpty(strPath))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (file.DirectoryExists(strPath))
                        {
                            return true;
                        }
                        file.CreateDirectory(strPath);
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Log.e("StorageIO", "createDir  path fail=" + exception);
                }
            }
            return false;
        }

        public static void deleteAllUserData()
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    deleteDir(file, "storage", true);
                }
            }
            catch
            {
                Log.d("storage", "delete file fail");
            }
        }

        public static void deleteDir(IsolatedStorageFile isf, string path, bool bDeleteDir = true)
        {
            foreach (string str in isf.GetFileNames(path + "/*"))
            {
                isf.DeleteFile(path + "/" + str);
                Log.d("storage", "delete file " + path + "/" + str);
            }
            foreach (string str2 in isf.GetDirectoryNames(path + "/*"))
            {
                deleteDir(isf, path + "/" + str2, bDeleteDir);
            }
            if (bDeleteDir)
            {
                isf.DeleteDirectory(path);
                Log.d("storage", "delete dir " + path);
            }
        }

        public static bool deleteFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (file.FileExists(fileName))
                        {
                            file.DeleteFile(fileName);
                        }
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Log.e("storage", string.Concat(new object[] { "DeleteFile file fail ", fileName, " \n", exception }));
                }
            }
            return false;
        }

        public static void deleteMsgData()
        {
            string[] strArray = new string[] { getThumbnailPath(), getImagePath(), getVoicePath(), getVideoPath() };
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    foreach (string str in strArray)
                    {
                        foreach (string str2 in file.GetFileNames(str + "/*"))
                        {
                            if (file.FileExists(str + "/" + str2))
                            {
                                file.DeleteFile(str + "/" + str2);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Log.e("storage", "deleteMsgData  exception  " + exception);
            }
        }

        public static bool dirExists(string path)
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return file.DirectoryExists(path);
                }
            }
            catch (Exception exception)
            {
                Log.e("storage", "fileExists Exception " + exception);
            }
            return false;
        }

        public static List<string> dirsDetail(string strPath)
        {
            if (!string.IsNullOrEmpty(strPath))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.DirectoryExists(strPath))
                        {
                            return null;
                        }
                        List<string> list = new List<string>();
                        foreach (string str in file.GetDirectoryNames(strPath + "/*"))
                        {
                            list.Add(strPath + "/" + str);
                        }
                        return list;
                    }
                }
                catch (Exception exception)
                {
                    Log.e("StorageIO", string.Concat(new object[] { "dirsDetail Exception=", strPath, " \n", exception }));
                }
            }
            return null;
        }

        public static int dirsNumber(string strPath)
        {
            if (!string.IsNullOrEmpty(strPath))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.DirectoryExists(strPath))
                        {
                            return 0;
                        }
                        return countDirsNumber(file, strPath);
                    }
                }
                catch (Exception exception)
                {
                    Log.e("StorageIO", string.Concat(new object[] { "countFiles Exception=", strPath, " \n", exception }));
                }
            }
            return 0;
        }

        public static bool emptyDir(string strPath)
        {
            if (!string.IsNullOrEmpty(strPath))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.DirectoryExists(strPath))
                        {
                            return false;
                        }
                        foreach (string str in file.GetFileNames(strPath + "/*"))
                        {
                            file.DeleteFile(strPath + "/" + str);
                            Log.d("StorageIO", "delete file " + strPath + "/" + str);
                        }
                        foreach (string str2 in file.GetDirectoryNames(strPath + "/*"))
                        {
                            deleteDir(file, strPath + "/" + str2, true);
                            Log.d("StorageIO", "delete dir " + strPath + "/" + str2);
                        }
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Log.e("StorageIO", string.Concat(new object[] { "emptyDir clear path fail=", strPath, " \n", exception }));
                }
            }
            return false;
        }

        public static bool emptyFile(string strPath)
        {
            if (!string.IsNullOrEmpty(strPath))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.DirectoryExists(strPath))
                        {
                            return false;
                        }
                        deleteDir(file, strPath, false);
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Log.e("StorageIO", string.Concat(new object[] { "emptyFile clear path fail=", strPath, " \n", exception }));
                }
            }
            return false;
        }

        public static bool fileExists(string path)
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return file.FileExists(path);
                }
            }
            catch (Exception exception)
            {
                Log.e("storage", "fileExists Exception " + exception);
            }
            return false;
        }

        public static long fileLength(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.FileExists(path))
                        {
                            return 0L;
                        }
                        using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(path, FileMode.Open, file))
                        {
                            return stream.Length;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log.e("storage", "fileLength Exception " + exception);
                }
            }
            return 0L;
        }

        public static List<FileDetail> filesDetail(string strPath)
        {
            if (!string.IsNullOrEmpty(strPath))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.DirectoryExists(strPath))
                        {
                            return null;
                        }
                        return getFilesDetail(strPath, file, new List<FileDetail>());
                    }
                }
                catch (Exception exception)
                {
                    Log.e("StorageIO", string.Concat(new object[] { "getFileDetail Exception=", strPath, " \n", exception }));
                }
            }
            return null;
        }

        public static int filesNumber(string strPath)
        {
            if (!string.IsNullOrEmpty(strPath))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.DirectoryExists(strPath))
                        {
                            return 0;
                        }
                        return countFilesNumber(file, strPath);
                    }
                }
                catch (Exception exception)
                {
                    Log.e("StorageIO", string.Concat(new object[] { "countFiles Exception=", strPath, " \n", exception }));
                }
            }
            return 0;
        }

        public static string getAccountPath(string userName)
        {
            return ("storage/" + userName + "/account");
        }

        public static string getAttachmentPath()
        {
            return (userPath + "/attachment");
        }

        public static string getBannerPath()
        {
            return (userPath + "/banner");
        }

        public static string getConfigPath()
        {
            return (userPath + "/config");
        }

        public static string getConversationBgPath()
        {
            return (userPath + "/Conversation_bg");
        }

        public static string getCustomImagePath()
        {
            return (userPath + "/custom_img");
        }

        public static string getDatabasePath()
        {
            return (userPath + "/db");
        }

        public static long getFileExistTime(string path)
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (!file.FileExists(path))
                    {
                        return 0L;
                    }
                    return (long) DateTime.Now.Subtract(file.GetCreationTime(path).DateTime).TotalSeconds;
                }
            }
            catch (Exception exception)
            {
                Log.e("storage", "getCreationTime file fail" + exception);
            }
            return 0L;
        }

        private static List<FileDetail> getFilesDetail(string strPath, IsolatedStorageFile isf, List<FileDetail> fileList)
        {
            foreach (string str in isf.GetFileNames(strPath + "/*"))
            {
                FileDetail item = new FileDetail {
                    strFileName = strPath + "/" + str
                };
                TimeSpan span = (TimeSpan) (DateTime.Now - isf.GetLastAccessTime(item.strFileName));
                item.idleMinutes = Math.Abs((int) span.TotalMinutes);
                fileList.Add(item);
            }
            foreach (string str2 in isf.GetDirectoryNames(strPath + "/*"))
            {
                getFilesDetail(strPath + "/" + str2, isf, fileList);
            }
            return fileList;
        }

        public static string getGlobaDataPath()
        {
            return "data";
        }

        public static string getGlobalConfigPath()
        {
            return "config";
        }

        public static string getHeadImgHDPath()
        {
            return (userPath + "/head_img_hd");
        }

        public static string getHeadImgPath()
        {
            return (userPath + "/head_img");
        }

        public static string getHeadImgQQPath()
        {
            return (userPath + "/head_img_qq");
        }

        public static string getHeadImgTempPath()
        {
            return (userPath + "/head_img_tmp");
        }

        public static string getImagePath()
        {
            return (userPath + "/image");
        }

        public static string getMassDataPath()
        {
            return (userPath + "/mass_data");
        }

        public static string getPluginPath(string pluginName)
        {
            return (userPath + "/plugin/" + pluginName);
        }

        public static string getPluginRootPath()
        {
            return (userPath + "/plugin");
        }

        public static string getRooDir()
        {
            return "storage";
        }

        public static string getShakePath()
        {
            return (userPath + "/shake");
        }

        public static string getSnsPath()
        {
            return (userPath + "/sns");
        }

        public static string getTempPath()
        {
            return (userPath + "/temp");
        }

        public static string getThumbnailPath()
        {
            return (userPath + "/thumbnail");
        }

        public static string getUserPath()
        {
            return userPath;
        }

        public static string getUserPath(string dir)
        {
            return (userPath + dir);
        }

        public static string getVideoPath()
        {
            return (userPath + "/video");
        }

        public static string getVoicePath()
        {
            return (userPath + "/voice");
        }

        private static string getXmlFileName()
        {
            return (userPath + "/config/io.xml");
        }

        public static void initGlobalConfigRootDir()
        {
            initRootDir();
        }

        public static void initRootDir()
        {
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!file.DirectoryExists("storage"))
                {
                    file.CreateDirectory("storage");
                }
                if (!file.DirectoryExists("config"))
                {
                    file.CreateDirectory("config");
                }
                if (!file.DirectoryExists("data"))
                {
                    file.CreateDirectory("data");
                }
            }
        }

        //public static void initUserDataDir(string userName)
        //{
        //    userPath = "storage/" + userName;
        //    if (needInitUserDir())
        //    {
        //        using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
        //        {
        //            file.CreateDirectory(userPath);
        //            foreach (string str in userDataDirList)
        //            {
        //                file.CreateDirectory(userPath + str);
        //            }
        //        }
        //        setInitUserReady();
        //    }
        //}

        //private static bool needInitUserDir()
        //{
        //    StorageIOInfo info = StorageXml.loadObject<StorageIOInfo>(getXmlFileName());
        //    return ((info == null) || (info.lastInitClientVersion != ConstantsProtocol.CLIENT_VERSION));
        //}

        public static byte[] readFromFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.FileExists(fileName))
                        {
                            return null;
                        }
                        using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Open, file))
                        {
                            if (stream.Length <= 0L)
                            {
                                return null;
                            }
                            byte[] buffer = new byte[stream.Length];
                            int num = stream.Read(buffer, 0, buffer.Length);
                            if (num != stream.Length)
                            {
                                Log.e("StorageIO", string.Concat(new object[] { "readFromFile error, read count = ", stream.Length, " , ret = ", num }));
                                return null;
                            }
                            return buffer;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log.e("storage", string.Concat(new object[] { "readFromFile file fail ", fileName, " ", exception }));
                }
            }
            return null;
        }

        public static byte[] readFromFile(string fileName, int offset, int count)
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Open, file))
                    {
                        stream.Seek((long) offset, SeekOrigin.Begin);
                        byte[] buffer = new byte[count];
                        int num = stream.Read(buffer, 0, buffer.Length);
                        if (num != count)
                        {
                            Log.e("StorageIO", string.Concat(new object[] { "readFromFile error, read count = ", count, " , ret = ", num }));
                            return null;
                        }
                        return buffer;
                    }
                }
            }
            catch (Exception exception)
            {
                Log.d("storage", "readFromFile file fail" + exception);
            }
            return null;
        }

        //private static void setInitUserReady()
        //{
        //    StorageIOInfo item = StorageXml.loadObject<StorageIOInfo>(getXmlFileName());
        //    if ((item == null) || (item.lastInitClientVersion != ConstantsProtocol.CLIENT_VERSION))
        //    {
        //        if (item == null)
        //        {
        //            item = new StorageIOInfo();
        //        }
        //        item.lastInitClientVersion = ConstantsProtocol.CLIENT_VERSION;
        //        StorageXml.saveObject<StorageIOInfo>(item, getXmlFileName());
        //    }
        //}

        public static void test()
        {
        }

        public static void testCreateFile()
        {
            int num;
            //DebugEx.getTimeSpan();
            byte[] buffer = new byte[10];
            IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
            userStoreForApplication.CreateDirectory("test");
            for (num = 0; num < 300; num++)
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("test/" + num, FileMode.Create, userStoreForApplication))
                {
                    stream.Write(buffer, 0, 10);
                }
            }
            for (num = 0; num < 300; num++)
            {
                userStoreForApplication.GetLastAccessTime("test/" + num);
            }
            userStoreForApplication.Dispose();
        }

        public static void testDeletFile()
        {
            //DebugEx.getTimeSpan();
            IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
            for (int i = 0; i < 300; i++)
            {
                userStoreForApplication.DeleteFile("test/" + i);
            }
            userStoreForApplication.Dispose();
        }

        public static bool writeToFile(string fileName, byte[] data, bool bCreateDir = false)
        {
            if (!string.IsNullOrEmpty(fileName) && (data != null))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (bCreateDir)
                        {
                            string directoryName = Path.GetDirectoryName(fileName);
                            if (!file.DirectoryExists(directoryName))
                            {
                                file.CreateDirectory(directoryName);
                            }
                        }
                        using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.Create, file))
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Log.d("storage", "writeToFile file fail" + exception);
                }
            }
            return false;
        }

        public static void writeToFile(string fileName, int offset, params byte[][] dataList)
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, file))
                    {
                        stream.Seek((long) offset, SeekOrigin.Begin);
                        foreach (byte[] buffer in dataList)
                        {
                            stream.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Log.e("storage", "writeToFile file fail" + exception);
            }
        }
    }
}

