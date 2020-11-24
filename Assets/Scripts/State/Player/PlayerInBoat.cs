using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInBoat : PlayerStateLocation
{
    public PlayerInBoat(Player player)
    {
        this.player = player;
    }

   

    public override void MoveCanceled()
    {
        player.animator.SetFloat("WalkValue", 0);
        player.boat.valueToMove = 0;
    }

    public override void MovePerformed(InputAction.CallbackContext ctx)
    {
        player.animator.SetFloat("WalkValue", 0);
        player.boat.valueToMove = ctx.ReadValue<float>();
    }




    public override void GoOutOfBoat(Ground ground)
    {
        player.transform.SetParent(null);
        

        //set the position on the ground
        player.transform.position = ground.pointToAccost.transform.position;
        player.rigidbody.velocity = Vector2.zero;
        //J'sais pas pourquoi il se déplaçait de temps en temps
        player.movementValue = 0;
        player.SetStateLocation(new PlayerOnGround(player));
    }

    public override void Update()
    {
       
        player.spriteRenderer.flipX = false;
        player.transform.position = new Vector3(player.boat.navigationPlaceTransform.position.x,
            player.boat.navigationPlaceTransform.position.y, player.boat.navigationPlaceTransform.position.z + 0.1f);
        
    }

}
