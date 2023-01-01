using VadimskyiLab.Events;

namespace Events {
	public class BusinessMultUpgradeEvent : EventBase<BusinessMultUpgradeEvent, BusinessMultUpgradeEventData> {}

	public readonly struct BusinessMultUpgradeEventData {
		public readonly int UpgradedEntityIndex;
		public readonly int UpgradedIndex;
		public BusinessMultUpgradeEventData(int entityIndex, int upgradeIndex) {
			UpgradedEntityIndex = entityIndex;
			UpgradedIndex       = upgradeIndex;
		}
	}
}