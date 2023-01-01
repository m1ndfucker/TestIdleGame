using VadimskyiLab.Events;

namespace Events {
	public class AfterBusinessUpgradeEvent : EventBase<AfterBusinessUpgradeEvent, AfterBusinessUpgradeEventData> {}

	public readonly struct AfterBusinessUpgradeEventData {
		public readonly int UpgradedEntityIndex;
		public  AfterBusinessUpgradeEventData(int entityIndex) => UpgradedEntityIndex = entityIndex;
	}
}