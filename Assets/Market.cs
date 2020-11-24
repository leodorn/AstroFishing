using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : InteractableObject
{
    public override void DoInteraction()
    {
        if(Player.instance.GetStateAction() is PlayerExplore)
        {
            Debug.Log("Enter in Market");
        }
        else if(Player.instance.GetStateAction() is PlayerCarryFish)
        {
            MarketManager.instance.PutFishInMarket(Player.instance.GetStateAction().fish);
        }
    }

    public override bool ConditionShowText()
    {
        if ((Player.instance.GetStateAction() is PlayerExplore || Player.instance.GetStateAction() is PlayerCarryFish)
            && Player.instance.GetStateLocation() is PlayerOnGround)
        {
            return true;
        }
        return false;
    }
}
