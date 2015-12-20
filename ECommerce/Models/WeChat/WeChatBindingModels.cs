using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models.WeChat
{
    public class WeChatOAuth2BindingModel
    {
        [Required]
        public string Code { get; set; }
    }
}