using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "PlayerData",menuName = "My Game/PlayerData")]
public class PlayerData : ScriptableObject
{
    //[Header("Déplacement du personnage")]
    [Header("Déplacement du personnage")]
    public float speed;

    [Header("Pour lançer la canne à pêche")]
    public Vector2 directionToApplyToDecoy;
    public float forceForRideUpDecoy;
    public float forceToLaunchFishingRode;
    public float forceToApplyToDecoy;

    [Header("Characteristic")]
    public GameObject decoyPrefab;
    public int money;
    public float forceGivenPerEachTapForStruggle;
}
