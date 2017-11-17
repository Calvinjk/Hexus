using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class MapVisuals : MonoBehaviour {

        public GameObject Map { get; protected set; }

        public delegate Vector3 TileWorldPositionCalculator(Vector2Int pos);

        [SerializeField]
        private Material mapMaterial;

        private int vertsPerTile;
        [SerializeField]
        bool tilesHaveCentroid = false;

        // Creates a gameobject with a mesh containing all tiles
        public void CreateMapGeometry(Vector2Int mapSize) {
            vertsPerTile = 6;
            TileWorldPositionCalculator offsetCalculator = HexConversions.OffsetCoordToWorldPosition;
            List<Polygon> polygons = new List<Polygon>();
            Polygon hexagon = Polygon.CreateNGon(vertsPerTile, Vector3.forward, Vector3.up, tilesHaveCentroid);
            vertsPerTile = hexagon.Vertices.Length;

            for (int y = 0; y < mapSize.y; y++) {
                for (int x = 0; x < mapSize.x; x++) {
                    Vector3 tileOffset = offsetCalculator(new Vector2Int(x, y));
                    Polygon p = hexagon.OffsetPosition(tileOffset);
                    polygons.Add(p);
                }
            }

            Polygon combined = Polygon.Combine(polygons);

            Map = new GameObject();
            Map.AddComponent<MeshFilter>();
            Map.AddComponent<MeshRenderer>();

            Map.GetComponent<MeshFilter>().mesh = combined.ToMesh();
            Map.GetComponent<MeshRenderer>().material = mapMaterial;
            Map.name = "Map";
        }
    }
}
