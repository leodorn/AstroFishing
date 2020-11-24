using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : InteractableObject
{
    Animator animator;
    public static SpaceShip instance;
    bool isOpen;

    public void Awake()
    {
        isOpen = false;
        if(instance == null)
        {
            instance = this;
        }
        animator = GetComponent<Animator>();
    }
    public void OpenOrCloseDoor()
    {
        isOpen = !isOpen;
        animator.SetTrigger("OpenOrCloseDoor");
    }

    public override void DoInteraction() 
    {
        if(!isOpen)
        {
            OpenOrCloseDoor();
        }
            
    }

    public override bool ConditionShowText()
    {
        if(Player.instance.GetStateAction() is PlayerExplore && Player.instance.GetStateLocation() is PlayerOnGround)
        {
            return true;
        }
        return false;
    }

    public void OpenMenu()
    {
        //Ici pour ouvrir le menu;
    }

    
}
