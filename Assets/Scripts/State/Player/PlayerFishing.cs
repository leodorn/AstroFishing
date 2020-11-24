using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : PlayerStateAction
{
    bool canCheckDecoyAround;
    bool rideUp;
    public PlayerFishing(Player player)
    {
        this.player = player;
        rideUp = false;
        canCheckDecoyAround = true;
    }


    public override void RideUpFishingRodePerformed() => rideUp = true;

    public override void RideUpFishingRodeCanceled() => rideUp = false;
   
    

   

    

    public override void LaunchFishingRode()
    {
        player.StartCoroutine(WaitBeforeCheckDecoy());
        GameObject decoy = Instantiate(player.playerData.decoyPrefab, player.spawnDecoyTransform.position, Quaternion.identity);
        player.fishingRode = decoy.GetComponent<FishingRode>();
        player.fishingRode.AddForce(player.playerData.directionToApplyToDecoy, player.playerData.forceToApplyToDecoy, player.playerData.forceToLaunchFishingRode);
        player.cameraGame.AttachCameraAndSetPosition(player.fishingRode.transform);
    }

    private IEnumerator WaitBeforeCheckDecoy()
    {
        canCheckDecoyAround = false;
        yield return new WaitForSeconds(2f);
        canCheckDecoyAround = true;
    }

    public override void FailFishing()
    {
        player.barLaunchFishingRode.SetActive(false);
        player.animator.SetBool("Fishing", false);
        player.animator.SetBool("ThrowFishingRode", false);
        player.SetStateAction(new PlayerExplore(player));      
    }

    public override void Update()
    {

        if (Physics2DUtils.CheckIfColliderAround(player.transform, 2, "Decoy") && canCheckDecoyAround)
        {
            Physics2DUtils.GetColliderAround(player.transform, 3, "Decoy").GetComponent<FishingRode>().GetBackFish();
            player.animator.SetBool("Fishing", false);
            player.animator.SetBool("ThrowFishingRode", false);
        }
    }

    public override void FixedUpdate()
    {
        if (rideUp && player.fishingRode != null && player.fishingRode.InWaterOrNot())      
        {
            player.fishingRode.MoveTowards(player.transform, player.playerData.forceForRideUpDecoy);
        }
    }



}
