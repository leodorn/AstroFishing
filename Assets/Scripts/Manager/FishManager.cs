using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;
    private List<Fish> fishInWaterList;
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    List<GameObject> fishPrefabList;
    [SerializeField]
    List<int> numeroFishInWaterDependOnLevelList;
    List<Fish> fishToAddList, fishToDestroyList;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        fishInWaterList = new List<Fish>();
    }
    // Start is called before the first frame update
    void Start()
    {
        fishToAddList = new List<Fish>();
        fishToDestroyList = new List<Fish>();
        fishPrefabList = fishPrefabList.OrderBy(o => o.GetComponent<Fish>().fishData.levelOfFish).ToList();
        CheckFish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckFish()
    {
        for (int level = 0; level < maxLevel+1; level++)
        {
            fishInWaterList.AddRange(fishToAddList);
            foreach (Fish fish in fishToDestroyList)
            {
                fishInWaterList.Remove(fish);
            }
            fishToAddList.Clear();
            fishToDestroyList.Clear();
            CheckIfEnoughtFish(level);
        }
        
    }

    void CheckIfEnoughtFish(int level)
    {
        int numberFish = 0;
        foreach(Fish fish in fishInWaterList)
        {
            if(fish.fishData.levelOfFish == level)
            {
                numberFish += 1;
            }
        }
        for (int i= numberFish; i< numeroFishInWaterDependOnLevelList[level]; i++)
        {
            SpawnRandomlyFish(level);
        }
        
    }

    void SpawnRandomlyFish(int level)
    {
        List<Fish> fishCanSpawnList = new List<Fish>();
        foreach(GameObject fish in fishPrefabList)
        {
            if(fish.GetComponent<Fish>().fishData.levelOfFish == level)
            {
                fishCanSpawnList.Add(fish.GetComponent<Fish>());
            }
           
        }
        fishCanSpawnList = fishCanSpawnList.OrderBy(o => o.GetComponent<Fish>().fishData.chanceToSpawn).ToList();
        int amount = 0;
        foreach(Fish fish in fishCanSpawnList)
        {
            amount += fish.fishData.chanceToSpawn;
        }
        if(amount != 100)
        {
            Debug.Log("La probabilité de spawn de tous les poissons combinés du niveau " + level +
                " n'est pas égale à 100");
        }
        else
        {
            int randomChance = Random.Range(0, 100);
            int seuilProb=0;
            bool fishFind = false;
            //foreach(Fish fish in fishCanSpawnList)
            //{
            //    float randomXSpawn = Random.Range(fish.spawnMin.x, .spawnMax.x);
            //    Debug.Log(fish.spawnMin.x);
            //}
            for (int i = 0; i < fishCanSpawnList.Count && !fishFind; i++)
            {
                if (seuilProb < randomChance && randomChance < seuilProb + fishCanSpawnList[i].fishData.chanceToSpawn)
                {
                    float randomXSpawn = Random.Range(fishCanSpawnList[i].fishData.leftMax, fishCanSpawnList[i].fishData.rightMax);
                    float randomYSpawn = Random.Range(fishCanSpawnList[i].fishData.depthMax, fishCanSpawnList[i].fishData.depthMin);
                    GameObject fishSpawned = Instantiate(fishCanSpawnList[i].gameObject, new Vector2(randomXSpawn, randomYSpawn), Quaternion.identity);
                    fishToAddList.Add(fishSpawned.GetComponent<Fish>());
                    fishFind = true;
                }
                else
                {
                    seuilProb += seuilProb + fishCanSpawnList[i].fishData.chanceToSpawn;
                }
            }
        }
        
    }

    public void DestroyFish(Fish fish)
    {
        Debug.Log("Je détruis");
        fishToDestroyList.Add(fish);
        Destroy(fish.gameObject);
        CheckFish();
    }


}
