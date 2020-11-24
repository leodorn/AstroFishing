using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public static StockManager instance;
    List<SlotStockFishByType> slotStockList;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        slotStockList = new List<SlotStockFishByType>();
    }

    public void StockFish(Fish fish)
    {
        FishMarket fishMarket = new FishMarket(fish);
        bool findGoodSlot = false;
        foreach(SlotStockFishByType slot in slotStockList)
        {
            if(slot.fishTypePrefab == fishMarket.fishMarketData.fishPrefab)
            {
                slot.AddFish(fishMarket);
                findGoodSlot = true;
            }
        }
        if(!findGoodSlot)
        {
            CreateNewSlot(fishMarket);
        }
        FishManager.instance.DestroyFish(fish);
    }

    void CreateNewSlot(FishMarket fishMarket)
    {
        //Ici il faudra voir pour l'instancier au bon endroit
        SlotStockFishByType slot = Instantiate(slotPrefab).GetComponent<SlotStockFishByType>();
        slot.AddFish(fishMarket);

    }
}
