using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StationForeground : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> _itemPrefabs;
	[SerializeField]
	private InventoryScroller _scroller;

	// Use this for initialization
	void Start ()
	{
		SpriteRenderer renderer = GetComponent <SpriteRenderer> ();
		if (renderer == null)
			return;
		string foregroundName = CurrentStation.GetCurrentStationInfo ().Foreground;
		renderer.sprite = Resources.Load<Sprite> (foregroundName) as Sprite;
		List<string> itemNames = CurrentStation.GetCurrentStationInfo ().SpecialItems;
		foreach (string itemName in itemNames) {
			SpecialItemInfo itemInfo = new SpecialItemInfo (itemName);
			if (Randomizer.GetPercentageBasedBoolean (itemInfo.Probability)) {
				GameObject prefab = _itemPrefabs.Find (gameObject => {
					return gameObject.name == itemName;
				});
				if (prefab != null) {
					GameObject instantiated = (GameObject)Instantiate (prefab, transform);	
					float x = Random.Range (transform.position.x - (renderer.bounds.size.x * 0.5f), transform.position.x + (renderer.bounds.size.x * 0.5f));
					float y = Random.Range (transform.position.y - (renderer.bounds.size.y * 0.5f), transform.position.y + (renderer.bounds.size.y * 0.5f));
					Vector3 position = new Vector3 (x, y, -1);
					instantiated.transform.position = position;
					instantiated.GetComponent<SpawnedSpecialItem> ().SetInventoryScroller (_scroller);
					instantiated.GetComponent<SpawnedSpecialItem> ().SpecialItem = itemInfo;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
