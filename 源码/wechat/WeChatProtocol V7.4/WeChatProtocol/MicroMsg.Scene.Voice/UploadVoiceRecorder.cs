//namespace MicroMsg.Scene.Voice
//{
//    using System;

//    public class UploadVoiceRecorder
//    {
//        private const string TAG = "UploadVoiceRecorder";

//        public static void checkDeviceStop()
//        {
//        }

//        public static UploadVoiceContext getCurrentContext()
//        {
//            if (RecorderVoice.mVoiceHandler != null)
//            {
//                return (RecorderVoice.mVoiceHandler.mRecorderContext as UploadVoiceContext);
//            }
//            return null;
//        }

//        public static bool isRunning()
//        {
//            return RecorderVoice.isRunning();
//        }

//        public static void start(UploadVoiceContext voiceContext)
//        {
//            RecorderVoice.start(voiceContext, null);
//        }

//        public static bool stop()
//        {
//            return RecorderVoice.stop();
//        }
//    }
//}

