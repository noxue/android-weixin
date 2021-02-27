import tornado.ioloop
import tornado.web
import json
from xzy import interface
from tornado.ioloop import IOLoop
from xzy.client_tornado import ChatClient
from xzy.utils.JsonUtils import JsonUtils
from xzy.utils.RowsToJson import RowsToJson
from xzy.model.ApiRtn import InitRtn,ContactRtn


 #服务初始化
class Init(tornado.web.RequestHandler):
    def post(self):
        interface.init_all()
        Obj = InitRtn(0, "初始化成功")
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

 #安卓登录
class Login(tornado.web.RequestHandler):
    def post(self):
        username=self.get_body_argument('username')
        password=self.get_body_argument('password')
        Obj=interface.Login(username,password)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr.encode('utf-8'))

 #首次登录设备初始化
class NewInit(tornado.web.RequestHandler):
    def post(self):
        interface.new_init()
        Obj = InitRtn(0, "好友列表加载成功")
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# 获取好友列表
class GetContactList(tornado.web.RequestHandler):
    def post(self):
        Objs=interface.get_contact_list()
        returnStr = RowsToJson.ContactToJson(Objs)
        self.write(returnStr)

#获取群列表
class GetChatroomList(tornado.web.RequestHandler):
    def post(self):
        Objs = interface.get_group_list()
        returnStr = RowsToJson.ContactToJson(Objs)
        self.write(returnStr)

#获取公众号列表
class GetGZHList(tornado.web.RequestHandler):
    def post(self):
        Objs = interface.get_gzh_list()
        returnStr = RowsToJson.ContactToJson(Objs)
        self.write(returnStr)

#获取黑名单列表
class GetBlackList(tornado.web.RequestHandler):
    def post(self):
        Objs = interface.get_hmd_list()
        returnStr = RowsToJson.ContactToJson(Objs)
        self.write(returnStr)

#获取删除好友列表
class GetDelContactList(tornado.web.RequestHandler):
    def post(self):
        Objs = interface.get_delcontact_list()
        returnStr = RowsToJson.ContactToJson(Objs)
        self.write(returnStr)

