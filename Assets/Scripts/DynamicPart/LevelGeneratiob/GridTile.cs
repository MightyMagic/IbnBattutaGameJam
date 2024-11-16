using UnityEngine;

[System.Serializable]
public class GridTile
{
    public int X;
    public int Z;
    public Vector3 TilePosition;

    public bool isOccupied = false;

    public GridTile(int x, int z, int yCoord)
    {
        X = x;
        Z = z;
        TilePosition = new Vector3(x, yCoord, z);
        isOccupied = false;
    }
}