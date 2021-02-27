namespace MicroMsg.Scene
{
    using MicroMsg.Protocol;
    using MicroMsg.Storage;
    using System;

    public class MsgResultRet
    {
        public ChatMsg msgChat;
        public RetConst retCode = RetConst.MM_ERR_SYS;

        public MsgResultRet(ChatMsg msg, RetConst ret)
        {
            this.msgChat = msg;
            this.retCode = ret;
        }
    }
}

