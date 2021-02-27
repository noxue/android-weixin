import json
import collections

class RowsToJson:
    @classmethod
    def ContactToJson(self,rows):
        objects_list = []
        for row in rows:
            d = collections.OrderedDict()
            d["wxid"]=row[0]
            d["nickname"] = row[1]
            d["remarkname"] = row[2]
            d["alias"] = row[3]
            d["headimg"] = row[4]
            d["v1"] = row[5]
            d["type"] = row[6]
            d["sex"] = row[7]
            d["country"] = row[8]
            d["sheng"] = row[9]
            d["shi"] = row[10]
            d["qianming"] = row[11]
            d["registerbody"] = row[12]
            d["src"] = row[13]
            d["chatroomowner"] = row[14]
            d["chatroomserverVer"] = row[15]
            d["chatroommaxmember"] = row[16]
            d["chatroommembercnt"] = row[17]
            objects_list.append(d)
        j = json.dumps(objects_list)
        return j
