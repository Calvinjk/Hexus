using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class HexusMapGenerator : MonoBehaviour {
        [SerializeField]
        protected Vector2Int mapSize;
        public float boundrySize = .9f; // This scales the hexagon size down to boundrySize % to create borders of space between hexs
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
                    tile.GetComponent<BoxCollider>().size = new Vector3(HexMetrics.hexSize, 0.1f, HexMetrics.hexSize);

                    // Make the tile visible
                    Mesh mesh = tile.GetComponent<MeshFilter>().mesh = new Mesh();
                    List<Vector3> verticies = new List<Vector3>();
                    List<int> triangles = new List<int>();

					Vector3 tilePosition = tile.transform.position;

                    //Create each hexagon using 4 triangles
                    int counter = 0;
					verticies.Add(HexMetrics.pointyCorners[3] * boundrySize);
                    verticies.Add(HexMetrics.pointyCorners[4] * boundrySize);
                    verticies.Add(HexMetrics.pointyCorners[5] * boundrySize);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    verticies.Add(HexMetrics.pointyCorners[3] * boundrySize);
                    verticies.Add(HexMetrics.pointyCorners[5] * boundrySize);
                    verticies.Add(HexMetrics.pointyCorners[0] * boundrySize);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    verticies.Add(HexMetrics.pointyCorners[3] * boundrySize);
                    verticies.Add(HexMetrics.pointyCorners[0] * boundrySize);
                    verticies.Add(HexMetrics.pointyCorners[1] * boundrySize);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    verticies.Add(HexMetrics.pointyCorners[3] * boundrySize);
                    verticies.Add(HexMetrics.pointyCorners[1] * boundrySize);
                    verticies.Add(HexMetrics.pointyCorners[2] * boundrySize);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    triangles.Add(counter++);

                    //Put the triangles into the mesh
                    mesh.vertices = verticies.ToArray();
                    mesh.triangles = triangles.ToArray();
                    mesh.RecalculateNormals();
					mesh.RecalculateTangents();
                    mesh.RecalculateBounds();

                    // Add generated tile to array
                    tiles[x, y] = tile;
                }
            }

            return tiles;
        }
    }
}