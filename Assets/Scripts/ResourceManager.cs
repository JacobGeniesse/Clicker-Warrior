using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{

    public Dictionary<string, float> Currency = new Dictionary<string, float>()
    {
        {"Gold", 0},
        {"Ruby", 0}
    };
}

public class Gold
{
    public float gold = 0;
}

public class Ruby
{
    public float ruby = 0;
}
