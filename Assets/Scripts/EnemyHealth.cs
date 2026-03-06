using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    //Floats for enemy HP
    public float CurrentHP;
    public float MaxHP;

    //Reference to Game Manager
    private GameManager GM;

    //Text for enemy health
    private TMP_Text HitPointsUI;

    void Start()
    {
        //Define gamemanager, hit point text, and set the enemy HP
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        HitPointsUI = GameObject.Find("HPText").GetComponent<TMP_Text>();
        NewHP(GM.wave);
    }

    void Update()
    {
        //Update hitpoint text
        HitPointsUI.text = "HP: " + CurrentHP;
        if (CurrentHP <= 0)
        {
            //Kill the enemy if at or below 0 health
            Die();
        }
    }

    //Setting the enemy's new HP for a new wave
    private void NewHP(int floorTier)
    {
        //If wave = 1, just max hp
        if(GM.wave <= 1)
        {
            CurrentHP = MaxHP;
        }
        //If up to wave 1000, Add a random value multiplied by the current wave
        else if(GM.wave >= 2 && GM.wave <= 1000)
        {
            CurrentHP = MaxHP + Random.Range(5, 10) * GM.wave;
        }
        else
        {
            //If over a 1000 waves in, kill the player with a nigh impossible amount of enemy health
            CurrentHP = MaxHP + Mathf.Pow(2, 31);
        }
    }

    //Advance to the next wave, and set the enemy HP
    private void Die()
    {
        GM.wave += 1;
        NewHP(GM.wave);
        GM.AddTime();
    }

    //Reset enemy health back to its intial value when called by Game manager.
    public void ResetHP()
    {
        CurrentHP = MaxHP;
    }
}
