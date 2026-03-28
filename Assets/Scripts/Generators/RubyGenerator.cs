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
        rubyRef.Currency["Ruby"][3] += TempArr[3]; // Billion block
        rubyRef.Currency["Ruby"][2] += TempArr[2]; //Million block
        rubyRef.Currency["Ruby"][1] += TempArr[1]; //Thousands block
        rubyRef.Currency["Ruby"][0] += TempArr[0]; //1s 10s 100s block
       // ManageBloat(ref rubyRef); //Further sorting of numbers
    }

    /*public void ManageBloat(ref ResourceManager rubyRef)
    {
        while (rubyRef.Currency["Ruby"] > 999) //If ruby is 1000 or more give to thousands and take from ruby
        {
            rubyRef.Currency["Ruby"] -= 1000;
            rubyRef.Currency["Ruby Thousand"] += 1;
        }

        while (rubyRef.Currency["Ruby"] < 0) //If ruby is somehow less than 0 take from thousand and give to ruby
        {
            rubyRef.Currency["Ruby"] += 1000;
            rubyRef.Currency["Ruby Thousand"] -= 1;
        }

        while (rubyRef.Currency["Ruby Thousand"] > 999) //If thousands is greater than 999 give to million and take from ruby
        {
            rubyRef.Currency["Ruby Thousand"] -= 1000;
            rubyRef.Currency["Ruby Million"] += 1;
        }

        while (rubyRef.Currency["Ruby Thousand"] < 0) //Take from million and give to thousand if thousand is less than 0
        {
            rubyRef.Currency["Ruby Thousand"] += 1000;
            rubyRef.Currency["Ruby Million"] -= 1;
        }

        while (rubyRef.Currency["Ruby Million"] > 999) //Take from million and give to billion if million is greater than 999
        {
            rubyRef.Currency["Ruby Million"] -= 1000;
            rubyRef.Currency["Ruby Billion"] += 1;
        }

        while (rubyRef.Currency["Ruby Million"] < 0) //Take from billion and give to million if million is less than 0
        {
            rubyRef.Currency["Ruby Million"] += 1000;
            rubyRef.Currency["Ruby Billion"] -= 1;
        }


        if (rubyRef.Currency["Ruby Billion"] < 0) //Emergency func for if billion somehow winds up under 0
        {
            Debug.LogWarning("Billion Under 0 Somehow!!!");
            rubyRef.Currency["Ruby Billion"] = 0;
        }
    }*/

}
