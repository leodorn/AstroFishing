using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMarket : MonoBehaviour
{
    public FishMarketData fishMarketData;
    public int price;

    public FishMarket(Fish fish)
    {
        fishMarketData = fish.fishMarketData;
        price = fish.getPriceDependOnWeight();
    }




}
