using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Resource Class
    public ResourceManager Resources = new ResourceManager();

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
        //Define enemy, Gold text, Ruby text, and set the time value to its maximum
        enemy = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
        GoldUI = GameObject.Find("Gold Amount").GetComponent<TMP_Text>();
        RubyUI = GameObject.Find("Ruby Amount").GetComponent<TMP_Text>();
        TimerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        WaveText = GameObject.Find("WaveText").GetComponent<TMP_Text>();
        WaveText.text = "Wave: " + wave;
        TimerValue = MaxTimerValue;
    }

    void Update()
    {
        //Set the Gold and Ruby text to match their variable counterparts
        GoldUI.text = "Gold: " + Resources.Currency["Gold"];
        RubyUI.text = "Rubies: " + Resources.Currency["Ruby"];

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
            TimerValue += 5 * (UM.Upgrades[2].UpgradeTier + 1);
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
            Resources.Currency["Ruby"] = 0;
            Resources.Currency["Gold"] = 0;
            ResetUpgrades();
            PlushieStatus(false);
        }
        //Take away the voucher regardless in case player used one
        UM.Upgrades[9].UpgradeTier = 0;
        //Reward rubies based on wave
        if(UM.Upgrades[7].UpgradeTier > 0)
        {
            Resources.Currency["Ruby"] += Mathf.Ceil(Mathf.Floor(wave / 10) * (1.5f * UM.Upgrades[7].UpgradeTier));
        }
        else
        {
            Resources.Currency["Ruby"] += Mathf.Floor(wave / 10);
        }
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
        Plushie.SetActive(Status);
    }

    //Set the upgradetier values back to 0
    private void ResetUpgrades()
    {
        for(int i = 0; i < UM.Upgrades.Length; i++)
        {
            UM.Upgrades[i].UpgradeTier = 0;
            UM.Upgrades[i].UpgradeCostCurrent = UM.Upgrades[i].UpgradeCostOriginal;
        }
    }

    //Give player gold based on damage
    public void AddGold(float DamageValue)
    {
        if (UM.Upgrades[5].UpgradeTier > 0)
        {
            Resources.Currency["Gold"] += Mathf.Ceil((DamageValue * (1.5f * UM.Upgrades[5].UpgradeTier)));
        }
        else
        {
            Resources.Currency["Gold"] += DamageValue;
        }
    }

    public void IncrementWave()
    {
        wave++;
        WaveText.text = "Wave: " + wave;
    }

}
