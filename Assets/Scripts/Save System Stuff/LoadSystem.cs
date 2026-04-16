using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LoadSystem
{
    public static SaveData LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/SaveData.txt"; //File path

        try
        {
            if (File.Exists(filePath))
            {
                using (StreamReader SR = new StreamReader(filePath))
                {
                    string jsonString = SR.ReadToEnd(); //Grab the json string from the file
                    SaveData CurrentSave = JsonUtility.FromJson<SaveData>(jsonString); //Convert from the json format back to SaveData's format
                    return CurrentSave; //Return that save data
                }
            }
            else
            {
                throw new ArgumentException("No save file found!");
            }
        }
        catch (ArgumentException argumentException)
        {
            Debug.LogWarning(argumentException.ToString());
            return null; //If there's no data to load return nothing
        }
    }
}
