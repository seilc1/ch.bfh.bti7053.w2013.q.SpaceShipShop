using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uniques.Models;

namespace Uniques.Controllers
{
	public class TemplateController : Controller
	{
		// GET: /Template/{name}
		public ActionResult Index(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Template must be specified", "name");
			}

			try
			{
				return View(new TemplateModel { TemplateName = name });
			}
			catch(InvalidOperationException)
			{
				return null;
			}
		}
	}
}
