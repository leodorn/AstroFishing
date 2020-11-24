using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishState : MonoBehaviour
{
    protected Fish fish;
    public virtual void Update() { }
    public virtual void CreateMovementPoint() { }
    public virtual void BeFree() { }

    public virtual void StopStruggling() { }
    public virtual void Jump() { }
}
