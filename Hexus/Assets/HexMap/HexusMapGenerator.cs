using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class HexusMapGenerator : MonoBehaviour {
        [SerializeField]
        protected Vector2Int mapSize;
        public GameObject tilePrefab;

        public void SetMapSize(Vector2Int mapSize) {
            this.mapSize = mapSize;
        }

        public MapInfo GenerateMap() {
            GameObject[,] tiles = GenerateTiles();

            MapInfo map = new MapInfo(mapSize, tiles);
            return map;
        }

        private GameObject[,] GenerateTiles() {
            GameObject[,] tiles = new GameObject[mapSize.x, mapSize.y];

            for (int y = 0; y < mapSize.y; ++y) {
                for (int x = 0; x < mapSize.x; ++x) {
                    // TODO - Deal with terrain randomization here

                    Vector2Int offsetCoordinates = new Vector2Int(x, y);
                    Vector3Int cubeCoordinates = HexConversions.OffsetCoordToCubeCoord(offsetCoordinates);
                    Vector3 worldCoordinates = HexConversions.OffsetCoordToWorldPosition(offsetCoordinates);

                    // Create the tile itself in the world view and assign its variables    
                    GameObject tile = GameObject.Instantiate(tilePrefab);
                    tile.transform.SetParent(transform, true);
					tile.transform.localPosition = worldCoordinates;
                    tile.name = cubeCoordinates.ToString();
                    ((Tile)tile.GetComponent(typeof(Tile))).cubeCoordinates = cubeCoordinates;

                    // Add generated tile to array
                    tiles[x, y] = tile;
                }
            }

            return tiles;
        }
    }
}