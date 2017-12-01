using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HexMetrics is a const/static class that holds information about hexagon geometry that is used in other files
/// </summary>

public class HexMetrics : MonoBehaviour {

    public const float hexSize = 10f;

    public const float outerRadius = hexSize;
    public const float innerRadius = outerRadius * 0.866025404f;

    // This will hold the corner points of the hexagons (pointy side up)
    public static Vector3[] pointyCorners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
    };
}