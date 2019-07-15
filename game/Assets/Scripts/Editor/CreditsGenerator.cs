using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class CreditsGenerator {
	
	private struct CreditPerson {
		public string Name;
		public string Job;
		public string Specifically;
		public string Website;
		public string License;

		public CreditPerson(string name, string job, string specifically, string website, string license) {
			Name = name;
			Job = job;
			Specifically = specifically;
			Website = website;
			License = license;
		}

		public string ToString() {
			return $"{Name} - {Job}\n\tResponsible for: {Specifically}\n\tWebsite: {Website}\n\tLicense: {License}";
		}
	}

	[MenuItem("Tools/Generate credits")]
	public static void Generate() {

		List<CreditPerson> credits = new List<CreditPerson>();
		
		// Default credits
		credits.Add(new CreditPerson("Sascha de Waal", "Programmer, Design", "Created this game", "https://www.developersascha.nl/", "Personal"));
		credits.Add(new CreditPerson("Gert-Jan van den boom", "Artist", "Created the assembly line", "https://www.fluffstudios.nl/", "Personal"));
		
		// Music
		string[] objects = AssetDatabase.GetAllAssetPaths();
		string[] extensions = new[] {".wav", ".ogg", ".aiff"};
		
		foreach (string objName in objects) {
			string fileName = Path.GetFileNameWithoutExtension(objName).ToLower();
			string extension = Path.GetExtension(objName).ToLower();
			
			if (extensions.Contains(extension)) {
				string[] data = fileName.Split('_');
				string website = $"https://freesound.org/people/{data[2]}/sounds/{ data[0]}";
				credits.Add(new CreditPerson(data[2], "Music artist", $"Created the sound {data[4]}", website, "Creative commons. See website for more information"));
			}
		}

		string[] lines = credits.ConvertAll<string>(c => $"{c.ToString()}\n\n").ToArray();
		
		System.IO.File.WriteAllLines($"{Application.dataPath}/Credits.txt", lines);
		AssetDatabase.Refresh();
		
		Debug.Log($"Created credit file at {Application.dataPath}/Credits.txt");

	}
	
	public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
	{
		List<T> assets = new List<T>();
		string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
		for( int i = 0; i < guids.Length; i++ )
		{
			string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
			T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
			if( asset != null )
			{
				assets.Add(asset);
			}
		}
		return assets;
	}
	
}
