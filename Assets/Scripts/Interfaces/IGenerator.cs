using UnityEngine;

public interface IGenerator
{
    void Produce(ref float additionAmount, UpgradeState charmRef, ref ResourceManager goldRef); //Template from production function
}
