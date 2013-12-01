using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniques.Library.Data;

namespace Uniques.Library.Localization
{
	public class DbTranslationManager
	{
		private readonly Func<UniquesDataContext> _dbContextGetter;
		private readonly Func<CultureInfo> _cultureInfoGetter;

		public DbTranslationManager(Func<UniquesDataContext> dbContextGetter, Func<CultureInfo> cultureInfoGetter)
		{
			_dbContextGetter = dbContextGetter;
			_cultureInfoGetter = cultureInfoGetter;
		}

		public string Translate(string key, int lcid)
		{
			
		}

		public string Translate(string key, CultureInfo culture)
		{
			return Translate(key, culture.LCID);
		}

		public string Translate(string key)
		{
			return Translate(key, _cultureInfoGetter());
		}
	}
}
