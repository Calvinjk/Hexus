# Hexus

How to generate a HexMap:
1) Make sure your scene has both a MapManager and a GameManager prefab.
2) Make sure the GameManager has a reference to the MapManager by dragging and dropping it in the inspector.
3) Set your height and width in the MapManager inspector and run the scene to generate the hex grid.

*If you need to convert to or from any of these three coordinate types: (Cube, Offset, World), I have convenient static methods to do this in the HexConversions script.  Each Tile contains only its CubeCoordinate to save memory.*
