using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotStockFishByType : MonoBehaviour
{
    //public FishMarketData fishMarketData;
    [HideInInspector] public GameObject fishTypePrefab;
    List<FishMarket> fishStockedInSlotList;

    public void Awake()
    {
        fishStockedInSlotList = new List<FishMarket>();
    }

    public void SetTypeFish(GameObject fishTypePrefab)
    {
        this.fishTypePrefab = fishTypePrefab;
    }

    public void AddFish(FishMarket fishMarket)
    {
        fishStockedInSlotList.Add(fishMarket);
    }

    public void SellFish(FishMarket fishMarket)
    {
        fishStockedInSlotList.Remove(fishMarket);
    }

}
