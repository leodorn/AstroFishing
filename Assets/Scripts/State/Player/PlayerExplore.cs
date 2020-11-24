using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplore : PlayerStateAction
{
    public PlayerExplore(Player player)
    {
        this.player = player;
    }

    public override void PressForFishing()
    {
        if (Physics2DUtils.CheckIfColliderAround(player.transform, 3, "Water"))
        {
            StartFishing();
            player.animator.SetBool("ThrowFishingRode", true);
        }
        else
        {
            Debug.Log("Vous êtes trop loin de l'eau");
        }
    }

    public void StartFishing()
    {
        GameObject barLaunchFishingRode = player.barLaunchFishingRode;
        if (barLaunchFishingRode.activeSelf == false)
        {
            barLaunchFishingRode.SetActive(true);
            barLaunchFishingRode.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Launch");
        }
        else if (barLaunchFishingRode.activeSelf == true)
        {
            
            if (player.goodShoot)
            {
                SetForce(2);
            }
            else
            {
                SetForce(1);
            }
            player.animator.SetBool("Fishing", true);
            player.animator.SetBool("ThrowFishingRode", false);
            barLaunchFishingRode.SetActive(false);
            player.SetStateAction(new PlayerFishing(player));
        }
    }

    public void SetForce(float forceToLaunchFishingRode)
    {
        player.playerData.forceToLaunchFishingRode = forceToLaunchFishingRode;
    }

    public override void FailFishing()
    {
        player.barLaunchFishingRode.SetActive(false);
        player.animator.SetBool("Fishing", false);
        player.animator.SetBool("ThrowFishingRode", false);
        player.SetStateAction(new PlayerExplore(player));
    }



}
