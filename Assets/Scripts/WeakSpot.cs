using UnityEngine;
using UnityEngine.UI;

public class WeakSpot : MonoBehaviour
{
    //Other Scripts
    private GameManager GM;
    private EnemyHealth enemy;

    //Prefab for the weakspot
    public GameObject WeakSpotPrefab;

    //Active weakspot if there is one
    private GameObject activeWeakSpot;

    //Is there an active weakspot?
    private bool ActiveWeakSpot = false;

    //Timer before a new weakspot can appear
    public float WeakSpotTimer = 2.5f;

    void Start()
    {
        //Find the other scripts
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemy = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
    }

    void Update()
    {
        //Can a weakspot spawn?
        if(WeakSpotTimer <= 0 && GM.UpgradeTier[3] > Random.Range(0, 51) && ActiveWeakSpot != true)
        {
            //There is an active weakspot
            ActiveWeakSpot = true;
            //Generate a spawn point
            Vector3 randSpawn = new Vector3(Random.Range(-175, 200), Random.Range(-300, 36), 0);
            //Generate a weakspot
            activeWeakSpot = Instantiate(WeakSpotPrefab, Vector3.zero, Quaternion.Euler(0,0,0));
            //Set the enemy as the parent of the weakspot
            activeWeakSpot.transform.SetParent(enemy.transform);
            //Randomly set the weakspot's position within that enemy's hitbox
            activeWeakSpot.transform.localPosition = randSpawn;
        }
        else if(WeakSpotTimer <= 0 && GM.UpgradeTier[3] < Random.Range(0, 101) && ActiveWeakSpot != true)
        {
            WeakSpotTimer = 2.5f;
        }
        else if (WeakSpotTimer > 0 && ActiveWeakSpot != true)
        {
            //If there isn't a weakspot lower the time before a new weakspot can potentially spawn
            WeakSpotTimer -= Time.deltaTime;
        }
    }

    public void CriticalHit()
    {
        //Destroy the prefab
        Destroy(activeWeakSpot);
        //Set time to max
        WeakSpotTimer = 2.5f;
        //Run damage numbers
        float damageValue = (GM.UpgradeTier[0] + 1) * (10 * (GM.UpgradeTier[4] + 1));
        enemy.CurrentHP -= damageValue;
        //Award gold
        GM.AddGold(damageValue);
        //No active weakspot
        ActiveWeakSpot = false;
    }

}
