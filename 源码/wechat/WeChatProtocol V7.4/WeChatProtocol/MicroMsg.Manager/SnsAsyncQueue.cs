namespace MicroMsg.Manager
{
    using MicroMsg.Storage;
    using System;
    using System.Collections.Generic;


    public class SnsAsyncQueue
    {
        public List<ulong> delList = new List<ulong>();
        public List<SnsOpLog> OpList = new List<SnsOpLog>();
    }
}

