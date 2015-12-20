using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ECommerce.Models.WeChat
{
     [DataContract]
    public class GetAccessTokenResult
    {
         [DataMember(IsRequired = false)]
         public string access_token { get; set; }

         [DataMember(IsRequired = false)]
         public int expires_in { get; set; }

         [DataMember(IsRequired = false)]
         public string refresh_token { get; set; }

         [DataMember(IsRequired = false)]
         public string openid { get; set; }

         [DataMember(IsRequired = false)]
         public string scope { get; set; }

         [DataMember(IsRequired = false)]
         public string unionid { get; set; }

         [DataMember(IsRequired = false)]
         public int errcode { get; set; }

         [DataMember(IsRequired = false)]
         public string errmsg { get; set; }
    }
}