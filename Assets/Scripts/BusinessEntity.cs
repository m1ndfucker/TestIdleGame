using System;
using Data;
using Events;

public class BusinessEntity {
	BusinessConfigData _businessConfig;

	public int         EntityIndex   { get; private set; }
	public float       IncomeDelay   { get; private set; }
	public int         EntityLevel   { get; private set; }
	public float       CurrentTime   { get; private set; }

	public float  FirstUpgradeMult  { get; private set; }
	public float  SecondUpgradeMult { get; private set; }
	public double GetLevelCost()    => (EntityLevel + 1) * _businessConfig.GetBusinessBaseLevelCost(EntityIndex);

	public Action<float> OnTick;
	public Action        OnLoad;

	public BusinessEntity(int entityIndex, BusinessConfigData businessConfig) {
		_businessConfig = businessConfig;

		EntityIndex   = entityIndex;
		IncomeDelay   = _businessConfig.GetIncomeDelay(EntityIndex);

		BusinessUpgradeEvent.Subscribe(OnBusinessLevelUp);
		BusinessMultUpgradeEvent.Subscribe(OnMultUpgrade);
	}

	public void Deinit() {
		BusinessUpgradeEvent.Unsubscribe(OnBusinessLevelUp);
	}

	// Update loop
	public void EntityTick(float deltaTime) {
		if ( EntityLevel == 0 ) {
			return;
		}

		CurrentTime += deltaTime;
		if ( CurrentTime > IncomeDelay ) {
			CurrentTime = 0f;
			ChangeBalanceEvent.Invoke(new ChangeBalanceEventData(CurrentIncome()));
		}
		var percent = CurrentTime / IncomeDelay * 100f;
		OnTick?.Invoke(percent);
	}

	public void UpgradeEntity() {
		EntityLevel++;
		AfterBusinessUpgradeEvent.Invoke(new AfterBusinessUpgradeEventData(EntityIndex));
	}

	// Событие на левелАп бизнеса
	void OnBusinessLevelUp(BusinessUpgradeEventData evt) {
		if ( evt.UpgradedEntityIndex != EntityIndex ) {
			return;
		}
		UpgradeEntity();
	}

	// Событие на апгрейд мультипликатора дохода
	void OnMultUpgrade(BusinessMultUpgradeEventData evt) {
		if ( EntityIndex != evt.UpgradedEntityIndex ) {
			return;
		}
		if ( evt.UpgradedIndex == 0 ) {
			FirstUpgradeMult = _businessConfig.GetUpgradeIncomeMultPercentByIndex(EntityIndex, evt.UpgradedIndex)/100;
		} else {
			SecondUpgradeMult = _businessConfig.GetUpgradeIncomeMultPercentByIndex(EntityIndex, evt.UpgradedIndex)/100;
		}
		OnLoad?.Invoke();
	}

	// Получаем текущий доход
	public double CurrentIncome() {
		var baseIncome = _businessConfig.GetBusinessIncomeByIndex(EntityIndex);
		return EntityLevel * baseIncome * (1 + FirstUpgradeMult + SecondUpgradeMult);
	}

	public void LoadState(BusinessEntityState entityStateData) {
		EntityLevel       = entityStateData.EntityLevel;
		CurrentTime       = entityStateData.CurrentTime;
		FirstUpgradeMult  = entityStateData.FirstUpgradeMult;
		SecondUpgradeMult = entityStateData.SecondUpgradeMult;
		OnLoad?.Invoke();
	}

	public BusinessEntityState GetStateForSave() {
		var state = new BusinessEntityState {
			EntityLevel       = EntityLevel,
			CurrentTime       = CurrentTime,
			FirstUpgradeMult  = FirstUpgradeMult,
			SecondUpgradeMult = SecondUpgradeMult,
		};
		return state;
	}
}