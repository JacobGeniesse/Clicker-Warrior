using UnityEngine;
using System;

[Serializable]
public class UpgradeState
{
    public string UpgradeName;

    public float[] OriginalCost;
    public int UpgradeTier;
    public float[] UpgradeCostCurrent;

    public enum PurchaseState
    {
        Locked,
        Available,
        Purchased
    };

    public PurchaseState Availabilitiy;

    public UpgradeState(string Name, int Tier, float[] Cost, float[] CostCurrent)
    {
        UpgradeName = Name;
        UpgradeTier = Tier;
        OriginalCost = Cost;
        UpgradeCostCurrent = CostCurrent;
        Availabilitiy = PurchaseState.Available;
    }
}
