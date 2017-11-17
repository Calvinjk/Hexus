using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexusMapGenerator : MonoBehaviour {
    [SerializeField]
    protected Vector2Int mapSize;
    [SerializeField]
    protected MapVisualisation mapVisualisation;

    public void SetMapSize(Vector2Int mapSize) {
        this.mapSize = mapSize;
    }

    //TODO
}
