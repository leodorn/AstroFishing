using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStruggle : PlayerStateAction
{
    bool inBoat;
    public PlayerStruggle(Player player)
    {
        this.player = player;
    }

  
    public override void FightingStruggle() => player.cursorStruggle.SetForceGiven(player.cursorStruggle.GetForceGivenByPlayer() + player.playerData.forceGivenPerEachTapForStruggle);


    public override void Update()
    {
        Debug.Log("struggle");
    }

}
