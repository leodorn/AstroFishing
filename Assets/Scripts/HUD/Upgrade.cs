using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Upgrade : MonoBehaviour
{
    private string upgradeLabel;
    private int upgradeCost;
    private int upgradeIcon;
    private UpgradeType upgradeType;
    private FishingRodUpgrade currentFishingRodUpgrade;
    private BoatUpgrade currentBoatUpgrade;
    public bool maxUpgraded;


    public TextAsset UpgradesJSONList;

    public Upgrade(UpgradeType upgradeType)
    {
        this.upgradeType = upgradeType;
        switch(upgradeType)
        {
            case (UpgradeType.FISHING_ROD): currentFishingRodUpgrade = FishingRodUpgrade.WOOD; SetCurrentUpdateLevel(); break;
            case (UpgradeType.BETTER_BOAT): currentBoatUpgrade = BoatUpgrade.LITTLE_BOAT; SetCurrentUpdateLevel(); break;
        }
        this.maxUpgraded = false;
    }

    public enum UpgradeType
    {
        FISHING_ROD,
        GAIN_EFFICIENCY,
        BETTER_BOAT,
        BIGGER_LAKE
    }

    public enum FishingRodUpgrade
    {
        WOOD,
        SILVER,
        IRON,
        DIAMOND,
        TITANIUM,
        UNBREAKABLE

    }

    public enum BoatUpgrade
    {
        LITTLE_BOAT,
        AIR_BOAT,
        WOOD_BOAT,
        FISHERMAN_BOAT,
        BEAUTIFUL_FISHERMAN_BOAT
    }

    public int GetNextCost()
    {
        return upgradeCost;
    }

    public string GetNextLabel()
    {
        return upgradeLabel;
    }

    public FishingRodUpgrade GetFishingRodUpgrade()
    {
        return currentFishingRodUpgrade;
    }

    public BoatUpgrade GetBoatUpgrade()
    {
        return currentBoatUpgrade;
    }

    public void SetCurrentUpdateLevel()
    {
        switch(upgradeType)
        {
            case(UpgradeType.FISHING_ROD):
                SetCurrentFishingRod();
                break;
            case (UpgradeType.BETTER_BOAT):
                SetCurrentBoat();
                break;
            case (UpgradeType.GAIN_EFFICIENCY):

                break;
            case (UpgradeType.BIGGER_LAKE):

                break;
        }
    }

    public void SetCurrentFishingRod()
    {
        switch(currentFishingRodUpgrade)
        {
            case (FishingRodUpgrade.WOOD):
                upgradeCost = UpgradeInfo.SILVER_FISHING_ROD_COST;
                upgradeLabel = UpgradeInfo.SILVER_FISHING_ROD_LABEL;
                break;
            case (FishingRodUpgrade.SILVER):
                upgradeCost = UpgradeInfo.IRON_FISHING_ROD_COST;
                upgradeLabel = UpgradeInfo.IRON_FISHING_ROD_LABEL;
                break;
            case (FishingRodUpgrade.IRON):
                upgradeCost = UpgradeInfo.DIAMOND_FISHING_ROD_COST;
                upgradeLabel = UpgradeInfo.DIAMOND_FISHING_ROD_LABEL;
                break;
            case (FishingRodUpgrade.DIAMOND):
                upgradeCost = UpgradeInfo.TITANIUM_FISHING_ROD_COST;
                upgradeLabel = UpgradeInfo.TITANIUM_FISHING_ROD_LABEL;
                break;
            case (FishingRodUpgrade.TITANIUM):
                upgradeCost = UpgradeInfo.UNBREAKABLE_FISHING_ROD_COST;
                upgradeLabel = UpgradeInfo.UNBREAKABLE_FISHING_ROD_LABEL;
                break;
        }
    }

    public void UpgradeFishingRodLevel()
    {
        switch (currentFishingRodUpgrade)
        {
            case (FishingRodUpgrade.WOOD):
                currentFishingRodUpgrade = FishingRodUpgrade.SILVER;
                SetCurrentFishingRod();
                break;
            case (FishingRodUpgrade.SILVER):
                currentFishingRodUpgrade = FishingRodUpgrade.IRON;
                SetCurrentFishingRod();
                break;
            case (FishingRodUpgrade.IRON):
                currentFishingRodUpgrade = FishingRodUpgrade.DIAMOND;
                SetCurrentFishingRod();
                break;
            case (FishingRodUpgrade.DIAMOND):
                currentFishingRodUpgrade = FishingRodUpgrade.TITANIUM;
                SetCurrentFishingRod();
                break;
            case (FishingRodUpgrade.TITANIUM):
                SetCurrentFishingRod();
                maxUpgraded = true;
                break;
        }
    }

    public void SetCurrentBoat()
    {
        switch (currentBoatUpgrade)
        {
            case (BoatUpgrade.LITTLE_BOAT):
                upgradeCost = UpgradeInfo.AIR_BOAT_COST;
                upgradeLabel = UpgradeInfo.AIR_BOAT_LABEL;
                break;
            case (BoatUpgrade.AIR_BOAT):
                upgradeCost = UpgradeInfo.WOOD_BOAT_COST;
                upgradeLabel = UpgradeInfo.WOOD_BOAT_LABEL;
                break;
            case (BoatUpgrade.WOOD_BOAT):
                upgradeCost = UpgradeInfo.FISHERMAN_BOAT_COST;
                upgradeLabel = UpgradeInfo.FISHERMAN_BOAT_LABEL;
                break;
            case (BoatUpgrade.FISHERMAN_BOAT):
                upgradeCost = UpgradeInfo.BEAUTIFUL_FISHERMAN_BOAT_COST;
                upgradeLabel = UpgradeInfo.BEAUTIFUL_FISHERMAN_BOAT_LABEL;
                break;
        }
    }

    public void UpgradeBoatLevel()
    {
        switch (currentBoatUpgrade)
        {
            case (BoatUpgrade.LITTLE_BOAT):
                currentBoatUpgrade = BoatUpgrade.AIR_BOAT;
                SetCurrentBoat();
                break;
            case (BoatUpgrade.AIR_BOAT):
                currentBoatUpgrade = BoatUpgrade.WOOD_BOAT;
                SetCurrentBoat();
                break;
            case (BoatUpgrade.WOOD_BOAT):
                currentBoatUpgrade = BoatUpgrade.FISHERMAN_BOAT;
                SetCurrentBoat();
                break;
            case (BoatUpgrade.FISHERMAN_BOAT):
                currentBoatUpgrade = BoatUpgrade.BEAUTIFUL_FISHERMAN_BOAT;
                SetCurrentBoat();
                maxUpgraded = true;
                break;
        }
    }



}
