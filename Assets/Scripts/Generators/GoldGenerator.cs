using UnityEngine;
using NumberControlExtension;

public class GoldGenerator : MonoBehaviour, IGenerator
{
    public void Produce(ref float additionAmount, UpgradeState charmRef, ref ResourceManager goldRef)
    {
        float TempGold; //Float for holding raw number
        float[] TempArr = new float[4]; //Temporary blocks to store divided number
        if (charmRef.UpgradeTier > 0) //Does the player have at least one gold tier amount?
        {
            TempGold = Mathf.Ceil(additionAmount * (1.5f * charmRef.UpgradeTier)); //Forumla to calc gold gained placing raw number in temp var
        }
        else //If the player doesn't have gold charm use a different formula
        {
            TempGold = additionAmount;
        }
        TempArr.Sort(TempGold); //Use NumberControlExtension to sort the temp var's raw number into blocks
        TempArr.ManageNumbers(); //Manage Numbers just in case
        goldRef.Currency["Gold"].BlockAddition(TempArr); //Do addition with the main resource manager hub
    }
}