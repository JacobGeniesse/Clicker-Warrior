using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    //Reference to Plushie Game Object
    public GameObject Plushie;


    //Currency Dictionary
    public Dictionary<string, float> Resources = new Dictionary<string, float>()
    {
        {"Gold", 0},
        {"Ruby", 0}
    };

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
        TimerValue = MaxTimerValue;
    }

    void Update()
    {
        //Set the Gold and Ruby text to match their variable counterparts
        GoldUI.text = "Gold: " + Resources["Gold"];
        RubyUI.text = "Rubies: " + Resources["Ruby"];

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
        TimerValue += UpgradeTier[1];
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
        if (UpgradeTier[9] < 1)
        {
            //If no voucher take away everything from them
            Resources["Ruby"] = 0;
            Resources["Gold"] = 0;

            ResetUpgrades();
            SM.ResetCosts();
            Plushie.SetActive(false);
            Debug.Log("YOU DIED!");
        }
        //Take away the voucher regardless in case player used one
        UpgradeTier[9] = 0;
        //Reward rubies based on wave
        Resources["Ruby"] += Mathf.Floor(wave / 10) * (UpgradeTier[7] + 1);
        //Set wave to one
        wave = 1;
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
        Resources["Gold"] += (DamageValue + (5 * UpgradeTier[2]));
    }

}
