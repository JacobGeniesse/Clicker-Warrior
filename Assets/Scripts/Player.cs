using NUnit.Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References to other scripts
    private GameManager GM;
    private EnemyHealth enemy;

    //Carpel Tunnel Protection Bool
    public bool CTProtectOn = false; //Autoclicker toggle

    //passive damage timers
    private float CTTimer = 0.3f;
    private float GolemTimer = 0.5f;
    private float RobotTimer = 0.25f;

    void Start()
    {
        //Define Game manager, and enemy
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemy = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        //Crude Golem Passive Damage
        if (GM.UM.UpgradeTier[5] > 0 && GM.TimerPause != true && GolemTimer > 0)
        {
            GolemTimer -= Time.deltaTime;
        }
        else if (GM.UM.UpgradeTier[5] > 0 && GM.TimerPause != true && GolemTimer <= 0)
        { 
            GolemAttack();
            GolemTimer = 0.5f;
        }

        //Robo-Hero Passive Damage
        if (GM.UM.UpgradeTier[8] > 0 && GM.TimerPause != true && RobotTimer > 0)
        {
            RobotTimer -= Time.deltaTime;
        }
        else if (GM.UM.UpgradeTier[8] > 0 && GM.TimerPause != true && RobotTimer <= 0)
        {
            RobotAttack();
            RobotTimer = 0.25f;
        }

        //Auto Clicker Passive Damage
        if (CTProtectOn == true && GM.TimerPause != true && CTTimer > 0)
        {
            CTTimer -= Time.deltaTime;
        }
        else if (CTProtectOn == true && GM.TimerPause != true && CTTimer <= 0)
        {
            EnemyDamage();
            CTTimer = 0.25f;
        }
    }

    //Calculate player damage
    public void EnemyDamage()
    {
        //Player damage dealt
        float damageValue = 1 + (GM.UM.UpgradeTier[0]);
        //Enemy Health Lost
        enemy.CurrentHP -= damageValue;
        //Gold recieved
        GM.AddGold(damageValue);
    }

    //Calculate golem damage
    private void GolemAttack()
    {
        //Golem Damage dealt
        float damageValue = 3 * GM.UM.UpgradeTier[5];
        //Enemy Health lost
        enemy.CurrentHP -= damageValue;
        //Gold recieved
        GM.AddGold(damageValue);
    }

    //Calculate Robo-Hero Damage
    private void RobotAttack()
    {
        //Robot Damage Dealt
        float damageValue = (1 + GM.UM.UpgradeTier[0]) * GM.UM.UpgradeTier[8];
        //Enemy Health Lost
        enemy.CurrentHP -= damageValue;
        //Gold Recieved
        GM.AddGold(damageValue);
    }
}
