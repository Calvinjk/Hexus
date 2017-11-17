using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {

	public class Player : MonoBehaviour {
		
		public enum Colors { red, blue }; 			// for now
		private List<UnitGroup> myUnitGroups;
		private List<Unit> myUnits;
		private List<UnitOrder> myUnitOrders;


		public Player() {
		}
	}
}

