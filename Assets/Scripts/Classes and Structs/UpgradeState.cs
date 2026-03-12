using UnityEngine;

public struct UpgradeState
{
    public string UpgradeName;

    public int UpgradeTier;

    public float UpgradeCost;
    public enum PurchaseState
    {
        Locked = 0,
        Available = 0,
        Purchased = 0
    };

    public PurchaseState availabilitiy;

    public UpgradeState(string Name, int Tier, float Cost)
    {
        UpgradeName = Name;
        UpgradeTier = Tier;
        UpgradeCost = Cost;
        availabilitiy = PurchaseState.Available;
    }
}
