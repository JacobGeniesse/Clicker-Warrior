using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NumberControlExtension;

public class GameManager : MonoBehaviour
{
    //Resource Class
    public ResourceManager Resources = new ResourceManager();
    public GoldGenerator GoldGen;
    public RubyGenerator RubyGen;

    //Death Timer Variables
    public float TimerValue;
    public float MaxTimerValue;
    public bool TimerPause = false;

    //Wave Variable
    public int wave = 1;

    //Reference to EnemyHealthScript
    private EnemyHealth enemy;

    //Reference to Shop Manager (Public due to starting the game inactive)
    public ShopManager SM;

    //Reference to the currency texts
    private TMP_Text GoldUI;
    private TMP_Text RubyUI;

    //referemce to the timerUI
    private TMP_Text TimerText;

    //ref to wave text
    private TMP_Text WaveText;

    //Reference to Plushie Game Object
    public GameObject Plushie;

    public UpgradeManager UM = new UpgradeManager();

    void Start()
    {
        //Define enemy, GoldGen, RubyGen, Gold text, Ruby text, and set the time value to its maximum
        enemy = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
        GoldGen = this.GetComponent<GoldGenerator>();
        RubyGen = this.GetComponent<RubyGenerator>();
        GoldUI = GameObject.Find("Gold Amount").GetComponent<TMP_Text>();
        RubyUI = GameObject.Find("Ruby Amount").GetComponent<TMP_Text>();
        TimerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        WaveText = GameObject.Find("WaveText").GetComponent<TMP_Text>();
        WaveText.text = "Wave: " + wave;
        TimerValue = MaxTimerValue;

        //After the places to assign are assigned grab the save data
        SaveData SD = LoadSystem.LoadGameData();
        if (SD != null)
        {
            UM.Upgrades = SD.UM;
            Resources.Currency["Gold"] = SD.Gold;
            Resources.Currency["Ruby"] = SD.Rubies;
            TimerValue = SD.TimerValue;
            enemy.CurrentHP = SD.CurrentHP;
            enemy.UpdateHPText();
            wave = SD.Wave;
            UpdateWaveText();
        }
    }

    void Update()
    {
        //Set the Gold and Ruby text to match their variable counterparts
        UpdateCurrencyText();

        //Timer Stuff
        if (TimerValue > 0 && TimerPause != true)
        {
            TimerValue -= Time.deltaTime;
            TimerText.text = "Attacking In: " + Mathf.Ceil(TimerValue);
        }
        else if (TimerValue <= 0 && TimerPause == false)
        {
            TimerPause = true;
            //Kill the player if time is up
            TimeUp();
        }
    }


    //Add Time to the Timer
    public void AddTime()
    {
        if (wave % 10 == 0)
        {
            TimerValue += 10 * (UM.Upgrades[2].UpgradeTier + 1);
        }
    }

    //Pause death timer
    public void PauseTimer()
    {
        TimerPause = true;
    }

    //Unpause death timer
    public void UnpauseTimer()
    {
        TimerPause = false;
    }

    //Death at end of timer
    private void TimeUp()
    {
        //Check if the player has a voucher
        if (UM.Upgrades[9].UpgradeTier < 1)
        {
            //If no voucher take away everything from them
            ResetMoney();
            ResetUpgrades();
            PlushieStatus(false);
        }
        //Take away the voucher regardless in case player used one
        UM.Upgrades[9].UpgradeTier = 0;
        //Reward rubies based on wave
        float rubyAward = Mathf.Floor(wave / 10);
        RubyGen.Produce(ref rubyAward, UM.Upgrades[7], ref Resources);
        Debug.Log("YOU DIED!");
        //Set wave to one
        wave = 1;
        WaveText.text = "Wave: " + wave;
        //Reset the enemy
        enemy.ResetHP();
        TimerValue = MaxTimerValue;
        TimerText.text = "Attacking In: " + Mathf.Ceil(TimerValue);
        TimerPause = false;
    }

    //Give the plushie to the player
    public void PlushieStatus(bool Status)
    {
        Plushie.SetActive(Status); //Set the plushie game object's status to true or false
    }

    private void ResetMoney()
    {
        //Reset Gold and Ruby Money Blocks
        for(int i = 0; i < Resources.Currency["Gold"].Length; i++)
        {
            Resources.Currency["Gold"][i] = 0;
            Resources.Currency["Ruby"][i] = 0;
        }
    }

    //Set the upgradetier values back to 0
    private void ResetUpgrades()
    {
        for (int i = 0; i < UM.Upgrades.Length; i++)
        {
            UM.Upgrades[i].UpgradeTier = 0;
            UM.Upgrades[i].UpgradeCostCurrent = UM.Upgrades[i].UpgradeCostOriginal;
        }
    }

    //Give player gold based on damage
    public void AddGold(float DamageValue)
    {
        GoldGen.Produce(ref DamageValue, UM.Upgrades[5], ref Resources); //Call upon gold gen to calc gold amount
    }

    public void IncrementWave() //Go to the next wave
    {
        wave++;
        UpdateWaveText();
    }

    public void UpdateCurrencyText()
    {
        if (Resources.Currency["Gold"][3] > 0) //If gold is in the billions
        {
            GoldUI.text = "Gold: " + Resources.Currency["Gold"][3] + "." + Mathf.Floor(Resources.Currency["Gold"][2] / 100) + "B";
        }
        else if (Resources.Currency["Gold"][2] > 0 && Resources.Currency["Gold"][3] == 0) //If gold is in the millions
        {
            GoldUI.text = "Gold: " + Resources.Currency["Gold"][2] + "." + Mathf.Floor(Resources.Currency["Gold"][1] / 100) + "M";
        }
        else if (Resources.Currency["Gold"][1] > 0 && Resources.Currency["Gold"][2] == 0) //If gold is in the thousands
        {
            GoldUI.text = "Gold: " + Resources.Currency["Gold"][1] + "." + Mathf.Floor(Resources.Currency["Gold"][0] / 100) + "K";
        }
        else //If gold is in the 1s, 10s, or 100s
        {
            GoldUI.text = "Gold: " + Resources.Currency["Gold"][0];
        }

        if (Resources.Currency["Ruby"][3] > 0) //If rubies are in the billions (Somehow)
        {
            RubyUI.text = "Rubies: " + Resources.Currency["Ruby"][3] + "." + Mathf.Floor(Resources.Currency["Ruby"][2] / 100) + "B";
        }
        else if (Resources.Currency["Ruby"][2] > 0 && Resources.Currency["Ruby"][3] == 0) //If rubies are in the millions
        {
            RubyUI.text = "Rubies: " + Resources.Currency["Ruby"][2] + "." + Mathf.Floor(Resources.Currency["Ruby"][1] / 100) + "M";
        }
        else if (Resources.Currency["Ruby"][1] > 0 && Resources.Currency["Ruby"][2] == 0) //If rubies are in the thousands
        {
            RubyUI.text = "Rubies: " + Resources.Currency["Ruby"][1] + "." + Mathf.Floor(Resources.Currency["Ruby"][0] / 100) + "K";
        }
        else //If rubies are in the 1s, 10s, or hundreds
        {
            RubyUI.text = "Rubies: " + Resources.Currency["Ruby"][0];
        }
    }

    public void UpdateWaveText()
    {
        WaveText.text = "Wave: " + wave;
    }

    void OnApplicationQuit() //On quitting the game save the data
    {
        SaveSystem.SaveGame(this, enemy);
    }
}
