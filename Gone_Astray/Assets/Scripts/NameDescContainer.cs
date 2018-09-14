using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum NameType {
    Item, Story
};

public static class NameDescContainer {
    private static bool genBool = false;


    static List<List<string>> names = new List<List<string>> { };
    static List<List<string>> descriptions = new List<List<string>> { };

    public static void GenerateNames(List<string> namelist, List<string> descList)
    {
        if (!genBool)
        {
            for (int i = 0; i < System.Enum.GetNames(typeof(NameType)).Length; i++)
            {
                PopulateListList(names);
                PopulateListList(descriptions);
            }
            genBool = true;
            foreach (string str in namelist)
            {
                string[] splitStr = str.Split("_".ToCharArray());
                NameType nametype = (NameType)System.Enum.Parse(typeof(NameType), splitStr[1]);
                int nameIndex = -1;
                if (int.TryParse(splitStr[2], out nameIndex))
                {
                }
                names[Convert.ToInt32(nametype)][nameIndex] = splitStr[3];
            }

            foreach (string str in descList)
            {
                string[] splitStr = str.Split("_".ToCharArray());
                NameType nametype = (NameType)System.Enum.Parse(typeof(NameType), splitStr[1]);
                int descIndex = -1;
                if (int.TryParse(splitStr[2], out descIndex))
                {
                }
                descriptions[Convert.ToInt32(nametype)][descIndex] = splitStr[3];
            }
        }
        else
        {
            Debug.LogError("GenerateNames called twice or more");
        }
    }

    private static void PopulateListList(List<List<string>> list)
    {
        List<string> tempList = new List<string> { };
        for (int i = 0; i < 100; i++)
        {
            tempList.Add("default");
        }
        list.Add(tempList);
    }

    public static string GetName(NameType subtype, int index)
    {
        return names[Convert.ToInt32(subtype)][index];
    }

    public static string GetDescription(NameType subtype, int index)
    {
        return descriptions[Convert.ToInt32(subtype)][index];
    }


}
