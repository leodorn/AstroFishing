using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateAction : MonoBehaviour
{
    protected Player player;
    [HideInInspector] public Fish fish;
    public virtual void RideUpFishingRodePerformed() { }
    public virtual void RideUpFishingRodeCanceled() { }

    public virtual void PressForFishing() { }
    public virtual void LaunchFishingRode() { }
    public virtual void FailFishing() { }
    public virtual void FightingStruggle() { }
    public virtual void PutFishInMarket() { }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    

}
