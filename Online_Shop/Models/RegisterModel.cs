using BotDetect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Shop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }

        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        public string UserName { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage ="Mật khẩu không được bỏ trống")]
        [StringLength(20, MinimumLength = 6, ErrorMessage ="Mật khẩu phải có độ dài từ 6 đến 20 ký tự")]
        public string Password { get; set; }

        [DisplayName("Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage ="Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Họ tên")]
        public string Name { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Hãy nhập Email")]
        public string Email { get; set; }

        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

        [DisplayName("Tỉnh/thành")]
        public string ProvinceID { get; set; }

        [DisplayName("Quận/huyện")]
        public string DistrictID { get; set; }

        [DisplayName("Xã/phường")]
        public string PrecinctID { get; set; }
    }
}