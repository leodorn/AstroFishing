using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : InteractableObject
{

    
    public Transform navigationPlaceTransform, fishStockPlaceTransform;
    public int stockInBoatForFish;
    [SerializeField]
    float radiusToGoInBoat,speed;
    [HideInInspector]
    public float valueToMove;
    public List<Fish> fishCaughtList;

    private void Start()
    {
        stockInBoatForFish = 3;
        fishCaughtList = new List<Fish>();
    }
    public override void Update()
    {
        base.Update();
        if(Physics2DUtils.GetColliderAround(transform,radiusToGoInBoat, "Player") != null )
        {
            Player.instance.boat = this ;
        } 
    }

    //public override void CheckHideText(Transform transformRightToCheckAround, Transform transformLeftToCheckAround)
    //{
    //    if(Player.instance.GetStateLocation() is PlayerInBoat &&  !(Player.instance.GetStateAction() is PlayerExplore))
    //    { 
    //        if (Physics2DUtils.GetColliderAround(transformRightToCheckAround, 5, "Ground") == null || Physics2DUtils.GetColliderAround(transformLeftToCheckAround, 5, "Ground") == null)
    //        {
    //            showInteractObject.SetActive(false);
    //        }
    //    }
    //    else if (!(Player.instance.GetStateAction() is PlayerExplore))
    //    {
    //        if (Physics2DUtils.GetColliderAround(transformRightToCheckAround, 5, "Player") == null || Physics2DUtils.GetColliderAround(transformLeftToCheckAround, 5, "Player") == null)
    //        {
    //            showInteractObject.SetActive(false);
    //        }
    //    }
    //}

    public override bool ConditionShowText()
    {
        if(Player.instance.GetStateAction() is PlayerExplore && 
            ((Player.instance.GetStateLocation() is PlayerInBoat && Physics2DUtils.CheckIfColliderAround(transform, 5, "Ground")) || Player.instance.GetStateLocation() is PlayerOnGround))
        {
            return true;
        }
        return false;
    }

    public override void ShowText()
    {
        base.ShowText();
        if (Physics2DUtils.GetColliderAround(transform, 5, "Ground") != null && Player.instance.GetStateLocation() is PlayerInBoat)
        {
            textZone.text = "Out of boat";
            showInteractObject.SetActive(true);
        }            
        else 
        {
            textZone.text = description;
            showInteractObject.SetActive(true);          
        }
        
        
    }

    private void FixedUpdate()
    {
        if(Player.instance.GetStateLocation() is PlayerInBoat)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime * valueToMove);
        }
    }


    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(transform.position, radiusToGoInBoat);
        //Go out the boat
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
    }

    

    public override void DoInteraction()
    {
        if (Player.instance.GetStateLocation() is PlayerInBoat && Physics2DUtils.CheckIfColliderAround(transform, 5, "Ground"))
        {
            Player.instance.GoOutOfBoat(Physics2DUtils.GetColliderAround(transform, 5, "Ground").GetComponent<Ground>());
        }
        else
        {
            Player.instance.GoInBoat();
        }
    }

    
}
