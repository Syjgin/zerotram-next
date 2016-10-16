using System;
using System.Collections.Generic;

public class StationInfo
{
	private const string StationBgPrefix = "StationBg/";

	public string Background {get; private set; } 

	public string Foreground {get; private set; } 

	public string Name {get; private set; } 

	public int MaxCharacters {get; private set; } 

	public List<string> PassengerNames {get; private set; } 

	public List<string> SpecialItems {get; private set; } 

	public StationInfo (JSONObject stationInfoObject)
	{
		Name = stationInfoObject.GetField ("name").str;
		Background = StationBgPrefix + stationInfoObject.GetField ("background").str;
		Foreground = StationBgPrefix + stationInfoObject.GetField ("foreground").str;
		MaxCharacters = (int)stationInfoObject.GetField ("maxCharacters").n;
		List<JSONObject> characterNames = stationInfoObject.GetField ("characters").list;
		PassengerNames = new List<string> ();
		foreach(JSONObject character in characterNames) {
			PassengerNames.Add (character.GetField ("name").str);
		}
		List<JSONObject> specials = stationInfoObject.GetField ("specialItems").list;
		SpecialItems = new List<string> ();
		foreach(JSONObject special in specials) {
			SpecialItems.Add (special.GetField ("name").str);
		}
	}
}

