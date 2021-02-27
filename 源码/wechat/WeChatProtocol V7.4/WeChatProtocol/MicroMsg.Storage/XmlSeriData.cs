namespace MicroMsg.Storage
{
    using System;
    using System.Runtime.Serialization;

    public class XmlSeriData<T> where T: class
    {

        public T data;

        public XmlSeriData()
        {
        }

        public XmlSeriData(T args)
        {
            this.data = args;
        }
    }
}

