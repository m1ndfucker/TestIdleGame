using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data {
	[CreateAssetMenu(menuName = "Game/Business Localisation")]
	public class BusinessLocalsData : ScriptableObject {
		[Serializable]
		public class LocalsEntity {
			public string Tag;
			public string Locale;
		}
		public List<LocalsEntity> AllLocals;
	}
}