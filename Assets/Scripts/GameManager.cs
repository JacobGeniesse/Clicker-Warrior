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

    //Upgrade Array
    public int[] UpgradeTier = new int[]
    {
        0, //Sword Reforge
        0, //"Magic Stopwatch"
        0, //"Gold Charm"
        0, //"Training Manual"
        0, //"Assassin's Lens"
        0, //"Crude Golem"

        //Ruby Upgrades
        0, //"Gold"
        0, //"Ruby Amulet"
        0, //RoboHero
        0, //"New Game+ Voucher"
        0, //Plushie
    };

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
            TimerValue += 5 * (UpgradeTier[2] + 1);
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
        Debug.Log(UpgradeTier[9]);
        //Check if the player has a voucher
        if (UpgradeTier[9] < 1)
        {
            //If no voucher take away everything from them
            Resources.Currency["Ruby"] = 0;
            Resources.Currency["Gold"] = 0;
        }
        //Take away the voucher regardless in case player used one
        UpgradeTier[9] = 0;
        //Reward rubies based on wave
        Resources.Currency["Ruby"] += Mathf.Floor(wave / 10) * (UpgradeTier[7] + 1);
        //Take away upgrades if no voucher (Waiting for ruby calc)
        if (UpgradeTier[9] < 1)
        {
            ResetUpgrades();
            SM.ResetCosts();
            Plushie.SetActive(false);
            Debug.Log("YOU DIED!");
        }
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
    public void AwakenPlushie()
    {
        Plushie.SetActive(true);
    }

    //Set the upgradetier values back to 0
    private void ResetUpgrades()
    {
        for(int i = 0; i < UpgradeTier.Length; i++)
        {
            UpgradeTier[i] = 0;
        }
    }

    //Give player gold based on damage
    public void AddGold(float DamageValue)
    {
        if (UpgradeTier[2] > 0)
        {
            Resources.Currency["Gold"] += Mathf.Ceil((DamageValue * (1.5f * UpgradeTier[2])));
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
