using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace erp.Models
{
    public class UserModel
    {
        //putting validation is in model
        public int id { get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage ="This field is required.")]
        public string username { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        public string password { get; set; }
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Phone must be a natural number")]
        public string phoneNumber { get; set; }
    }


    public class LoginModel
    {
        [Required(ErrorMessage = "This field is required.")]
        public string username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        public string password { get; set; }
    }
}