using UnityEngine;

public class GoldGenerator : MonoBehaviour, IGenerator
{
    public void Produce(ref float additionAmount, UpgradeState charmRef, float goldRef)
    {
        if (charmRef.UpgradeTier > 0)
        {
            goldRef += Mathf.Ceil(additionAmount * (1.5f * charmRef.UpgradeTier));
        }
        else
        {
            goldRef += additionAmount;
        }
    }
}
