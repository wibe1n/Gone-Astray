using UnityEngine;
using System.Collections.Generic;

//copypastaa youtube tutorialista https://www.youtube.com/watch?v=51y8kU_nEvc
//lisää tallennettavia muuttujia saa lisättyä laittamalla ne publiceina "//serialized"in alle

public class SaveGame
{
	//serialized
	public Vector3 playerPosition = Vector3.zero;
	public List<Firefly> fireflies;
	public List<Firefly> fiaFamily;
    public int level;

	private static string _gameDataFileName = "data.json";
	private static SaveGame _instance;
	public static SaveGame Instance
	{
		get
		{
			if (_instance == null)
				Load();
			return _instance;
		}

	}

	public static void Save()
	{
		FileManager.Save(_gameDataFileName, _instance);
	}

	public static void Load()
	{
		_instance = FileManager.Load<SaveGame>(_gameDataFileName);
	}
}