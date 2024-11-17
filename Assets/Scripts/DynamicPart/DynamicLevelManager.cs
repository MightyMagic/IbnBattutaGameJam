using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DynamicLevelManager : MonoBehaviour
{
    [Header("Water")]
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

    public float timerToEnd;
    public float timer = 0f;
    //[Header("Story")]
    //[SerializeField] GameObject CanvasObject;
    //[SerializeField] Image storyImage;
    //
    //[SerializeField] List<Sprite> storySprites;
    //
    //[SerializeField] float storyTiming;


    void Start()
    {
       
        // Spawn water
        waterList.Add(waterSurface);
        AppendNewWaterSegment();

        //environmentGeneration.PopulateWaterTile(waterSurface);
        //GameObject waterGO = Instantiate(waterPrefab, waterNewSpawn.position, Quaternion.identity);
        //waterList.Add(waterGO);

        // Fetch passed info about the upcoming level

        // Setup the timer

        // Generate the level based on traits

    }

   //IEnumerator  StoryCoroutine()
   //{
   //    CanvasObject.SetActive(true);
   //    storyImage.sprite = storySprites[Random.Range(0, storySprites.Count)];
   //    yield return new WaitForEndOfFrame();
   //    yield return new WaitForSeconds(storyTiming);
   //
   //    CanvasObject.SetActive(false);
   //
   //}

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > timerToEnd)
        {
            SceneManager.LoadScene("MapScene");
        }

        MoveWater();
        MaintainWater();
        FloatingObjects();
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

    void FloatingObjects()
    {
        for( int i = 0; i < waterList.Count; i++)
        {
            foreach(Transform child in waterList[i].transform)
            {
                if(child.gameObject.GetComponent<FloatingRb>() != null)
                {
                    child.gameObject.GetComponent<FloatingRb>().FloatAndMove();
                }
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
    
}
