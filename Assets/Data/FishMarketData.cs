using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="FishMarketData",menuName ="My Game/FishMarketData")]
public class FishMarketData : ScriptableObject
{
    public int price;
    public string description;
    public Sprite sprite;
    public GameObject fishPrefab;
}
