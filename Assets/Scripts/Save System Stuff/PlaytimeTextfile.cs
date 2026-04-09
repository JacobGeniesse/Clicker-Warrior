using UnityEngine;
using System.IO;

public class PlaytimeTextfile : MonoBehaviour
{
    private float[] Uptime = new float[4];
    private string filePath;
    private void Start()
    {
        filePath = Application.persistentDataPath + "/playtime.txt";
        if (File.Exists(filePath) == false)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("Playtime Tracker\n");
            }
        }
    }

    private void Update()
    {
        Uptime[0] += 1;
        if (Uptime[0] > 60)
        {
            Uptime[0] -= 60;
            Uptime[1] += 1;
        }
        if(Uptime[1] > 60)
        {
            Uptime[1] -= 60;
            Uptime[2] += 1;
        }
    }

    private void OnApplicationQuit()
    {
        if (File.Exists(filePath))
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine("Playtime this session: " + Uptime[3].ToString() + ":" + Uptime[2].ToString() + ":" + Uptime[1].ToString() + ":" + Uptime[0].ToString());
            }
        }
    }
}
