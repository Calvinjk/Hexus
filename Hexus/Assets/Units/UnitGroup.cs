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
		public List<Unit> units; 			// Units that make up the group
		public UnitOrder myOrder;			// Order that this group of units are assigned
		private Player player;				// Player this unit group belongs to
        protected Vector3Int curPosition;   // The cube coordinates of the hex this group is occupying

        private GameManager gameManager;    // Reference to the GameManager

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


        ///////////////////////////////////////////// METHODS /////////////////////////////////////////////

        // Get a reference to the GameManager upon awake
        void Awake() {
            gameManager = (GameManager)(GameObject.Find("GameManager").GetComponent(typeof(GameManager)));
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

        // If the player "selects" this UnitGroup, lets allow it actions!
        void OnMouseUpAsButton() {
            if (gameManager.selectedUnitGroup) { gameManager.selectedUnitGroup.HightlightCurrentHex(Color.blue); }
            gameManager.selectedUnitGroup = this;
            HightlightCurrentHex(Color.blue);
            HighlightMovementRange(Color.green);
        }

        private List<Vector3> getUnitPositions(int numUnits) {
            List<Vector3> positions = new List<Vector3>();
            switch (numUnits) {
                case 9:
                    positions.Add(new Vector3(-4, 0, 4));
                    positions.Add(new Vector3(0, 0, 4));
                    positions.Add(new Vector3(4, 0, 4));
                    positions.Add(new Vector3(-4, 0, 0));
                    positions.Add(new Vector3(0, 0, 0));
                    positions.Add(new Vector3(4, 0, 0));
                    positions.Add(new Vector3(-4, 0, -4));
                    positions.Add(new Vector3(0, 0, -4));
                    positions.Add(new Vector3(4, 0, -4));
                    break;
                case 8:
                    positions.Add(new Vector3(-4, 0, 4));
                    positions.Add(new Vector3(0, 0, 4));
                    positions.Add(new Vector3(4, 0, 4));
                    positions.Add(new Vector3(-2, 0, 0));
                    positions.Add(new Vector3(2, 0, 0));
                    positions.Add(new Vector3(-4, 0, -4));
                    positions.Add(new Vector3(0, 0, -4));
                    positions.Add(new Vector3(4, 0, -4));
                    break;
                case 7:
                    positions.Add(new Vector3(-4, 0, 4));
                    positions.Add(new Vector3(0, 0, 4));
                    positions.Add(new Vector3(4, 0, 4));
                    positions.Add(new Vector3(0, 0, 0));
                    positions.Add(new Vector3(-4, 0, -4));
                    positions.Add(new Vector3(0, 0, -4));
                    positions.Add(new Vector3(4, 0, -4));
                    break;
                case 6:
                    positions.Add(new Vector3(0, 0, 4));
                    positions.Add(new Vector3(-4, 0, 2));
                    positions.Add(new Vector3(4, 0, 2));
                    positions.Add(new Vector3(-4, 0, -2));
                    positions.Add(new Vector3(4, 0, -2));
                    positions.Add(new Vector3(0, 0, -4));
                    break;
                case 5:
                    positions.Add(new Vector3(-4, 0, 4));
                    positions.Add(new Vector3(4, 0, 4));
                    positions.Add(new Vector3(0, 0, 0));
                    positions.Add(new Vector3(-4, 0, -4));
                    positions.Add(new Vector3(4, 0, -4));
                    break;
                case 4:
                    positions.Add(new Vector3(-3, 0, 3));
                    positions.Add(new Vector3(3, 0, 3));
                    positions.Add(new Vector3(-3, 0, -3));
                    positions.Add(new Vector3(3, 0, -3));
                    break;
                case 3:
                    positions.Add(new Vector3(0, 0, 4));
                    positions.Add(new Vector3(-4, 0, -2));
                    positions.Add(new Vector3(4, 0, -2));
                    break;
                case 2:
                    positions.Add(new Vector3(-3, 0, 0));
                    positions.Add(new Vector3(3, 0, 0));
                    break;
                case 1:
                    positions.Add(new Vector3(0, 0, 0));
                    break;
            }
            return positions;
        }

        // Give a unitgroup a hex to move to and it should issue orders to all of its sub-units to move there
        // after checking if the move is possible
        // TODO - More sophisticated checking if a move is valid
        public bool MoveTo(Vector3Int destinationHex) {
            // Check if the destination is within the UnitGroup's speed
            if (Hex.Distance(curPosition, destinationHex) <= this.Speed) {
                // Set new current hex and move all sub-units to the new hex
                // TODO - for now, the code works for one unit only.  This function will need to tell each unit where to go so it looks nice in the hex.
                HightlightCurrentHex(Color.white);
                HighlightMovementRange(Color.white);
                curPosition = destinationHex;
                this.transform.position = HexConversions.CubeCoordToWorldPosition(destinationHex);
                List<Vector3> unitPositions = getUnitPositions(units.Count);
                for(int i = 0; i < units.Count; ++i) {
                    units[i].MoveTo(this.transform.position + unitPositions[i]);
                }
                return true;
            } else {
                Debug.Log("Squad attempted to move to a position outside of its range of movement");
                return false;
            }
        }

        public void HightlightCurrentHex(Color color) {
            Vector2Int hexPos = HexConversions.CubeCoordToOffsetCoord(curPosition);
            Tile tile = (Tile)gameManager.map.Tiles[hexPos.x, hexPos.y].GetComponent(typeof(Tile));
            tile.HightlightHex(color);
        }

        public void HighlightMovementRange(Color color) {
            List<Vector2Int> validHexs = Hex.GetAllWithinManhattanRange(curPosition, Speed, false);
            foreach (Vector2Int hexOffset in validHexs) {
                // Sterilize first
                if (hexOffset.x < 0 || hexOffset.y < 0) continue;
                if (hexOffset.x >= gameManager.map.MapSize.x || hexOffset.y >= gameManager.map.MapSize.y) continue;

                Tile tile = (Tile)gameManager.map.Tiles[hexOffset.x, hexOffset.y].GetComponent(typeof(Tile));
                tile.HightlightHex(color);
            }
        }
    }
}

