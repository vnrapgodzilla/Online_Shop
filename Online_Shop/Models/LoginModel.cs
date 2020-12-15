using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Shop.Models
{
    public class LoginModel
    {
        [Key]

        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage ="Tên đăng nhập không được bỏ trống")]
        public string UserName { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string Password { get; set; }
    }
}