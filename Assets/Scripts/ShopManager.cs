using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //Reference to game manager
    private GameManager GM;

    public UpgradeManager UM;

    //Array for storing text that displays cost
    public TMP_Text[] CostDisplay;

    // Buttons for buying things, to be disabled if insufficent funds or too many purchased
    public Button[] UpgradeButtons;

    void OnEnable()
    {   //Define gamemanager
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UM = GM.UM;


        //Set cost for Standard Shop
        for(int i = 0; i < 6; i++)
        {
            CostDisplay[i].text = "Cost: " + UM.UpgradeCost[i] + " Gold";
        }

        //Set cost for bonus shop
        for(int i = 6; i < CostDisplay.Length; i++)
        {
            CostDisplay[i].text = "Cost: " + UM.UpgradeCost[i] + " Rubies";
        }
    }

    public void CheckForInvalidButton()
    {
        //Check if the player has insufficent funds for anything, and disable the button if that is the case
        for(int i = 0; i < 6; i++)
        {
            if (GM.Resources.Currency["Gold"] < UM.UpgradeCost[i])
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
            if (GM.Resources.Currency["Ruby"] < UM.UpgradeCost[i])
            {
                UpgradeButtons[i].interactable = false;
            }
            else
            {
                UpgradeButtons[i].interactable = true;
            }
        }

        //Special Cases with purchase limits where disabling is required
        if(GM.UM.UpgradeTier[3] >= 50)
        {
            UpgradeButtons[3].interactable = false;
        }

        if(GM.UM.UpgradeTier[9] >= 1)
        {
            UpgradeButtons[9].interactable = false;
        }
        if(GM.UM.UpgradeTier[10] >= 1)
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
        if (GM.Resources.Currency["Gold"] >= UM.UpgradeCost[0])
        {
            GM.Resources.Currency["Gold"] -= UM.UpgradeCost[0];
            CalculateCost(0, 0);
            UM.UpgradeTier[0] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade the magic stopwatch
    public void UpgradeStopwatch()
    {
        if (GM.Resources.Currency["Gold"] >= UM.UpgradeCost[1])
        {
            GM.Resources.Currency["Gold"] -= UM.UpgradeCost[1];
            CalculateCost(1, 1);
            UM.UpgradeTier[1] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade gold charm
    public void UpgradeGoldCharm()
    {
        if (GM.Resources.Currency["Gold"] >= UM.UpgradeCost[2])
        {
            GM.Resources.Currency["Gold"] -= UM.UpgradeCost[2];
            CalculateCost(2, 2);
            UM.UpgradeTier[2] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade training manual
    public void UpgradeManual()
    {
        if (GM.Resources.Currency["Gold"] >= UM.UpgradeCost[3])
        {
            GM.Resources.Currency["Gold"] -= UM.UpgradeCost[3];
            CalculateCost(3, 3);
            UM.UpgradeTier[3] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade assassin's lens
    public void UpgradeLens()
    {
        if (GM.Resources.Currency["Gold"] >= UM.UpgradeCost[4])
        {
            GM.Resources.Currency["Gold"] -= UM.UpgradeCost[4];
            CalculateCost(4, 4);
            UM.UpgradeTier[4] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade Crude golem
    public void UpgradeGolem()
    {
        if (GM.Resources.Currency["Gold"] >= UM.UpgradeCost[5])
        {
            GM.Resources.Currency["Gold"] -= UM.UpgradeCost[5];
            CalculateCost(5, 5);
            UM.UpgradeTier[5] += 1;
            CheckForInvalidButton();
        }
    }

    //Buy gold for rubies
    public void BuyGold()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.UpgradeCost[6])
        {
            GM.Resources.Currency["Ruby"] -= UM.UpgradeCost[6];
            GM.Resources.Currency["Gold"] += 500;
            CheckForInvalidButton();
        }
    }

    //Upgrade ruby amulet
    public void UpgradeAmulet()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.UpgradeCost[7])
        {
            GM.Resources.Currency["Ruby"] -= UM.UpgradeCost[7];
            CalculateCost(7, 7);
            UM.UpgradeTier[7] += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade Robo-Hero
    public void UpgradeRobot()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.UpgradeCost[8])
        {
            GM.Resources.Currency["Ruby"] -= UM.UpgradeCost[8];
            CalculateCost(8, 8);
            UM.UpgradeTier[8] += 1;
            CheckForInvalidButton();
        }
    }

    //Buy NG+ Voucher
    public void BuyVoucher()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.UpgradeCost[9])
        {
            GM.Resources.Currency["Ruby"] -= UM.UpgradeCost[9];
            CalculateCost(9, 9);
            UM.UpgradeTier[9] += 1;
            CheckForInvalidButton();
        }
    }

    //Buy the plushie
    public void BuyPlushie()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.UpgradeCost[10])
        {
            GM.Resources.Currency["Ruby"] -= UM.UpgradeCost[10];
            GM.AwakenPlushie();
            UM.UpgradeTier[10] += 1;
            CheckForInvalidButton();
        }
    }

    //Calculate the new cost to buy post-upgrade
    private void CalculateCost(int UpgradeType, int UpgradeTier)
    {
        UM.UpgradeCost[UpgradeType] = Mathf.Ceil((UM.UpgradeTier[UpgradeTier] + 2) * UM.UpgradeCost[UpgradeType] - UM.UpgradeCost[UpgradeType]/2);
        if(UpgradeType < 6)
        {
            CostDisplay[UpgradeType].text = "Cost: " + UM.UpgradeCost[UpgradeType] + " Gold";
        }
        else
        {
            CostDisplay[UpgradeType].text = "Cost: " + UM.UpgradeCost[UpgradeType] + " Rubies";
        }
    }


    //Function for resetting the cost when the game restarts
    public void ResetCosts()
    {
        UM.UpgradeCost[0] = 10;
        UM.UpgradeCost[1] = 15;
        UM.UpgradeCost[2] = 40;
        UM.UpgradeCost[3] = 15;
        UM.UpgradeCost[4] = 25;
        UM.UpgradeCost[5] = 30;
        UM.UpgradeCost[6] = 1;
        UM.UpgradeCost[7] = 3;
        UM.UpgradeCost[8] = 5;
        UM.UpgradeCost[9] = 25;
        UM.UpgradeCost[10] = 9999;
    }
}
