using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour
{
    [SerializeField] int xSize;
    [SerializeField] int zSize;
                     
    [SerializeField] int yCoordinateForStones;
    [SerializeField] int yCoordinate;
    [SerializeField] int yCoordinateForChests;

    [SerializeField] int distanceBetweenCells;

    [SerializeField] float centerOffset;

    public List<GridTile> Grid = new List<GridTile>(); 
    public List<GridContent> prefabList = new List<GridContent>();

    [Header("Runtime lists")]
    public List<GridContent> obstaclesList = new List<GridContent>();
    public List<GridContent> collectiblesList = new List<GridContent>();
    public List<GridContent> stoneList = new List<GridContent>();

    GameObject bufferGo;

    void Start()
    {
        

        
    }

    void Update()
    {
        
    }

    public void PopulateWaterTile(GameObject waterTile)
    {
        GenerateGrid();
        PopulateGrid(waterTile);


    }

    public void GenerateGrid()
    {
        // Clear the grid to avoid duplicate entries
        Grid.Clear();

        // Determine the origin of the grid
        Vector3 origin = this.transform.position;

        // Generate the grid
        for (int x = -xSize / 2 + (int)origin.x; x < xSize / 2 + (int)origin.x; x+=distanceBetweenCells)
        {
            for (int z = -zSize / 2 + (int)origin.z; z < xSize / 2 + (int)origin.z; z += distanceBetweenCells)
            {
                
                // Create a new GridTile object and add it to the Grid list
                GridTile tile = new GridTile(x, z, yCoordinate);
                Grid.Add(tile);
            }
        }

        // Populate the lists
        stoneList = prefabList.Where(x => x.Type == ItemType.Stone).ToList();
        collectiblesList = prefabList.Where(x => x.Type == ItemType.Collectible).ToList();
        obstaclesList = prefabList.Where(x => x.Type == ItemType.Obstacle).ToList();
    }

    public void PopulateGrid(GameObject waterTile)
    {
        for(int j = 0; j < Grid.Count; j++)
        {
            Vector3 currentPosition = Grid[j].TilePosition;
            if(j % 2 == 0)
            {

                float probabilityThreshold = 0.5f;
                bool success = Random.value <= Mathf.Clamp01(probabilityThreshold);
                Debug.Log(success ? "Success!" : "Failure!");

                if(success)
                {
                    //bufferGo = Instantiate(stoneList[Random.Range(0, stoneList.Count)].Prefab, currentPosition, Quaternion.identity);
                    //bufferGo.transform.parent = waterTile.transform;

                    SpawnObject(stoneList[Random.Range(0, stoneList.Count)].Prefab, waterTile, currentPosition, yCoordinateForStones);
                }
            }
            else
            {
                float probabilityThreshold = 0.3f;
                bool success = Random.value <= Mathf.Clamp01(probabilityThreshold);
                Debug.Log(success ? "Success!" : "Failure!");

                if (success)
                {
                    //bufferGo = Instantiate(collectiblesList[Random.Range(0, collectiblesList.Count)].Prefab, currentPosition, Quaternion.identity);
                    //bufferGo.transform.parent = waterTile.transform;

                    SpawnObject(collectiblesList[Random.Range(0, collectiblesList.Count)].Prefab, waterTile, currentPosition, yCoordinateForChests);
                }
                else
                {
                    //bufferGo = Instantiate(obstaclesList[Random.Range(0, obstaclesList.Count)].Prefab, currentPosition, Quaternion.identity);
                    //bufferGo.transform.parent = waterTile.transform;

                    SpawnObject(obstaclesList[Random.Range(0, obstaclesList.Count)].Prefab, waterTile, currentPosition, 0f);
                }
            }
        }
    }

    void SpawnObject(GameObject goToSpawn, GameObject waterTile, Vector3 centerPoint, float yOffset)
    {
        // Calculate a random radius between 0 and maxRadius
        float radius = Random.Range(0f, centerOffset);

        // Calculate a random angle between 0 and 360 degrees
        float angle = Random.Range(0f, 360f);

        // Convert angle to radians
        float angleRad = angle * Mathf.Deg2Rad;

        // Calculate x and z position using polar coordinates (radius, angle)
        float x = centerPoint.x + radius * Mathf.Cos(angleRad);
        float z = centerPoint.z + radius * Mathf.Sin(angleRad);

        float y = yOffset;

        

        // Set the object's position
        Vector3 spawnPoint = new Vector3(x, y, z);

        GameObject go = Instantiate(goToSpawn, spawnPoint, Quaternion.identity);
        RotateRandomY(go);
        go.transform.parent = waterTile.transform;
    }

    public void RotateRandomY(GameObject go)
    {
        // Generate a random Y angle between 0 and 360
        float randomYAngle = Random.Range(0f, 360f);

        // Get the current rotation
        Vector3 currentRotation = go.transform.eulerAngles;

        // Apply the random Y angle, keeping X and Z unchanged
        go.transform.eulerAngles = new Vector3(currentRotation.x, randomYAngle, currentRotation.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(xSize, 10f, zSize));

        Gizmos.color = Color.black;
        for (int i = 0; i < Grid.Count; i++)
        {
            Gizmos.DrawCube(Grid[i].TilePosition, 10f * Vector3.one);
        }
    }
}


