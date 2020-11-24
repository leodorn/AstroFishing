using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static MarketManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PutFishInMarket(Fish fish)
    {
        StockManager.instance.StockFish(fish);
        Player.instance.SetStateAction(new PlayerExplore(Player.instance));
    }
}
