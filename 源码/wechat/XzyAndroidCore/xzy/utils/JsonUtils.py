import json

class JsonUtils:
    @classmethod
    def Obj2Json(cls,obj):
        dictobj = cls.Obj2Dict(obj)
        return json.dumps(dictobj)

    @classmethod
    def List2Json(cls,data):
        jsondt = []
        jsondic = {}
        for index in range(len(data)):
            jsondt.append(index)
        dicdatas=[]
        for obj in data:
            dictobj = cls.Obj2Dict(obj)
            dicdatas.append(dictobj)
        jsondic = dict(zip(jsondt, dicdatas))
        return json.dumps(jsondic)


    @classmethod
    def Obj2Dict(cls, obj):
        pr = {}
        for name in dir(obj):
            value = getattr(obj, name)
            if not name.startswith('__') and not callable(value):
                pr[name] = value
        return pr

