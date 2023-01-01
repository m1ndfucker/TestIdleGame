using VadimskyiLab.Events;

namespace Events {
	public class BusinessUpgradeEvent : EventBase<BusinessUpgradeEvent, BusinessUpgradeEventData> {}

	public readonly struct BusinessUpgradeEventData {
		public readonly int UpgradedEntityIndex;
		public BusinessUpgradeEventData(int entityIndex) => UpgradedEntityIndex = entityIndex;
	}
}