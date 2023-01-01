using Controllers;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UpgradeMultButtonView : MonoBehaviour {
		const string PercentTemplate = "Доход: + {0}%";
		const string CostTemplate    = "Цена: {0}$";
		const string BoughtTemplate  = "Куплено";

		const string ButtonNameTag = "BusinessUpgrade{0}";

		public TMP_Text NameText;
		public TMP_Text IncomeText;
		public TMP_Text CostText;
		public Button   Button;

		BusinessInfoProvider _businessInfoProvider;
		LocaleController     _localeController;

		int    _entityIndex;
		int    _upgradeIndex;
		double _cost;
		bool   _isBought;

		public void Init(BusinessInfoProvider businessInfoProvider, LocaleController localeController, int entityIndex, int upgradeIndex) {
			_businessInfoProvider = businessInfoProvider;
			_localeController     = localeController;
			_entityIndex          = entityIndex;
			_upgradeIndex         = upgradeIndex;
			NameText.SetText(_localeController.GetLocaleText(string.Format(ButtonNameTag, _upgradeIndex + 1)));
			_cost = _businessInfoProvider.GetUpgradeIncomeMultCostByIndex(_entityIndex, _upgradeIndex);

			CheckButtonAviability();

			Button.onClick.AddListener(() => {
				ChangeBalanceEvent.Invoke(new ChangeBalanceEventData(-_cost));
				BusinessMultUpgradeEvent.Invoke(new BusinessMultUpgradeEventData(_entityIndex, upgradeIndex));
				_isBought = true;
			});
			BalanceChangedEvent.Subscribe(OnBalanceChanged);
		}

		void OnDestroy() {
			Button.onClick.RemoveAllListeners();
		}

		public void UpdateInfo() {
			var percent = _businessInfoProvider.GetUpgradeIncomePercentByIndex(_entityIndex, _upgradeIndex);
			var cost    = _businessInfoProvider.GetUpgradeIncomeMultCostByIndex(_entityIndex, _upgradeIndex);

			if ( _isBought ) {
				CostText.SetText(BoughtTemplate);
			} else {
				CostText.SetText(string.Format(CostTemplate, cost));
			}
			IncomeText.SetText(string.Format(PercentTemplate, percent));
		}

		void OnBalanceChanged(BalanceChangedEventData evt) {
			CheckButtonAviability();
		}

		void CheckButtonAviability() {
			_isBought = _upgradeIndex == 0
				? _businessInfoProvider.IsFirstUpgradeMultBought(_entityIndex)
				: _businessInfoProvider.IsSecondUpgradeMultBought(_entityIndex);

			Button.interactable = !_isBought && _businessInfoProvider.CanBuyUpgrade(_cost);
		}
	}
}