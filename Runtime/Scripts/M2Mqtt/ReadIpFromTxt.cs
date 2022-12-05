using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadIpFromTxt : MonoBehaviour
{
    public string ReadString()
    {
        string path = Application.persistentDataPath + "/BrokerIp.txt";

        string tmpIp = "";

        //Read the text from directly from the test.txt file
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            tmpIp = reader.ReadToEnd();
            Debug.Log("Broker IP in Conf: " + tmpIp);
            reader.Close();
        } else
        {
            Debug.Log("Keine Broker IP Config gefunden, standard IP wird verwendet");
        }

        return tmpIp;
    }
}
