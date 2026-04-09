using UnityEngine;
using System.Xml;
using System.IO;

public class BaseCostXML : MonoBehaviour
{
    private string filePath;
    private UpgradeManager UM;
    private int index;

    // Run This Method Once
    void Start()
    {
        filePath = Application.persistentDataPath + "/baseCost.xml";
        UM = GameObject.Find("GameManager").GetComponent<GameManager>().UM;

        using(FileStream fs = File.Create(filePath)){
            XmlWriter xmlwriter = XmlWriter.Create(fs);

            xmlwriter.WriteStartElement("BaseCost");

            //SwordReforgeArray
            xmlwriter.WriteElementString("SwordReforge", 10.ToString());

            xmlwriter.WriteElementString("MagicStopwatch", 15.ToString());

            xmlwriter.WriteElementString("TrainingManual", 15.ToString());

            xmlwriter.WriteElementString("AssassinsLens", 25.ToString());

            xmlwriter.WriteElementString("CrudeGolem", 35.ToString());

            xmlwriter.WriteElementString("GoldCharm", 40.ToString());

            xmlwriter.WriteElementString("Gold", 1.ToString());

            xmlwriter.WriteElementString("RubyAmulet", 3.ToString());

            xmlwriter.WriteElementString("RoboHero", 5.ToString());

            xmlwriter.WriteElementString("NGVoucher", 25.ToString());

            xmlwriter.WriteElementString("CommemorativePlushie", 9999.ToString());

            xmlwriter.WriteEndElement();


            xmlwriter.Close();
        }
    }
}
