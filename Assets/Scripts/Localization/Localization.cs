using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization
{

	private static Dictionary<string, string> loadedLanguage;

	public static void LoadLanguage(string lang)
	{
		loadedLanguage = new Dictionary<string, string>();
		Debug.Log("Loading language : Lang/" + lang);
		TextAsset langData = Resources.Load<TextAsset>("Lang/" + lang);
		string[] lines = langData.text.Split('\n');
		foreach (string line in lines)
		{
			string[] pair = line.Split(new char[] { '=' }, 2);
			if (pair.Length > 1)
			{
				loadedLanguage[pair[0]] = pair[1];
			}
		}
	}

	public static string Translate(string key)
	{
		if (loadedLanguage.ContainsKey(key))
		{
			return loadedLanguage[key];
		}
		else
		{
			return key;
		}
	}

}
