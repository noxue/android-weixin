namespace MicroMsg.Storage
{
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;


    public class OpLog : StorageItem
    {

 
    
        public byte[] bytesCmdBuf;
   
        public int nCmdId;

        public int nID;

        public OpLog()
        {
        }

        public OpLog(int id, byte[] data)
        {
            this.nCmdId = id;
            this.bytesCmdBuf = data;
        }
    }
}

