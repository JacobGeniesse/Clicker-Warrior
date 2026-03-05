using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float CurrentHP;
    public float MaxHP;

    public Player player;
    public int wave = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        NewHP(wave);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHP < 0)
        {
            Die();
        }
    }
    private void NewHP(int floorTier)
    {
        CurrentHP = MaxHP + Mathf.Pow(5, floorTier);
    }

    private void Die()
    {
        wave += 1;
        NewHP(wave);
        player.AddTime();
    }
}
