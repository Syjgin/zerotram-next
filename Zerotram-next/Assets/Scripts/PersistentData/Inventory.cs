using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

	private static Inventory _instance;

	private List<SpecialItemInfo> _items;

	const string KeyName = "inventoryItems";

	public static Inventory GetInstance() {
		if(_instance == null) {
			_instance = new Inventory ();
		}
		return _instance;
	}

	private Inventory() {
		InitItems ();
	}

	public List<SpecialItemInfo> GetItems() {
		if(_items == null) {
			InitItems ();	
		}
		return _items;
	}

	private void InitItems() {
		_items = new List<SpecialItemInfo> ();
		string namesString = PlayerPrefs.GetString (KeyName, string.Empty);
		if(namesString != string.Empty) {
			string[] names = PlayerPrefs.GetString (KeyName).Split (',');
			foreach(string name in names) {
				if(name != string.Empty ) {
					SpecialItemInfo info = new SpecialItemInfo (name);
					_items.Add (info);	
				}
			}
		}
	}

	public bool AddItem(SpecialItemInfo info) {
		foreach(SpecialItemInfo currentItem in _items) {
			if (currentItem.Name.Equals (info.Name))
				return false;
		}
		_items.Add (info);
		RefreshItemNames ();
		return true;
	}

	public void RemoveItem(string name) {
		for(int i = 0; i < _items.Count; i++) {
			SpecialItemInfo info = _items [i];
			if(info.Name == name) {
				_items.RemoveAt (i);
				break;
			}
		}
		RefreshItemNames ();
	}

	private void RefreshItemNames() {
		if (_items == null)
			return;
		string resultNames = "";
		foreach(SpecialItemInfo info in _items) {
			resultNames += info.Name + ",";
		}
		PlayerPrefs.SetString (KeyName, resultNames);
	}
}
