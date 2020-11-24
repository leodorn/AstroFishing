using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Goal", fileName ="New Goal")]
public class GoalData : ScriptableObject
{
    public string name;
    public string description;
    // Value to achieve for goal unlock
    public float goalToAchieve;
    // Actual value of player progression
    public float currentValue;
    public Sprite goalIcon;


}
