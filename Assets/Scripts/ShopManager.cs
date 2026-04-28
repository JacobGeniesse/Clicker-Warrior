using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NumberControlExtension;

public class ShopManager : MonoBehaviour
{
    //Reference to game manager
    private GameManager GM;

    private GoldGenerator GoldGen;
    private RubyGenerator RubyGen;

    public UpgradeManager UM;

    //Array for storing text that displays cost
    public TMP_Text[] CostDisplay;

    // Buttons for buying things, to be disabled if insufficent funds or too many purchased
    public Button[] UpgradeButtons;

    private bool successfulTransaction;

    void OnEnable()
    {   //Define gamemanager
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UM = GM.UM;
        GoldGen = GM.GoldGen;
        RubyGen = GM.RubyGen;

        //Set cost for Standard Shop
        for(int i = 0; i < 6; i++)
        {
            UpdateText(true, i, ref GM.Resources);
        }

        //Set cost for bonus shop
        for (int i = 6; i < CostDisplay.Length; i++)
        {
            UpdateText(false, i, ref GM.Resources);
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
            if (GoldCheck(i, ref GM.Resources) == false || UM.Upgrades[i].Availabilitiy != UpgradeState.PurchaseState.Available)
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
            if (RubyCheck(i, ref GM.Resources) == false || UM.Upgrades[i].Availabilitiy != UpgradeState.PurchaseState.Available)
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
        if (GoldCheck(0, ref GM.Resources) == true)
        {
            TakeCash(0, ref GM.Resources);
            CalculateCost(0);
            UpdateText(true, 0, ref GM.Resources);
            UM.Upgrades[0].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade the magic stopwatch
    public void UpgradeStopwatch()
    {
        if (GoldCheck(1, ref GM.Resources) == true)
        {
            TakeCash(1, ref GM.Resources);
            CalculateCost(1);
            UpdateText(true, 1, ref GM.Resources);
            UM.Upgrades[1].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade training manual
    public void UpgradeManual()
    {
        if (GoldCheck(2, ref GM.Resources) == true)
        {
            TakeCash(2, ref GM.Resources);
            CalculateCost(2);
            UpdateText(true, 2, ref GM.Resources);
            UM.Upgrades[2].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade assassin's lens
    public void UpgradeLens()
    {
        if (GoldCheck(3, ref GM.Resources) == true)
        {
            TakeCash(3, ref GM.Resources);
            CalculateCost(3);
            UpdateText(true, 3, ref GM.Resources);
            UM.Upgrades[3].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade Crude golem
    public void UpgradeGolem()
    {
        if (GoldCheck(4, ref GM.Resources) == true)
        {
            TakeCash(4, ref GM.Resources);
            CalculateCost(4);
            UpdateText(true, 4, ref GM.Resources);
            UM.Upgrades[4].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade gold charm
    public void UpgradeGoldCharm()
    {
        if (GoldCheck(5, ref GM.Resources) == true)
        {
            TakeCash(5, ref GM.Resources);
            CalculateCost(5);
            UpdateText(true, 5, ref GM.Resources);
            UM.Upgrades[5].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Buy gold for rubies
    public void BuyGold()
    {
        if (RubyCheck(6, ref GM.Resources))
        {
            TakeCash(6, ref GM.Resources);
            UpdateText(false, 6, ref GM.Resources);
            GM.AddGold(500);
            CheckForInvalidButton();
        }
    }

    //Upgrade ruby amulet
    public void UpgradeAmulet()
    {
        if (RubyCheck(7, ref GM.Resources))
        {
            TakeCash(7, ref GM.Resources);
            CalculateCost(7);
            UpdateText(false, 7, ref GM.Resources);
            UM.Upgrades[7].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Upgrade Robo-Hero
    public void UpgradeRobot()
    {
        if (RubyCheck(8, ref GM.Resources))
        {
            TakeCash(8, ref GM.Resources);
            CalculateCost(8);
            UpdateText(false, 8, ref GM.Resources);
            UM.Upgrades[8].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Buy NG+ Voucher
    public void BuyVoucher()
    {
        if (RubyCheck(9, ref GM.Resources))
        {
            TakeCash(9, ref GM.Resources);
            CalculateCost(9);
            UpdateText(false, 9, ref GM.Resources);
            UM.Upgrades[9].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    //Buy the plushie
    public void BuyPlushie()
    {
        if (RubyCheck(10, ref GM.Resources))
        {
            TakeCash(10, ref GM.Resources);
            UpdateText(false, 9, ref GM.Resources);
            GM.PlushieStatus(true);
            UM.Upgrades[10].UpgradeTier += 1;
            CheckForInvalidButton();
        }
    }

    void TakeCash(int UpgradeType, ref ResourceManager currencyRef)
    {
        if(UpgradeType < 6)
        {
            currencyRef.Currency["Gold"].BlockSubtraction(UM.Upgrades[UpgradeType].UpgradeCostCurrent);
        }
        else
        {
            currencyRef.Currency["Ruby"].BlockSubtraction(UM.Upgrades[UpgradeType].UpgradeCostCurrent);
        }
    }

    //Calculate the new cost to buy post-upgrade
    private void CalculateCost(int UpgradeType)
    {
        UM.Upgrades[UpgradeType].UpgradeCostCurrent.BlockMultiplication(2);
        //UM.Upgrades[UpgradeType].UpgradeCostCurrent = Mathf.Ceil((UM.Upgrades[UpgradeType].UpgradeTier + 2) * UM.Upgrades[UpgradeType].UpgradeCostCurrent - UM.Upgrades[UpgradeType].UpgradeCostCurrent / 2);
    }

    private bool GoldCheck(int UpgradeType, ref ResourceManager CurrencyRef) //Check if the player has the appropriate ruby amount
    {
        int PurchaseStatus = CurrencyRef.Currency["Gold"].BlockGreater(UM.Upgrades[UpgradeType].UpgradeCostCurrent);

        if (PurchaseStatus == 1 || PurchaseStatus == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool RubyCheck(int UpgradeType, ref ResourceManager CurrencyRef) //Check if the player has the appropriate ruby count
    {
        int PurchaseStatus = CurrencyRef.Currency["Ruby"].BlockGreater(UM.Upgrades[UpgradeType].UpgradeCostCurrent);

        if(PurchaseStatus == 1 || PurchaseStatus == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateText(bool UsesGold, int UpgradeType, ref ResourceManager CurrencyRef) //Update  the text showing the upgrade cost
    {
        if(UM.Upgrades[UpgradeType].Availabilitiy == UpgradeState.PurchaseState.Purchased)
        {
            CostDisplay[UpgradeType].text = "Cost: SOLD OUT";
        }
        else
        {
            if (UsesGold == true) //For gold
            {
                if (UM.Upgrades[UpgradeType].UpgradeCostCurrent[3] > 0)
                {
                    CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent[3] + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[2].ToString("000") + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[1].ToString("000") + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[0].ToString("000") + " Gold";
                }
                else if (UM.Upgrades[UpgradeType].UpgradeCostCurrent[2] > 0 && UM.Upgrades[UpgradeType].UpgradeCostCurrent[3] == 0)
                {
                    CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent[2] + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[1].ToString("000") + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[0].ToString("000") + " Gold";
                }
                else if (UM.Upgrades[UpgradeType].UpgradeCostCurrent[1] > 0 && UM.Upgrades[UpgradeType].UpgradeCostCurrent[2] == 0)
                {
                    CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent[1] + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[0].ToString("000") + " Gold";
                }
                else
                {
                    CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent[0] + " Gold";
                }
            }
            else //For rubies
            {
                if (UM.Upgrades[UpgradeType].UpgradeCostCurrent[3] > 0)
                {
                    CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent[3] + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[2].ToString("000") + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[1].ToString("000") + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[0].ToString("000") + " Rubies";
                }
                else if (UM.Upgrades[UpgradeType].UpgradeCostCurrent[2] > 0 && UM.Upgrades[UpgradeType].UpgradeCostCurrent[3] == 0)
                {
                    CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent[2] + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[1].ToString("000") + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[0].ToString("000") + " Rubies";
                }
                else if (UM.Upgrades[UpgradeType].UpgradeCostCurrent[1] > 0 && UM.Upgrades[UpgradeType].UpgradeCostCurrent[2] == 0)
                {
                    CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent[1] + "," + UM.Upgrades[UpgradeType].UpgradeCostCurrent[0].ToString("000") + " Rubies";
                }
                else
                {
                    CostDisplay[UpgradeType].text = "Cost: " + UM.Upgrades[UpgradeType].UpgradeCostCurrent[0] + " Rubies";
                }
            }
        }
    }
}
