using VadimskyiLab.Events;

namespace Events {
	public class BalanceChangedEvent : EventBase<BalanceChangedEvent, BalanceChangedEventData> {}

	public readonly struct BalanceChangedEventData {
		public readonly double NewBalance;
		public BalanceChangedEventData(double balance) => NewBalance = balance;
	}
}