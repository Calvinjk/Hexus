using UnityEngine;

[System.Serializable]
public class HexCoordinates {

    // Define private member variables that are able to be interacted with in the Inspector
    [SerializeField]
    private int x, z;

    // Only x and z need to be stored as we can calculate Y on-the-fly easily since x+y+z=0 in Hex Coordinates
    public int X { get { return x; } }
    public int Z { get { return z; } }
    public int Y { get { return -X - Z; } }
    public HexCoordinates (int x, int z) {
        this.x = x;
        this.z = z;
    }

    // This function can be called to transform HexCoordinates into World Coordinates
    public static Vector3 HexToWorld(HexCoordinates hexCoordinates, bool flatUp) {
        if (flatUp) { // Flat side up
            // TODO
            return new Vector3(0, 0, 0);
        } else { // Pointy side up
            return new Vector3(((2 * hexCoordinates.X) + hexCoordinates.Z) * HexMetrics.innerRadius, 0f, 1.5f * hexCoordinates.Z * HexMetrics.outerRadius);
        }
    }

    // This function can be called to transform World Coordinates into HexCoordinates
    public static HexCoordinates WorldToHex(Vector3 worldCoordinates, bool flatUp) {
        return new HexCoordinates(0, 0);
    }

    // This simply deals with the zig-zagging X coordinate and makes it straight
    public static HexCoordinates FromOffsetCoordinates(int x, int z) {
        return new HexCoordinates(x - z / 2, z);
    }

    // I overwrote the ToString() function to print in a more readable format
    public override string ToString() {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines() {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
}