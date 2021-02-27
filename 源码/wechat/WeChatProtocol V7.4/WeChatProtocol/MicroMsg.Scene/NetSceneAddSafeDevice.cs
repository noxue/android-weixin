namespace MicroMsg.Scene
{
    using micromsg;
    using MicroMsg.Common.Event;
    using MicroMsg.Common.Utils;
    using MicroMsg.Manager;
    using MicroMsg.Protocol;
    using System;

    internal class NetSceneAddSafeDevice : NetSceneBaseEx<AddSafeDeviceRequest, AddSafeDeviceResponse, AddSafeDeviceRequest.Builder>
    {
        private const string TAG = "NetSceneAddSafeDevice";

        public bool doScene(string ticket)
        {
            base.beginBuilder();
            base.mBuilder.BaseRequest = NetSceneBase.makeBaseRequest(0);
            base.mBuilder.AuthTicket = ticket;
            base.mBuilder.Name = Util.ByteArrayToString(base.mBuilder.BaseRequest.DeviceID.ToByteArray());
            base.mBuilder.DeviceType = ConstantsProtocol.DEVICE_TYPE;
            base.mSessionPack.mCmdUri = "/cgi-bin/micromsg-bin/addsafedevice";
            base.mSessionPack.mConnectMode = 2;
            base.endBuilder();
            return true;
        }

        protected override void onFailed(AddSafeDeviceRequest request, AddSafeDeviceResponse response)
        {
            Log.e("NetSceneAddSafeDevice", "Add Safe Device failed system error!");
            EventCenter.postEvent(EventConst.ON_ADDSAFEDEVICE_ERROR, RetConst.MM_ERR_SYS, null);
        }

        protected override void onSuccess(AddSafeDeviceRequest request, AddSafeDeviceResponse response)
        {
            if ((response == null) || (response.BaseResponse.Ret != 0))
            {
                Log.e("NetSceneAddSafeDevice", "Add Safe Device not invalidate ret = " + ((RetConst) response.BaseResponse.Ret).ToString());
                EventCenter.postEvent(EventConst.ON_ADDSAFEDEVICE_ERROR, response.BaseResponse.Ret, null);
            }
            else
            {
                //SafeDeviceService.saveSafeDeviceList(response.SafeDeviceList);
                EventCenter.postEvent(EventConst.ON_ADDSAFEDEVICE_SUCCESS, null, null);
            }
        }
    }
}

