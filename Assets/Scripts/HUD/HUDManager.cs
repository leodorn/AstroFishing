using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{



    // Variables à modifier
    public int moneyValue;
    public int fishCount;

    public bool playerIsInShop;

    public Upgrade fishingRodUpgrade;
    public Upgrade boatUpgrade;

    //GameObjects HUD
    public GameObject shopDisplay;
    public Color textShopColor;
    public TextMeshProUGUI moneyDisplay;

    public TextMeshProUGUI fishingRodUpgradeCostDisplay;
    public TextMeshProUGUI fishingRodUpgradeLabelDisplay;

    public TextMeshProUGUI boatUpgradeCostDisplay;
    public TextMeshProUGUI boatUpgradeLabelDisplay;

    public Button fishingRodUpdateButton;
    public Button boatUpdateButton;


    void Start()
    {
        moneyValue = 1000;
        playerIsInShop = true;
        textShopColor = fishingRodUpgradeLabelDisplay.color;

        fishingRodUpgrade = new Upgrade(Upgrade.UpgradeType.FISHING_ROD);
        boatUpgrade = new Upgrade(Upgrade.UpgradeType.BETTER_BOAT);

        //Initialisation des GameObjects
        moneyDisplay.text = moneyValue.ToString();
        fishingRodUpgradeCostDisplay.text = fishingRodUpgrade.GetNextCost().ToString();
        fishingRodUpgradeLabelDisplay.text = fishingRodUpgrade.GetNextLabel();
        boatUpgradeCostDisplay.text = boatUpgrade.GetNextCost().ToString();
        boatUpgradeLabelDisplay.text = boatUpgrade.GetNextLabel();




    }

    
    void Update()
    {
        if (moneyValue >= fishingRodUpgrade.GetNextCost())
        {
            fishingRodUpgradeCostDisplay.text = "Upgrade for " + fishingRodUpgrade.GetNextCost().ToString();
            fishingRodUpgradeCostDisplay.color = textShopColor;
        } else
        {
            NotEnoughMoney();
        }

    }


    public void UpgradeFishingRod()
    {
        
        int fishingRodUpgradeCost = fishingRodUpgrade.GetNextCost();
        if (moneyValue >= fishingRodUpgradeCost)
        {
            // Achat validé donc on fait tout le traitement
            RemoveMoney(fishingRodUpgradeCost);
            fishingRodUpgrade.UpgradeFishingRodLevel();
            if (!fishingRodUpgrade.maxUpgraded)
            {
                fishingRodUpgradeCostDisplay.text = "Upgrade for " + fishingRodUpgrade.GetNextCost().ToString();
                fishingRodUpgradeLabelDisplay.text = fishingRodUpgrade.GetNextLabel();
            } else
            {
                fishingRodUpgradeCostDisplay.text = "";
                fishingRodUpgradeLabelDisplay.text = "Max upgrade reached";
                fishingRodUpdateButton.interactable = false;
            }
        } else
        {
            // Pas assez d'argent
            NotEnoughMoney();
        }
    }

    public void UpgradeBoat()
    {
        
        int boatUpgradeCost = boatUpgrade.GetNextCost();
        if (moneyValue >= boatUpgradeCost)
        {
            // Achat validé donc on fait tout le traitement
            RemoveMoney(boatUpgradeCost);
            boatUpgrade.UpgradeBoatLevel();
            if (!boatUpgrade.maxUpgraded)
            {
                boatUpgradeCostDisplay.text = boatUpgrade.GetNextCost().ToString();
                boatUpgradeLabelDisplay.text = boatUpgrade.GetNextLabel();
            }
            else
            {
                boatUpgradeCostDisplay.text = "MAX";
                boatUpgradeLabelDisplay.text = "Max upgrade reached";
                boatUpdateButton.interactable = false;
            }
        }
        else
        {
            // Pas assez d'argent
            NotEnoughMoney();
        }
    }

    public void NotEnoughMoney()
    {
        
        fishingRodUpgradeCostDisplay.color = Color.red;
        fishingRodUpgradeCostDisplay.text = fishingRodUpgrade.GetNextCost().ToString() + " needed";
    }

    public void AddMoney(int value)
    {
        moneyValue += value;
        moneyDisplay.text = moneyValue.ToString();
    }

    public void RemoveMoney(int value)
    {
        moneyValue -= value;
        moneyDisplay.text = moneyValue.ToString();
    }

    public void OpenShop()
    {
        shopDisplay.SetActive(true);
        playerIsInShop = true;
    }

    public void CloseShop()
    {
        shopDisplay.SetActive(false);
        playerIsInShop = false;
    }

    public void pushButtonShop()
    {
        if (playerIsInShop) { CloseShop(); } else { OpenShop(); }
    }
}
