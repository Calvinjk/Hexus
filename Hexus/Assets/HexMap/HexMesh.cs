using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {

    Mesh hexMesh;
    List<Vector3> verticies;
    List<int> triangles;

    private bool flipOrientation;

    void Awake() {
        flipOrientation = GetComponentInParent<HexGrid>().flipOrientation;

        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "HexMesh";
        verticies = new List<Vector3>();
        triangles = new List<int>();
    }

    public void Triangulate(HexCell[] cells) {
        hexMesh.Clear();
        verticies.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++) {
            Triangulate(cells[i]);
        }
        hexMesh.vertices = verticies.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
    }

    void Triangulate(HexCell cell) {
        Vector3 center = cell.transform.localPosition;
        if (flipOrientation) { // Flat side up
            AddTriangle(center + HexMetrics.flatCorners[4], center + HexMetrics.flatCorners[5], center + HexMetrics.flatCorners[0]);
            AddTriangle(center + HexMetrics.flatCorners[4], center + HexMetrics.flatCorners[0], center + HexMetrics.flatCorners[1]);
            AddTriangle(center + HexMetrics.flatCorners[4], center + HexMetrics.flatCorners[1], center + HexMetrics.flatCorners[2]);
            AddTriangle(center + HexMetrics.flatCorners[4], center + HexMetrics.flatCorners[2], center + HexMetrics.flatCorners[3]);
        } else { // Pointy side up
            AddTriangle(center + HexMetrics.pointyCorners[3], center + HexMetrics.pointyCorners[4], center + HexMetrics.pointyCorners[5]);
            AddTriangle(center + HexMetrics.pointyCorners[3], center + HexMetrics.pointyCorners[5], center + HexMetrics.pointyCorners[0]);
            AddTriangle(center + HexMetrics.pointyCorners[3], center + HexMetrics.pointyCorners[0], center + HexMetrics.pointyCorners[1]);
            AddTriangle(center + HexMetrics.pointyCorners[3], center + HexMetrics.pointyCorners[1], center + HexMetrics.pointyCorners[2]);
        }
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3) {
        int vertexIndex = verticies.Count;
        verticies.Add(v1);
        verticies.Add(v2);
        verticies.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
}
