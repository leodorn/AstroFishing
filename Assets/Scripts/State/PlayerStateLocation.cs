using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerStateLocation : MonoBehaviour
{
    protected Player player;
    public virtual void MovePerformed(InputAction.CallbackContext ctx) { }
    public virtual void MoveCanceled()
    {
        player.animator.SetFloat("WalkValue", 0);
        player.movementValue = 0;
    }
    public virtual void InteractWithObject()
    {
        InteractableObject interactableObjectNear = null;
        if (player.interactableObjectNearList.Count>0)
        {
            Transform transformToCompare;
            
            interactableObjectNear = player.interactableObjectNearList[0];
            if(Vector2.Distance(interactableObjectNear.transformRightToCheckAround.position, player.transform.position) < Vector2.Distance(interactableObjectNear.transformLeftToCheckAround.position, player.transform.position))
            {
                transformToCompare = interactableObjectNear.transformRightToCheckAround;
            }
            else
            {
                transformToCompare = interactableObjectNear.transformLeftToCheckAround;
            }
            foreach (InteractableObject interactableObject in player.interactableObjectNearList)
            {
                Transform transformToCompareTemp;
                if (Vector2.Distance(interactableObject.transformRightToCheckAround.position, player.transform.position) < Vector2.Distance(interactableObject.transformLeftToCheckAround.position, player.transform.position))
                {
                    transformToCompareTemp = interactableObject.transformRightToCheckAround;
                }
                else
                {
                    transformToCompareTemp = interactableObject.transformLeftToCheckAround;
                }
                if(Vector2.Distance(transformToCompareTemp.position, player.transform.position) < Vector2.Distance(transformToCompare.position, player.transform.position))
                {
                    transformToCompare = transformToCompareTemp;
                    interactableObjectNear = interactableObject;
                }
            }
            interactableObjectNear.Interact();
        }

    }
    
    public virtual void GoInBoat() { }
    public virtual void GoOutOfBoat(Ground ground) { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }





}
