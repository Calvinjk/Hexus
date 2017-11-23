using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class GameManager : MonoBehaviour {

        [SerializeField]
        private HexusMapGenerator mapGenerator = null;

        public MapInfo map;

        void Start() {
            InitMap();
        }

        private void InitMap() {
            map = mapGenerator.GenerateMap();
        }
    }
}