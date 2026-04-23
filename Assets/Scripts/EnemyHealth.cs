using UnityEngine;
using TMPro;
using NumberControlExtension;

public class EnemyHealth : MonoBehaviour
{
    //Floats for enemy HP
    public float[] CurrentHP = new float[4] { 10, 0, 0, 0 }; //HP float
    public float[] MaxHP = new float[4] { 10, 0, 0, 0 }; //Flat HP float

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
        if (CheckForDeath() == true)
        {
            //Kill the enemy if at or below 0 health
            Die();
        }
    }

    //Setting the enemy's new HP for a new wave
    private void NewHP(int floorTier)
    {
        //If wave = 1, just max hp
        if (GM.wave <= 1)
        {
            ResetHP();
        }
        //If up to wave 10000, Add a random value multiplied by the current wave
        else if (GM.wave >= 2 && GM.wave <= 1000)
        {
            CurrentHP[0] = MaxHP[0];
            CurrentHP[0] += Mathf.Floor(Random.Range(5f, 10f) * (float)(GM.wave + 1));
            CurrentHP.ManageNumbers();
            UpdateHPText();
        }
        else
        { 
            //If over a 10000 waves in, kill the player with a nigh impossible amount of enemy health
            CurrentHP = new float[4] { 999, 999, 999, 999 };
            UpdateHPText();
        }
    }

    public void TakeDamage(float damageAmount) //Function for taking damage
    {
        float[] DamageBlock = new float[4] { 0, 0, 0, 0 }; //Declare damageblock
        DamageBlock.Sort(damageAmount); //Sort damageAmount into damage block
        DamageBlock.ManageNumbers(); //Manage the numbers just in case
        CurrentHP.BlockSubtraction(DamageBlock); //Subtract DamageBlock from EnemyHP
        UpdateHPText(); //Update the HP text
    }

    private bool CheckForDeath()
    {
        for(int i = CurrentHP.Length - 1; i > -1; i--) //If all of the HP slots are 0 the enemy is dead
        {
            if (CurrentHP[i] > 0)
            {
                return false;
            }
        }
        return true;
    }

    //Advance to the next wave, and set the enemy HP
    private void Die()
    {
        GM.IncrementWave();
        NewHP(GM.wave);
        GM.AddTime();
    }

    //Reset enemy health back to its intial value when called by Game manager.
    public void ResetHP()
    {
        for(int i = 0; i < CurrentHP.Length; i++)
        {
            CurrentHP[i] = MaxHP[i];
        }
        UpdateHPText();
    }

    public void UpdateHPText()
    {
        if (CurrentHP[3] > 0) //If health is in the billions
        {
            HitPointsUI.text = "HP: " + CurrentHP[3] + "." + Mathf.Floor(CurrentHP[2] / 100) + "B";
        }
        else if (CurrentHP[2] > 0 && CurrentHP[3] == 0) //If health is in the millions
        {
            HitPointsUI.text = "HP: " + CurrentHP[2] + "." + Mathf.Floor(CurrentHP[1] / 100) + "M";
        }
        else if (CurrentHP[1] > 0 && CurrentHP[2] == 0) //If health is in the thousands
        {
            HitPointsUI.text = "HP: " + CurrentHP[1] + "." + Mathf.Floor(CurrentHP[0] / 100) + "K";
        }
        else //If health is in the 1s, 10s, or 100s
        {
            HitPointsUI.text = "HP: " + CurrentHP[0];
        }
    }
}