namespace MicroMsg.Scene
{
    using MicroMsg.Common.Utils;
    using MicroMsg.Network;
    using MicroMsg.Storage;
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.InteropServices;
    using Google.ProtocolBuffers;

    public abstract class DownloadContextBase<TRequest, TResponse, TBuilder> : IContextBase where TRequest: AbstractMessageLite<TRequest, TBuilder> where TBuilder: AbstractBuilderLite<TRequest, TBuilder>
    {
        public class BlockItemInfo
        {
            public int end;
            public int offset;
            public int start;
        }

        public class BlocksInfo
        {
            public DownloadContextBase<TRequest, TResponse, TBuilder>.BlockItemInfo[] blockInfo;
            public int blockNum;
            public bool hasInfo;

            public BlocksInfo()
            {
                this.blockInfo = new DownloadContextBase<TRequest, TResponse, TBuilder>.BlockItemInfo[6];
            }
        }
        public int BLOCK_MIN_SIZE;
        private const int BLOCKINFO_LEN = 0x4c;
        private double creaeteTimestamp;
        private const int MAXBLOCK_NUM = 6;
        private BlocksInfo  mBlockInfo;
        public int mEndDataPos;
        public DownNetSceneBaseEx<TRequest, TResponse, TBuilder>[] mMultiSceneList;
        public Stream mSaveFileStream = new MemoryStream();
        public DownNetSceneBaseEx<TRequest, TResponse, TBuilder> mSingleScene;
        private int mSpeedSceneId;
        private int mStartDataPos;
        private const string TAG = "DownContextBase";

        protected DownloadContextBase()
        {
            this.creaeteTimestamp = Util.getNowMilliseconds();
            this.mBlockInfo = new BlocksInfo();
            this.BLOCK_MIN_SIZE = 0x7d000;//0xf000 ==60KB  153600 ==150KB
        }

        public void appendDownData(byte[] buf, uint startPos, uint ndataLen)
        {
            if (((buf != null) && (ndataLen > 0)) && (this.mSaveFileStream != null))
            {
                try
                {
                    this.mSaveFileStream.Seek((long) startPos, SeekOrigin.Begin);
                    this.mSaveFileStream.Write(buf, 0, (int) ndataLen);
                }
                catch (Exception exception)
                {
                    Log.e("DownContextBase", exception.Message);
                }
            }
        }

        public void closeAllScene()
        {
            if (this.mMultiSceneList != null)
            {
                Log.i("DownContextBase", string.Concat(new object[] { "[*]multi scenes(", this.mMultiSceneList.Length, ") finished ,run time = ", Util.getNowMilliseconds() - this.creaeteTimestamp, ",rate = ", (int) (((double) (this.mEndDataPos - this.mStartDataPos)) / (Util.getNowMilliseconds() - this.creaeteTimestamp)), "KBps" }));
                for (int i = 0; i < this.mMultiSceneList.Length; i++)
                {
                    this.mMultiSceneList[i].doCompleted();
                    this.mMultiSceneList[i] = null;
                }
                this.mMultiSceneList = null;
            }
            if (this.mSingleScene != null)
            {
                Log.i("DownContextBase", string.Concat(new object[] { "[*]single scene finished ,run time = ", Util.getNowMilliseconds() - this.creaeteTimestamp, ",rate = ", (int) (((double) (this.mEndDataPos - this.mStartDataPos)) / (Util.getNowMilliseconds() - this.creaeteTimestamp)), "KBps" }));
                this.mSingleScene.doCompleted();
                this.mSingleScene = null;
            }
            if (this.mSaveFileStream != null)
            {
                this.mSaveFileStream.Close();
                this.mSaveFileStream.Dispose();
                this.mSaveFileStream = null;
            }
        }

        public void closeFileStream()
        {
            if (this.mSaveFileStream != null)
            {
                this.mSaveFileStream.Close();
                this.mSaveFileStream = null;
            }
        }

        protected abstract DownNetSceneBaseEx<TRequest, TResponse, TBuilder> createDownSceneInstance();

        public byte[] getBlockInfo()
        {
            if (this.mMultiSceneList == null)
            {
                return null;
            }
            byte[] buf = new byte[0x4c];
            int offset = 0;
            Util.writeInt(this.mMultiSceneList.Length, ref buf, ref offset);
            for (int i = 0; i < this.mMultiSceneList.Length; i++)
            {
                Util.writeInt(this.mMultiSceneList[i].mStartPos, ref buf, ref offset);
                Util.writeInt(this.mMultiSceneList[i].mOffsetPos, ref buf, ref offset);
                Util.writeInt(this.mMultiSceneList[i].mEndPos, ref buf, ref offset);
            }
            return buf;
        }

        public virtual bool isRunning()
        {
            return false;
        }

        public bool needMultiBlock()
        {
            if (this.mEndDataPos < (this.mStartDataPos + 0x4b000))
            {
                return false;
            }

            return true;
        }

        public virtual bool needToClean()
        {
            return false;
        }

        public virtual bool needToHandle()
        {
            return false;
        }

