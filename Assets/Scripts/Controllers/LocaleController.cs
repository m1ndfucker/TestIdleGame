using Data;

namespace Controllers {
	public class LocaleController: BaseController {
		BusinessLocalsData _localsData;

		public LocaleController(BusinessLocalsData localsData) => _localsData = localsData;

		public string GetLocaleText(string stringTag) {
			var localeEntity = _localsData.AllLocals.Find(x => x.Tag == stringTag);
			return localeEntity == null ? "///" : localeEntity.Locale;
		}
	}
}