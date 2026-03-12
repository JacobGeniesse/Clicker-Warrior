using UnityEngine;

[System.Serializable]
public class UpgradeState
{
    public string UpgradeName;

    public int UpgradeTier;

    public float UpgradeCostCurrent;
    public float UpgradeCostOriginal;

    public enum PurchaseState
    {
        Locked,
        Available,
        Purchased
    };

    public PurchaseState Availabilitiy;

    public UpgradeState(string Name, int Tier, float Cost)
    {
        UpgradeName = Name;
        UpgradeTier = Tier;
        UpgradeCostOriginal = Cost;
        UpgradeCostCurrent = UpgradeCostOriginal;
        Availabilitiy = PurchaseState.Available;
    }
}
