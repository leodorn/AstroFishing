using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishingRodeData", menuName = "My Game/FishingRodeData")]
public class FishingRodeData : ScriptableObject
{
    public float depthLimit;
    [Range(-20, 0)]
    public float forceToGoDownInWater;
    public GameObject deadFishPrefab;
}
