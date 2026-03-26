using UnityEngine;

public interface IGenerator
{
    void Produce(ref float additionAmount, UpgradeState charmRef, float goldRef);
}