        public void onBlockCompleted(int sceneID)
        {
            if ((sceneID == this.mSpeedSceneId) && (this.mSingleScene != null))
            {
                this.startSceneFromNewBlock(this.mStartDataPos + (this.BLOCK_MIN_SIZE * 2), this.mEndDataPos, this.mSingleScene.mMiniRTT);
                Log.i("DownContextBase", string.Concat(new object[] { "[*]single scene finished ,run time = ", Util.getNowMilliseconds() - this.creaeteTimestamp, ",rate = ", (int) (((double) (this.BLOCK_MIN_SIZE * 2)) / (Util.getNowMilliseconds() - this.creaeteTimestamp)), "KBps" }));
                this.mSingleScene.doCompleted();
                this.mSingleScene = null;
            }
        }

        protected abstract string onPrepareDataFilePath(IsolatedStorageFile currentIsolatedStorage);


        public void startSceneFromNewBlock(int startPos, int endPos, int rtt)
        {
            int num = endPos - startPos;
            if (num > 0)
            {
                int num2 = ((num + this.BLOCK_MIN_SIZE) - 1) / this.BLOCK_MIN_SIZE;
                int num3 = 2;
                if (rtt < 500)
                {
                    num3 = 4;
                }
                if (rtt > 0x3e8)
                {
                    num3 = 1;
                }
                int num4 = ((num2 + num3) - 1) / num3;
                this.mMultiSceneList = new DownNetSceneBaseEx<TRequest, TResponse, TBuilder>[num3];
                int num5 = this.BLOCK_MIN_SIZE * num4;
                Log.i("DownContextBase", "start multi-scene for down , scenenum = " + num3);
                int num6 = 0;
                int offsetPos = startPos;
                for (int i = 0; i < num3; i++)
                {
                    if ((num5 + offsetPos) > endPos)
                    {
                        num5 = endPos - offsetPos;
                    }
                    this.mMultiSceneList[i] = this.createDownSceneInstance();
                    this.mMultiSceneList[i].doScene((DownloadContextBase<TRequest, TResponse, TBuilder>) this, num6, offsetPos, offsetPos + num5, i >= 3);
                    num6 = offsetPos + num5;
                    offsetPos = num6;
                }
            }
        }

        public bool startSceneWithRange(int transLen, int totalLen, bool singleMode = false)
        {
            if (transLen >= totalLen)
            {
                return false;
            }
            this.mSpeedSceneId = 0;
            this.mStartDataPos = transLen;
            this.mEndDataPos = totalLen;
            if (singleMode)
            {
                Log.i("DownContextBase", "start singel-scene for cdn down .. ");
                this.mSingleScene = this.createDownSceneInstance();
                return this.mSingleScene.doScene((DownloadContextBase<TRequest, TResponse, TBuilder>) this, 0, this.mStartDataPos, this.mEndDataPos, false);
            }
            if (this.mBlockInfo.hasInfo)
            {
                return this.startSceneWithResumeBlock();
            }
            this.mSingleScene = this.createDownSceneInstance();
            if (!this.needMultiBlock())
            {
                Log.i("DownContextBase", "start singel-scene for down .. ");
                return this.mSingleScene.doScene((DownloadContextBase<TRequest, TResponse, TBuilder>) this, 0, this.mStartDataPos, this.mEndDataPos, false);
            }
            bool flag = this.mSingleScene.doScene((DownloadContextBase<TRequest, TResponse, TBuilder>) this, 0, this.mStartDataPos, this.mStartDataPos + (this.BLOCK_MIN_SIZE * 2), false);
            this.mSpeedSceneId = this.mSingleScene.mSceneID;
            return flag;
        }

        public bool startSceneWithResumeBlock()
        {
            if (this.mBlockInfo.blockNum <= 0)
            {
                return false;
            }
            this.mMultiSceneList = new DownNetSceneBaseEx<TRequest, TResponse, TBuilder>[this.mBlockInfo.blockNum];
            Log.i("DownContextBase", "resume multi-scene for down , scenenum = " + this.mBlockInfo.blockNum);
            bool flag = true;
            for (int i = 0; i < this.mBlockInfo.blockNum; i++)
            {
                this.mMultiSceneList[i] = this.createDownSceneInstance();
                if (!this.mMultiSceneList[i].doScene((DownloadContextBase<TRequest, TResponse, TBuilder>) this, this.mBlockInfo.blockInfo[i].start, this.mBlockInfo.blockInfo[i].offset, this.mBlockInfo.blockInfo[i].end, i >= 3))
                {
                    flag = false;
                }
            }
            return flag;
        }

        protected void updateBlockInfo(bool complete, int totalLen)
        {
            if (this.mSaveFileStream != null)
            {
                if (complete)
                {
                    this.mSaveFileStream.SetLength((long) totalLen);
                }
                else
                {
                    byte[] buf = this.getBlockInfo();
                    if (buf != null)
                    {
                        this.appendDownData(buf, (uint) totalLen, (uint) buf.Length);
                    }
                }
            }
        }


    }
}

