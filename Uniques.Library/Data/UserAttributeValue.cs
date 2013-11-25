using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Data
{
    public class UserAttributeValue
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        [Required]
        [MaxLength(255)]
        public string Value { get; set; }

        public UserAttribute AttributeType { get; set; }
    }
}
