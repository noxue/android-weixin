namespace MicroMsg.Scene.Image
{
 
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Scene;
    using MicroMsg.Storage;
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
 
 

    public class UpLoadImgContext : IContextBase
    {
        public long beginTime;
        public ChatMsg chatMsgInfo;
        public string filename = "";
        public MsgTrans imgInfo;
        public bool isResend;
        private const int JPEG_MIN_QUALITY = 50;
        private const int JPEG_NORMAL_QUALITY = 70;
        private const string JPEG_POSTFIX = ".jpg";
        public MemoryStream mBigImageMemoryStream;
        //private IsolatedStorageFile mCurrentIsolatedStorage;
        private int mLastRatio;
        public int mMsgType = 3;
        public ParamEx mParamEx;
        public int mStatus;
        private MemoryStream mThumbMemStream;
        //private string newBigImgName = "";
        //public Stream origStream;
        public  byte[] imgBUf;
        public object senceHandle;
        public const int STATUS_COMPLETE = 5;
        public const int STATUS_ERROR = 4;
        public const int STATUS_INIT = 0;
        public const int STATUS_READY = 1;
        public const int STATUS_SENDING = 2;
        private const string TAG = "UpLoadImgContext";
        public string talkerName = "";

        private bool copyImageStreamToMem(byte[] buf)
        {
           // byte[] buffer = this.readFromStream(fileStream, 0, (int) fileStream.Length);
            this.mBigImageMemoryStream = new MemoryStream(buf);

            if (this.mBigImageMemoryStream == null)
            {
                Log.e("UpLoadImgContext", "bigImageMemoryStream null return");
                return false;
            }

            return true;
        }

        public void doFinished()
        {
            this.updateUploadImgContext(true);
            if (this.mBigImageMemoryStream != null)
            {
                this.mBigImageMemoryStream.Close();
                this.mBigImageMemoryStream.Dispose();
                this.mBigImageMemoryStream = null;
            }
            if (this.mThumbMemStream != null)
            {
                this.mThumbMemStream.Close();
                this.mThumbMemStream.Dispose();
                this.mThumbMemStream = null;
            }
            //if(imgBUf!=null)
        }

        public bool fillContextWithOrigStream()
        {
            if ((this.imgBUf == null) || (this.imgBUf.Length <= 0L))
            {
                return false;
            }
            copyImageStreamToMem(imgBUf);
            if ((this.chatMsgInfo == null) || string.IsNullOrEmpty(this.chatMsgInfo.strClientMsgId))
            {
                return false;
            }
            if (this.imgInfo == null)
            {
                this.imgInfo = new MsgTrans();
                this.imgInfo.nCreateTime = (long) (Util.getNowMilliseconds() / 1000.0);
                this.imgInfo.strToUserName = this.talkerName;
                this.imgInfo.nStatus = 2;
                this.imgInfo.nTransType = 1;
                this.imgInfo.strClientMsgId = this.chatMsgInfo.strClientMsgId;

                this.imgInfo.nTotalDataLen = imgBUf.Length;
                this.imgInfo.nTransDataLen = 0;

                //if (!this.saveImageOnLocal(this.filename, this.origStream))
                //{
                //    Log.e("UpLoadImgContext", "saveImageOnLocal fail!");
                //    return false;
                //}
                if (this.imgInfo.nTotalDataLen <= 0)
                {
                    Log.e("UpLoadImgContext", "saveImageOnLocal fail len =" + this.imgInfo.nTotalDataLen);
                    return false;
                }
                if (this.chatMsgInfo == null)
                {
                    this.chatMsgInfo = new ChatMsg();
                }
                this.chatMsgInfo.strTalker = this.talkerName;
                this.chatMsgInfo.nMsgType = this.mMsgType;
                this.chatMsgInfo.nStatus = 0;
                this.chatMsgInfo.nIsSender = 1;
                this.chatMsgInfo.strThumbnail = this.imgInfo.strThumbnail;
                this.chatMsgInfo.strPath = this.imgInfo.strImagePath;
                //StorageMgr.chatMsg.updateMsg(this.chatMsgInfo);
                this.imgInfo.nMsgLocalID = this.chatMsgInfo.nMsgLocalID;
               // StorageMgr.msgImg.add(this.imgInfo);
                //copyImageStreamToMem()
            }
            return true;
        }

        public byte[] getRemainThumbToSend()
        {
            if (!this.initThumbMemStream())
            {
                return null;
            }
            int length = (int) this.mThumbMemStream.Length;
            byte[] buffer = new byte[length];
            this.mThumbMemStream.Seek(0L, SeekOrigin.Begin);
            if (this.mThumbMemStream.Read(buffer, 0, length) != this.mThumbMemStream.Length)
            {
                return null;
            }
            return buffer;
        }

        public bool initThumbMemStream()
        {
            if (this.mThumbMemStream != null)
            {
                return true;
            }
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    IsolatedStorageFileStream stream = file.OpenFile(this.imgInfo.strThumbnail, FileMode.Open, FileAccess.Read);
                    if (stream == null)
                    {
                        return false;
                    }
                    int length = (int) stream.Length;
                    this.mThumbMemStream = new MemoryStream();
                    stream.Seek(0L, SeekOrigin.Begin);
                    stream.CopyTo(this.mThumbMemStream, length);
                    stream.Dispose();
                    stream.Close();
                    mThumbMemStream.Dispose();
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.e("UpLoadImgContext", exception.Message);
            }
            return false;
        }

        public bool isRunning()
        {
            if ((this.mStatus != 1) && (this.mStatus != 2))
            {
                return false;
            }
            return true;
        }

        public bool isUploadCompleted()
        {
            return ((this.imgInfo.nTransDataLen == this.imgInfo.nTotalDataLen) && (this.imgInfo.nTotalDataLen > 0));
        }

        //public void loadUploadImgContext(int nImgMsgTranId)
        //{
        //    MsgTrans item = null;
        //    this.isResend = true;
        //    if (nImgMsgTranId > 0)
        //    {
        //        item = StorageMgr.msgImg.getByMsgTransID(nImgMsgTranId);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //    if (item != null)
        //    {
        //        if (item.nTransDataLen == item.nTotalDataLen)
        //        {
        //            this.updateChatMsgState(2);
        //            StorageMgr.msgImg.delByTransID(item.nMsgTransID);
        //        }
        //        else
        //        {
        //            this.imgInfo = item;
        //            this.chatMsgInfo = StorageMgr.chatMsg.getMsg(this.imgInfo.nMsgLocalID);
        //            if (((this.chatMsgInfo != null) && !string.IsNullOrEmpty(this.imgInfo.strImagePath)) && !string.IsNullOrEmpty(this.imgInfo.strClientMsgId))
        //            {
        //                this.mMsgType = this.chatMsgInfo.nMsgType;
        //                try
        //                {
        //                    using (this.mCurrentIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
        //                    {
        //                        if (this.mCurrentIsolatedStorage.FileExists(this.imgInfo.strImagePath))
        //                        {
        //                            IsolatedStorageFileStream fileStream = this.mCurrentIsolatedStorage.OpenFile(this.imgInfo.strImagePath, FileMode.Open, FileAccess.Read);
        //                            if (this.imgInfo.nTransDataLen < this.imgInfo.nTotalDataLen)
        //                            {
        //                                Log.i("UpLoadImgContext", "Resend image exists file = " + this.imgInfo.strImagePath);
        //                                if (this.copyImageStreamToMem(fileStream))
        //                                {
        //                                    this.chatMsgInfo.nStatus = 0;
        //                                    item.nStatus = 2;
        //                                    //StorageMgr.msgImg.update(item);
        //                                    //StorageMgr.chatMsg.updateMsg(this.chatMsgInfo);
        //                                }
        //                            }
        //                            fileStream.Close();
        //                            fileStream = null;
        //                        }
        //                    }
        //                }
        //                catch (Exception exception)
        //                {
        //                    Log.e("UpLoadImgContext", exception.Message);
        //                    item = null;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        this.updateChatMsgState(2);
        //    }
        //}

        public bool needToClean()
        {
            return ((this.mStatus == 5) || (this.mStatus == 4));
        }

        public bool needToHandle()
        {
            return (this.mStatus == 0);
        }

        private byte[] readFromStream(IsolatedStorageFileStream fileStream, int startPos, int dataLen)
        {
            try
            {
                if ((fileStream == null) || (dataLen > fileStream.Length))
                {
                    return null;
                }
                byte[] buffer = null;
                buffer = new byte[dataLen];
                int num = 0;
                fileStream.Seek((long) startPos, SeekOrigin.Begin);
                num = fileStream.Read(buffer, 0, dataLen);
                if (num <= 0)
                {
                    Log.e("UpLoadImgContext", "read from stream lenght = " + num);
                    return null;
                }
                return buffer;
            }
            catch (Exception exception)
            {
                Log.e("UpLoadImgContext", exception.Message);
                return null;
            }
        }

        //public bool saveBigImage(Stream origStream, ref IsolatedStorageFileStream targStream, bool isNeedScale = true)
        //{
        //    if ((origStream == null) || (targStream == null))
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        if (!isNeedScale)
        //        {
        //            origStream.Seek(0L, SeekOrigin.Begin);
        //            origStream.CopyTo(targStream, (int) origStream.Length);
        //            return true;
        //        }
        //        BitmapImage image = new BitmapImage();
        //        BitmapImage image2 = new BitmapImage();
        //        image2.set_DecodePixelHeight(20);
        //        image2.SetSource(origStream);
        //        if (image2.get_PixelHeight() > image2.get_DecodePixelWidth())
        //        {
        //            image.set_DecodePixelHeight(960);
        //        }
        //        else
        //        {
        //            image.set_DecodePixelWidth(960);
        //        }
        //        image2 = null;
        //        image.SetSource(origStream);
        //        WriteableBitmap bitmap = new WriteableBitmap(image);
        //        if (((image.get_PixelHeight() > 960) || (image.get_PixelWidth() > 960)) || (origStream.Length > 0xdc00L))
        //        {
        //            if ((image.get_PixelWidth() > 960) || (image.get_PixelHeight() > 960))
        //            {
        //                double num = 0.0;
        //                if (image.get_PixelHeight() > image.get_PixelWidth())
        //                {
        //                    num = image.get_PixelHeight() * 0.0010416666666666667;
        //                }
        //                else
        //                {
        //                    num = image.get_PixelWidth() * 0.0010416666666666667;
        //                }
        //                double num2 = ((double) bitmap.get_PixelWidth()) / num;
        //                double num3 = ((double) bitmap.get_PixelHeight()) / num;
        //                int num4 = (int) num2;
        //                int num5 = (int) num3;
        //                Log.i("UpLoadImgContext", "iScale = " + num);
        //                Extensions.SaveJpeg(bitmap, targStream, num4, num5, 0, 70);
        //                if (targStream.Length > 0xdc00L)
        //                {
        //                    targStream.Close();
        //                    if (this.mCurrentIsolatedStorage.FileExists(this.newBigImgName))
        //                    {
        //                        this.mCurrentIsolatedStorage.DeleteFile(this.newBigImgName);
        //                    }
        //                    targStream = this.mCurrentIsolatedStorage.CreateFile(this.newBigImgName);
        //                    Extensions.SaveJpeg(bitmap, targStream, num4, num5, 0, 50);
        //                }
        //            }
        //            else
        //            {
        //                int num6 = 70;
        //                if (origStream.Length >= 0x25800L)
        //                {
        //                    num6 = 30;
        //                }
        //                else if (origStream.Length >= 0x20800L)
        //                {
        //                    num6 = 40;
        //                }
        //                else if (origStream.Length >= 0x19000L)
        //                {
        //                    num6 = 50;
        //                }
        //                Extensions.SaveJpeg(bitmap, targStream, bitmap.get_PixelWidth(), bitmap.get_PixelHeight(), 0, num6);
        //            }
        //        }
        //        else
        //        {
        //            Extensions.SaveJpeg(bitmap, targStream, bitmap.get_PixelWidth(), bitmap.get_PixelHeight(), 0, 70);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.e("UpLoadImgContext", exception.Message);
        //        return false;
        //    }
        //    return true;
        //}

        //public bool saveImageOnLocal(string filename, Stream origImgStream)
        //{
        //    bool flag;
        //    if ((origImgStream == null) || (origImgStream.Length <= 0L))
        //    {
        //        Log.e("UpLoadImgContext", "Image's Length = 0 ");
        //        return false;
        //    }
        //    string str = ".jpg";
        //    byte[] buffer = new byte[origImgStream.Length];
        //    int num = origImgStream.Read(buffer, 0, (int) origImgStream.Length);
        //    ImageFromat jpeg = ImageFromat.jpeg;
        //    if (num > 0)
        //    {
        //        jpeg = ImageUtil.getImageFormat(buffer);
        //    }
        //    if (jpeg == ImageFromat.bmp)
        //    {
        //        str = "bmp";
        //    }
        //    else if (jpeg == ImageFromat.gif)
        //    {
        //        str = "gif";
        //    }
        //    else if (jpeg == ImageFromat.png)
        //    {
        //        str = "png";
        //    }
        //    try
        //    {
        //        using (this.mCurrentIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
        //        {
        //            if (!this.mCurrentIsolatedStorage.DirectoryExists(StorageIO.getImagePath()))
        //            {
        //                this.mCurrentIsolatedStorage.CreateDirectory(StorageIO.getImagePath());
        //            }
        //            string str2 = filename;
        //            if (str2 == null)
        //            {
        //                Log.e("UpLoadImgContext", "origImgName == null ");
        //                return false;
        //            }
        //            this.newBigImgName = StorageIO.getImagePath() + "/" + str2 + "_send" + str;
        //            if (this.mCurrentIsolatedStorage.FileExists(this.newBigImgName))
        //            {
        //                this.mCurrentIsolatedStorage.DeleteFile(this.newBigImgName);
        //            }
        //            IsolatedStorageFileStream targStream = this.mCurrentIsolatedStorage.CreateFile(this.newBigImgName);
        //            if (targStream == null)
        //            {
        //                Log.e("UpLoadImgContext", "Can not Create big Image File");
        //                return false;
        //            }
        //            this.imgInfo.strImagePath = this.newBigImgName;
        //            Log.i("UpLoadImgContext", "Big Img file open complete file name = " + this.newBigImgName);
        //            if (this.mParamEx != null)
        //            {
        //                if (!this.mParamEx.isNeedScale)
        //                {
        //                    this.saveBigImage(origImgStream, ref targStream, false);
        //                }
        //                else
        //                {
        //                    this.saveBigImage(origImgStream, ref targStream, true);
        //                }
        //            }
        //            else
        //            {
        //                this.saveBigImage(origImgStream, ref targStream, true);
        //            }
        //            if (!this.copyImageStreamToMem(targStream))
        //            {
        //                targStream.Close();
        //                targStream = null;
        //                return false;
        //            }
        //            targStream.Close();
        //            targStream = null;
        //            this.imgInfo.nTotalDataLen = (int) this.mBigImageMemoryStream.Length;
        //            this.imgInfo.nTransDataLen = 0;
        //            this.imgInfo.nTransType = 1;
        //            if ((this.mParamEx != null) && !string.IsNullOrEmpty(this.mParamEx.thumbPath))
        //            {
        //                this.imgInfo.strThumbnail = this.mParamEx.thumbPath;
        //            }
        //            if (string.IsNullOrEmpty(this.imgInfo.strThumbnail))
        //            {
        //                string hashString = MD5Core.GetHashString(str2 + Util.getNowMilliseconds());
        //                hashString = StorageIO.getThumbnailPath() + "/" + hashString + ".jpg";
        //                if (this.mCurrentIsolatedStorage.FileExists(hashString))
        //                {
        //                    this.mCurrentIsolatedStorage.DeleteFile(hashString);
        //                }
        //                IsolatedStorageFileStream stream2 = this.mCurrentIsolatedStorage.CreateFile(hashString);
        //                this.imgInfo.strThumbnail = hashString;
        //                if (stream2 == null)
        //                {
        //                    Log.e("UpLoadImgContext", "Can not Create Image thumb file");
        //                    return false;
        //                }
        //                this.saveThumbImage(origImgStream, ref stream2, true);
        //                stream2.Close();
        //                stream2.Dispose();
        //                stream2 = null;
        //            }
        //            this.imgInfo.nStatus = 2;
        //            flag = true;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.e("UpLoadImgContext", exception.Message);
        //        flag = false;
        //    }
        //    return flag;
        //}

        //public bool saveThumbImage(Stream origStream, ref IsolatedStorageFileStream targStream, bool isNeedScale = true)
        //{
        //    if ((origStream == null) || (targStream == null))
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        BitmapImage image = new BitmapImage();
        //        BitmapImage image2 = new BitmapImage();
        //        image2.set_DecodePixelHeight(20);
        //        image2.SetSource(origStream);
        //        if (image2.get_PixelHeight() > image2.get_DecodePixelWidth())
        //        {
        //            image.set_DecodePixelHeight(100);
        //        }
        //        else
        //        {
        //            image.set_DecodePixelWidth(100);
        //        }
        //        image2 = null;
        //        image.SetSource(origStream);
        //        WriteableBitmap bitmap = new WriteableBitmap(image);
        //        if ((image.get_PixelHeight() > 100) || (image.get_PixelWidth() > 100))
        //        {
        //            double num = 0.0;
        //            if (image.get_PixelHeight() > image.get_PixelWidth())
        //            {
        //                num = image.get_PixelHeight() * 0.01;
        //            }
        //            else
        //            {
        //                num = image.get_PixelWidth() * 0.01;
        //            }
        //            double num2 = ((double) bitmap.get_PixelWidth()) / num;
        //            double num3 = ((double) bitmap.get_PixelHeight()) / num;
        //            int num4 = (int) num2;
        //            int num5 = (int) num3;
        //           // Extensions.SaveJpeg(bitmap, targStream, num4, num5, 0, 70);
        //            if (targStream.Length > 0x2800L)
        //            {
        //                targStream.Close();
        //                if (this.mCurrentIsolatedStorage.FileExists(this.imgInfo.strThumbnail))
        //                {
        //                    this.mCurrentIsolatedStorage.DeleteFile(this.imgInfo.strThumbnail);
        //                }
        //                targStream = this.mCurrentIsolatedStorage.CreateFile(this.imgInfo.strThumbnail);
        //                if (targStream == null)
        //                {
        //                    Log.e("UpLoadImgContext", "Can not Create Image thumb file");
        //                    return false;
        //                }
        //               // Extensions.SaveJpeg(bitmap, targStream, num4, num5, 0, 50);
        //            }
        //        }
        //        else
        //        {
        //          //  Extensions.SaveJpeg(bitmap, targStream, bitmap.get_PixelWidth(), bitmap.get_PixelHeight(), 0, 70);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.e("UpLoadImgContext", exception.Message);
        //        return false;
        //    }
        //    return true;
        //}

        public void updateChatMsgState(int status)
        {
            if (this.chatMsgInfo == null)
            {
              //  this.chatMsgInfo = StorageMgr.chatMsg.getMsg(this.imgInfo.nMsgLocalID);
              //  if (this.chatMsgInfo != null)
              //  {
               //     this.chatMsgInfo.nStatus = status;
                   // StorageMgr.chatMsg.modifyMsg(this.chatMsgInfo);
               // }
            }
            else
            {
                this.chatMsgInfo.nStatus = status;
                //StorageMgr.chatMsg.updateMsg(this.chatMsgInfo);
            }
        }

        public void updateProgressInfo()
        {
            this.updateProgressInfo(this.imgInfo.nTransDataLen, this.imgInfo.nTotalDataLen);
        }

        public void updateProgressInfo(int sendedLen, int totalLen)
        {
            if (((sendedLen > 0) && (sendedLen <= totalLen)) && (totalLen > 0))
            {
                int nProgress = (sendedLen * 100) / totalLen;
                if (nProgress <= 5)
                {
                    nProgress = 5;
                }
                if ((nProgress > (this.mLastRatio + 10)) || (nProgress >= 100))
                {
                    this.mLastRatio = nProgress;
                    EventCenter.postEvent(EventConst.ON_NETSCENE_SENDIMG_PROGRESS, new ParamReturn(nProgress, this.imgInfo.nMsgLocalID, this.talkerName), null);
                }
            }
        }

        public bool updateUploadImgContext(bool isUpdatechatMsg)
        {
            if (this.imgInfo == null)
            {
                return false;
            }
            MsgUIStatus processing = MsgUIStatus.Processing;
            if (this.mStatus == 4)
            {
                processing = MsgUIStatus.Fail;
            }
            else if (this.mStatus == 5)
            {
                processing = MsgUIStatus.Success;
            }
            if (this.mStatus == 5)
            {
                if (isUpdatechatMsg)
                {
                    this.updateChatMsgState((int) processing);
                }
               // StorageMgr.msgImg.delByTransID(this.imgInfo.nMsgTransID);
            }
            else
            {
                if (isUpdatechatMsg)
                {
                    this.updateChatMsgState((int) processing);
                }
                //MsgTrans trans = StorageMgr.msgImg.getByMsgTransID(this.imgInfo.nMsgTransID);
                //if (trans != null)
                //{
                //    if (trans.nTotalDataLen == trans.nTransDataLen)
                //    {
                //        if (isUpdatechatMsg)
                //        {
                //            this.updateChatMsgState(2);
                //        }
                //       // StorageMgr.msgImg.delByTransID(this.imgInfo.nMsgTransID);
                //        return true;
                //    }
                //    this.imgInfo.nStatus = this.mStatus;
                //    this.imgInfo.nLastModifyTime = (uint) (Util.getNowMilliseconds() / 1000.0);
                //   // StorageMgr.msgImg.update(this.imgInfo);
                //}
            }
            return true;
        }
    }
}

