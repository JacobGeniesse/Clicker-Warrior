using UnityEngine;
using System;
using System.IO;
using System.Text;

public static class SaveSystem
{
    public static void SaveGame(GameManager GM, EnemyHealth HP) //Func for saving game
    {
        string filePath = Application.persistentDataPath + "/SaveData.txt"; //File path
        SaveData SD = new SaveData(GM, HP); //Create a class of SaveData
        string DataToSave = JsonUtility.ToJson(SD); //Convert it to a json format
        using (StreamWriter stream = File.CreateText(filePath))
        {
            stream.WriteLine(DataToSave); // Write the data to the savefile
        }
    }

}

[Serializable]
public class SaveData //Class with all of the data to save
{
    [SerializeField] public UpgradeState[] UM;
    [SerializeField] public float[] Gold;
    [SerializeField] public float[] Rubies;
    [SerializeField] public float TimerValue;
    [SerializeField] public float[] CurrentHP;
    [SerializeField] public int Wave;

    public SaveData(GameManager GM, EnemyHealth HP) //Method to put parameters for what needs to be grabbed
    {
        UM = GM.UM.Upgrades;
        Gold = GM.Resources.Currency["Gold"];
        Rubies = GM.Resources.Currency["Ruby"];
        TimerValue = GM.TimerValue;
        CurrentHP = HP.CurrentHP;
        Wave = GM.wave;

    }
}
