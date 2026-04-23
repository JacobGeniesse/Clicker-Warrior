using UnityEngine;
using UnityEngine.UI;

public class WeakSpot : MonoBehaviour
{
    //Other Scripts
    private GameManager GM;
    private UpgradeManager UM;
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
        UM = GM.UM;
        enemy = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
    }

    void Update()
    {
        //Can a weakspot spawn?
        if(WeakSpotTimer <= 0 && UM.Upgrades[2].UpgradeTier > Random.Range(0, 51) && ActiveWeakSpot != true)
        {
            //There is an active weakspot
            ActiveWeakSpot = true;
            //Generate a spawn point
            Vector3 randSpawn = new Vector3(Random.Range(-200, 150), Random.Range(-200, 125), 0);
            //Generate a weakspot
            activeWeakSpot = Instantiate(WeakSpotPrefab, Vector3.zero, Quaternion.Euler(0,0,0));
            //Set the enemy as the parent of the weakspot
            activeWeakSpot.transform.SetParent(enemy.transform);
            //Randomly set the weakspot's position within that enemy's hitbox
            activeWeakSpot.transform.localPosition = randSpawn;
        }
        else if(WeakSpotTimer <= 0 && UM.Upgrades[2].UpgradeTier < Random.Range(0, 101) && ActiveWeakSpot != true)
        {
            WeakSpotTimer = 2.5f;
        }
        else if (WeakSpotTimer > 0 && ActiveWeakSpot != true)
        {
            //If there isn't a weakspot lower the time before a new weakspot can potentially spawn
            WeakSpotTimer -= Time.deltaTime;
        }
    }

    public void ResetWeakSpot() //For use when player dies
    {
        Destroy(activeWeakSpot);
        WeakSpotTimer = 2.5f;
        ActiveWeakSpot = false;
    }

    public void CriticalHit()
    {
        //Destroy the prefab
        Destroy(activeWeakSpot);
        //Set time to max
        WeakSpotTimer = 2.5f;
        //Run damage numbers
        float damageValue = (UM.Upgrades[0].UpgradeTier + 1) * (10 * (UM.Upgrades[3].UpgradeTier + 1));
        enemy.TakeDamage(damageValue);
        //Award gold
        GM.AddGold(damageValue);
        //No active weakspot
        ActiveWeakSpot = false;
    }

}
