using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Tile : MonoBehaviour {

        public Vector3Int cubeCoordinates { get; private set; }

        public Tile(Vector3Int coord) {
            cubeCoordinates = coord;
        }
        
    }
}
