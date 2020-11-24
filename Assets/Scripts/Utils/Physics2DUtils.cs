using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics2DUtils 
{
    public static bool CheckIfColliderAround(Transform transformToCheckAround,float radius, string tag)
    {
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(transformToCheckAround.position, radius);
        foreach (Collider2D collider in listCollider)
        {
            if (collider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    public static Collider2D GetColliderAround(Transform transformToCheckAround,float radius,string tag)
    {
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(transformToCheckAround.position, radius);
        foreach (Collider2D collider in listCollider)
        {
            if (collider.CompareTag(tag))
            {
                return collider;
            }
        }
        return null;
    }

    public static IInteractable GetInteractableAround(Transform transformToCheckAround, float radius)
    {
        Collider2D[] listCollider = Physics2D.OverlapCircleAll(transformToCheckAround.position, radius);
        foreach (Collider2D collider in listCollider)
        {
            
            if(collider.transform.root.GetComponent<IInteractable>() != null)
            {
                return collider.transform.root.GetComponent<IInteractable>();
               
            }
        }
        return null;
    }

}
