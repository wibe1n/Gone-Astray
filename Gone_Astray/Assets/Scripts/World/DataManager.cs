using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public enum DataManagerDictionaryType {
    itemDesc, story, npc, tutorial
};

public static class DataManager {

    static private bool readBool = false;
    

    static List<Dictionary<string, string>> configDatas = new List<Dictionary<string, string>> { };

    public static List<string> nameListGeneric = new List<string> { };
    public static List<string> descriptionListGeneric = new List<string> { };

    //Ladataan tekstidata ja tarkistetaan että data löytyy, lähetetään virheviesti jos tyhjää

    static public string ReadDataString(string entryName) {
        if (!readBool) {
            DownloadTextData();
        }
        string[] splitted = entryName.Split("_".ToCharArray());
        string identifier = splitted[0];

        try {
            DataManagerDictionaryType dicType = (DataManagerDictionaryType)System.Enum.Parse(typeof(DataManagerDictionaryType), identifier);
            try {
                return configDatas[System.Convert.ToInt32(dicType)][entryName];
            }
            catch {
                return null;
            }
        }
        catch {
            if (identifier != "nonexistent") {
                Debug.LogError("Identifier part is not defined in DataManager: " + identifier);
            }
            return null;
        }
    }

    static private void DownloadTextData() {
        //Luodaan jokaista tiedostoa kohti dictionary ja ladataan tiedostot yksitellen. Sen jälkeen lähetetään tiedostot Containeriin
        for (int i = 0; i < System.Enum.GetNames(typeof(DataManagerDictionaryType)).Length; i++) {
            configDatas.Add(new Dictionary<string, string>());
        }
        DownloadSingleFile("StoryConfig", configDatas[(int)DataManagerDictionaryType.story], nameListGeneric);
        DownloadSingleFile("ItemDescConfig", configDatas[(int)DataManagerDictionaryType.itemDesc], nameListGeneric);
        DownloadSingleFile("npcConfig", configDatas[(int)DataManagerDictionaryType.npc], nameListGeneric);
        DownloadSingleFile("tutorialConfig", configDatas[(int)DataManagerDictionaryType.tutorial], nameListGeneric);
        readBool = true;

        NameDescContainer.GenerateNames(nameListGeneric, descriptionListGeneric); // Must be before any other stuffgeneration
    }

    static private void DownloadSingleFile(string filename, Dictionary<string, string> dic, List<string> namelist) {
        //Otetaan datareitti yksitellen, jos ei löydy niin virheviesti
        //Parsetaan data niin että % merkki aloittaa uuden dictionary avaimen ja & merkki on sen sisältö # on kommentteja eli ne säilötään irrallisina

        string path = "Texts/" + filename;
        TextAsset fullData = Resources.Load(path) as TextAsset;
        if (fullData == null) {
            Debug.LogError("FILE: " + path + " NOT FOUND!");
        }
        string[] data = fullData.text.Split("\r\n".ToCharArray());
        foreach (string line in data) {
            if (line.Length > 0) {
                if (line[0] == "&"[0]) {
                    descriptionListGeneric.Add(line.Substring(1));
                }
                else if (line[0] == "%"[0]) {
                    namelist.Add(line.Substring(1));
                }
                else if (line[0] != "#"[0]) {
                    string[] keyValue = line.Split("="[0]);
                    dic.Add(keyValue[0], keyValue[1]);
                }
            }
        }
    }



}
