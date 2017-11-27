using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class HexConversions {

        private static float SQRT3 = Mathf.Sqrt(3);

        public static Vector2Int WorldPositionToOffsetCoord(Vector3 p) {

            float x = p.x / SQRT3;
            float z = p.z;
            float temp = Mathf.Floor(x + z + 1);

            float c = Mathf.Floor((Mathf.Floor(2 * x + 1) + temp) / 3f);
            float r = Mathf.Floor((temp + Mathf.Floor(-x + z + 1)) / 3);

            return new Vector2Int((int)(c - (r + ((int)r & 1)) / 2), (int)r);
        }

        public static Vector3Int WorldPositionToCubeCoord(Vector3 p) {
            float x = p.x / SQRT3;
            float z = p.z;
            float temp = Mathf.Floor(x + z + 1);

            float q = Mathf.Floor((Mathf.Floor(2 * x + 1) + temp) / 3f);
            float r = Mathf.Floor((temp + Mathf.Floor(-x + z + 1)) / 3);

            int cX = (int)q - (int)r;
            int cY = (int)r;
            int cZ = -cX - cY;
            return new Vector3Int(cX, cY, cZ);
        }

        // Potential TODO
        // This is the only one that accounts for HexSize....
        public static Vector3 OffsetCoordToWorldPosition(Vector2Int offsetPos) {
            float offsetXAdjustment;
            if (offsetPos.y % 2 == 0) offsetXAdjustment = 0;
            else offsetXAdjustment = HexMetrics.innerRadius;

            float offsetX = offsetPos.x * (2 * HexMetrics.innerRadius) + offsetXAdjustment;
            float offsetZ = offsetPos.y * HexMetrics.outerRadius * 1.5f;
            return new Vector3(offsetX, 0, offsetZ);
        }

        public static Vector3Int OffsetCoordToCubeCoord(Vector2Int o) {
            int x = o.x - (o.y - (o.y & 1)) / 2;
            int y = o.y;
            int z = -x - y;
            return new Vector3Int(x, y, z);
        }

        public static Vector3 CubeCoordToWorldPosition(Vector3Int cubeCoord, int yCoord = 0) {
            float x = SQRT3 * (cubeCoord.x + cubeCoord.y / 2f);
            float z = 3 / 2f * cubeCoord.y;
            float y = yCoord;
            return new Vector3(x, y, z);
        }

        public static Vector2Int CubeCoordToOffsetCoord(Vector3Int c) {
            int x = c.x + (c.y - (c.y & 1)) / 2;
            int y = c.y;
            return new Vector2Int(x, y);
        }

        public static Vector3 EdgeCoordToWorldPosition(Vector3Int edgeCoord) {
            Vector3 worldPos = HexConversions.CubeCoordToWorldPosition(edgeCoord);
            worldPos = new Vector3(worldPos.x / 2f, 1, worldPos.z / 2f);
            return worldPos;
        }
    }
}
