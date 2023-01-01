using Controllers;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class UpgradeButtonView : MonoBehaviour {
		public const string ButtonNameTemplate = "Level Up";
		public const string CostTemplate       = "Цена:\n{0}$";

		public TMP_Text NameText;
		public TMP_Text CostText;
		public Button   UpgradeButton;

		BusinessInfoProvider _businessInfoProvider;
		BalanceController    _balanceController;

		int    _entityIndex;
		double _levelUpCost;

		public void Init(BusinessInfoProvider businessInfoProvider, BalanceController balanceController, int entityIndex) {
			_businessInfoProvider = businessInfoProvider;
			_entityIndex          = entityIndex;
			_balanceController    = balanceController;

			UpdateInfo();
			CheckButtonAviability(_balanceController.GetBalance());

			UpgradeButton.onClick.AddListener(() => {
				ChangeBalanceEvent.Invoke(new ChangeBalanceEventData(-_businessInfoProvider.GetBusinessLevelCost(_entityIndex)));
				BusinessUpgradeEvent.Invoke(new BusinessUpgradeEventData(entityIndex));
				UpdateCost();
			});

			BalanceChangedEvent.Subscribe(OnBalanceChanged);
		}

		void OnBalanceChanged(BalanceChangedEventData evt) {
			CheckButtonAviability(evt.NewBalance);
		}

		void OnDestroy() {
			UpgradeButton.onClick.RemoveAllListeners();
		}

		public void UpdateInfo() {
			NameText.SetText(ButtonNameTemplate);
			UpdateCost();
		}

		public void UpdateCost() {
			_levelUpCost = _businessInfoProvider.GetBusinessLevelCost(_entityIndex);
			CostText.SetText(string.Format(CostTemplate, _levelUpCost));
		}

		void CheckButtonAviability(double curBalance) {
			UpgradeButton.interactable = curBalance >= _levelUpCost;
		}
	}
}