namespace MicroMsg.Network
{
    using System;

    public class Queue
    {
        public ListNode _head = new ListNode(null, 0);
        public int _size;
        public ListNode _tail;

        public Queue()
        {
            this._tail = this._head;
            this._size = 0;
        }

        public void changeID(int old, int newid)
        {
            if (this._size != 0)
            {
                for (ListNode node = this._head._next; node != null; node = node._next)
                {
                    if (node._id == old)
                    {
                        node._id = newid;
                        return;
                    }
                }
            }
        }

        public object get(int id)
        {
            if (this._size != 0)
            {
                for (ListNode node = this._head._next; node != null; node = node._next)
                {
                    if (node._id == id)
                    {
                        return node._obj;
                    }
                }
            }
            return null;
        }

        public bool isEmpty()
        {
            return (this._size == 0);
        }

        public void moveToTail(int id)
        {
            if (this._size >= 2)
            {
                ListNode node = this._head;
                for (ListNode node2 = this._head._next; node2 != null; node2 = node2._next)
                {
                    if ((node2._id == id) && (node2._id != this._tail._id))
                    {
                        node._next = node2._next;
                        node2._next = null;
                        this._tail._next = node2;
                        this._tail = this._tail._next;
                        return;
                    }
                    node = node2;
                }
            }
        }

        public void putToHead(object obj, int id)
        {
            ListNode node = new ListNode(null, 0);
            this._head._obj = obj;
            this._head._id = id;
            node._next = this._head;
            this._head = node;
            this._size++;
        }

        public void putToTail(object obj, int id)
        {
            this._tail._next = new ListNode(obj, id);
            this._tail = this._tail._next;
            this._size++;
        }

        public bool remove(int id)
        {
            if (this._size != 0)
            {
                ListNode node = this._head._next;
                ListNode node2 = this._head;
                while (node != null)
                {
                    if (node._id == id)
                    {
                        node2._next = node._next;
                        this._size--;
                        if (node._next == null)
                        {
                            this._tail = node2;
                        }
                        return true;
                    }
                    node2 = node;
                    node = node._next;
                }
            }
            return false;
        }

        public void reset()
        {
            this._head._next = null;
            this._tail = this._head;
            this._size = 0;
        }

        public int size()
        {
            return this._size;
        }
    }
}

