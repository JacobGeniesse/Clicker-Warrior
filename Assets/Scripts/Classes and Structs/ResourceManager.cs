using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{

    public Dictionary<string, float[]> Currency = new Dictionary<string, float[]>()
    {
        {"Gold", new float[4] {0, 0, 0, 0}},

        {"Ruby", new float[4] {0, 0, 0, 0}}
    };
}