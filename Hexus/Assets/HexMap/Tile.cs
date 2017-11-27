using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Tile : MonoBehaviour {

        public Vector3Int cubeCoordinates;
        private GameManager gameManager;

        public Tile(Vector3Int coord) {
            cubeCoordinates = coord;
        }

        void Awake() {
            gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
        }

        // Basic unit movement
        void OnMouseDownAsButton() {
            print(cubeCoordinates.ToString() + " was clicked on!");
            if (gameManager.selectedUnitGroup) {
                gameManager.selectedUnitGroup.MoveTo(this.cubeCoordinates);

                // Once the unitgroup has moved, deselect the unitgroup
                gameManager.selectedUnitGroup = null;
            }
        }
    }
}
