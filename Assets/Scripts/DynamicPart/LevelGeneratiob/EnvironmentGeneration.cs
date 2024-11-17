using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour
{
    [SerializeField] int xSize;
    [SerializeField] int zSize;
                     
    [SerializeField] int yCoordinate;

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

                    SpawnObject(stoneList[Random.Range(0, stoneList.Count)].Prefab, waterTile, currentPosition);
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

                    SpawnObject(collectiblesList[Random.Range(0, collectiblesList.Count)].Prefab, waterTile, currentPosition);
                }
                else
                {
                    //bufferGo = Instantiate(obstaclesList[Random.Range(0, obstaclesList.Count)].Prefab, currentPosition, Quaternion.identity);
                    //bufferGo.transform.parent = waterTile.transform;

                    SpawnObject(obstaclesList[Random.Range(0, obstaclesList.Count)].Prefab, waterTile, currentPosition);
                }
            }
        }
    }

    void SpawnObject(GameObject goToSpawn, GameObject waterTile, Vector3 centerPoint)
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

        // You can leave the y-position as is, or randomize it as well if needed.
        float y = centerPoint.y;

        // Set the object's position
        Vector3 spawnPoint = new Vector3(x, y, z);

        GameObject go = Instantiate(goToSpawn, spawnPoint, Quaternion.identity);
        go.transform.parent = waterTile.transform;
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


