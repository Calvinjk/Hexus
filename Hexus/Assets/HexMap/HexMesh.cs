using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This file actually creates the visuals of the Hexagons
/// </summary>

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {

    Mesh hexMesh;
    List<Vector3> verticies;
    List<int> triangles;

    // True = Flat side up
    // False = Pointy side up
    // Do not mess with this, it is set in HexGrid 
    private bool flipOrientation;

    void Awake() {
        // Figure out the orientation of the hexs based on the information in HexGrid
        flipOrientation = GetComponentInParent<HexGrid>().flipOrientation;

        // Create a new mesh and set up necessary lists.
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "HexMesh";
        verticies = new List<Vector3>();
        triangles = new List<int>();
    }

    // Given an array of HexCells (their positions are already set),
    // start to make them visible with the mesh.
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

    // Create each hexagon using 4 triangles.
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

    // Given a triangle's vector points, add it to the mesh
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
