/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace ChatRoomXmlData
{
    [XmlRoot(ElementName = "ChatRoomData")]
    public class ChatRoomData
    {
        [XmlElement(ElementName = "ChatRoomMemberData")]
        public List<ChatRoomMemberData> ChatRoomMemberData { get; set; }
        [XmlElement(ElementName = "ChatRoomOwner")]
        public string ChatRoomOwner { get; set; }
        [XmlElement(ElementName = "ChatroomUserName")]
        public string ChatroomUserName { get; set; }


        [XmlElement(ElementName = "ChatRoomMemberCount")]
        public int ChatRoomMemberCount { get; set; }
    }

    [XmlRoot(ElementName = "ChatRoomMemberData")]
    public class ChatRoomMemberData
    {
        [XmlElement(ElementName = "BigHeadImgUrl")]
        public string BigHeadImgUrl { get; set; }
        [XmlElement(ElementName = "DisplayName")]
        public string DisplayName { get; set; }
        [XmlElement(ElementName = "InviterUserName")]
        public string InviterUserName { get; set; }
        [XmlElement(ElementName = "NickName")]
        public string NickName { get; set; }
        [XmlElement(ElementName = "UserName")]
        public string UserName { get; set; }
    }

}
