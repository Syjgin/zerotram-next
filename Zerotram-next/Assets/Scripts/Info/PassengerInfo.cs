using UnityEngine;
using System.Collections;

public class PassengerInfo
{

	public string PrefabName { get; private set; }

	public int StationCount { get; private set; }

	public int HP { get; private set; }

	public int Velocity { get; private set; }

	public int Attack { get; private set; }

	public int Vision { get; private set; }

	public int Initiative { get; private set; }

	public int Accuracy { get; private set; }

	public int Range { get; private set; }

	public int Bravery { get; private set; }

	public int Agility { get; private set; }

	public PassengerInfo (string name)
	{
		JSONObject config = ConfigReader.GetConfig ();
		string filteredName = name.Replace ("(Clone)", "");
		JSONObject json = config.GetField ("characters").GetField (filteredName);
		PrefabName = json.GetField ("prefabName").str;
		StationCount = (int)json.GetField ("stationCount").n;
		HP = (int)json.GetField ("hp").n;
		Velocity = (int)json.GetField ("velocity").n;
		Attack = (int)json.GetField ("attack").n;
		Vision = (int)json.GetField ("vision").n;
		Initiative = (int)json.GetField ("initiative").n;
		Accuracy = (int)json.GetField ("accuracy").n;
		Range = (int)json.GetField ("range").n;
		Bravery = (int)json.GetField ("bravery").n;
		Agility = (int)json.GetField ("agility").n;
	}


}
