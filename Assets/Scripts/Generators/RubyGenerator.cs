using UnityEngine;

public class RubyGenerator : MonoBehaviour, IGenerator
{
    public void Produce(ref float additionAmount, UpgradeState charmRef, float rubyRef)
    {
        if (charmRef.UpgradeTier > 0)
        {
            rubyRef += Mathf.Ceil(additionAmount * (1.5f * charmRef.UpgradeTier));
        }
        else
        {
            rubyRef += additionAmount;
        }
    }

}
