using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisuals : MonoBehaviour {

    public GameObject Map { get; protected set; }

    public delegate Vector3 TileWorldPositionCalculator(Vector2Int pos);

    private int vertsPerTile;
    [SerializeField]
    bool tilesHaveCentroid = false;

    // Creates a gameobject with a mesh containing all tiles
    public void CreateMapGeometry(Vector2Int mapSize) {
        vertsPerTile = 6;
        TileWorldPositionCalculator offsetCalculator = HexConverter.OffsetCoordToWorldPosition;
        //TODO
    }
}
