using UnityEngine;
using System.IO;

//copypastaa youtube tutorialista https://www.youtube.com/watch?v=51y8kU_nEvc
//älä koske ellet tiedä ihan varmasti mitä teet

public class FileManager
{
	/// <summary>
	/// Load File
	/// </summary>
	/// <typeparam name="T">Data Model Type</typeparam>
	/// <param name="filename">File Name</param>
	/// <returns>Instance</returns>
	public static T Load<T>(string filename) where T : new()
	{
		string filePath = Path.Combine(Application.persistentDataPath, filename);
		T output;

		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			output = JsonUtility.FromJson<T>(dataAsJson);
		}
		else
		{
			output = new T();
		}

		return output;
	}

	/// <summary>
	/// Save File
	/// </summary>
	/// <typeparam name="T">Model Type</typeparam>
	/// <param name="filename">File Name</param>
	/// <param name="content">Model Content</param>
	public static void Save<T>(string filename, T content)
	{
		string filePath = Path.Combine(Application.persistentDataPath, filename);

		string dataAsJson = JsonUtility.ToJson(content);
		File.WriteAllText(filePath, dataAsJson);

	}
}