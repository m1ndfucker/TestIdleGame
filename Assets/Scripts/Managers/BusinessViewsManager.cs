using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Controllers {
	public class BusinessViewsManager : MonoBehaviour {
		public List<BusinessPanelView> BusinessViewList;
		public Transform               BusinessPanelsRoot;

		BusinessInfoProvider _businessInfoProvider;
		BalanceController    _balanceController;
		LocaleController     _localeController;

		public void Init(BusinessInfoProvider businessInfoProvider, BalanceController balanceController, LocaleController localeController) {
			_businessInfoProvider = businessInfoProvider;
			_balanceController    = balanceController;
			_localeController     = localeController;
		}

		public void AddBusinessView(BusinessEntity businessEntity) {
			var businessView = Instantiate(_businessInfoProvider.GetBusinessPrefab(), BusinessPanelsRoot);
			businessView.Init(businessEntity, _businessInfoProvider, _balanceController, _localeController);
			BusinessViewList.Add(businessView);
		}
	}
}