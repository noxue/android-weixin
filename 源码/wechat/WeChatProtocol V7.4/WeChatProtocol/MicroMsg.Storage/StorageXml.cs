namespace MicroMsg.Storage
{
    using MicroMsg.Common.Utils;
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Reflection;

    using System.Runtime.Serialization;
    using System.Text;
 
 

    public class StorageXml
    {
        public static void deleteFile<T>(string xmlFilePath = null) where T: class
        {
            if (xmlFilePath == null)
            {
                xmlFilePath = StorageIO.getRooDir() + "/" + typeof(T).Name + ".xml";
            }
            StorageIO.deleteFile(xmlFilePath);
        }

        public static void FilterInvalidCharacters(object o)
        {
            try
            {
                foreach (FieldInfo info in o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    if ((info.MemberType == MemberTypes.Field) && (info.FieldType == typeof(string)))
                    {
                        string str = info.GetValue(o) as string;
                        if (!string.IsNullOrEmpty(str))
                        {
                            string str2 = FilterString(str);
                            if (!(str2 == str))
                            {
                                info.SetValue(o, str2);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private static string FilterString(string tmp)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in tmp)
            {
                int num = ch;
                if ((((num >= 0) && (num <= 8)) || ((num >= 11) && (num <= 12))) || ((num >= 14) && (num <= 0x1f)))
                {
                    builder.Append(' ');
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        public static T loadFromBuffer<T>(byte[] xmlBuffer) where T: class
        {
            if ((xmlBuffer != null) && (xmlBuffer.Length > 0))
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream(xmlBuffer))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(T));

                        return (serializer.ReadObject(stream) as T);
                    }
                }
                catch (Exception exception)
                {
                    Log.e("storage", "StorageXml read objcet fail " + exception);
                }
            }
            return default(T);
        }

        public static T loadFromFile<T>(string xmlFilePath) where T: class
        {
            if (!string.IsNullOrEmpty(xmlFilePath))
            {
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        if (!file.FileExists(xmlFilePath))
                        {
                            return default(T);
                        }
                        using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(xmlFilePath, FileMode.Open, file))
                        {
                            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                            return (serializer.ReadObject(stream) as T);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log.e("storage", "StorageXml read objcet fail " + exception);
                }
            }
            return default(T);
        }

        public static T loadFromStream<T>(Stream xmlStram) where T: class
        {
            if (xmlStram != null)
            {
                try
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    return (serializer.ReadObject(xmlStram) as T);
                }
                catch (Exception exception)
                {
                    Log.e("storage", "StorageXml read stream fail " + exception);
                }
            }
            return default(T);
        }

        public static T loadObject<T>(string xmlFilePath = null) where T: class
        {
            if (xmlFilePath == null)
            {
                xmlFilePath = StorageIO.getRooDir() + "/" + typeof(T).Name + ".xml";
            }
            XmlSeriData<T> data = loadFromFile<XmlSeriData<T>>(xmlFilePath);
            if (data != null)
            {
                return data.data;
            }
            return default(T);
        }


        public static bool saveObject<T>(T item, string xmlFilePath = null) where T: class
        {
            XmlSeriData<T> dataInfo = new XmlSeriData<T>(item);
            if (xmlFilePath == null)
            {
                xmlFilePath = StorageIO.getRooDir() + "/" + typeof(T).Name + ".xml";
            }
            return saveToFile<XmlSeriData<T>>(dataInfo, xmlFilePath);
        }

        public static byte[] saveToBuffer<T>(T dataInfo) where T: class
        {
            if (dataInfo != null)
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        new DataContractSerializer(typeof(T)).WriteObject(stream, dataInfo);
                        return stream.ToArray();
                    }
                }
                catch (Exception exception)
                {
                    Log.e("storage", "StorageXml save MemoryStream fail " + exception);
                }
            }
            return null;
        }

        public static bool saveToFile<T>(T dataInfo, string xmlFilePath) where T: class
        {
            if (dataInfo != null)
            {
                if (string.IsNullOrEmpty(xmlFilePath))
                {
                    return false;
                }
                try
                {
                    using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(xmlFilePath, FileMode.Create, file))
                        {
                            new DataContractSerializer(typeof(T)).WriteObject(stream, dataInfo);
                            return true;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log.e("storage", "StorageXml save objcet fail " + exception);
                }
            }
            return false;
        }

        public static void test()
        {
            //testXmlData data = loadFromFile<testXmlData>("test.xml");
            //if (data == null)
            //{
            //    testXmlData dataInfo = new testXmlData {
            //        aaa = 0x6f,
            //        bbb = 0xde
            //    };
            //    saveToFile<testXmlData>(dataInfo, "test.xml");
            //}
            //else
            //{
            //    data.aaa = 0x14d;
            //}
        }
    }
}

