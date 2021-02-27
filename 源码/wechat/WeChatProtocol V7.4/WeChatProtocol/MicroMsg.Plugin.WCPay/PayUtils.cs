namespace MicroMsg.Plugin.WCPay
{
    using MicroMsg.Common.Utils;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
 

    internal class PayUtils
    {
        private const string TAG = "PayUtils";

        public static Dictionary<string, object> deserializeToDictionary(string jo)
        {
            try
            {
                Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> pair in dictionary)
                {
                    if (pair.Value.GetType().FullName.Contains("Newtonsoft.Json.Linq.JObject"))
                    {
                        Dictionary<string, object> dictionary3 = JsonConvert.DeserializeObject<Dictionary<string, object>>(pair.Value.ToString());
                        if (dictionary3 != null)
                        {
                            dictionary2.Add(pair.Key, dictionary3);
                        }
                    }
                    else
                    {
                        dictionary2.Add(pair.Key, pair.Value);
                    }
                }
                return dictionary2;
            }
            catch (Exception exception)
            {
                Log.e("PayUtils", exception.Message);
                return null;
            }
        }

        public static Dictionary<string, object> deserializeToDictionaryEx(string jo)
        {
            if (string.IsNullOrWhiteSpace(jo))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> pair in dictionary)
                {
                    string fullName = pair.Value.GetType().FullName;
                    if (fullName.Contains("Newtonsoft.Json.Linq.JObject"))
                    {
                        Dictionary<string, object> dictionary3 = deserializeToDictionaryEx(pair.Value.ToString());
                        if (dictionary3 != null)
                        {
                            dictionary2.Add(pair.Key, dictionary3);
                        }
                    }
                    else if (fullName.Contains("Newtonsoft.Json.Linq.JArray"))
                    {
                        List<object> list = JsonConvert.DeserializeObject<List<object>>(pair.Value.ToString());
                        if (list != null)
                        {
                            List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
                            int num = 0;
                            foreach (object obj2 in list)
                            {
                                string str2 = obj2.GetType().FullName;
                                if (str2.Contains("Newtonsoft.Json.Linq.JObject") || str2.Contains("Newtonsoft.Json.Linq.JArray"))
                                {
                                    string str3 = obj2.ToString();
                                    if (!string.IsNullOrEmpty(str3))
                                    {
                                        Dictionary<string, object> item = deserializeToDictionaryEx(str3);
                                        if (item != null)
                                        {
                                            list2.Add(item);
                                        }
                                    }
                                }
                                else
                                {
                                    Dictionary<string, object> dictionary5 = new Dictionary<string, object>();
                                    dictionary5.Add(num.ToString(), obj2.ToString());
                                    num++;
                                    list2.Add(dictionary5);
                                }
                            }
                            dictionary2.Add(pair.Key, list2);
                        }
                    }
                    else
                    {
                        dictionary2.Add(pair.Key, pair.Value);
                    }
                }
                return dictionary2;
            }
            catch (Exception exception)
            {
                Log.e("PayUtils", exception.Message);
                return null;
            }
        }

     
        public static List<Dictionary<string, object>> getSafeArray(Dictionary<string, object> dic, string key)
        {
            if (dic == null)
            {
                return new List<Dictionary<string, object>>();
            }
            if (!dic.ContainsKey(key))
            {
                return new List<Dictionary<string, object>>();
            }
            List<Dictionary<string, object>> list = dic[key] as List<Dictionary<string, object>>;
            if (list == null)
            {
                return new List<Dictionary<string, object>>();
            }
            return list;
        }

        public static Dictionary<string, object> getSafeDic(Dictionary<string, object> dic, string key)
        {
            if (dic == null)
            {
                return new Dictionary<string, object>();
            }
            if (!dic.ContainsKey(key))
            {
                return new Dictionary<string, object>();
            }
            Dictionary<string, object> dictionary = dic[key] as Dictionary<string, object>;
            if (dictionary == null)
            {
                return new Dictionary<string, object>();
            }
            return dictionary;
        }

        public static int getSafeInt(Dictionary<string, object> dic, string key)
        {
            return Util.stringToInt(getSafeValue(dic, key));
        }

        public static long getSafeLong(Dictionary<string, object> dic, string key)
        {
            return Util.stringToLong(getSafeValue(dic, key));
        }

        public static uint getSafeUInt(Dictionary<string, object> dic, string key)
        {
            return (uint) Util.stringToInt(getSafeValue(dic, key));
        }

        public static ulong getSafeULong(Dictionary<string, object> dic, string key)
        {
            return (ulong) Util.stringToLong(getSafeValue(dic, key));
        }

        public static string getSafeValue(Dictionary<string, object> dic, string key)
        {
            if (dic == null)
            {
                return "";
            }
            if (!dic.ContainsKey(key))
            {
                return "";
            }
            object obj2 = dic[key];
            if (obj2 == null)
            {
                return "";
            }
            if (obj2 is string)
            {
                return (obj2 as string);
            }
            return obj2.ToString();
        }

        //public static bool isBindCardInfoFull(WCPayBindCardInfo info)
        //{
        //    return ((((info.m_physicalDayTranscationLimit > 0) && (info.m_physicalSignalTranscationLimit > 0)) && (info.m_virtualDayTranscationLimit > 0)) && (info.m_virtualSingalTranscationLimit > 0));
        //}

        public static void printfDictionary(string tag, Dictionary<string, object> dic)
        {
            if (dic != null)
            {
                using (Dictionary<string, object>.Enumerator enumerator = dic.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<string, object> current = enumerator.Current;
                    }
                }
            }
        }
    }
}

