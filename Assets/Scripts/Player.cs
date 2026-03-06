using NUnit.Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References to other scripts
    private GameManager GM;
    private EnemyHealth enemy;

    //passive damage timers
    private float GolemTimer = 1;
    private float RobotTimer = 1;

    void Start()
    {
        //Define Game manager, and enemy
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemy = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        //Crude Golem Passive Damage
        if (GM.UpgradeTier[5] > 0 && GM.TimerPause != true && GolemTimer > 0)
        {
            GolemTimer -= Time.deltaTime;
        }
        else if (GM.UpgradeTier[5] > 0 && GM.TimerPause != true && GolemTimer <= 0)
        { 
            GolemAttack();
            GolemTimer = 1;
        }

        //Robo-Hero Passive Damage
        if (GM.UpgradeTier[8] > 0 && GM.TimerPause != true && RobotTimer > 0)
        {
            RobotTimer -= Time.deltaTime;
        }
        else if (GM.UpgradeTier[8] > 0 && GM.TimerPause != true && RobotTimer <= 0)
        {
            RobotAttack();
            RobotTimer = 0.5f;
        }
    }

    //Calculate player damage
    public void EnemyDamage()
    {
        //Player damage dealt
        float damageValue = 1 + (GM.UpgradeTier[0]);
        //Enemy Health Lost
        enemy.CurrentHP -= damageValue;
        //Gold recieved
        GM.AddGold(damageValue);
    }

    //Calculate golem damage
    private void GolemAttack()
    {
        //Golem Damage dealt
        float damageValue = 5 * GM.UpgradeTier[5];
        //Enemy Health lost
        enemy.CurrentHP -= damageValue;
        //Gold recieved
        GM.AddGold(damageValue);
    }

    //Calculate Robo-Hero Damage
    private void RobotAttack()
    {
        //Robot Damage Dealt
        float damageValue = (1 + GM.UpgradeTier[0]) * GM.UpgradeTier[8];
        //Enemy Health Lost
        enemy.CurrentHP -= damageValue;
        //Gold Recieved
        GM.AddGold(damageValue);
    }
}
