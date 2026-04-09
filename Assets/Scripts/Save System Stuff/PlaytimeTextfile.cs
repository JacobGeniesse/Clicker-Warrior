using UnityEngine;
using System.IO;

public class PlaytimeTextfile : MonoBehaviour
{
    private float[] Uptime = new float[4]; //Number blocks representing miliseconds, seconds, minutes, hours
    private string filePath; //Filepath to write to
    private void Start()
    {
        filePath = Application.persistentDataPath + "/playtime.txt"; //File's name
        if (File.Exists(filePath) == false) //If the file doesn't already exist on the PC
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("Playtime Tracker\n"); //Create a file with the header of Playtime tracker
            }
        }
    }

    private void Update()
    {
        //Stuff for the playtime timer
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
        if (Uptime[2] > 60)
        {
            Uptime[2] -= 60;
            Uptime[3] += 1;
        }
    }

    /*
     * Upon quitting the game, append the playtime this session to the document from earlier
     */

    private void OnApplicationQuit()
    {
        if (File.Exists(filePath))
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                //Writes playtime in this format: Hours:Minutes:Seconds:Miliseconds
                sw.WriteLine("Playtime this session: " + Uptime[3].ToString() + ":" + Uptime[2].ToString() + ":" + Uptime[1].ToString() + ":" + Uptime[0].ToString());
            }
        }
    }
}
