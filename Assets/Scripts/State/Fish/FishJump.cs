using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishJump : FishState
{

    public FishJump(Fish fish)
    {
        this.fish = fish;
        fish.StopAllCoroutines();
    }
    public override void Jump()
    {
        fish.transform.SetParent(null);
        fish.transform.rotation = Quaternion.Euler(fish.transform.rotation.x, fish.transform.rotation.y, -180);
        fish.rigidBodyFish.gravityScale = 0.5f;
        fish.rigidBodyFish.AddForce(new Vector2(50, 200));       
        fish.StartCoroutine(WaitBeforeMove());
    }

    private IEnumerator WaitBeforeMove()
    {
        yield return new WaitForSeconds(2f);
        fish.rigidBodyFish.velocity = Vector2.zero;
        fish.rigidBodyFish.gravityScale = 0;
        fish.SetState(new FishFree(fish, true));
        
    }

    

}
