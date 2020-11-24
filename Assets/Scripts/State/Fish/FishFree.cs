using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFree : FishState
{
    bool seeTheDecoy;
    bool canBeCatch;
    int totalMovementPoint = 10;
    float minDistanceBetweenMovementPoint;
    public FishFree(Fish fish,bool activeRun)
    {
        this.fish = fish;        
        seeTheDecoy = false;
        fish.StopAllCoroutines();
        if(activeRun)
        {
            fish.StartCoroutine(TimeToRun());
        }
        else
        {
            canBeCatch = true;
        }
        
        fish.StartCoroutine(RandomGainSpeed());
        minDistanceBetweenMovementPoint = Mathf.Abs(fish.fishData.leftMax - fish.fishData.rightMax) / 10;
    }

    void GrabDecoy(GameObject fishingRode)
    {
        fish.transform.SetParent(fishingRode.transform);
        fish.transform.rotation = Quaternion.Euler(fish.transform.rotation.x, fish.transform.rotation.y, -90); ;
        fish.transform.position = new Vector3(fishingRode.transform.position.x, fishingRode.transform.position.y, fishingRode.transform.position.z - 0.1f);
        fishingRode.GetComponent<FishingRode>().fish = fish;
        fish.fishingRode = fishingRode.GetComponent<FishingRode>();
        fish.SetState(new FishCatch(fish));
    }

    IEnumerator RandomGainSpeed()
    {

        bool hasGainSpeed = false;
        int randomChance = Random.Range(0, 100);
        if (randomChance < fish.fishData.chanceToGainSpeed && !seeTheDecoy)
        {
            hasGainSpeed = true;
            fish.StartCoroutine(GainSpeedSlowly(fish.fishData.initialSpeed, fish.speed, false));
        }
        if (!hasGainSpeed)
        {

            yield return new WaitForSeconds(3f);
            fish.StartCoroutine(RandomGainSpeed());
        }
    }

    IEnumerator GainSpeedSlowly(float startSpeed, float actualSpeed, bool hasSpeedMax)
    {
        if (seeTheDecoy)
        {
            fish.StopCoroutine(GainSpeedSlowly(startSpeed, actualSpeed, hasSpeedMax));
            fish.speed = startSpeed;
            yield return new WaitForSeconds(0);
        }
        else
        {
            if (actualSpeed + 0.01 > startSpeed * 2)
            {
                hasSpeedMax = true;
            }
            if (hasSpeedMax)
            {
                actualSpeed -= 0.01f;
            }
            else
            {
                actualSpeed += 0.01f;
            }
            fish.speed = actualSpeed;
            if (actualSpeed <= startSpeed && hasSpeedMax)
            {
                fish.StartCoroutine(RandomGainSpeed());
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
                fish.StartCoroutine(GainSpeedSlowly(startSpeed, actualSpeed, hasSpeedMax));
            }
        }


    }

    public override void CreateMovementPoint()
    {
        float oldRandomX = fish.transform.position.x;
        float randomX, randomY;
        for (int i = 0; i < totalMovementPoint; i++)
        {
            do
            {
                randomX = Random.Range(fish.fishData.leftMax, fish.fishData.rightMax);
                randomY = Random.Range(fish.fishData.depthMax, fish.fishData.depthMin);
            }
            while (Mathf.Abs(randomX - oldRandomX) < minDistanceBetweenMovementPoint);
            fish.movementPointList.Add(new Vector2(randomX, randomY));
            oldRandomX = randomX;
        }
    }
    public void MoveRandomly()
    {
        if ((fish.pointToGo.x == 0 && fish.pointToGo.y == 0) || Vector2.Distance(fish.transform.position, fish.pointToGo) < 0.25)
        {
            fish.pointToGo = ChooseRandomPoint(fish.pointToGo);
        }
        GoAndLook(fish.pointToGo, 1);

    }

    void GoAndLook(Vector2 toGo, float speedMultiply)
    {
        Vector2 diff = (Vector2)fish.transform.position - toGo;
        diff.Normalize();


        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(rotZ) >= 90)
        {
            fish.spriteRenderer.flipY = true;
        }
        else
        {
            fish.spriteRenderer.flipY = false;
        }
        fish.transform.rotation = Quaternion.Euler(fish.transform.rotation.x, fish.transform.rotation.y, rotZ);
        fish.transform.position = Vector2.MoveTowards(fish.transform.position, toGo, fish.speed * speedMultiply * Time.deltaTime);
    }

    Vector2 ChooseRandomPoint(Vector2 oldMovementPoint)
    {
        Vector2 pointToReturn;
        do
        {
            pointToReturn = fish.movementPointList[Random.Range(0, fish.movementPointList.Count)];
        }
        while (pointToReturn == oldMovementPoint);
        return pointToReturn;
    }

    IEnumerator TimeToRun()
    {
        canBeCatch = false;
        yield return new WaitForSeconds(fish.fishData.timeBeforeBeAttractedByDecoy);
        canBeCatch = true;
    }

    public override void Update()
    {
     
        fish.animator.SetBool("Swim", true);
        GameObject decoy = null;
        Collider2D decoyCollider = Physics2DUtils.GetColliderAround(fish.transform, fish.fishData.rangeToSeeDecoy, "Decoy");
        if (decoyCollider != null)
        {
            decoy = decoyCollider.gameObject;
            seeTheDecoy = true;
        }
        else
        {
            seeTheDecoy = false;
        }
        if (decoy != null && decoy.transform.position.y < 0 && decoy.GetComponent<FishingRode>().fish == null && canBeCatch)
        {


            if (Vector2.Distance(fish.transform.position, decoy.transform.position) <= fish.fishData.rangeToPickDecoy)
            {
                GrabDecoy(decoy);
            }
            else if (Vector2.Distance(fish.transform.position, decoy.transform.position) <= fish.fishData.rangeToSlowNearDecoy)
            {
                GoAndLook(decoy.transform.position, 0.3f);
            }
            else
            {
                GoAndLook(decoy.transform.position, 1);
            }

        }
        else
        {

            MoveRandomly();
        }
        
    }

   

}
