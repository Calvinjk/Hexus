using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo{

    public Vector2Int MapSize { get; private set; }
    public MapVisualisation MapVisualisation { get; private set; }
    public Tile[,] Tiles { get; private set; }

    public MapInfo(Vector2Int mapSize, Tile[,] tiles, MapVisualisation visualisation) {
        MapSize = mapSize;
        Tiles = tiles;
        Edges = edges;
        WrapsHorizontal = wrapsHorizontal;
        MapVisualisation = visualisation;
    }

    public void InitVisualisation() {
        MapVisualisation.CreateMapGeometry(MapSize);
        //TODO
    }
}
