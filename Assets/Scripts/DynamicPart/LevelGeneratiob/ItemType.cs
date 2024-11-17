using UnityEngine;

public enum ItemType
{
    Stone,
    Obstacle,
    Collectible
}

[System.Serializable]
public class GridContent
{
    public ItemType Type;
    public GameObject Prefab;
}
