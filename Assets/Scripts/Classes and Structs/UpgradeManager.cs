using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager
{
    //Array with Struct for each upgrade
    public UpgradeState[] Upgrades = new UpgradeState[11]
    {
        //Format UpgradeState(name string, int upgradetier, float base upgradecost)
        new UpgradeState("Sword Reforge", 0, new float[4], new float[4]), // 0 = Sword Reforge
        new UpgradeState("Magic Stopwatch", 0, new float[4], new float[4]), // 1 = Magic Stopwatch
        new UpgradeState("Training Manual", 0, new float[4], new float[4]), // 2 = Training Manual
        new UpgradeState("Assassin's Lens", 0, new float[4], new float[4]), // 3 = Assassin's Lens
        new UpgradeState("Crude Golem", 0, new float[4], new float[4]), // 4 = Crude Golem
        new UpgradeState("Gold Charm", 0, new float[4], new float[4]), // 5 = Gold Charm

        new UpgradeState("Gold", 0, new float[4], new float[4]), // 6 = Gold
        new UpgradeState("Ruby Amulet", 0, new float[4], new float[4]), // 7 = Ruby Amulet
        new UpgradeState("Robo-Hero", 0, new float[4], new float[4]), // 8 = Robo-Hero
        new UpgradeState("New Game+ Voucher", 0, new float[4], new float[4]), // 9 = NG+ Voucher
        new UpgradeState("Commemorative Plushie", 0, new float[4], new float[4]) // 10 = Commermorative Plush
    };
}
