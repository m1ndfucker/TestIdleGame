using VadimskyiLab.Events;

namespace Events {
	public class ChangeBalanceEvent : EventBase<ChangeBalanceEvent, ChangeBalanceEventData> {}

	public readonly struct ChangeBalanceEventData {
		public readonly double BalanceDifference;
		public ChangeBalanceEventData(double balanceDiff) => BalanceDifference = balanceDiff;
	}
}