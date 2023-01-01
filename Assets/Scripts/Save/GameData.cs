using System;
using System.Collections.Generic;

namespace SaveSystem {
	[Serializable]
	public class GameData {
		public double                    Balance;
		public List<BusinessEntityState> EntitiesStates;

		public GameData() {
			Balance        = 0.0d;
			EntitiesStates = new List<BusinessEntityState>();
		}
	}
}