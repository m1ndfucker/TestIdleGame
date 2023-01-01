using System.IO;
using UnityEngine;

namespace SaveSystem {
	public static class SaveFileHandler {
		public static void WriteDataToJson(GameData data) {
			var jsonFilePath = DataPath();
			CheckFileExistence(jsonFilePath);

			var dataString = JsonUtility.ToJson(data);
			File.WriteAllText(jsonFilePath, dataString);
		}

		public static GameData ReadDataFromJson() {
			var jsonFilePath = DataPath();

			if ( !File.Exists(jsonFilePath) ) {
				return null;
			}

			var dataString = File.ReadAllText(jsonFilePath);
			var loadedData = JsonUtility.FromJson<GameData>(dataString);
			return loadedData;
		}

		static string DataPath() {
			if ( Directory.Exists(Application.persistentDataPath) ) {
				return Path.Combine(Application.persistentDataPath, "idleTestSave");
			}
			return Path.Combine(Application.streamingAssetsPath, "idleTestSave");
		}

		static void CheckFileExistence(string filePath) {
			if ( !File.Exists(filePath) ) {
				File.Create(filePath).Close();
			}
		}
	}
}