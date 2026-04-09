using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class BaseCostXML : MonoBehaviour
{
    public static BaseCostXML Instance;

    private string filePath; //filepath to wrtie to
    private UpgradeManager UM; //Upgrade manager reference
    private List<float[]> BaseCosts = new List<float[]> //Base costs to write to a file if the file doesn't exist
    {
        new float[4] { 10, 0, 0, 0}, //SwordReforge
        new float[4] { 15, 0, 0,0}, //Magic Stopwatch
        new float[4] { 15, 0, 0, 0}, //Training Manual
        new float[4] { 25, 0, 0, 0}, //Assasin's Lens
        new float[4] { 35, 0, 0, 0}, //Crude golem
        new float[4] { 40, 0, 0, 0}, //Gold charm

        new float[4] { 1, 0, 0, 0}, //Gold
        new float[4] { 3, 0, 0, 0}, //Ruby Amulet
        new float[4] { 5, 0, 0, 0}, //Robo-Hero
        new float[4] { 25, 0, 0, 0}, //NG+ Voucher
        new float[4] { 999, 9, 0, 0 } //Commemorative Plushie
    };

    public List<float[]> CashNeeded = new List<float[]>(); //List to store the float arrays after reading the file


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Run This Method Once
    void Start()
    {
        filePath = Application.persistentDataPath + "/baseCost.xml"; //Assign filepath
        UM = GameObject.Find("GameManager").GetComponent<GameManager>().UM; //Find upgrademanager

        //Run these functions
        SerializeFiles(filePath); 
        DeserializeFiles(filePath);

        if(CashNeeded.Count == UM.Upgrades.Length) //Check if the files were loaded correctly
        {
            for(int i = 0; i < CashNeeded.Count; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    //Assign base cost and current cost based on what's pulled from the XML File
                    UM.Upgrades[i].UpgradeCostOriginal[j] = CashNeeded[i][j];
                    UM.Upgrades[i].UpgradeCostCurrent[j] = CashNeeded[i][j];
                    //Debug Message since save system makes this difficult to spot
                    //Debug.Log("XML File Loaded: " + UM.Upgrades[i].UpgradeCostOriginal[j] + "Into Current Cost and Original Cost");
                }
            }
        }
    }

    void SerializeFiles(string filePath)
    {
        var xmlserializer = new XmlSerializer(typeof(List<float[]>));
        //If the file doesn't exist make one with the values stored in this file.
        if (!File.Exists(filePath))
        {
            using (FileStream fs = File.Create(filePath))
            {
                xmlserializer.Serialize(fs, BaseCosts);
            }
        }
    }

    void DeserializeFiles(string filePath)
    {
        var xmlserializer = new XmlSerializer(typeof(List<float[]>));
        //if the file with base cost values exists pull from it and add those values to the Lsit
        if (File.Exists(filePath))
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                var Costs = (List<float[]>)xmlserializer.Deserialize(fs);
                foreach (float[] Block in Costs)
                {
                    Instance.CashNeeded.Add(Block);
                }
            }
        }
        else
        {
            Debug.LogError("File Not Found!");
        }
    }
}
