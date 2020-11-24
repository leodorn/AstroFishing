using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOnGround : PlayerStateLocation
{
    public PlayerOnGround(Player player)
    {
        this.player = player;
    }



    public override void MovePerformed(InputAction.CallbackContext ctx)
    {
        player.movementValue = ctx.ReadValue<float>();
        player.animator.SetFloat("WalkValue", player.movementValue);
    }




    public override void GoInBoat()
    {
        
        player.rigidbody.velocity = Vector2.zero;

        //set the position in the navigation place
        player.transform.position = new Vector3(player.boat.navigationPlaceTransform.position.x,
            player.boat.navigationPlaceTransform.position.y, player.boat.navigationPlaceTransform.position.z + 0.1f);
        player.transform.SetParent(player.boat.transform);
        player.SetStateLocation(new PlayerInBoat(player));
    }

    public override void Update()
    {
       
    }

    public override void FixedUpdate()
    {
        player.transform.Translate(Vector2.right * player.movementValue * player.playerData.speed * Time.deltaTime);
        //Turn the player if he walk to the left
        if (player.movementValue < 0)
        {
            player.spriteRenderer.flipX = true;
        }
        else if (player.movementValue > 0)
        {
            player.spriteRenderer.flipX = false;
        }
    }
}
