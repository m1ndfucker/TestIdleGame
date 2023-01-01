using System.Collections.Generic;
using Controllers;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class BusinessPanelView : MonoBehaviour {
		const string LevelTextTemplate  = "LVL:\n{0}";
		const string IncomeTextTemplate = "Доход:\n{0}$";
		const string BusinessNameTag    = "Business{0}Name";

		public TMP_Text BusinessNameText;
		public TMP_Text LevelText;
		public TMP_Text IncomeText;

		public List<UpgradeMultButtonView> UpgradeMultButtonsList;
		public Slider                      TimeSlider;
		public UpgradeButtonView           UpgradeButton;

		BusinessInfoProvider _businessInfoProvider;
		BusinessEntity       _businessEntity;
		BalanceController    _balanceController;
		LocaleController     _localeController;

		public void Init(BusinessEntity businessEntity, BusinessInfoProvider businessInfoProvider, BalanceController balanceController, LocaleController localeController) {
			_businessEntity       = businessEntity;
			_businessInfoProvider = businessInfoProvider;
			_balanceController    = balanceController;
			_localeController     = localeController;

			_businessEntity.OnTick += OnEntityTick;
			_businessEntity.OnLoad += OnLoadEntity;

			InitView();
			AfterBusinessUpgradeEvent.Subscribe(OnBusinessUpgrade);
		}

		void InitView() {
			BusinessNameText.SetText(_localeController.GetLocaleText($"Business{_businessEntity.EntityIndex + 1}Name"));
			for ( var i = 0; i < UpgradeMultButtonsList.Count; i++ ) {
				UpgradeMultButtonsList[i].Init(_businessInfoProvider, _localeController, _businessEntity.EntityIndex, i);
				UpgradeMultButtonsList[i].UpdateInfo();
			}
			UpgradeButton.Init(_businessInfoProvider, _balanceController, _businessEntity.EntityIndex);
			UpdateBusinessInfo();
		}

		void OnDestroy() {
			AfterBusinessUpgradeEvent.Unsubscribe(OnBusinessUpgrade);
			_businessEntity.OnTick -= OnEntityTick;
			_businessEntity.OnLoad -= OnLoadEntity;
		}

		void OnEntityTick(float progress) {
			TimeSlider.value = progress;
		}

		public void UpdateBusinessInfo() {
			LevelText.SetText(string.Format(LevelTextTemplate, _businessEntity.EntityLevel));
			IncomeText.SetText(string.Format(IncomeTextTemplate, _businessEntity.CurrentIncome()));
			UpgradeButton.UpdateCost();
		}

		public void UpdateBusinessStatsInfo() {
			foreach ( var upgradeMultButtonView in UpgradeMultButtonsList ) {
				upgradeMultButtonView.UpdateInfo();
			}
		}

		void OnBusinessUpgrade(AfterBusinessUpgradeEventData evt) {
			if ( _businessEntity.EntityIndex == evt.UpgradedEntityIndex ) {
				UpdateBusinessInfo();
			}
		}

		void OnLoadEntity() {
			UpdateBusinessInfo();
			UpdateBusinessStatsInfo();
		}
	}
}