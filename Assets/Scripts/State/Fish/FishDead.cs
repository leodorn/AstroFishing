using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDead : FishState
{
    Player player;
    public FishDead(Fish fish,Player player)
    {
        this.fish = fish;
        this.player = player;
        fish.transform.SetParent(player.transform);
        fish.transform.rotation = Quaternion.Euler(fish.transform.rotation.x,fish.transform.rotation.y,180);
        fish.animator.SetBool("Dead", true);
    }
    public override void Update()
    {
        fish.transform.position = player.carryFishTransfrom.position;
        fish.spriteRenderer.flipX = player.spriteRenderer.flipX;
        
    }
}
