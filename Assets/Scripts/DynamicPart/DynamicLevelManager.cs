using System.Collections.Generic;
using UnityEngine;

public class DynamicLevelManager : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] Transform waterIsGone;
    [SerializeField] Transform waterIsNeeded;
    [SerializeField] Transform waterNewSpawn;
    [SerializeField] GameObject waterSurface;
    [SerializeField] float waterSpeed;


    public List<GameObject> waterList = new List<GameObject>();
    [SerializeField] List<EnvironmentData> environmentData;
    [SerializeField] List<FloatingRb> delmeObjects;

    [SerializeField] EnvironmentGeneration environmentGeneration;

    void Start()
    {
        // Spawn water
        waterList.Add(waterSurface);
        AppendNewWaterSegment();
        //environmentGeneration.PopulateWaterTile(waterSurface);
        //GameObject waterGO = Instantiate(waterPrefab, waterNewSpawn.position, Quaternion.identity);
        //waterList.Add(waterGO);

        // Fetch passed info about the upcoming level

        // Randomize weather

        // Generate the level based on traits

    }

    void Update()
    {
        for(int i = 0; i < delmeObjects.Count; i++)
        {
            delmeObjects[i].FloatAndMove();
        }

        MoveWater();
        MaintainWater();
    }

    void MoveWater()
    {
        for(int i = 0; i < waterList.Count; i++)
        {
            waterList[i].transform.position += new Vector3(0f, 0f, waterSpeed * Time.deltaTime);
        }

    }

    void MaintainWater()
    {
        for (int i = 0; i < waterList.Count; i++)
        {
            if (waterList[i].transform.position.z < waterIsNeeded.position.z)
            {

                
                RemoveWaterSegment(waterList[i]);


                AppendNewWaterSegment();
            }
        }
    }

    void RemoveWaterSegment(GameObject go)
    {
        waterList.Remove(go);
        Destroy(go);

        
    }

    void AppendNewWaterSegment()
    {
        GameObject waterGO = Instantiate(waterPrefab, waterNewSpawn.position, Quaternion.identity);
        waterList.Add(waterGO);

        environmentGeneration.PopulateWaterTile(waterGO);
    }

    void SpawnEnvironmentObjectsOnWater()
    {

    }

    
}
