namespace MicroMsg.Scene.Voice
{
    using MicroMsg.Protocol;
    using MicroMsg.Storage;
    using System;

    public class UploadVoiceEventArgs
    {
        public ChatMsg chatMsg;
        public RetConst retCode;

        public UploadVoiceEventArgs(ChatMsg msg, RetConst ret)
        {
            this.chatMsg = msg;
            this.retCode = ret;
        }
    }
}

