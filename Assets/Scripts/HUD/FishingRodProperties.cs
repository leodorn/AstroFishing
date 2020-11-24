using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodProperties
{
    private float forceToGoDownInWater;
    private float depthLimit;
    private float touchWaterPoint;
    private int fishMax;

    public Upgrade currentUpgrade;

    public int getFishMax()
    {

        switch(currentUpgrade.GetFishingRodUpgrade())
        {
            case (Upgrade.FishingRodUpgrade.WOOD): fishMax = 1; break;
            case (Upgrade.FishingRodUpgrade.SILVER): fishMax = 1; break;
            case (Upgrade.FishingRodUpgrade.IRON): fishMax = 2; break;
            case (Upgrade.FishingRodUpgrade.DIAMOND): fishMax = 3; break;
            case (Upgrade.FishingRodUpgrade.TITANIUM): fishMax = 4; break;
            case (Upgrade.FishingRodUpgrade.UNBREAKABLE): fishMax = 6; break;
            
        }
        return fishMax;
    }

    public void setFishMax(int value)
    {
        fishMax = value;
    }

    public float getForceToGoDownInWater()
    {
        return forceToGoDownInWater;
    }

    public void setForceToGoDownInWater(float value)
    {
        forceToGoDownInWater = value;
    }

    public float getDepthLimit()
    {
        return depthLimit;
    }

    public void setDepthLimit(float value)
    {
        depthLimit = value;
    }

    public float getTouchWaterPoint()
    {
        return touchWaterPoint;
    }

    public void setTouchWaterPoint(float value)
    {
        touchWaterPoint = value;
    }

    public Upgrade getCurrentUpgrade()
    {
        return currentUpgrade;
    }

    public void setCurrentUpgrade(Upgrade value)
    {
        currentUpgrade = value;
    }

}
    
