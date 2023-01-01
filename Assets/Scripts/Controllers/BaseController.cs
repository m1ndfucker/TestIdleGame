using System.Collections.Generic;

namespace Controllers {
	public abstract class BaseController {
		public static HashSet<BaseController> Instances { get; } = new();

		protected BaseController() {
			Instances.Add(this);
		}

		public virtual void Deinit() {
			Instances.Remove(this);
		}
	}
}