#获取群成员信息
class GetChatroomMemberList(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        Objs = interface.get_chatroom_member_list(wxid)
        returnStr = RowsToJson.ContactToJson(Objs)
        self.write(returnStr)

#新消息同步
class NewSync(tornado.web.RequestHandler):
    def post(self):
        Obj=interface.new_sync()
        returnStr = JsonUtils.List2Json(Obj)
        self.write(returnStr)

#刷新好友信息
class ContactSync(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        array = interface.get_contact(wxid)
        if len(array) > 0:
            wx=array[0]
            Obj = ContactRtn(wx.wxid, wx.nickname,wx.remark_name, wx.alias, wx.avatar_big,wx.v1_name,wx.type,wx.sex,wx.country,wx.sheng,wx.shi,wx.qianming,wx.register_body,wx.src)
            returnStr = JsonUtils.Obj2Json(Obj)
            self.write(returnStr)
        else:
            Obj = InitRtn(0, "执行错误")
            returnStr = JsonUtils.Obj2Json(Obj)
            self.write(returnStr)

#通过好友验证
class VerifyFriend(tornado.web.RequestHandler):
    def post(self):
        code = self.get_body_argument('code')
        wxid = self.get_body_argument('wxid')
        v1 = self.get_body_argument('v1')
        ticket = self.get_body_argument('ticket')
        antiticket = self.get_body_argument('antiticket')
        content = self.get_body_argument('content')
        wxid = interface.verify_user(
            code, wxid, v1, ticket, antiticket, content)
        Obj = InitRtn(0, wxid)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

#发送文字消息
class SendMsgText(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        content = self.get_body_argument('content')
        (ret_code, svrid) = interface.new_send_msg(
            wxid, content.encode(encoding='utf-8'), [])
        Obj = InitRtn(ret_code, svrid)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

#发送链接
class SendMsgApp(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        title = self.get_body_argument('title')
        desc = self.get_body_argument('desc')
        url = self.get_body_argument('url')
        thumburl = self.get_body_argument('thumburl')
        (ret_code, svrid) = interface.send_app_msg(
            wxid, title, desc, url, thumburl)
        Obj = InitRtn(ret_code, svrid)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)
 
# 创建群
class CreateChatroom(tornado.web.RequestHandler):
    def post(self):
        wxidlist = self.get_body_argument('wxidlist')
        idlist = wxidlist.split(',')
        chatroomid = interface.create_chatroom(idlist)
        Obj = InitRtn(0, chatroomid)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# 群聊加人
class AddChatroomMember(tornado.web.RequestHandler):
    def post(self):
        chatroomid = self.get_body_argument('chatroomid')
        memberlist = self.get_body_argument('memberlist')
        idlist = memberlist.split(',')
        (code, msg) = interface.add_chatroom_member(chatroomid, idlist)
        Obj = InitRtn(code, msg)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# @群众所有人
class AtAllInGroup(tornado.web.RequestHandler):
    def post(self):
        chatroomid = self.get_body_argument('chatroomid')
        memberlist = self.get_body_argument('memberlist')
        idlist = memberlist.split(',')
        interface.at_all_in_group(chatroomid, idlist)
        Obj = InitRtn(0, "成功")
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# 设置群内备注
class SetGroupNickName(tornado.web.RequestHandler):
    def post(self):
        chatroomid = self.get_body_argument('chatroomid')
        nickname = self.get_body_argument('nickname')
        (code,msg)=interface.set_group_nick_name(chatroomid, nickname)
        Obj = InitRtn(code, msg)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# 消息撤回
class RevokeMsg(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        msgid = self.get_body_argument('msgid')
        (code,msg)=interface.revoke_msg(wxid,int(msgid))
        Obj = InitRtn(code, msg)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# 从通讯录中删除好友/恢复好友(删除对方后可以用此接口再添加对方)
# 群聊使用此接口可以保存到通讯录
class DelFriend(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        deltype = self.get_body_argument('deltype')
        delete=True
        if str(deltype) == "1":
            delete=True
        else:
            delete = False
        (code, msg) = interface.delete_friend(wxid, delete)
        Obj = InitRtn(code, msg)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# 拉黑/恢复 好友关系
class BanFriend(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        bantype = self.get_body_argument('bantype')
        ban = True
        if str(bantype) == "1":
            ban = True
        else:
            ban = False
        (code, msg) = interface.ban_friend(wxid,ban)
        Obj = InitRtn(code, msg)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# 设置好友备注名/群聊名
class SetFriendName(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        nickname = self.get_body_argument('nickname')
        (code, msg) = interface.set_friend_name(wxid, nickname)
        Obj = InitRtn(code, msg)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

# 设置群公告
class SetChatroomAnnouncement(tornado.web.RequestHandler):
    def post(self):
        wxid = self.get_body_argument('wxid')
        text = self.get_body_argument('text')
        (code, msg) = interface.set_chatroom_announcement(wxid, text)
        Obj = InitRtn(code, msg)
        returnStr = JsonUtils.Obj2Json(Obj)
        self.write(returnStr)

application = tornado.web.Application([
    (r"/Init", Init),
    (r"/Login", Login),
    (r"/NewInit", NewInit),
    (r"/GetContactList", GetContactList),
    (r"/GetChatroomList", GetChatroomList),
    (r"/GetGZHList", GetGZHList),
    (r"/GetBlackList", GetBlackList),
    (r"/GetDelContactList", GetDelContactList),
    (r"/GetChatroomMemberList", GetChatroomMemberList),
    (r"/NewSync", NewSync),
    (r"/ContactSync", ContactSync),
    (r"/VerifyFriend", VerifyFriend),
    (r"/SendMsgText", SendMsgText),
    (r"/SendMsgApp", SendMsgApp),
    (r"/CreateChatroom", CreateChatroom),
    (r"/AddChatroomMember", AddChatroomMember),
    (r"/AtAllInGroup", AtAllInGroup),
    (r"/SetGroupNickName", SetGroupNickName),
    (r"/RevokeMsg", RevokeMsg),
    (r"/DelFriend", DelFriend),
    (r"/BanFriend", BanFriend),
    (r"/SetFriendName", SetFriendName),
    (r"/SetChatroomAnnouncement", SetChatroomAnnouncement),
    ])
 
if __name__ == "__main__":
    prot = open('port.txt').read()
    application.listen(int(prot))
    print(str(prot)+" 启动")
    tornado.ioloop.IOLoop.instance().start()
