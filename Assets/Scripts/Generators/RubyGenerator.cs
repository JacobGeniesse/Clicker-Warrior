using UnityEngine;
using NumberControlExtension;

public class RubyGenerator : MonoBehaviour, IGenerator
{
    public void Produce(ref float additionAmount, UpgradeState charmRef, ref ResourceManager rubyRef)
    {
        float TempRuby; //Temp raw number
        float[] TempArr = new float[4]; //Temp holding for blocked numbers
        if (charmRef.UpgradeTier > 0) //Player has ruby amulet
        {
            TempRuby = Mathf.Ceil(additionAmount * (1.5f * charmRef.UpgradeTier)); //calc raw number
        }
        else //Player doesn't have ruby amulet
        {
            TempRuby = additionAmount; //calc raw number
        }
        TempArr.Sort(TempRuby); //Sort raw number into blocks
        rubyRef.Currency["Ruby"].BlockAddition(TempArr); //Do addition with the main resource manager hub
    }
}