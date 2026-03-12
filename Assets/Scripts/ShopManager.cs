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
            CostDisplay[i].text = "Cost: " + UM.Upgrades[i].UpgradeCostCurrent + " Gold";
        }

        //Set cost for bonus shop
        for(int i = 6; i < CostDisplay.Length; i++)
        {
            CostDisplay[i].text = "Cost: " + UM.Upgrades[i].UpgradeCostCurrent + " Rubies";
        }
    }

    public void CheckForInvalidButton()
    {
        //Special Cases with purchase limits where disabling is required
        if (UM.Upgrades[2].UpgradeTier >= 50)
        {
            UM.Upgrades[2].Availabilitiy = UpgradeState.PurchaseState.Purchased;
        }

        if (UM.Upgrades[9].UpgradeTier >= 1)
        {
            UM.Upgrades[9].Availabilitiy = UpgradeState.PurchaseState.Purchased;
        }
        if (UM.Upgrades[10].UpgradeTier >= 1)
        {
            UM.Upgrades[10].Availabilitiy = UpgradeState.PurchaseState.Purchased;
        }


        //Check if the player has insufficent funds for anything, and disable the button if that is the case
        for (int i = 0; i < 6; i++)
        {
            if (GM.Resources.Currency["Gold"] < UM.Upgrades[i].UpgradeCostCurrent || UM.Upgrades[i].Availabilitiy != UpgradeState.PurchaseState.Available)
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
            if (GM.Resources.Currency["Ruby"] < UM.Upgrades[i].UpgradeCostCurrent || UM.Upgrades[i].Availabilitiy != UpgradeState.PurchaseState.Available)
            {
                UpgradeButtons[i].interactable = false;
            }
            else
            {
                UpgradeButtons[i].interactable = true;
            }
        }
    }


    /*Functions for upgrading various Upgrade items
     * Done via individuals functions so that way it would work with unity's button system.
     * That system doesn't like Parameters so this is necessary unfortunately.
     */

    //Upgrade the sword
    public void UpgradeSword()
    {
        if (GM.Resources.Currency["Gold"] >= UM.Upgrades[0].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Gold"] -= UM.Upgrades[0].UpgradeCostCurrent;
            CalculateCost(0);
            UM.Upgrades[0].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade the magic stopwatch
    public void UpgradeStopwatch()
    {
        if (GM.Resources.Currency["Gold"] >= UM.Upgrades[1].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Gold"] -= UM.Upgrades[1].UpgradeCostCurrent;
            CalculateCost(1);
            UM.Upgrades[1].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade training manual
    public void UpgradeManual()
    {
        if (GM.Resources.Currency["Gold"] >= UM.Upgrades[2].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Gold"] -= UM.Upgrades[2].UpgradeCostCurrent;
            CalculateCost(2);
            UM.Upgrades[2].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade assassin's lens
    public void UpgradeLens()
    {
        if (GM.Resources.Currency["Gold"] >= UM.Upgrades[3].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Gold"] -= UM.Upgrades[3].UpgradeCostCurrent;
            CalculateCost(3);
            UM.Upgrades[3].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade Crude golem
    public void UpgradeGolem()
    {
        if (GM.Resources.Currency["Gold"] >= UM.Upgrades[4].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Gold"] -= UM.Upgrades[4].UpgradeCostCurrent;
            CalculateCost(4);
            UM.Upgrades[4].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade gold charm
    public void UpgradeGoldCharm()
    {
        if (GM.Resources.Currency["Gold"] >= UM.Upgrades[5].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Gold"] -= UM.Upgrades[5].UpgradeCostCurrent;
            CalculateCost(5);
            UM.Upgrades[5].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Buy gold for rubies
    public void BuyGold()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.Upgrades[6].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Ruby"] -= UM.Upgrades[6].UpgradeCostCurrent;
            GM.Resources.Currency["Gold"] += 500;
            CheckForInvalidButton();
        }
    }

    //Upgrade ruby amulet
    public void UpgradeAmulet()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.Upgrades[7].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Ruby"] -= UM.Upgrades[7].UpgradeCostCurrent;
            CalculateCost(7);
            UM.Upgrades[7].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade Robo-Hero
    public void UpgradeRobot()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.Upgrades[8].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Ruby"] -= UM.Upgrades[8].UpgradeCostCurrent;
            CalculateCost(8);
            UM.Upgrades[8].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Buy NG+ Voucher
    public void BuyVoucher()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.Upgrades[9].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Ruby"] -= UM.Upgrades[9].UpgradeCostCurrent;
            CalculateCost(9);
            UM.Upgrades[9].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Buy the plushie
    public void BuyPlushie()
    {
        if (GM.Resources.Currency["Ruby"] >= UM.Upgrades[10].UpgradeCostCurrent)
        {
            GM.Resources.Currency["Ruby"] -= UM.Upgrades[10].UpgradeCostCurrent;
            GM.PlushieStatus(true);
            UM.Upgrades[10].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Calculate the new cost to buy post-upgrade
    private void CalculateCost(int UpgradeType)
    {
        UM.Upgrades[UpgradeType].UpgradeCostCurrent = Mathf.Ceil((UM.Upgrades[UpgradeType].UpgradeTier + 2) * UM.Upgrades[UpgradeType].UpgradeCostCurrent - UM.Upgrades[UpgradeType].UpgradeCostCurrent / 2);
        if(UpgradeType < 6)
        {
            CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent + " Gold";
        }
        else
        {
            CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent + " Rubies";
        }
    }
}
