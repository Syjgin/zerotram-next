using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryDraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler  {

	public SpecialItemInfo SpecialItem { get; set;}
	private Vector2 _startPosition;
	private static Canvas _canvas;
	private Transform _transformParent;
	private static UnityEngine.UI.Text _messageBox;

	void Start() {
		_transformParent = transform.parent;
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		_startPosition = transform.position;
		if(_canvas == null) {
			_canvas = GameObject.Find ("Canvas").GetComponent <Canvas> ();
		}
		transform.SetParent (_canvas.transform, false);
	}
		
	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}
		
	public void OnEndDrag (PointerEventData eventData)
	{
		Vector3 touch = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(touch.x,touch.y), Vector2.zero, Mathf.Infinity);
		if (hit.collider != null) {
			Transform foundObject = hit.transform;
			SpawnedSpecialItem specialItem = foundObject.GetComponent <SpawnedSpecialItem> ();
			if(specialItem == null) {
				EndDragNormally ();
			} else {
				if (_messageBox == null) {
					_messageBox = GameObject.Find ("MessageBox").GetComponent<UnityEngine.UI.Text> ();
				}
				if(specialItem.SpecialItem.InteractWith == SpecialItem.Name) {
					_messageBox.text = StringResources.GetLocalizedString (specialItem.SpecialItem.InteractMessage);
					Inventory.GetInstance ().RemoveItem (SpecialItem.Name);
					Destroy (gameObject);	
					Destroy (specialItem.gameObject);
				} else {
					_messageBox.text = StringResources.GetLocalizedString ("incorrect_items_pair");
					EndDragNormally ();
				}
			}
		} else {
			EndDragNormally ();
		}
	}

	private void HideText() {
		_messageBox.text = "";
	}
		

	void EndDragNormally ()
	{
		transform.SetParent (_transformParent);
		transform.position = _startPosition;
	}
}
