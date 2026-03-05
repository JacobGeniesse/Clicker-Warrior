using NUnit.Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    ResourceManager Currency = new ResourceManager();

    UpgradeManager Upgrades = new UpgradeManager();

    public float TimerValue;
    public float MaxTimerValue;
    private bool TimerPause = false;

    private EnemyHealth enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<EnemyHealth>();
        TimerValue = MaxTimerValue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            EnemyDamage();
        }

        //Timer Stuff
        if(TimerValue > 0 && TimerPause != true)
        {
            TimerValue -= Time.deltaTime;
        }
        else if(TimerPause != false)
        {
            TimerPause = true;
            TimeUp();
        }

        //Crude Golem Passive Damage
        if (Upgrades.Upgrades["Crude Golem"] > 0)
        {
            float damageValue = 5 * Upgrades.Upgrades["Crude Golem"] * Time.deltaTime;
            enemy.CurrentHP -= damageValue;
            Currency.Resources["Gold"] = AddGold(Currency.Resources["Gold"], damageValue);
        }

        //Mechanization MassiveDamage
        if (Upgrades.Upgrades["Robo-Hero"] > 0)
        {
            float damageValue = ((1 + (2 * Upgrades.Upgrades["Sword Reforge"])) + Upgrades.Upgrades["Robo-Hero"]) * Time.deltaTime;
            enemy.CurrentHP -= damageValue;
            Currency.Resources["Gold"] = AddGold(Currency.Resources["Gold"], damageValue);
        }
    }

    private float AddGold(float BaseValue, float DamageValue)
    {
        return BaseValue + (DamageValue + (5 * Upgrades.Upgrades["Gold Charm"]));
    }


    //Add Time to the Timer
    public void AddTime()
    {
        TimerValue = MaxTimerValue + Upgrades.Upgrades["Secret Stopwatch"];
    }

    //Death at end of timer
    private void TimeUp()
    {

    }


    private void EnemyDamage()
    {
        float damageValue = 1 + (2 * Upgrades.Upgrades["Sword Reforge"]);
        enemy.CurrentHP -= damageValue;
        Currency.Resources["Gold"] = AddGold(Currency.Resources["Gold"], damageValue);
    }
}
