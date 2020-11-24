using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{

    public GoalData goalData;
    public TextMeshProUGUI goalDisplay;
    public Image goalIcon;


    // Start is called before the first frame update
    void Start()
    {
        goalDisplay.text = goalData.currentValue + "/" + goalData.goalToAchieve;
        goalIcon.sprite = goalData.goalIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
