namespace MicroMsg.Storage
{
    using System;
    using System.Reflection;
    using System.Xml.Linq;

    public abstract class TloXmlItem
    {
        public virtual void dump(ref string strInfo)
        {
            strInfo = strInfo + base.GetType().Name + "={";
            foreach (FieldInfo info in base.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!info.FieldType.IsGenericType && (info.MemberType == MemberTypes.Field))
                {
                    if (info.FieldType.BaseType == typeof(TloXmlItem))
                    {
                        TloXmlItem item = info.GetValue(this) as TloXmlItem;
                        if (item != null)
                        {
                            item.dump(ref strInfo);
                        }
                    }
                    else if ((info.Name == "strUrl") || (info.Name == "strThumb"))
                    {
                        string str = info.GetValue(this) as string;
                        if (string.IsNullOrEmpty(str))
                        {
                            strInfo = strInfo + info.Name + "= ";
                        }
                        else
                        {
                            string str2 = strInfo;
                            strInfo = str2 + info.Name + "=" + str.Substring(0, 7) + " ";
                        }
                    }
                    else
                    {
                        object obj2 = strInfo;
                        strInfo = string.Concat(new object[] { obj2, info.Name, "=", info.GetValue(this), " " });
                    }
                }
            }
            strInfo = strInfo + "}";
        }

        public virtual void dumpObj()
        {
            string strInfo = "";
            this.dump(ref strInfo);
        }

        public static bool IsEmpty(XElement xml)
        {
            if ((xml != null) && !xml.IsEmpty)
            {
                foreach (XElement element in xml.Elements())
                {
                    if (!element.IsEmpty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

