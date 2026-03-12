using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager
{
    public string[] UpgradeName = new string[]
    {
        //Gold Upgrades
        "Sword Reforge",
        "Magic Stopwatch",
        "Gold Charm",
        "Training Manual",
        "Assassin's Lens",
        "Crude Golem",

        //Ruby Upgrades
        "Gold",
        "Ruby Amulet",
        "Robo-Hero",
        "New Game+ Voucher",
        "Commemorative Plushie"
    };


    public int[] UpgradeTier = new int[]
    {
        0, //Sword Reforge
        0, //"Magic Stopwatch"
        0, //"Gold Charm"
        0, //"Training Manual"
        0, //"Assassin's Lens"
        0, //"Crude Golem"

        //Ruby Upgrades
        0, //"Gold"
        0, //"Ruby Amulet"
        0, //RoboHero
        0, //"New Game+ Voucher"
        0, //Plushie
    };

    public float[] UpgradeCost = new float[11]
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

    //public UpgradeState[] Upgrades = new UpgradeState[11]
    //{
    //    Upgrades[0] = UpgradeState("Sword Reforge", 0, 10),
    //    Upgrades[1] = UpgradeState("Magic Stopwatch", 0, 15),
    //    Upgrades[2] = UpgradeState("Training Manual", 0, 15),
    //    Upgrades[3] = UpgradeState("Assassin's Lens", 0, 25),
    //    Upgrades[4] = UpgradeState("Crude Golem", 0, 30);
    //    Upgrades[5] = UpgradeState("Gold Charm", 0, 40);

    //    Upgrades[6] = UpgradeState("Gold", 0, 1);
    //    Upgrades[7] = UpgradeState("Ruby Amulet", 0, 3);
    //    Upgrades[8] = UpgradeState("Robo-Hero", 0, 5);
    //    Upgrades[9] = UpgradeState("New Game+ Voucher", 0, 25);
    //    Upgrades[10] = UpgradeState("Commemorative Plushie", 0, 9999);
    //};

    //public UpgradeState SwordState = new UpgradeState("Sword Reforge", 0, 10);
    //public UpgradeState WatchState = new UpgradeState("Magic Stopwatch", 0, 15);
    //public UpgradeState ManualState = new UpgradeState("Training Manual", 0, 15);
    //public UpgradeState LensState = new UpgradeState("Assassin's Lens", 0, 25);
    //public UpgradeState GolemState = new UpgradeState("Crude Golem", 0, 30);
    //public UpgradeState CharmState = new UpgradeState("Gold Charm", 0, 40);

    //public UpgradeState GoldState = new UpgradeState("Gold", 0, 1);
    //public UpgradeState AmuletState = new UpgradeState("Ruby Amulet", 0, 3);
    //public UpgradeState RobotState = new UpgradeState("Robo-Hero", 0, 5);
    //public UpgradeState VoucherState = new UpgradeState("New Game+ Voucher", 0, 25);
    //public UpgradeState PlushState = new UpgradeState("Commemorative Plushie", 0, 9999);
}
