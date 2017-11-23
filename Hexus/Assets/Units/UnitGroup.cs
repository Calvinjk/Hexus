using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace placeholder.Hexus {

	// Unit groups can't have a total size exceeding their Max Size
	public class UnitGroupSizeException : Exception {
		public UnitGroupSizeException(string message): base(message) {
		}
	}
		
	public class UnitGroup : MonoBehaviour {

		public static int MaxSize = 10;		// Total default size that a UnitGroup can hold
		private List<Unit> units; 			// Units that make up the group
		public UnitOrder myOrder;			// Order that this group of units are assigned
		private Player player;				// Player this unit group belongs to

		public Player Player {
			get {
				return this.player;
			}
		}

		public UnitGroup() {
			this.units = new List<Unit>();
		}
		public UnitGroup(List<Unit> units) {
			this.units = units;
		}

		// Size is summed by the size of the individual units
		public int Size {
			get {
				int totalSize = 0;
				foreach (Unit unit in this.units) {
					totalSize += unit.Size;
				}
				return totalSize;
			}
		}
			
		// Power is summed by the power of the individual units
		public int Power {
			get {
				int totalPower = 0;
				foreach (Unit unit in this.units) {
					totalPower += unit.Power;
				}
				return totalPower;
			}
		}

		// Speed is the minimum speed of the units
		public int Speed {
			get {
				int lowestSpeed = int.MaxValue;
				foreach (Unit unit in this.units) {
					if (unit.Speed < lowestSpeed)
						lowestSpeed = unit.Speed;
				}
				return lowestSpeed;
			}
		}

		// Range is the minimum range of the units
		public int Range {
			get {
				int lowestRange = int.MaxValue;
				foreach (Unit unit in this.units) {
					if (unit.Range < lowestRange)
						lowestRange = unit.Range;
				}
				return lowestRange;
			}
		}

		// Defense is summed by the defense of the individual units
		public int Defense {
			get {
				int totalDefense = 0;
				foreach (Unit unit in this.units) {
					totalDefense += unit.Defense;
				}
				return totalDefense;
			}
		}

		// Incorporate another unit into this unit group
		public void addUnit (Unit unit) {
			if (this.Size + unit.Size > UnitGroup.MaxSize)
				throw new UnitGroupSizeException("Unit cannot fit into the unit group");
			else
				this.units.Add(unit);
		}

		// Remove an existing unit from group
		public void removeUnit (Unit unit) {
			if (this.Size - unit.Size <= 0)
				throw new UnitGroupSizeException("Last unit cannot be removed from a unit group");
			else
				this.units.Remove(unit);
            this.units.Remove(unit);
		}

		// Apply damage to affect the individual units making up the unit group
		public void ApplyDamage(int damage) {
			System.Random rnd = new System.Random();
			for (int i = 0; i < damage; i++) {
				// Apply the damage amongst alive units. Units may die per iteration
				List<Unit> aliveUnits = this.units.FindAll(delegate(Unit unit) {
					return Unit.GameStatus.Alive == unit.Status;
				});
				int index = rnd.Next(aliveUnits.Count);
				aliveUnits[index].ApplyDamage(1);
			}
            // Probably want to add code later to remove dead units from group
		}

        // Retrieve a list of orders this unit group can perform.
        // Orders are limited to orders that are common throughout all units
		public List<UnitOrder.OrderType> AvailableOrders {
			get {
				// Count all the order types that the units have, store in dict
				Dictionary<UnitOrder.OrderType, int> orderTypeCounts = new Dictionary<UnitOrder.OrderType, int>();
				foreach (Unit unit in this.units) {
					foreach (UnitOrder.OrderType unitOrder in unit.AvailableOrders) {
						// orderType already is in the dictionary, increment its count
						if (orderTypeCounts.ContainsKey(unitOrder)) {
							orderTypeCounts.Add(unitOrder, orderTypeCounts[unitOrder]++);
						}
						else {
							orderTypeCounts.Add(unitOrder, 1);
						}
					}
				}
				// Compare the counts of each unit type; if count == # units, all units have that order type
				int totalUnits = this.units.Count;
				List<UnitOrder.OrderType> availableOrders = new List<UnitOrder.OrderType>();
				foreach (KeyValuePair<UnitOrder.OrderType, int> pair in orderTypeCounts) {
					if (pair.Value == totalUnits)
						availableOrders.Add(pair.Key);
				}
				return availableOrders;
			}
        }
	}
}

