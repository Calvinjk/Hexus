 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

namespace placeholder.Hexus {
    public static class Hex {

        private static Vector2Int mapSize = new Vector2Int(int.MaxValue, int.MaxValue);
        public static Vector2Int MapSize { get { return mapSize; } }

        public static void SetMapAttributes(Vector2Int size) {
            mapSize = size;
        }

        /// Returns the hex-grid manhattan distance between 2 points
        public static int Distance(Vector3Int posA, Vector3Int posB) {

            int DeltaX = Mathf.Abs(posA.x - posB.x);
            int DeltaY = Mathf.Abs(posA.y - posB.y);
            int DeltaZ = Mathf.Abs(posA.z - posB.z);

            return Mathf.Max(DeltaX, DeltaY, DeltaZ);
        }

        /// Returns all tiles within a certain hex-grid manhattan distance of the origin
        public static HashSet<Vector3Int> GetAllWithinManhattanRange(Vector3Int origin, int range, bool includeSelf) {

            //Performance Tip if this turns out to be a bottleNeck in your games Performance: precalculate all the offsets for range 1,2,3,4,5,6... etc just once and store them in a list or dictionary
            //Don't optimize it when not needed though, keeping things simple should be the priority
            HashSet<Vector3Int> positions = new HashSet<Vector3Int>();

            int minX = origin.x - range;
            int maxX = origin.x + range;
            int minY = origin.y - range;
            int maxY = origin.y + range;

            for (int x = minX; x <= maxX; x++) {
                for (int y = minY; y <= maxY; y++) {
                    int z = -x - y;
                    if (Mathf.Abs(z - origin.z) > range) continue;
                    positions.Add(new Vector3Int(x, y, z));
                }
            }

            if (!includeSelf) positions.Remove(origin);

            return positions;
        }

        /// Returns all tiles which have a specific distance to the origin. Thickness goes inwards
        public static HashSet<Vector3Int> GetRing(Vector3Int origin, int radius, int thickness) {
            //This is also not the most performant way to do it but again for almost all use cases it should be absolutely no issue.
            //If you happen to need a faster solution, again just precalculate the offets for each range once, store it in a list or dictionary and used that stored data 
            HashSet<Vector3Int> ring = new HashSet<Vector3Int>();
            HashSet<Vector3Int> allInManhattanrange = GetAllWithinManhattanRange(origin, radius, false);
            foreach (var v in allInManhattanrange) {
                if (Distance(origin, v) > radius - thickness) {
                    ring.Add(v);
                }
            }

            return ring;
        }

        /// Returns an List forming a line between 2 points.
        public static List<Vector3Int> GetLine(Vector3Int origin, Vector3Int target, float offsetFromOriginCenter, int trimStart, int trimEnd) {
            List<Vector3Int> lineCells = new List<Vector3Int>();

            var dist = Distance(origin, target);
            for (int i = trimStart; i <= dist - trimEnd; i++) {
                Vector3 lerped = HexUtility.LerpCube(origin, target, offsetFromOriginCenter, (1f / dist) * i);
                Vector3Int cell = HexUtility.RoundCube(lerped);
                lineCells.Add(cell);
            }

            return lineCells;
        }

        /// Returns all adjacent tiles of a tile. Uses hardcoded offsets for speed
        public static List<Vector3Int> GetNeighbours(Vector3Int origin) {
            List<Vector3Int> neighbours = new List<Vector3Int> {
                new Vector3Int(origin.x + 1, origin.y, origin.z - 1),
                new Vector3Int(origin.x + 1, origin.y - 1, origin.z),
                new Vector3Int(origin.x, origin.y - 1, origin.z + 1),
                new Vector3Int(origin.x - 1, origin.y, origin.z + 1),
                new Vector3Int(origin.x - 1, origin.y + 1, origin.z),
                new Vector3Int(origin.x, origin.y + 1, origin.z - 1)
            };

            return neighbours;
        }

