using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the class that actually creates the hex board.
/// Attach this script to a gameobject and throw it in the scene to create a hex board on load.
/// 
/// It will generate a width x height board of hexs that have the specified orientation.
/// You can optionally show the coordinates of each hex on top of them.  This should be turned off for release.
/// </summary>

public class HexGrid : MonoBehaviour {

    [Tooltip("For testing and debugging turn this on to see coordinates")]
    public bool showCoordinates = true;
    [Tooltip("If true, hexagons will be oriented flat side up.  Else pointy side up.")]
    public bool flipOrientation = false;

    public Text cellLabelPrefab;
    Canvas gridCanvas;

    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;

    private HexCell[] cells;
    private HexMesh hexMesh;

    void Awake() {
        // The canvas is used to show the coordinates, the mesh to render the hexagons
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; ++z) {
            for (int x = 0; x < width; ++x) {
                CreateCell(x, z, i++);
            }
        }
    }

    // The positions of every cell was created in awake, now we need to render them
    void Start() {
        hexMesh.Triangulate(cells);
    }

    // Creates cells in the correct positions, offsets to make hexagons line up nicely
    void CreateCell(int x, int z, int i) {
        Vector3 position;
        if (flipOrientation) { // Flat side up
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.outerRadius * 3f);
            position.y = 0f;
            position.z = z * (HexMetrics.innerRadius * 1f);
        } else { // Pointy side up
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.outerRadius * 1.5f);
        }

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        if (showCoordinates) {
            Text label = Instantiate<Text>(cellLabelPrefab);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text = cell.coordinates.ToStringOnSeparateLines();
        }
    }
}
