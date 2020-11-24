using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{

    public string description;
    public TextMeshProUGUI textZone;
    public Sprite spriteKey;
    public Image imageKey;
    public GameObject showInteractObject;
    public Transform transformRightToCheckAround, transformLeftToCheckAround;
    float radiusToCheckPlayer = 2.5f;
    
    public void Interact()
    {
        if(showInteractObject.activeSelf == true)
        {
            DoInteraction();
        }
    }

    public virtual void DoInteraction()
    {
        Debug.Log("Interaction pas implementée");
    }
    public virtual void ShowText()
    {
        showInteractObject.SetActive(true);
        imageKey.sprite = spriteKey;
        textZone.text = description;
    }

    public void HideText()
    {
        showInteractObject.SetActive(false);
    }

    public virtual void CheckPlayerAround()
    {
        if(Physics2DUtils.GetColliderAround(transformRightToCheckAround, radiusToCheckPlayer, "Player") != null || Physics2DUtils.GetColliderAround(transformLeftToCheckAround, radiusToCheckPlayer, "Player") != null)
        {
            if(ConditionShowText())
            {
                ShowText();
            }
            else
            {
                HideText();
            }
        }
        else
        {
            HideText();
        }
    }



    public virtual void Update()
    {
        if(Player.instance != null)
        {
            if(Physics2DUtils.GetColliderAround(transformRightToCheckAround, radiusToCheckPlayer, "Player") != null || Physics2DUtils.GetColliderAround(transformLeftToCheckAround, radiusToCheckPlayer, "Player") != null)
            {
                if(!Player.instance.interactableObjectNearList.Contains(this)) { Player.instance.interactableObjectNearList.Add(this); }
                
            }
            else
            {
                if (Player.instance.interactableObjectNearList.Contains(this))
                {
                    Player.instance.interactableObjectNearList.Remove(this);
                }
            }
        }
        CheckPlayerAround()/*CheckHideText(transformRightToCheckAround, transformLeftToCheckAround)*/;

    } 
        

    public virtual bool ConditionShowText()
    {
        Debug.Log("Pas implémenté les conditions d'affichage de texte ! ");
        return false;
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transformRightToCheckAround.position, radiusToCheckPlayer);
        Gizmos.DrawWireSphere(transformLeftToCheckAround.position, radiusToCheckPlayer);
    }




}
