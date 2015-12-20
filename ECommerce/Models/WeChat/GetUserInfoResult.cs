using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ECommerce.Models.WeChat
{
    [DataContract]
    public class GetUserInfoResult
    {
        [DataMember(IsRequired = false)]
        public string openid { get; set; }

        [DataMember(IsRequired = false)]
        public string nickname { get; set; }

        [DataMember(IsRequired = false)]
        public string sex { get; set; }

        [DataMember(IsRequired = false)]
        public string province { get; set; }

        [DataMember(IsRequired = false)]
        public string city { get; set; }

        [DataMember(IsRequired = false)]
        public string country { get; set; }

        [DataMember(IsRequired = false)]
        public string headimgurl { get; set; }

        [DataMember(IsRequired = false)]
        public string unionid { get; set; }
    }
}