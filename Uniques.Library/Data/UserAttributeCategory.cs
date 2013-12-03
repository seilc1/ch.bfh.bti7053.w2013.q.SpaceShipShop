using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Uniques.Library.Data
{
	public class UserAttributeCategory
	{
		[Key]
		public int Id { get; set; }

		public string TextKey { get; set; }
	}
}
