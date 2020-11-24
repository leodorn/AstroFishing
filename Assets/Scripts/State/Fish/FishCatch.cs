using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCatch : FishState
{
    bool canTryToStruggle;
    bool isStruggling;
    public FishCatch(Fish fish)
    {
        this.fish = fish;
        canTryToStruggle = true;
        isStruggling = false;
        fish.StopAllCoroutines();
    }

    private IEnumerator TryToStruggle()
    {
        canTryToStruggle = false;
        yield return new WaitForSeconds(fish.fishData.timeToRetryToStruggle);

        int randomChanceToStruggle = Random.Range(0, 100);
        Debug.Log("randomChanceToStruggle " + randomChanceToStruggle + "chanceToStruggle" + (fish.fishData.chanceToStruggle + (fish.weight * fish.fishData.levelOfFish)));
        if (randomChanceToStruggle <= fish.fishData.chanceToStruggle + (fish.weight * fish.fishData.levelOfFish))
        {
            Struggle();
        }
        else
        {

            canTryToStruggle = true;
        }

    }

    void Struggle()
    {
        GameManager.instance.SetBarCircleActive(true, fish.fishData.minForce, fish.fishData.maxForce, fish.fishData.timeToChanceForce, fish.fishData.sizeOfTheGreenZone, fish);
        isStruggling = true;
        fish.animator.SetBool("Swim", true);
    }

    public override void BeFree()
    {
        Debug.Log("libre");
        fish.fishingRode.fish = null;
        fish.fishingRode = null;
        fish.transform.parent = null;
        //Set true for make the fish running
        fish.SetState(new FishFree(fish,true));

    }

    public override void Update()
    {
        fish.transform.position = fish.fishingRode.transform.position;
        if (canTryToStruggle)
        {
            fish.StartCoroutine(TryToStruggle());
        }   
        else if (!isStruggling)
        {
            fish.animator.SetBool("Swim", false);
        }
    }

    public override void StopStruggling()
    {
        isStruggling = false;
        canTryToStruggle = true;
    }

    

    
}
