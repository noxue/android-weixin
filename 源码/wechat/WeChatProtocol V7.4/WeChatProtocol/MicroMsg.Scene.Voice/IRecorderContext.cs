namespace MicroMsg.Scene.Voice
{
    using System;

    public interface IRecorderContext
    {
        bool isNeedData();
        bool onAppendOutputData(byte[] buffer, int offset, int count);
        void onStartup(int creatTime, object args);
        void onStop(byte[] voiceBuffer, int dataLength, int timeLength);
        void onVoiceTimeChanged(int timeLength);
    }
}

