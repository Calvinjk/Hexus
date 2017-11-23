using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class HexusMapGenerator : MonoBehaviour {
        [SerializeField]
        protected Vector2Int mapSize;

        public Tile tilePrefab;

        public void SetMapSize(Vector2Int mapSize) {
            this.mapSize = mapSize;
        }

        public MapInfo GenerateMap() {
            Tile[,] tiles = GenerateTiles();

            MapInfo map = new MapInfo(mapSize, tiles);
            return map;
        }

        private Tile[,] GenerateTiles() {
            Tile[,] tiles = new Tile[mapSize.x, mapSize.y];

            for (int y = 0; y < mapSize.y; ++y) {
                for (int x = 0; x < mapSize.x; ++x) {
                    // TODO - Deal with terrain randomization here

                    Vector2Int offsetCoordinates = new Vector2Int(x, y);
                    Vector3Int cubeCoordinates = HexConversions.OffsetCoordToCubeCoord(offsetCoordinates);
                    Vector3 worldCoordinates = HexConversions.OffsetCoordToWorldPosition(offsetCoordinates);

                    // Add generated tile to array
                    Tile t = new Tile(cubeCoordinates);
                    tiles[x, y] = t;

                    // Create the tile itself in the world view and assign its variables    
                    Tile tile = GameObject.Instantiate<Tile>(tilePrefab);
                    tile.transform.SetParent(transform, false);
                    tile.transform.localPosition = worldCoordinates;
                    tile.name = cubeCoordinates.ToString();
                    ((Tile)tile.GetComponent(typeof(Tile))).cubeCoordinates = cubeCoordinates;

                    // Make the tile visible
                    Mesh mesh = tile.GetComponent<MeshFilter>().mesh = new Mesh();
                    List<Vector3> verticies = new List<Vector3>();
                    List<int> triangles = new List<int>();

                    //Create each hexagon using 4 triangles
                    int counter = 0;
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[3]);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[4]);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[5]);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[3]);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[5]);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[0]);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[3]);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[0]);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[1]);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[3]);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[1]);
                    verticies.Add(worldCoordinates + HexMetrics.pointyCorners[2]);
                    triangles.Add(counter++);
                    triangles.Add(counter++);
                    triangles.Add(counter++);

                    //Put the triangles into the mesh
                    mesh.vertices = verticies.ToArray();
                    mesh.triangles = triangles.ToArray();
                    mesh.RecalculateNormals();
                }
            }

            return tiles;
        }
    }
}