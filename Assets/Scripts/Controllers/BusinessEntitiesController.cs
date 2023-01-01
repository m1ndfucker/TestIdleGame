using System.Collections.Generic;
using Data;
using SaveSystem;

namespace Controllers {
	public class BusinessEntitiesController : BaseController, ISavableEntity {
		public List<BusinessEntity> BusinessEntityList = new();
		BusinessConfigData          _businessConfig;

		public BusinessEntitiesController(BusinessConfigData businessConfig) => _businessConfig = businessConfig;

		public override void Deinit() {
			base.Deinit();
			foreach ( var businessEntity in BusinessEntityList ) {
				businessEntity.Deinit();
			}
		}

		public void UpdateEntitiesTick(float deltaTime) {
			for ( var i = 0; i < BusinessEntityList.Count; i++ ) {
				BusinessEntityList[i].EntityTick(deltaTime);
			}
		}

		public BusinessEntity TryAddBusinessEntity() {
			var newBusinessEntity = new BusinessEntity(BusinessEntityList.Count, _businessConfig);
			BusinessEntityList.Add(newBusinessEntity);
			return newBusinessEntity;
		}

		public void UpgradeBusinessByIndex(int businessEntityIndex) {
			BusinessEntityList[businessEntityIndex].UpgradeEntity();
		}

		public List<BusinessEntityState> GetAllBusinessEntitiesList() {
			var list = new List<BusinessEntityState>();
			foreach ( var businessEntity in BusinessEntityList ) {
				list.Add(businessEntity.GetStateForSave());
			}
			return list;
		}

		public void Save(ref GameData gameData) {
			gameData.EntitiesStates = new List<BusinessEntityState>(GetAllBusinessEntitiesList());
		}

		public void Load(GameData gameData) {
			for ( var i = 0; i < BusinessEntityList.Count; i++ ) {
				var businessEntity = BusinessEntityList[i];
				businessEntity.LoadState(gameData.EntitiesStates[i]);
			}
		}
	}
}