using NUnit.Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References to other scripts
    private GameManager GM;
    private UpgradeManager UM;
    private EnemyHealth enemy;

    //Carpel Tunnel Protection Bool
    public bool CTProtectOn = false; //Autoclicker toggle

    //passive damage timers
    private float CTTimer = 0.15f;
    private float GolemTimer = 0.5f;
    private float RobotTimer = 0.15f;

    void Start()
    {
        //Define Game manager, and enemy
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UM = GM.UM;
        enemy = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        //Crude Golem Passive Damage
        if (UM.Upgrades[5].UpgradeTier > 0 && GM.TimerPause != true && GolemTimer > 0)
        {
            GolemTimer -= Time.deltaTime;
        }
        else if (UM.Upgrades[5].UpgradeTier > 0 && GM.TimerPause != true && GolemTimer <= 0)
        { 
            GolemAttack();
            GolemTimer = 0.5f;
        }

        //Robo-Hero Passive Damage
        if (UM.Upgrades[8].UpgradeTier > 0 && GM.TimerPause != true && RobotTimer > 0)
        {
            RobotTimer -= Time.deltaTime;
        }
        else if (UM.Upgrades[8].UpgradeTier > 0 && GM.TimerPause != true && RobotTimer <= 0)
        {
            RobotAttack();
            RobotTimer = 0.15f;
        }

        //Auto Clicker Passive Damage
        if (CTProtectOn == true && GM.TimerPause != true && CTTimer > 0)
        {
            CTTimer -= Time.deltaTime;
        }
        else if (CTProtectOn == true && GM.TimerPause != true && CTTimer <= 0)
        {
            EnemyDamage();
            CTTimer = 0.15f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseGame();
        }
    }

    //Calculate player damage
    public void EnemyDamage()
    {
        //Player damage dealt
        float damageValue = 1 + (UM.Upgrades[0].UpgradeTier);
        //Enemy Health Lost
        enemy.TakeDamage(damageValue);
        //Gold recieved
        GM.AddGold(damageValue);
    }

    //Calculate golem damage
    private void GolemAttack()
    {
        //Golem Damage dealt
        float damageValue = 3 * UM.Upgrades[4].UpgradeTier;
        //Enemy Health lost
        enemy.TakeDamage(damageValue);
        //Gold recieved
        GM.AddGold(damageValue);
    }

    //Calculate Robo-Hero Damage
    private void RobotAttack()
    {
        //Robot Damage Dealt
        float damageValue = (1 + UM.Upgrades[0].UpgradeTier) * UM.Upgrades[8].UpgradeTier;
        //Enemy Health Lost
        enemy.TakeDamage(damageValue);
        //Gold Recieved
        GM.AddGold(damageValue);
    }

    private void CloseGame()
    {
        Application.Quit();
    }
}
