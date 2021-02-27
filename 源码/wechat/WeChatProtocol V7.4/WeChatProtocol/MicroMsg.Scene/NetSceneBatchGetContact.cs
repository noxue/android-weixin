namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using System;
    using System.Collections.Generic;

    public class NetSceneBatchGetContact : NetSceneBaseEx<GetContactRequest, GetContactResponse, GetContactRequest.Builder>
    {
        private const string TAG = "NetSceneBatchGetContact";

        public bool doScene(List<string> userNameList)
        {
            return new NetSceneBatchGetContact().doSceneEx(userNameList);
        }

        private bool doSceneEx(List<string> userNameList)
        {
         //   Log.i("NetSceneBatchGetContact", "doscene in");
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0, 369298705);
            //base.mBuilder.BaseRequest.ClientVersion = 1678050096;
            foreach (string str in userNameList)
            {
                base.mBuilder.UserNameListList.Add(Util.toSKString(str));
            }
            base.mBuilder.UserCount = (uint) base.mBuilder.UserNameListList.Count;
            //string aa = Util.byteToHexStr(base.mBuilder.Build().ToByteArray());
            base.mSessionPack.mCmdID = 0x47;
            base.endBuilder();
            return true;
        }

        private static ModBottleContact ModContactToModBottleContact(ModContact contact)
        {
            ModBottleContact.Builder builder = ModBottleContact.CreateBuilder();
            builder.UserName = contact.UserName.String;
            builder.Type = contact.ContactType;
            builder.Sex = (uint) contact.Sex;
            builder.City = contact.City;
            builder.Country = contact.Country;
            builder.Province = contact.Province;
            builder.Signature = contact.Signature;
            builder.ImgFlag = contact.ImgFlag;
            builder.HDImgFlag = contact.HasWeiXinHdHeadImg;
            builder.BigHeadImgUrl = contact.BigHeadImgUrl;
            builder.SmallHeadImgUrl = contact.SmallHeadImgUrl;
            return builder.Build();
        }

        protected override void onFailed(GetContactRequest request, GetContactResponse response)
        {
            Log.e("NetSceneBatchGetContact", "onFailed in");
            EventCenter.postEvent(EventConst.ON_NETSCENE_BATCH_GET_CONTACT_ERR, RetConst.MM_ERR_CLIENT, null);
        }

        protected override void onSuccess(GetContactRequest request, GetContactResponse response)
        {
            Log.i("NetSceneBatchGetContact", "onSuccess in");
            RetConst ret = (RetConst) response.BaseResponse.Ret;
            if (ret != RetConst.MM_OK)
            {
                Log.e("NetSceneBatchGetContact", "send BatchGetContactRequest failed ret =" + ret);
                EventCenter.postEvent(EventConst.ON_NETSCENE_BATCH_GET_CONTACT_ERR, ret, null);
            }
            else
            {
                List<object> cmdList = new List<object>();
                List<string> list2 = new List<string>();
                foreach (ModContact contact in response.ContactListList)
                {
                    string username = contact.UserName.String;
                    //Log.i("NetSceneBatchGetContact", string.Concat(new object[] { "BatchGetContactRequest update contact, userName = ", username, " contact.City=", contact.City, " contact.Province = ", contact.Province, " Sex=", contact.Sex, " Signature=", contact.Signature }));
                    if (ContactHelper.isBottleContact(username))
                    {
                        NetSceneNewSync.processModBottleContact(ModContactToModBottleContact(contact));
                    }
                    else
                    {
                        cmdList.Add(contact);
                        list2.Add(username);
                    }
                }
                if (cmdList.Count > 0)
                {
                    NetSceneNewSync.processModContactCmdList(cmdList);
                }
                //Ë¢ÐÂ
                //EventCenter.postEvent(EventConst.ON_NETSCENE_BATCH_GET_CONTACT_SUCCESS, list2, null);
            }
        }
    }
}

