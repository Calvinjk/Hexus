using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {

	abstract public class Unit : MonoBehaviour {

		public enum GameStatus { Alive, Dead };

		protected int size;
		protected int power;
	    protected int speed;
		protected int range;
		protected int defense;
		protected GameStatus myStatus = GameStatus.Alive;
		protected List<UnitOrder.OrderType> availableOrders;
		protected Player player;

		public Player Player {
			get {
				return this.player;
			}
		}

		public List<UnitOrder.OrderType> AvailableOrders {
            get {
                return this.availableOrders;
            }
        }

		public GameStatus MyStatus {
			get {
				return this.myStatus;
			}
		}

		public int Size {
			get {
				return this.size;
			}
		}

		public int Power {
			get {
				return this.power;
			}
			set {
				this.power = value;
			}
		}

		public int Speed {
			get {
				return this.speed;
			}
		}

		public int Range {
			get {
				return this.range;
			}
		}

		public int Defense {
			get {
				return this.defense;
			}
		}

		public GameStatus Status {
			get {
				return this.myStatus;
			}
		}

		// Apply a damage to reduce this unit's power
		public void ApplyDamage(int damage) {
			this.power -= damage;
			if (this.power <= 0) {
				this.Die();
			}
		}

		// Pronounce this unit dead
		private void Die() {
			this.myStatus = GameStatus.Dead;
		}
	}
}