namespace MicroMsg.Protocol
{

    using MicroMsg.Common.Utils;
    using MicroMsg.Network;
    using System;

    using WeChat.MicroMsg.Common.Utils;

    public class MMPack
    {
        private const string TAG = "MMPack";
        public static bool DecodePack(ref byte[] decryptedData, byte[] packBuf, byte[] key, ref byte[] serverId, ref int ret)
        {
            //if (!sessionPack.isUserPackHeader)
            //{
            //    Log.d("MMPack", "DecodePack no pack header");
            //    decryptedData = packBuf;
            //    ret = 0;
            //    serverId = new byte[0];
            //    return true;
            //}
            if (useMiniUnPack(packBuf))
            {
                Log.d("MMPack", "DecodePackMini");
                return DecodePackMini(ref decryptedData, packBuf, key, ref serverId, ref ret);
            }
            Log.d("MMPack", "DecodePackOld");
            return DecodePackOld(ref decryptedData, packBuf, key, ref serverId, ref ret);
        }

        public static bool DecodePackOld(ref byte[] decryptedData, byte[] packBuf, byte[] key, ref byte[] serverId, ref int ret)
        {

            try
            {
                if ((packBuf != null) && (packBuf.Length >= 2))
                {
                    MMTLVHeader tlvHeader = new MMTLVHeader();
                    byte[] cipherText = null;
                    if (!UnPack(packBuf, ref tlvHeader, ref cipherText))
                    {
                        Log.e("MMPack", " UnPack failed");
                        return false;
                    }
                    ret = tlvHeader.Ret;
                    // uin = tlvHeader.Uin;


                    if (tlvHeader.Ret == -13)
                    {
                        Log.e("MMPack", "DecodPack failed : session timeout");
                        return false;
                    }
                    if (tlvHeader.Ret != 0)
                    {
                        Log.e("MMPack", "DecodPack failed : retCode = " + tlvHeader.Ret);
                        return false;
                    }
                    serverId = tlvHeader.ServerId;
                    byte[] destinationArray = null;
                    if (tlvHeader.CryptAlgorithm == 5)
                    {
                        decryptedData = AES.Decrypt(cipherText, key);
                        if (decryptedData == null)
                        {
                            Log.e("MMPack", "AES: AESDecrypt failed");
                            return false;
                        }
                        destinationArray = decryptedData;
                    }
                    else if (tlvHeader.CryptAlgorithm == 0)
                    {
                        destinationArray = cipherText;
                    }
                    else
                    {
                        int outLen = cipherText.Length + 0x20;
                        decryptedData = new byte[outLen];
                        if (DES.Using_DES(decryptedData, ref outLen, cipherText, cipherText.Length, key, key.Length, 2) == 0)
                        {
                            Log.e("MMPack", "DES: DESDecrypt failed");
                            return false;
                        }
                        destinationArray = new byte[outLen];
                        Array.Copy(decryptedData, destinationArray, destinationArray.Length);
                    }
                    if (tlvHeader.CompressAlogrithm == 1)
                    {
                        if (!Zlib.Decompress(destinationArray, (int)tlvHeader.CompressLen, ref decryptedData))
                        {
                            Log.e("MMPack", " Zlib Compress failed");
                            return false;
                        }
                        return true;
                    }
                    if (tlvHeader.CompressAlogrithm == 2)
                    {
                        decryptedData = destinationArray;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool DecodePackMini(ref byte[] decryptedData, byte[] packBuf, byte[] key, ref byte[] serverId, ref int ret)
        {
            try
            {
                if ((packBuf != null) && (packBuf.Length >= 2))
                {
                    MMPKG_mini_header _header;
                    byte[] cipherText = null;
                    if (!UnPackMiniData(packBuf, out _header, out cipherText))
                    {
                        Log.e("MMPack", " UnPack failed");
                    }
                    ret = _header.ret;
                    //uin = (uint)_header.uin;
                    if (_header.ret == -13)
                    {
                        Log.e("MMPack", "DecodPack failed : session timeout");
                        return false;
                    }
                    if (_header.ret != 0)
                    {
                        Log.e("MMPack", "DecodPack failed : retCode = " + _header.ret);
                        return false;
                    }
                    serverId = _header.server_id;
                    byte[] destinationArray = null;
                    if (_header.encrypt_algo == 5)
                    {
                       // Log.e("MMPack", " AES key" + Util.byteToHexStr(key) + "  DATA \n" + Util.byteToHexStr(cipherText));
                        decryptedData = AES.Decrypt(cipherText, key);
                   
                        // decryptedData= Util.AESDecrypt(cipherText, key);
                        if (decryptedData == null)
                        {
                            Log.e("MMPack", "AES: AESDecrypt failed");
                            return false;
                        }
                        destinationArray = decryptedData;
                    }
                    else
                    {
                        if (_header.encrypt_algo == 0)
                        {
                            Log.e("MMPack", "not support NULL_ENCRYPT");
                            return false;
                        }
                        int outLen = cipherText.Length + 0x20;
                        decryptedData = new byte[outLen];
                        if (DES.Using_DES(decryptedData, ref outLen, cipherText, cipherText.Length, key, key.Length, 2) == 0)
                        {
                            Log.e("MMPack", "DES: DESDecrypt failed");
                            return false;
                        }
                        destinationArray = new byte[outLen];
                        Array.Copy(decryptedData, destinationArray, destinationArray.Length);
                    }
                    if (_header.compress_algo == 1)
                    {
                        if (!Zlib.Decompress(destinationArray, (int)_header.compress_len, ref decryptedData))
                        {
                            Log.e("MMPack", " Zlib Compress failed");
                            return false;
                        }
                        return true;
                    }
                    if (_header.compress_algo == 2)
                    {
                        decryptedData = destinationArray;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {

               
                return false;
            }
        }

        public static bool EncodePack(SessionPack sessionPack)
        {
            try
            {
                byte[] inBuf = sessionPack.requestToByteArray();
                byte[] key = sessionPack.getSessionKey(true);
                MMTLVHeader tlvHeader = new MMTLVHeader
                {
                    Ret = (int)ConstantsProtocol.CLIENT_MIN_VERSION,

                    Uin = (uint)SessionPackMgr.getAccount().getUin(),
                    CmdId = (ushort)sessionPack.getMMFuncID(),
                    ServerId = SessionPackMgr.getSeverID(),


                    DeviceId = Util.StringToByteArray(Util.getDeviceUniqueId()),
                    CompressLen = (uint)inBuf.Length,
                    CompressVersion = 0x3e9
                };
                byte[] outBuf = null;

                //Log.e("MMPack", " ServerId:" + Util.byteToHexStr(tlvHeader.ServerId));



                if (sessionPack.mNeedCompress)
                {
                    tlvHeader.CompressAlogrithm = 1;
                    if (!Zlib.Compress(inBuf, ref outBuf))
                    {
                        Log.e("MMPack", " Zlib Compress failed");
                        return false;
                    }
                }
                else
                {
                    tlvHeader.CompressAlogrithm = 2;
                    outBuf = inBuf;
                }
                tlvHeader.CompressedLen = (uint)outBuf.Length;
                short mEncrypt = sessionPack.mEncrypt;
                byte[] encryptText = null;
                if ((key == null) || (key.Length <= 0))
                {
                    //if (MicroMsg.Common.Algorithm.RSA.RSAEncrypt(out encryptText, outBuf) != 0)
                    //{
                    //    Log.e("MMPack", " RSAEncrypt failed");
                    //    return false;
                    //}


                    //RsaCertInfo new2 = sessionPack.getRsaCertInfo();
                    //if (new2 == null)
                    //{
                    //    Log.e("MMPack", "getRsaCertInfo failed");
                    //    return false;
                    //}
                    tlvHeader.CertVersion = 99;

                    //data = CdnRuntimeComponent.RSAEncrypt(outBuf, new2.strRsaKeyN, new2.strRsaKeyE);
                    //Util.RsaEncrypt(out encryptText, outBuf, new2.strRsaKeyN, new2.strRsaKeyE);
                    //Util.RSAEncrypt(out encryptText, outBuf, "DFE56EEE6506E5F9796B4F12C3A48121B84E548E9999D834E2C037E3CD276E9C4A2B1758C582A67F6D12895CE5525DDE51D0B92D32B8BE7B2C85827729C3571DCC14B581877BC634BCC7F9DA3825C97A25B341A64295098303C4B584EC579ECCA7C8B96782F65D650039EE7A0772C195DBEFC4488BDFB0B9A58C5C058E3AB04D", "010001");
                    encryptText = Util.RSAEncryptBlock(outBuf, "DFE56EEE6506E5F9796B4F12C3A48121B84E548E9999D834E2C037E3CD276E9C4A2B1758C582A67F6D12895CE5525DDE51D0B92D32B8BE7B2C85827729C3571DCC14B581877BC634BCC7F9DA3825C97A25B341A64295098303C4B584EC579ECCA7C8B96782F65D650039EE7A0772C195DBEFC4488BDFB0B9A58C5C058E3AB04D", "010001");

                    //MicroMsg.Common.Algorithm.RSA.RSAEncrypt(out data, outBuf);

                    if ((encryptText == null) || (encryptText.Length <= 0))
                    {
                        Log.e("MMPack", " RSAEncrypt failed");

                        return false;
                    }



                    return Pack(ref sessionPack.mCacheBodyBuffer, tlvHeader, encryptText);
                }
                if (mEncrypt == 5)
                {
                    tlvHeader.CryptAlgorithm = 5;
                    encryptText = AES.Encrypt(outBuf, key);
                    if (encryptText == null)
                    {
                        Log.e("MMPack", "AES: AESEncrypt failed");
                        return false;
                    }
                    return Pack(ref sessionPack.mCacheBodyBuffer, tlvHeader, encryptText);
                }
                tlvHeader.CryptAlgorithm = 4;
                encryptText = DES.EncryptBytes(outBuf, key, 1);
                if (encryptText == null)
                {
                    Log.e("MMPack", "DES: DESEncrypt failed");
                    return false;
                }
                return Pack(ref sessionPack.mCacheBodyBuffer, tlvHeader, encryptText);
            }
            catch (Exception exception)
            {
                Log.e("MMPack", exception.Message);
                return false;
            }
        }

        private static bool Pack(ref byte[] packBuf, MMTLVHeader tlvHeader, byte[] data)
        {
            TLVPack pack = new TLVPack(0, 1);
            pack.addByte(1, 190);
            pack.AddInt(2, (int)tlvHeader.Uin);
            pack.AddWord(3, tlvHeader.CmdId);
            pack.AddInt(4, tlvHeader.ServerId.Length);
            pack.addByteArray(5, tlvHeader.ServerId, tlvHeader.ServerId.Length);
            pack.addByteArray(6, tlvHeader.DeviceId, 0x10);
            pack.AddWord(7, tlvHeader.CompressVersion);
            pack.AddWord(8, tlvHeader.CompressAlogrithm);
            pack.AddWord(9, tlvHeader.CryptAlgorithm);
            pack.AddInt(10, (int)tlvHeader.CompressLen);
            pack.AddInt(11, (int)tlvHeader.CompressedLen);
            pack.addByteArray(12, data, data.Length);
            pack.AddInt(13, tlvHeader.Ret);
            pack.addByte(14, 0xed);
            pack.AddInt(15, (int)tlvHeader.CertVersion);
            pack.addByte(0x10, 0xed);
            packBuf = new byte[pack.getUsedSize() + 2];
            packBuf[0] = 190;
            packBuf[packBuf.Length - 1] = 0xed;
            int apiSize = 0;
            apiSize = pack.getUsedSize();
            if (pack.CopyTo(packBuf, 1, ref apiSize) != 0)
            {
                Log.e("MMPack", "tlvPack.CopyTo failed");
                return false;
            }
            return true;
        }


        public static bool UnPack(byte[] packBuf, ref MMTLVHeader tlvHeader, ref byte[] cipherText)
        {
            if ((packBuf == null) || (packBuf.Length < 2))
            {
                Log.e("MMPack", " UnPack failed: invlaide args");
                return false;
            }
            if ((190 != packBuf[0]) || (0xed != packBuf[packBuf.Length - 1]))
            {
                Log.e("MMPack", " UnPack failed: invalid args packBuf");
                return false;
            }
            TLVPack pack = new TLVPack(0, 1);
            if (pack.CopyFrom(packBuf, packBuf.Length - 2, 1) != 0)
            {
                return false;
            }
            int apiVal = 0;
            if (pack.GetInt(2, ref apiVal) != 0)
            {
                Log.e("MMPack", " UnPack failed : uin");
                return false;
            }
            tlvHeader.Uin = (uint)apiVal;
            short aphVal = 0;
            if (pack.GetShort(3, ref aphVal) != 0)
            {
                Log.e("MMPack", " UnPack failed : cmdId");
                return false;
            }
            tlvHeader.CmdId = (ushort)aphVal;
            byte[] apoVal = null;
            if (pack.getByteArray(5, ref apoVal) != 0)
            {
                Log.e("MMPack", " UnPack failed : serverId");
                return false;
            }
            tlvHeader.ServerId = apoVal;
            byte[] buffer2 = null;
            if (pack.getByteArray(6, ref buffer2) != 0)
            {
                Log.e("MMPack", " UnPack failed : deviceId");
                return false;
            }
            tlvHeader.DeviceId = buffer2;
            short num3 = 0;
            if (pack.GetShort(7, ref num3) != 0)
            {
                Log.e("MMPack", " UnPack failed : compressVersion");
                return false;
            }
            tlvHeader.CompressVersion = num3;
            short num4 = 0;
            if (pack.GetShort(8, ref num4) != 0)
            {
                Log.e("MMPack", " UnPack failed : compressAlgorithm");
                return false;
            }
            tlvHeader.CompressAlogrithm = num4;
            short num5 = 0;
            if (pack.GetShort(9, ref num5) != 0)
            {
                Log.e("MMPack", " UnPack failed : cryptAlgorithm");
                return false;
            }
            tlvHeader.CryptAlgorithm = num5;
            uint apuVal = 0;
            if (pack.GetUInt(10, ref apuVal) != 0)
            {
                Log.e("MMPack", " UnPack failed : compressLen");
                return false;
            }
            tlvHeader.CompressLen = apuVal;
            uint num7 = 0;
            if (pack.GetUInt(11, ref num7) != 0)
            {
                Log.e("MMPack", " UnPack failed : compressedLen");
                return false;
            }
            tlvHeader.CompressedLen = num7;
            if (pack.getByteArray(12, ref cipherText) != 0)
            {
                Log.e("MMPack", " UnPack failed : cipherText");
                return false;
            }
            int num8 = 0;
            if (pack.GetInt(13, ref num8) != 0)
            {
                Log.e("MMPack", " UnPack failed : ret");
                return false;
            }
            tlvHeader.Ret = num8;
            uint num9 = 0;
            if (pack.GetUInt(15, ref num9) != 0)
            {
                Log.e("MMPack", " UnPack failed : certversion");
                return false;
            }
            tlvHeader.CertVersion = num9;
            return true;
        }


        public static bool EncodePackMini(SessionPack sessionPack)
        {
            try
            {
                byte[] inBuf = sessionPack.requestToByteArray();
                byte[] key = sessionPack.getSessionKey(true);
                if (inBuf == null)
                {
                    Log.e("MMPack", " in Data is Null");
                    return false;
                }
                MMPKG_mini_header miniHeader = new MMPKG_mini_header
                {
                    ret = (int)ConstantsProtocol.CLIENT_MAX_VERSION
                };


                //369302560 °æ±¾ÁÙ½ç


                miniHeader.uin = (uint)SessionPackMgr.getAccount().getUin();


                miniHeader.cmd_id = (ushort)sessionPack.getMMFuncID();
                if (miniHeader.cmd_id == 805)
                {
                    miniHeader.ret = (int)ConstantsProtocol.CLIENT_MIN_VERSION;
                }

                miniHeader.server_id = SessionPackMgr.getSeverID();
                //miniHeader.device_type = 13;
                miniHeader.device_type = 1;

                miniHeader.compress_len = (uint)inBuf.Length;
                miniHeader.server_id_len = (byte)SessionPackMgr.getSeverID().Length;
                byte[] outBuf = null;
                if (sessionPack.mNeedCompress)
                {


                    miniHeader.compress_algo = 1;

                    if (!Zlib.Compress(inBuf, ref outBuf))
                    {
                        Log.e("MMPack", " Zlib Compress failed");
                        return false;
                    }
                }
                else
                {
                    miniHeader.compress_algo = 2;
                    outBuf = inBuf;
                }

                miniHeader.compressed_len = (uint)outBuf.Length;
                short mEncrypt = sessionPack.mEncrypt;
                byte[] data = null;
                if ((key == null) || (key.Length <= 0))
                {
                    //miniHeader.encrypt_algo = 1;
                    //RsaCertInfo new2 = sessionPack.getRsaCertInfo();
                    //if (new2 == null)
                    //{
                    //    Log.e("MMPack", "getRsaCertInfo failed");
                    //    return false;
                    //}
                    miniHeader.cert_version = 156;

                    //133
                    //BD6A54477640F0C0B209DB7747126896B27FB6B219AB9BC9C4CD9661F422E143A75AB2C34EAB88F44719D8D2E0D57CEC9713748BF821EC2014DF97B01CCE262F27CA24F4D89492F99DC8C1A414D0B8E760D815DF53A911D5D807CAF6827084BBE825A49C1BB9369675C4BE435597565B5C4222090235F6A5595003D5D5FA6780EBD51CEAC76D03D8EB9F97B45299719F7C352B2EF32449E0FDD09B562BA0317418B66FC0853EA9F5FFA85EAB8A14E2785C02B0CAC6AFD450EE5A6971C220E72FE6FA4B781235F39D206734C9974127E369E479BF3255FFF8C5FA4B133C642A5656A8E5F176472C5A3FE18D8816E40E58ABC2A4A32BA056EB0B504C86DAE05907
                    //160
                    //B577D7CC04490E8EEBAC4757BD1048234598AD3C4F9FD1AEDDF58D228752C6A977802F8C5183C3DE725B05C02B451D5F59C999510CBD0E40E7AF08EF021ED24628785BA8899A565027A4FC93805DB80000E35E174A341782D3D475D0B9D60C4F8424F45B6AC78D22D6F0B7080B6E9EC262B24212E03910BB03C9FD7094232859B14FA5A7747C07CCFA30AD016D07D1EB127ACC62B60697DAB27DBBE48238576A5569700E2C588FC3FBF513BDB12419CAAF9397E8DBBEE1F155B7543A4622725A1FD950A23A618FC6F536F18A4DA5D0C87268731AE3B1EDE7B196325DAAF9F6BD6FDB1CEB35F2E0AC00AC3FAF4D684E056B010A044C094B5D4CB7EC12C38869CF
                    //156
                    //C7587AC1B1CD3AFD44110CDD1796FCDE878BF7984E35715D784F3A32A63407E9F6B96158752D3313476D5340AC53657167E92C0A6D37AEA65768D8F262A94F4620F57A1B532553BE1FD4F2F4BFEF20127F51B349DF438D45D53814DE96A1482C7C571CA978D3F4A16995E7874960C73E05B480355F08F19997CD5DCB8293D34B2DAFECEA1F1AAC60532FB5FB83C9655FB0812FAF492E02E17123212C09F55CE326A1360B807972C87606C0243498FD47E3DE9BB5E597DE257AC2363E938BB865AEF090A5832E0A1990416B1090F466FFD1C3043A940EE93FA2C1FB85B87DAA2A797A28F8198AA8DE8563B283ACE5FD08F5320A07192E2BE4345C7E0CD3B6D72B
                    //135
                    //B5791473FDFACCE426058401B6125A3D6FEDD76C7DD1B0426A73D8A4182B29EA6D05F4F5E8D99A4D3D1C3E5CF3C8CB3CDDF935643C94D38927881B144D04F310F13307D1AE63A100A2797A714C0D1E2A5A0EF779FC3D6F7D3C3396276BF27DA6D66E2696A6557EFD4B6190C726894D35CE559E147969BAC04AFEBB0E3A235B2C795AC6A9818E14A33A4468F8FF6ABE8A54A74180042BF0FD38427F70B681B9431A099E774618D455F14D1F75121577DAE66C3853A2AA9C4F0F9C221A66F64A46D5F68B0D50F22C7E4FA0D84048B2F9179F4B86442A2720C8FE27BC68C5C6384DCC336F97914F2788B905E5FE98C5BB754488B0F6B09421BB27BFF518EF0E9299
                    try
                    {
                        //data = CdnRuntimeComponent.RSAEncrypt(outBuf, new2.strRsaKeyN, new2.strRsaKeyE);
                        // Util.RSAEncrypt(out data, outBuf, "DFE56EEE6506E5F9796B4F12C3A48121B84E548E9999D834E2C037E3CD276E9C4A2B1758C582A67F6D12895CE5525DDE51D0B92D32B8BE7B2C85827729C3571DCC14B581877BC634BCC7F9DA3825C97A25B341A64295098303C4B584EC579ECCA7C8B96782F65D650039EE7A0772C195DBEFC4488BDFB0B9A58C5C058E3AB04D", "010001");

                        string rsa = "C7587AC1B1CD3AFD44110CDD1796FCDE878BF7984E35715D784F3A32A63407E9F6B96158752D3313476D5340AC53657167E92C0A6D37AEA65768D8F262A94F4620F57A1B532553BE1FD4F2F4BFEF20127F51B349DF438D45D53814DE96A1482C7C571CA978D3F4A16995E7874960C73E05B480355F08F19997CD5DCB8293D34B2DAFECEA1F1AAC60532FB5FB83C9655FB0812FAF492E02E17123212C09F55CE326A1360B807972C87606C0243498FD47E3DE9BB5E597DE257AC2363E938BB865AEF090A5832E0A1990416B1090F466FFD1C3043A940EE93FA2C1FB85B87DAA2A797A28F8198AA8DE8563B283ACE5FD08F5320A07192E2BE4345C7E0CD3B6D72B";
                        int blockSize = 2048;
                        if (sessionPack.mNeedAutoAuth)
                        {
                            miniHeader.cert_version = 99;
                            blockSize = 1024;
                            rsa = "DFE56EEE6506E5F9796B4F12C3A48121B84E548E9999D834E2C037E3CD276E9C4A2B1758C582A67F6D12895CE5525DDE51D0B92D32B8BE7B2C85827729C3571DCC14B581877BC634BCC7F9DA3825C97A25B341A64295098303C4B584EC579ECCA7C8B96782F65D650039EE7A0772C195DBEFC4488BDFB0B9A58C5C058E3AB04D";
                        }
                        data = Util.RSAEncryptBlock(outBuf, rsa, "010001", blockSize);
                        miniHeader.encrypt_algo = 1;
                        //MicroMsg.Common.Utils.RSA.RSAEncrypt(out data, outBuf, new2.strRsaKeyN, new2.strRsaKeyE);
                        //MicroMsg.Common.Algorithm.RSA.RSAEncrypt(out data, outBuf);

                        if ((data == null) || (data.Length <= 0))
                        {
                            Log.e("MMPack", " RSAEncrypt failed");

                            return false;
                        }
                    }
                    catch (Exception exception)
                    {
                        Log.e("MMPack", " RSAEncrypt exception," + exception.Message);

                        return false;
                    }
                    return PackMiniData(out sessionPack.mCacheBodyBuffer, miniHeader, data);
                }
                if (mEncrypt == 5)
                {
                    miniHeader.encrypt_algo = 5;
                    data = AES.Encrypt(outBuf, key);
                    if (data == null)
                    {
                        Log.e("MMPack", "AES: AESEncrypt failed");
                        return false;
                    }
                    return PackMiniData(out sessionPack.mCacheBodyBuffer, miniHeader, data);
                }
                miniHeader.encrypt_algo = 4;
                data = DES.EncryptBytes(outBuf, key, 1);
                if (data == null)
                {
                    Log.e("MMPack", "DES: DESEncrypt failed");
                    return false;
                }
                return PackMiniData(out sessionPack.mCacheBodyBuffer, miniHeader, data);
            }
            catch (Exception exception2)
            {
                Log.e("MMPack", exception2.Message);
                return false;
            }
        }

        private static bool PackMiniData(out byte[] packBuf, MMPKG_mini_header miniHeader, byte[] data)
        {
            packBuf = null;
            byte[] outbuff = null;

            int num = PackMini.mmpkg_mini_head_hton(miniHeader, out outbuff);
            //string aa = Util.byteToHexStr(outbuff);
            if (num != 0)
            {
                Log.e("MMPack", "mmpkg_mini_head_hton failed, ret = " + num);
                return false;
            }
            int length = outbuff.Length;
            int num3 = data.Length;
            packBuf = new byte[length + num3];
            int dstOffset = 0;
            Buffer.BlockCopy(outbuff, 0, packBuf, dstOffset, outbuff.Length);
            dstOffset += outbuff.Length;
            Buffer.BlockCopy(data, 0, packBuf, dstOffset, data.Length);
            return true;
        }
        public static bool UnPackMiniData(byte[] packBuf, out MMPKG_mini_header miniHeader, out byte[] cipherText)
        {
            cipherText = null;
            if (PackMini.mmpkg_mini_head_ntoh(packBuf, out miniHeader) != 0)
            {
                Log.e("MMPack", " UnPack failed: invlaide args");
                return false;
            }
            int len = miniHeader.len;
            int count = packBuf.Length - miniHeader.len;
            cipherText = new byte[count];
            Buffer.BlockCopy(packBuf, len, cipherText, 0, count);
            return true;
        }
        private static bool useMiniUnPack(byte[] packBuf)
        {
            MMPKG_mini_header _header;
            int num = PackMini.mmpkg_mini_head_ntoh(packBuf, out _header);
            if (num != 0)
            {
                Log.d("MMPack", "not UseMiniUnPack, ret = " + num);
                return false;
            }
            if (_header.len >= 0x2f)
            {
                Log.d("MMPack", "not UseMiniUnPack, packBuf[0] = " + packBuf[0]);
                return false;
            }
            return true;
        }


    }
}