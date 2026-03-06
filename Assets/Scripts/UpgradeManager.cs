using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager
{
    public Dictionary<string, int> Upgrades = new Dictionary<string, int>()
    {
        //Guide to Keys: { Upgrade Name, Upgrade Tier }

        //Gold Upgrades
        { "Sword Reforge", 0 },
        { "Magic Stopwatch", 0 },
        { "Gold Charm", 0 },
        { "Training Manual", 0 },
        { "Assasin's Lens", 0 },
        { "Crude Golem", 0 },

        //Ruby Upgrades
        { "Gold", 0 },
        { "Ruby Amulet", 0 },
        { "Robo-Hero", 0 },
        { "New Game+ Voucher", 0 },
        { "Commerorative Plush", 0 }
    };
}
