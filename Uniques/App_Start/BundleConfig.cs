﻿using System.Web;
using System.Web.Optimization;

namespace Uniques
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery")
				.Include("~/Scripts/jquery/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui")
				.Include("~/Scripts/jquery/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval")
				.Include(
				"~/Scripts/jquery/jquery.unobtrusive*",
				"~/Scripts/jquery/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
				"~/Scripts/modernizr-*"));


			bundles.Add(new ScriptBundle("~/bundles/uniques")
				.IncludeDirectory("~/Scripts/uniques", "*.js", true));

			bundles.Add(new ScriptBundle("~/bundles/knockout")
				.Include(
#if DEBUG
				"~/Scripts/knockout/knockout-{version}.debug.js",
#else
				"~/Scripts/knockout/knockout-{version}.js",
#endif
				"~/Scripts/knockout/knockout-enterkey.js",
				"~/Scripts/knockout/knockout.*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap")
				.Include(
#if DEBUG
				"~/Scripts/bootstrap.js"
#else
				"~/Scripts/bootstrap.min.js"
#endif
				));

			bundles.Add(new StyleBundle("~/Content/css")
				.Include(
				"~/Content/bootstrap.min.css",
				"~/Content/bootstrap-theme.min.css",
				"~/Content/site.css"));
			bundles.Add(new StyleBundle("~/Content/themes/base/css")
				.Include(
				"~/Content/themes/base/jquery.ui.core.css",
				"~/Content/themes/base/jquery.ui.resizable.css",
				"~/Content/themes/base/jquery.ui.selectable.css",
				"~/Content/themes/base/jquery.ui.accordion.css",
				"~/Content/themes/base/jquery.ui.autocomplete.css",
				"~/Content/themes/base/jquery.ui.button.css",
				"~/Content/themes/base/jquery.ui.dialog.css",
				"~/Content/themes/base/jquery.ui.slider.css",
				"~/Content/themes/base/jquery.ui.tabs.css",
				"~/Content/themes/base/jquery.ui.datepicker.css",
				"~/Content/themes/base/jquery.ui.progressbar.css",
				"~/Content/themes/base/jquery.ui.theme.css"));

			BundleTable.EnableOptimizations = true;
#if DEBUG
			BundleTable.EnableOptimizations = false;
#endif
		}
	}
}