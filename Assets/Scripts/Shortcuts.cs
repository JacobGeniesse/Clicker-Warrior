using UnityEngine;

public class Shortcuts : MonoBehaviour
{
    private GameManager GM;
    private EnemyHealth enemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemy = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        //Deal Damage
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            enemy.TakeDamage(999);

            GM.AddGold(999);
        }

        //Give Rubies
        if (Input.GetKeyDown(KeyCode.Period))
        {
            float rubies = 9999f;
            GM.RubyGen.Produce(ref rubies, GM.UM.Upgrades[7], ref GM.Resources);
        }

        //Add Time
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            GM.TimerValue += 10f;
        }

        //Kill Player
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            GM.TimerValue = 0;
        }
    }
}
