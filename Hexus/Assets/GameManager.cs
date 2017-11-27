using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class GameManager : MonoBehaviour {

        [SerializeField]
        private HexusMapGenerator mapGenerator = null;

        public MapInfo map = null;

        public UnitGroup selectedUnitGroup = null;

        void Start() {
            InitMap();
        }

        private void InitMap() {
            map = mapGenerator.GenerateMap();
        }

        void Update() {
            // Allow deseletion of units
            if (Input.GetKeyDown(KeyCode.Escape)) {
                selectedUnitGroup = null;
            }
        }
    }
}