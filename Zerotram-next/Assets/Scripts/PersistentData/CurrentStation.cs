using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CurrentStation
{
	
	private const string StationKey = "StationKey";
	private static StationInfo currentStationInfo;
	private static Object lockObject;

	public static StationInfo GetCurrentStationInfo ()
	{
		if (currentStationInfo == null) {
			string savedName = PlayerPrefs.GetString (StationKey, "");
			RefreshStationInfo (savedName);
		}
		return currentStationInfo;
	}

	public static void SetCurrentStationName (string name)
	{
		PlayerPrefs.SetString (StationKey, name);
		RefreshStationInfo (name);
	}

	private static void RefreshStationInfo (string name)
	{
		if (lockObject == null)
			lockObject = new Object ();
		lock (lockObject) {
			JSONObject config = ConfigReader.GetConfig ();
			List<JSONObject> stations = config.GetField ("stations").list;
			int randIndex = Randomizer.GetInRange (0, stations.Count);
			currentStationInfo = new StationInfo (stations [randIndex]);
		}
	}
}
