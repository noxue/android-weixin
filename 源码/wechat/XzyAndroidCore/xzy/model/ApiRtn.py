class LoginRtn:
	def __init__(self, code, msg, session_key, uin, wxid, nickName, alias):  # 第一个参数必须是self
		self.code = code
		self.msg = str(msg)
		self.session_key = str(session_key)
		self.uin = uin
		self.wxid = wxid
		self.nickName = nickName
		self.alias = alias

class InitRtn:
	def __init__(self,code,msg):
		self.code=code
		self.msg=msg

class MsgRtn:
	def __init__(self, tagtype, msgtype, serverid, createTime, fromid, toid, content):
		self.tagtype = tagtype
		self.msgtype = msgtype
		self.serverid = serverid
		self.createTime = createTime
		self.fromid = fromid
		self.toid = toid
		self.content = content

class ContactRtn:
	def __init__(self,wxid,nickname,remarkname,alias,headimg,v1,ctype,sex,
	country,sheng,shi,qianming,registerbody,src):
		self.wxid=str(wxid)
		self.nickname = str(nickname)
		self.remarkname = str(remarkname)
		self.alias = alias
		self.headimg = headimg
		self.v1 = v1
		self.ctype = ctype
		self.sex = sex
		self.country = country
		self.sheng = sheng
		self.shi = shi
		self.qianming = qianming
		self.registerbody = registerbody
		self.src = src
		


		





