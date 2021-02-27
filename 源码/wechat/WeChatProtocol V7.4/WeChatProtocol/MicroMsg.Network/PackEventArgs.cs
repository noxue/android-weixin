namespace MicroMsg.Network
{
    using System;

    public class PackEventArgs : EventArgs
    {
        public PackResult result;

        public bool isSuccess()
        {
            return (this.result == PackResult.SUCCESS);
        }
    }
}

