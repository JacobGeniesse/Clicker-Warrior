using UnityEngine;

[System.Serializable]
public class UpgradeState
{
    public string UpgradeName;

    public int UpgradeTier;

    public float[] UpgradeCostCurrent = new float[4];
    public float[] UpgradeCostOriginal = new float[4];

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
        UpgradeCostOriginal = Cost;
        UpgradeCostCurrent = CostCurrent;
        Availabilitiy = PurchaseState.Available;
    }
}
