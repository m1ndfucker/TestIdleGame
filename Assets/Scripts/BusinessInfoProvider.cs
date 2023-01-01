using Controllers;
using Data;
using UI;

public class BusinessInfoProvider {
	BusinessConfigData         _configData;
	BusinessEntitiesController _businessEntitiesController;
	BalanceController          _balanceController;

	public BusinessInfoProvider(BusinessConfigData configData, BusinessEntitiesController businessEntitiesController, BalanceController balanceController) {
		_configData                 = configData;
		_businessEntitiesController = businessEntitiesController;
		_balanceController          = balanceController;
	}

	public BusinessPanelView GetBusinessPrefab() => _configData.BusinessViewPrefab;

	public string GetBusinessNameByIndex(int index) => _configData.GetBusinessNameByIndex(index);
	public double GetBusinessBaseIncome(int index)  => _configData.GetBusinessIncomeByIndex(index);

	public float  GetUpgradeIncomePercentByIndex(int entityIndex, int upgradeIndex)  => _configData.GetUpgradeIncomeMultPercentByIndex(entityIndex, upgradeIndex);
	public float  GetUpgradeIncomeMultCostByIndex(int entityIndex, int upgradeIndex) => _configData.GetUpgradeIncomeMultCostByIndex(entityIndex, upgradeIndex);
	public double GetBusinessBaseLevelCost(int entityIndex)                          => _configData.GetBusinessBaseLevelCost(entityIndex);
	public float  GetBaseIncomeDelay(int entityIndex)                                => _configData.GetIncomeDelay(entityIndex);

	public double GetBaseIncome(int entityIndex)            => _configData.GetIncome(entityIndex);
	public double GetBusinessLevelCost(int entityIndex)     => _businessEntitiesController.BusinessEntityList[entityIndex].GetLevelCost();
	public bool   IsFirstUpgradeMultBought(int entityIndex) => _businessEntitiesController.BusinessEntityList[entityIndex].FirstUpgradeMult > 0;
	public bool   IsSecondUpgradeMultBought(int entityIndex) => _businessEntitiesController.BusinessEntityList[entityIndex].SecondUpgradeMult > 0;

	public bool CanBuyUpgrade(double upgradeCost) => _balanceController.GetBalance() >= upgradeCost;

	public BusinessEntity GetBusinessEntityByIndex(int entityIndex) => _businessEntitiesController.BusinessEntityList[entityIndex];
}