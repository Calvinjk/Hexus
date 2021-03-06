﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class MapInfo {

        public Vector2Int MapSize { get; private set; }
        public GameObject[,] Tiles { get; private set; }

        public MapInfo(Vector2Int mapSize, GameObject[,] tiles) {
            MapSize = mapSize;
            Tiles = tiles;
        }

        public Tile GetTileData(Vector3Int coord) {
            Vector2Int offsetCoord = HexConversions.CubeCoordToOffsetCoord(coord);
            return (Tile)Tiles[offsetCoord.x, offsetCoord.y].GetComponent(typeof(Tile));
        }

        public Tile GetTileData(Vector2Int coord) {
            return (Tile)Tiles[coord.x, coord.y].GetComponent(typeof(Tile));
        }

        //TODO - update map if tile data changes
        /*
        public void SetTileData(Vector2Int coord, int value) {
            Tile t = Tiles[coord.x, coord.y];
            Tile newTile = new Tile(value);

            Tiles[coord.x, coord.y] = newTile;
            MapVisuals.UpdateTileFeatures(Tiles);
        }
        */
    }
}