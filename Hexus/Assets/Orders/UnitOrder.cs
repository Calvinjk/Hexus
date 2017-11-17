using System.Collections;
using System.Collections.Generic;

namespace placeholder.Hexus {

	// UnitOrder instances refer to each individual order that player can assign per unit group, per turn
	abstract public class UnitOrder {

		// OrderType is used to classify types that Units/Unit groups can perform
		public enum OrderType { MoveAttack, Move, Formation };

		protected OrderType myOrderType;

		public OrderType MyOrderType {
			get {
				return this.MyOrderType;
			}
		}

		UnitGroup unitGroup;
	}

	public class MoveAttackOrder : UnitOrder {
		protected OrderType myOrderType = OrderType.MoveAttack;
	}

	public class MoveOrder : UnitOrder {
		protected OrderType myOrderType = OrderType.Move;
	}

	public class FormationOrder : UnitOrder {
		protected OrderType myOrderType = OrderType.Formation;
	}
}

