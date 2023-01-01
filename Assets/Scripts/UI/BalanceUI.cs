using Events;
using TMPro;
using UnityEngine;

namespace UI {
	public class BalanceUI : MonoBehaviour {
		public const string BalanceTemplate = "Баланс: {0}";

		public TMP_Text BalanceText;

		void Awake() {
			BalanceChangedEvent.Subscribe(OnBalanceChanged);
		}

		void OnDestroy() {
			BalanceChangedEvent.Unsubscribe(OnBalanceChanged);
		}

		void OnBalanceChanged(BalanceChangedEventData evt) {
			BalanceText.SetText(string.Format(BalanceTemplate, evt.NewBalance.ToString()));
		}
	}
}