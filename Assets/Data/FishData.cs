using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "My Game/FishData")]
public class FishData : ScriptableObject
{
    [Header("Spawn and movement limits")]
    [Range(-8, 1000)]
    public float leftMax;
    [Range(-8, 1000)]
    public float rightMax;
    [Range(0, -1000)]
    public float depthMin;
    [Range(0, -1000)]
    public float depthMax;
    [Range(0, 100), Header("Ensemble proba même level poisson = 100 !! ")]
    public int chanceToSpawn;
    [Header("Movement")]
    [Range(0, 10)]
    public float initialSpeed;
    [Range(0, 100)] 
    public int chanceToGainSpeed;
    [Header("Reaction to the fishing rode")]
    [Range(0, 100)] 
    public float rangeToSeeDecoy, rangeToSlowNearDecoy, rangeToPickDecoy;
    [Header("DeadSprite")]
    public Sprite deadSprite;  
    [Header("Characteristic")]
    public int levelOfFish;
    [Range(0.5f, 3)]
    public float minWeight, maxWeight;
    [Range(0, 100)]
    [Header("Struggle")]
    public int chanceToStruggle, timeToRetryToStruggle;
    [Range(0, 20)]
    public float minForce, maxForce;
    [Range(0, 10)] 
    public float timeToChanceForce;
    [Range(0, 1)] 
    public float sizeOfTheGreenZone;
    [Range(0, 15)] 
    public int timeToBeFree, timeToBeCatch;
    [Range(0, 300)] 
    public float timeBeforeBeAttractedByDecoy;
}
