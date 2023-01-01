using Events;
using SaveSystem;

namespace Controllers {
	public class BalanceController : BaseController, ISavableEntity {
		double _balance;

		public BalanceController() {
			ChangeBalanceEvent.Subscribe(OnChangeBalance);
		}

		public override void Deinit() {
			base.Deinit();
			ChangeBalanceEvent.Unsubscribe(OnChangeBalance);
			;
		}

		public double GetBalance() => _balance;

		public void Save(ref GameData gameData) {
			gameData.Balance = GetBalance();
		}

		public void Load(GameData data) {
			_balance = data.Balance;
			BalanceChangedEvent.Invoke(new BalanceChangedEventData(_balance));
		}

		void OnChangeBalance(ChangeBalanceEventData evt) {
			_balance += evt.BalanceDifference;
			BalanceChangedEvent.Invoke(new BalanceChangedEventData(_balance));
		}
	}
}