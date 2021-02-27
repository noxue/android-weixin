namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Protocol;
    using System;
    using System.Collections.Generic;

    public class NetSceneSearchContact : NetSceneBaseEx<SearchContactRequest, SearchContactResponse, SearchContactRequest.Builder>
    {
        private const string TAG = "NetSceneSearchContact";

        public void doScene(string account)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            //base.mBuilder.
            base.mBuilder.UserName = Util.toSKString(account);
            base.mSessionPack.mConnectMode = 1;
            base.mSessionPack.mCmdID = 0x22;
            //base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/searchcontact";
            base.endBuilder();
        }

        protected override void onFailed(SearchContactRequest request, SearchContactResponse response)
        {
            EventCenter.postEvent(EventConst.ON_NETSCENE_SEARCHCONTACT_ERR, -800000, null);
        }

        //private static void onHandler(EventWatcher watcher, BaseEventArgs evtArgs)
        //{
        //    if (evtArgs.mEventID == EventConst.ON_NETSCENE_SEARCHCONTACT_SUCCESS)
        //    {
        //        Log.d("NetSceneSearchContact", "(1/1)on success");
        //    }
        //}

        protected override void onSuccess(SearchContactRequest request, SearchContactResponse response)
        {
            RetConst ret = (RetConst)response.BaseResponse.Ret;
            if (ret == RetConst.MM_OK)
            {
                //Log.d("NetSceneSearchContact", "success. username = " + response.UserName);
                SearchContactInfo info = new SearchContactInfo
                {
                    UserName = response.UserName.String,
                    NickName = response.NickName.String,
                    PYInitial = response.PYInitial.String,
                    QuanPin = response.QuanPin.String,
                    Sex = response.Sex,
                    ImgBuf = response.ImgBuf.Buffer.ToByteArray(),
                    Country = response.Country,
                    Province = response.Province,
                    City = response.City,
                    Signature = response.Signature,
                    PersonalCard = response.PersonalCard,
                    VerifyFlag = response.VerifyFlag,
                    VerifyInfo = response.VerifyInfo,
                    SnsFlag = response.SnsUserInfo.SnsFlag,
                    SnsBGImgID = response.SnsUserInfo.SnsBGImgID,
                    SnsBGObjectID = response.SnsUserInfo.SnsBGObjectID,
                    strAlias = response.Alias,
                    strSmallHeadImgUrl = response.SmallHeadImgUrl,
                    strBigHeadImgUrl = response.BigHeadImgUrl,
                    MyBrandList = response.MyBrandList,
                    CustomizedInfo = response.CustomizedInfo
                };
                Log.d(request.UserName.String, "-->username = " + response.UserName + "Country=" + info.Country + "Province=" + info.Country + "Signature=" + info.Signature);

                //List<SearchContactInfo> list = null;
                //if (response.ContactListCount > 0)
                //{
                //    list = respContactToInfoList(response.ContactListList);
                //}
                //EventCenter.postEvent(EventConst.ON_NETSCENE_SEARCHCONTACT_SUCCESS, info, list);
            }
            else
            {
                Log.d("NetSceneSearchContact", "failed,   ret = " + ret);
                EventCenter.postEvent(EventConst.ON_NETSCENE_SEARCHCONTACT_ERR, ret, null);
            }
        }

        //private static List<SearchContactInfo> respContactToInfoList(IList<SearchContactItem> contactItemList)
        //{
        //    List<SearchContactInfo> list = new List<SearchContactInfo>();
        //    foreach (SearchContactItem item in contactItemList)
        //    {
        //        SearchContactInfo info = new SearchContactInfo
        //        {
        //            UserName = item.UserName.String,
        //            NickName = item.NickName.String,
        //            PYInitial = item.PYInitial.String,
        //            QuanPin = item.QuanPin.String,
        //            Sex = item.Sex,
        //            ImgBuf = item.ImgBuf.Buffer.ToByteArray(),
        //            Country = item.Country,
        //            Province = item.Province,
        //            City = item.City,
        //            Signature = item.Signature,
        //            PersonalCard = item.PersonalCard,
        //            VerifyFlag = item.VerifyFlag,
        //            VerifyInfo = item.VerifyInfo,
        //            SnsFlag = item.SnsUserInfo.SnsFlag,
        //            SnsBGImgID = item.SnsUserInfo.SnsBGImgID,
        //            SnsBGObjectID = item.SnsUserInfo.SnsBGObjectID,
        //            strAlias = item.Alias,
        //            MyBrandList = item.MyBrandList,
        //            CustomizedInfo = item.CustomizedInfo,
        //            strSmallHeadImgUrl = item.SmallHeadImgUrl,
        //            strBigHeadImgUrl = item.BigHeadImgUrl
        //        };
        //        list.Add(info);
        //    }
        //    return list;
        //}

        public static void testSearchContact()
        {
            //EventWatcher eventWatcher = new EventWatcher(null, null, new EventHandlerDelegate(NetSceneSearchContact.onHandler));
            //EventCenter.registerEventWatcher(EventConst.ON_NETSCENE_SEARCHCONTACT_SUCCESS, eventWatcher);
            ServiceCenter.sceneSearchContact.doScene("halen5064");
        }
    }
}

