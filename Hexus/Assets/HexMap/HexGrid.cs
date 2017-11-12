using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; ++z) {
            for (int x = 0; x < width; ++x) {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start() {
        hexMesh.Triangulate(cells);
    }

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
