namespace MicroMsg.Network
{
    using System;

    public class ListNode
    {
        public int _id;
        public ListNode _next;
        public object _obj;

        public ListNode(object obj, int id)
        {
            this._obj = obj;
            this._id = id;
            this._next = null;
        }
    }
}

