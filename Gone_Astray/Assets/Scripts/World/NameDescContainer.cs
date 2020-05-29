using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Tekstityypit
public enum NameType {
    combatTutorial, item, chapter1, chapter2, chapter3, chapter4, npc1, npc2
};

//Tekstiosat
public enum ChapterParts
{
    part0, part1, part2, part3, part4, part5, part6, part7, part8, part9, part10, part11, part12, part13, part14, part15, part16, part17, part18, part19, part20, part21, part22, part23, part24, part25, part26, part27, part28, part29, part30,
    part31, part32, part33, part34, part35, part36, part37, part38, part39, part40, part41, part42, part43, part44, part45, part46, part47, part48, part49, part50, part51, part52, part53
};


public static class NameDescContainer {
    private static bool genBool = false;


    static List<List<string>> names = new List<List<string>> { };
    static List<List<string>> descriptions = new List<List<string>> { };


    //Jaotellaan saatu tekstidata oikeisiin tyyppeihin ja osioihin
    public static void GenerateNames(List<string> namelist, List<string> descList) {
        if (!genBool) {
            for (int i = 0; i < System.Enum.GetNames(typeof(NameType)).Length; i++) {
                PopulateListList(names);
                PopulateListList(descriptions);
            }
            genBool = true;
            //Jaotellaan Tekstin Indeksi tieto
            foreach (string str in namelist) {
                //alaviivan kohdalla katkaistaan saatu teksti
                string[] splitStr = str.Split("_".ToCharArray());
                // nimityyppi eli NameType on katkaistun tekstin toinen kohta
                NameType nametype = (NameType)System.Enum.Parse(typeof(NameType), splitStr[1]);
                int nameIndex = -1;
                if (int.TryParse(splitStr[2], out nameIndex))
                {
                }
                //tekstin osa eli "part" on katkaistun tekstin kolmas kohta
                else { 
                    nameIndex = (int)System.Enum.Parse(typeof(ChapterParts), splitStr[2]);
                }
                //tekstin indeksi on katkaistun tesktin neljäs kohta
                names[Convert.ToInt32(nametype)][nameIndex] = splitStr[3];
            }
            //Jaotellaan tekstin sisältö
            foreach (string str in descList) {
                string[] splitStr = str.Split("_".ToCharArray());
                // nimityyppi eli NameType on katkaistun tekstin toinen kohta
                NameType nametype = (NameType)System.Enum.Parse(typeof(NameType), splitStr[1]);
                int descIndex = -1;
                //tekstin osa eli "part" on katkaistun tekstin kolmas kohta
                if (int.TryParse(splitStr[2], out descIndex)) {
                }
                else { 
                    descIndex = (int)System.Enum.Parse(typeof(ChapterParts), splitStr[2]);
                }
                //tesktisisältö on katkaistun tekstin neljäs kohta
                descriptions[Convert.ToInt32(nametype)][descIndex] = splitStr[3];
            }
        }
        else {
            Debug.LogError("GenerateNames called twice or more");
        }
    }

    private static void PopulateListList(List<List<string>> list) {
        List<string> tempList = new List<string> { };
        for (int i = 0; i < 100; i++) {
            tempList.Add("default");
        }
        list.Add(tempList);
    }

    public static string GetName(NameType subtype, int index) {
        return names[Convert.ToInt32(subtype)][index];
    }

    //Hae data omasta lokerostaan
    public static string GetDescription(NameType subtype, int index) {
        return descriptions[Convert.ToInt32(subtype)][index];
    }

    public static string GetChapterPart(string part, NameType chapter) {
        return descriptions[Convert.ToInt32(chapter)][Convert.ToInt32((ChapterParts)System.Enum.Parse(typeof(ChapterParts), part))];
    }

    public static string GetSpeechBubble(string part, NameType npc) {
        return descriptions[Convert.ToInt32(npc)][Convert.ToInt32((ChapterParts)System.Enum.Parse(typeof(ChapterParts), part))];
    }

    public static string GetCombatTutorialPart (string part) {
        return descriptions[Convert.ToInt32(NameType.combatTutorial)][Convert.ToInt32((ChapterParts)System.Enum.Parse(typeof(ChapterParts), part))];
    }

}
