using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {

	public abstract class Unit : MonoBehaviour {

		public enum GameStatus { Alive, Dead }; 

        [SerializeField] protected string name;
        [SerializeField] protected int size;
        [SerializeField] protected int maxHealth;
        [SerializeField] protected int curHealth;
		[SerializeField] protected int power;
	    [SerializeField] protected int speed;
		[SerializeField] protected int range;
		[SerializeField] protected int defense;
		[SerializeField] protected GameStatus myStatus = GameStatus.Alive;
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

        public void MoveTo(Vector3 worldCoordinates) {
            this.gameObject.transform.position = worldCoordinates;
        }

		// Apply a damage to reduce this unit's health
		public void ApplyDamage(int damage) {
			this.curHealth -= damage;
			if (this.curHealth <= 0) {
				this.Die();
			}
		}

		// Pronounce this unit dead
		private void Die() {
			this.myStatus = GameStatus.Dead;
		}
	}
}