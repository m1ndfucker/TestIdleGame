using System.Collections.Generic;

namespace SaveSystem {
	public class SaveСontroller {
		GameData _data;

		List<ISavableEntity> _savableEntities;

		public SaveСontroller(GameStarter gameStarter) => _savableEntities = gameStarter.GetSavableControllers();

		public void NewGame() {
			_data = new GameData();
		}

		public void Save() {
			foreach ( var savableEntity in _savableEntities ) {
				savableEntity.Save(ref _data);
			}
			SaveFileHandler.WriteDataToJson(_data);
		}

		public void Load() {
			if ( _data == null ) {
				NewGame();
			}

			var data = SaveFileHandler.ReadDataFromJson();

			foreach ( var savableEntity in _savableEntities ) {
				savableEntity.Load(data);
			}
		}
	}
}