using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryScroller : MonoBehaviour
{

	private RectTransform _rect;
	private GameObject _currentSpawnedItem;

	public bool isMouseOver ()
	{
		Vector2 mousePosition = Input.mousePosition;
		Vector3[] worldCorners = new Vector3[4];
		_rect.GetWorldCorners (worldCorners);

		if (mousePosition.x >= worldCorners [0].x && mousePosition.x < worldCorners [2].x
		    && mousePosition.y >= worldCorners [0].y && mousePosition.y < worldCorners [2].y) {
			return true;
		}
		return false;
	}

	[SerializeField]
	private GameObject imageSpawn;

	// Use this for initialization
	void Start ()
	{
		//PlayerPrefs.DeleteAll ();
		_rect = GetComponent <RectTransform> ();
		int i = 0;
		foreach (SpecialItemInfo info in Inventory.GetInstance ().GetItems ()) {
			addFromItemInfo (info, (Screen.width * -0.5f) + 100 * i);
			i++;
		}
	}

	public void SetCurrentSpawnedItem (GameObject item)
	{
		_currentSpawnedItem = item;
	}

	void Update ()
	{
		if (_rect != null) {
			
			if (isMouseOver ()) {
				if (_currentSpawnedItem != null) {
					SpawnedSpecialItem spawnedItem = _currentSpawnedItem.GetComponent<SpawnedSpecialItem> ();
					if (Inventory.GetInstance ().AddItem (spawnedItem.SpecialItem)) {
						addFromItemInfo (spawnedItem.SpecialItem, 100 * (Inventory.GetInstance ().GetItems ().Count - 1));
						Destroy (_currentSpawnedItem);	
					} else {
						spawnedItem.OnEndDrag (null);
					}
				}
			}
		}
	}

	private void addFromItemInfo (SpecialItemInfo info, float offset)
	{
		GameObject newImageObject = Instantiate (imageSpawn);
		Image img = newImageObject.GetComponent <Image> ();
		Sprite loadedSprite = Resources.Load<Sprite> (SpecialItemInfo.ItemBgPrefix + info.Name) as Sprite;
		img.sprite = loadedSprite;
		newImageObject.GetComponent<Image> ().transform.SetParent (GetComponent<ScrollRect> ().content, false);
		newImageObject.GetComponent <RectTransform> ().transform.localPosition = new Vector2 (offset + 50, -50);
		newImageObject.GetComponent <InventoryDraggableImage> ().SpecialItem = info;
	}
}
