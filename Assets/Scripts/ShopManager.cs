using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //Reference to game manager
    private GameManager GM;

    //Array for storing upgrade costs;
    private float[] UpgradeCost = new float[11]
    {
        10, //0 = Sword Reforge
        15, // 1 = Magic Stopwatch
        40, // 2 = Gold Charm
        15, // 3 = Training Manual
        25, // 4 = Assassin's Lens
        30, // 5 = Crude Golem

        1, // 6 = Gold
        3, // 7 = Ruby Amulet
        5, // 8 = Robo-Hero
        25, // 9 = Newgame+ Voucher
        9999, // 10 = Commemorative Plushy
    };

    //Array for storing text that displays cost
    public TMP_Text[] CostDisplay;

    // Buttons for buying things, to be disabled if insufficent funds or too many purchased
    public Button[] UpgradeButtons;

    void OnEnable()
    {   //Define gamemanager
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Set cost for Standard Shop
        for(int i = 0; i < 6; i++)
        {
            CostDisplay[i].text = "Cost: " + UpgradeCost[i] + " Gold";
        }

        //Set cost for bonus shop
        for(int i = 6; i < CostDisplay.Length; i++)
        {
            CostDisplay[i].text = "Cost: " + UpgradeCost[i] + " Rubies";
        }
    }

    public void CheckForInvalidButton()
    {
        //Check if the player has insufficent funds for anything, and disable the button if that is the case
        for(int i = 0; i < 6; i++)
        {
            if (GM.Resources.Currency["Gold"] < UpgradeCost[i])
            {
                UpgradeButtons[i].interactable = false;
            }
            else
            {
                UpgradeButtons[i].interactable = true;
            }
        }

        for (int i = 6; i < 11 && i > 5; i++)
        {
            if (GM.Resources.Currency["Ruby"] < UpgradeCost[i])
            {
                UpgradeButtons[i].interactable = false;
            }
            else
            {
                UpgradeButtons[i].interactable = true;
            }
        }

        //Special Cases with purchase limits where disabling is required
        if(GM.UpgradeTier[3] >= 50)
        {
            UpgradeButtons[3].interactable = false;
        }

        if(GM.UpgradeTier[9] >= 1)
        {
            UpgradeButtons[9].interactable = false;
        }
        if(GM.UpgradeTier[10] >= 1)
        {
            UpgradeButtons[10].interactable = false;
        }
    }


    /*Functions for upgrading various Upgrade items
     * Done via individuals functions so that way it would work with unity's button system.
     * That system doesn't like Parameters so this is necessary unfortunately.
     */

    //Upgrade the sword
    public void UpgradeSword()
    {
        if (GM.Resources.Currency["Gold"] >= UpgradeCost[0])
        {
            GM.Resources.Currency["Gold"] -= UpgradeCost[0];
            CalculateCost(0, 0);
            GM.UpgradeTier[0] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade the magic stopwatch
    public void UpgradeStopwatch()
    {
        if (GM.Resources.Currency["Gold"] >= UpgradeCost[1])
        {
            GM.Resources.Currency["Gold"] -= UpgradeCost[1];
            CalculateCost(1, 1);
            GM.UpgradeTier[1] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade gold charm
    public void UpgradeGoldCharm()
    {
        if (GM.Resources.Currency["Gold"] >= UpgradeCost[2])
        {
            GM.Resources.Currency["Gold"] -= UpgradeCost[2];
            CalculateCost(2, 2);
            GM.UpgradeTier[2] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade training manual
    public void UpgradeManual()
    {
        if (GM.Resources.Currency["Gold"] >= UpgradeCost[3])
        {
            GM.Resources.Currency["Gold"] -= UpgradeCost[3];
            CalculateCost(3, 3);
            GM.UpgradeTier[3] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade assassin's lens
    public void UpgradeLens()
    {
        if (GM.Resources.Currency["Gold"] >= UpgradeCost[4])
        {
            GM.Resources.Currency["Gold"] -= UpgradeCost[4];
            CalculateCost(4, 4);
            GM.UpgradeTier[4] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade Crude golem
    public void UpgradeGolem()
    {
        if (GM.Resources.Currency["Gold"] >= UpgradeCost[5])
        {
            GM.Resources.Currency["Gold"] -= UpgradeCost[5];
            CalculateCost(5, 5);
            GM.UpgradeTier[5] += 1;
            CheckForInvalidButton();
        }
    }

    //Buy gold for rubies
    public void BuyGold()
    {
        if (GM.Resources.Currency["Ruby"] >= UpgradeCost[6])
        {
            GM.Resources.Currency["Ruby"] -= UpgradeCost[6];
            GM.Resources.Currency["Gold"] += 500;
            CheckForInvalidButton();
        }
    }

    //Upgrade ruby amulet
    public void UpgradeAmulet()
    {
        if (GM.Resources.Currency["Ruby"] >= UpgradeCost[7])
        {
            GM.Resources.Currency["Ruby"] -= UpgradeCost[7];
            CalculateCost(7, 7);
            GM.UpgradeTier[7] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade Robo-Hero
    public void UpgradeRobot()
    {
        if (GM.Resources.Currency["Ruby"] >= UpgradeCost[8])
        {
            GM.Resources.Currency["Ruby"] -= UpgradeCost[8];
            CalculateCost(8, 8);
            GM.UpgradeTier[8] += 1;
            CheckForInvalidButton();
        }
    }

    //Buy NG+ Voucher
    public void BuyVoucher()
    {
        if (GM.Resources.Currency["Ruby"] >= UpgradeCost[9])
        {
            GM.Resources.Currency["Ruby"] -= UpgradeCost[9];
            CalculateCost(9, 9);
            GM.UpgradeTier[9] += 1;
            CheckForInvalidButton();
        }
    }

    //Buy the plushie
    public void BuyPlushie()
    {
        if (GM.Resources.Currency["Ruby"] >= UpgradeCost[10])
        {
            GM.Resources.Currency["Ruby"] -= UpgradeCost[10];
            GM.AwakenPlushie();
            GM.UpgradeTier[10] += 1;
            CheckForInvalidButton();
        }
    }

    //Calculate the new cost to buy post-upgrade
    private void CalculateCost(int UpgradeType, int UpgradeTier)
    {
        UpgradeCost[UpgradeType] = Mathf.Ceil((GM.UpgradeTier[UpgradeTier] + 2) * UpgradeCost[UpgradeType] - UpgradeCost[UpgradeType]/2);
        if(UpgradeType < 6)
        {
            CostDisplay[UpgradeType].text = "Cost: " + UpgradeCost[UpgradeType] + " Gold";
        }
        else
        {
            CostDisplay[UpgradeType].text = "Cost: " + UpgradeCost[UpgradeType] + " Rubies";
        }
    }


    //Function for resetting the cost when the game restarts
    public void ResetCosts()
    {
        UpgradeCost[0] = 10;
        UpgradeCost[1] = 15;
        UpgradeCost[2] = 40;
        UpgradeCost[3] = 15;
        UpgradeCost[4] = 25;
        UpgradeCost[5] = 30;
        UpgradeCost[6] = 1;
        UpgradeCost[7] = 3;
        UpgradeCost[8] = 5;
        UpgradeCost[9] = 25;
        UpgradeCost[10] = 9999;
    }
}
