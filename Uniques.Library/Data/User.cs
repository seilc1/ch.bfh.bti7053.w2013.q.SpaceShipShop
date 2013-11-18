using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Data
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Loginname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordSalt { get; set; }
    }

    public class User : UserLogin
    {
        [Required]
        public string Displayname { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Localization.Uniques), ErrorMessageResourceName = "error_email_format")]
        public string Email { get; set; }

        public DateTime LastAction { get; set; }
    }

    public class CompleteUser : User
    {
        public List<UserAttributeValue> Attributes { get; set; }
    }
}
