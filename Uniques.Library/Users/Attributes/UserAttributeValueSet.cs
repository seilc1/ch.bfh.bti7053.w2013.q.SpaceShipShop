using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Users.Attributes
{
    public class UserAttributeValueSet
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public int AttributeTypeId { get; set; }
    }
}
