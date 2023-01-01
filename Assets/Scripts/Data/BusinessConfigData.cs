using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Data {
	[CreateAssetMenu(menuName = "Game/Business Settings")]
	public class BusinessConfigData : ScriptableObject {
		[Serializable]
		public class Business {
			public string            BusinessName;
			public float             Delay;
			public double            BaseCost;
			public double            BaseIncome;
			public BusinessUpgrade[] BusinessUpgrades;
		}

		[Serializable]
		public class BusinessUpgrade {
			public float UpgradeCost;
			public float IncomeMultPercent;
		}

		public BusinessPanelView BusinessViewPrefab;
		public List<Business>    AllBusinesses;

		public string GetBusinessNameByIndex(int index) => AllBusinesses[index].BusinessName;

		public double GetBusinessIncomeByIndex(int index)       => AllBusinesses[index].BaseIncome;
		public double GetBusinessBaseLevelCost(int entityIndex) => AllBusinesses[entityIndex].BaseCost;

		public float GetUpgradeIncomeMultPercentByIndex(int entityIndex, int upgradeIndex) => AllBusinesses[entityIndex].BusinessUpgrades[upgradeIndex].IncomeMultPercent;
		public float GetUpgradeIncomeMultCostByIndex(int entityIndex, int upgradeIndex)    => AllBusinesses[entityIndex].BusinessUpgrades[upgradeIndex].UpgradeCost;

		public float GetIncomeDelay(int entityIndex) => AllBusinesses[entityIndex].Delay;

		public double GetIncome(int entityIndex) => AllBusinesses[entityIndex].BaseIncome;
	}
}