using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    GameObject barCircle;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void SetBarCircleActive(bool active, float minForce, float maxForce,float timeToChanceForce,float sizeOfTheGreenZoneFish,Fish fish)
    {
        barCircle.SetActive(active);
        CursorStruggle cursor = GetCursorStruggle();
        cursor.fish = fish;
        cursor.SetMinAndMaxForceAndTime(minForce, maxForce, timeToChanceForce, sizeOfTheGreenZoneFish);
        cursor.LaunchCursor();
        
    }

    public void  SetBarCircleActive(bool active)
    {
        barCircle.SetActive(active);
    }

    public CursorStruggle GetCursorStruggle()
    {
        return barCircle.transform.GetChild(0).GetComponent<CursorStruggle>();
    }
}
