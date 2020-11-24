using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryFish : PlayerStateAction
{

    public PlayerCarryFish(Player player,Fish fish)
    {
        this.player = player;
        this.fish = fish;
        
    }

    public override void PutFishInMarket()
    {
        //MarketManager.instance.PutFishInMarket();
        FishManager.instance.DestroyFish(fish);
        player.SetStateAction(new PlayerExplore(player));
    }



}
