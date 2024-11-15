using System.Collections.Generic;
using UnityEngine;

public class DynamicLevelManager : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] Transform waterSpawn;
    [SerializeField] GameObject waterSurface;
    [SerializeField] float waterSpeed;

    [SerializeField] List<EnvironmentData> environmentData;
    [SerializeField] List<FloatingRb> delmeObjects;

    void Start()
    {
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
    }

    void MoveWater()
    {
        waterSurface.transform.position += new Vector3(0f, 0f, waterSpeed * Time.deltaTime);
    }

    
}
