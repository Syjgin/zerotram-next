using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class PassengerStorage
{

	private const string PassengersKey = "PassengersKey";
	private Dictionary<string, int> _passengers;
	private static PassengerStorage _instance;
	private static char Separator = ';';

	public static PassengerStorage GetStorage ()
	{
		if (_instance == null) {
			_instance = new PassengerStorage ();
		}
		return _instance;
	}

	private PassengerStorage ()
	{
		_passengers = new Dictionary<string, int> ();
		string passengerList = PlayerPrefs.GetString (PassengersKey, "");
		string[] parsedData = passengerList.Split (Separator);
		int size = parsedData.Length;
		string key = "";
		int value = 0;
		for (int i = 0; i < size; i++) {
			bool odd = (i % 2 == 0);
			if (odd) {
				key = parsedData [i];
			} else {
				value = int.Parse (parsedData [i]);
				_passengers.Add (key, value);
			}
		}
	}

	public Dictionary<string, int> GetPassengers ()
	{
		return _passengers;
	}

	public void AddPassenger (string name)
	{
		int count = 1;
		if (!_passengers.TryGetValue (name, out count)) {
			_passengers.Add (name, 1);
		} else {
			count++;
			_passengers [name] = count;
		}
		SyncWithPlayerPrefs ();
	}

	public void RemovePassenger (string name)
	{
		int count = 1;
		if (!_passengers.TryGetValue (name, out count)) {
			return;
		} else {
			count--;
			if (count == 0)
				_passengers.Remove (name);
			else
				_passengers [name] = count;
		}
		SyncWithPlayerPrefs ();
	}

	private void SyncWithPlayerPrefs ()
	{
		string res = "";
		foreach (KeyValuePair<string, int> pair in _passengers) {
			res += pair.Key;
			res += Separator;
			res += pair.Value.ToString ();
			res += Separator;
		}
		PlayerPrefs.SetString (PassengersKey, res);
	}
}