        /// Returns the edge coordinate between 2 tiles. Edge coordinate is the sum of both tile coordinates.
        public static Vector3Int GetEdgeCoordBetween(Vector3Int a, Vector3Int b) {
            Vector3Int EdgeCoordinate = a + b;
            return EdgeCoordinate;
        }

        /// Rotates the input position 60° in clockwise direction
        public static Vector3Int Rotate60DegreeClockwise(Vector3Int center, Vector3Int point) {
            Vector3Int direction = point - center;
            int rotatedX = -direction.z;
            int rotatedY = -direction.x;
            int rotatedZ = -direction.y;
            Vector3Int rotated = new Vector3Int(rotatedX, rotatedY, rotatedZ) + center;
            return rotated;
        }

        /// Rotates the input position 60° in counterclockwise direction
        public static Vector3Int Rotate60DegreeCounterClockwise(Vector3Int center, Vector3Int point) {
            Vector3Int direction = point - center;
            int rotatedX = -direction.y;
            int rotatedY = -direction.z;
            int rotatedZ = -direction.x;
            Vector3Int rotated = new Vector3Int(rotatedX, rotatedY, rotatedZ) + center;
            return rotated;
        }

        /// Returns the coordinates of all edges of a tile
        public static HashSet<Vector3Int> GetEdgeCoordinatesOfTile(Vector3Int origin) {
            HashSet<Vector3Int> edgeCoords = new HashSet<Vector3Int> {
                new Vector3Int(2*origin.x+1,2*origin.y,2*(origin.z - 1)+1),
                new Vector3Int(2*origin.x+1,2*(origin.y-1)+1,2*origin.z),
                new Vector3Int(2*origin.x,2*(origin.y-1)+1,2*origin.z+1),
                new Vector3Int(2*(origin.x-1)+1,2*origin.y,2*origin.z+1),
                new Vector3Int(2*(origin.x-1)+1,2*origin.y+1,2*origin.z),
                new Vector3Int(2*origin.x,2*origin.y+1,2*(origin.z-1)+1),
            };

            return edgeCoords;
        }

        /// Returns the tiles which share this edge.
        public static List<Vector3Int> GetAdjacentTilesOfEdge(Vector3Int edge, bool removeInvalid) {
            int tileAx = 0;
            int tileAy = 0;
            int tileAz = 0;
            int tileBx = 0;
            int tileBy = 0;
            int tileBz = 0;

            if (edge.x % 2 == 0) {
                tileAx = edge.x / 2;
                tileBx = edge.x / 2;
                tileAy = (edge.y - 1) / 2;
                tileAz = (edge.z + 1) / 2;
                tileBy = (edge.y + 1) / 2;
                tileBz = (edge.z - 1) / 2;
            }

            else if (edge.y % 2 == 0) {
                tileAy = edge.y / 2;
                tileBy = edge.y / 2;
                tileAx = (edge.x + 1) / 2;
                tileAz = (edge.z - 1) / 2;
                tileBx = (edge.x - 1) / 2;
                tileBz = (edge.z + 1) / 2;
            }

            else {
                tileAz = edge.z / 2;
                tileBz = edge.z / 2;
                tileAx = (edge.x - 1) / 2;
                tileAy = (edge.y + 1) / 2;
                tileBx = (edge.x + 1) / 2;
                tileBy = (edge.y - 1) / 2;
            }

            List<Vector3Int> tiles = new List<Vector3Int> {
                new Vector3Int(tileAx, tileAy, tileAz),
                new Vector3Int(tileBx, tileBy, tileBz)
            };

            return tiles.ToList();
        }

        /// returns the angle of the edge.
        public static float GetEdgeDirectionAngle(Vector3Int edge) {

            if (edge.y % 2 == 0) {
                return 0;
            } else if (edge.x % 2 == 0) {
                return 120;
            } else {
                return 240;
            }
        }
    }
}