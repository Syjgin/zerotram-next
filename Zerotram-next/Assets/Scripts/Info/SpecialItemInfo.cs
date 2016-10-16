using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialItemInfo {

	public const string ItemBgPrefix = "Items/";

	public string InteractMessage {get; private set; } 

	public bool CanTake {get; private set; } 

	public string Name {get; private set; } 

	public string InteractWith {get; private set; } 

	public int Probability {get; private set; } 

	public SpecialItemInfo (string itemName)
	{
		JSONObject config = ConfigReader.GetConfig ();
		List<JSONObject> infoObjects = config.GetField ("specialItems").list;
		foreach(JSONObject infoObject in infoObjects) {
			if(infoObject.GetField ("name").str == itemName) {
				Name = infoObject.GetField ("name").str;
				InteractMessage = infoObject.GetField ("interactMessage").str;
				CanTake = infoObject.GetField ("canTake").b;
				InteractWith = infoObject.GetField ("interactWith").str;
				Probability = (int)infoObject.GetField ("probability").n;		
			}	
		}
	}
}
