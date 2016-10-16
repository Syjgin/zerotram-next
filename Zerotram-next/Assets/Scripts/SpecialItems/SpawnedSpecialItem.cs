using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SpawnedSpecialItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	private bool _isMouseDown;

	Vector3 _startPosition;
	Vector3 _offsetToMouse;
	float _zDistanceToCamera;
	private InventoryScroller _scroller;
	public SpecialItemInfo SpecialItem { get; set; }

	public void SetInventoryScroller(InventoryScroller scroller) {
		_scroller = scroller;
	}

	// Use this for initialization
	void Start () {
	
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		_startPosition = transform.position;
		_zDistanceToCamera = Mathf.Abs (_startPosition.z - Camera.main.transform.position.z);
		_offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
		);
		_scroller.SetCurrentSpawnedItem (gameObject);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(Input.touchCount > 1)
			return;

		transform.position = Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
		) + _offsetToMouse;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		_offsetToMouse = Vector3.zero;
		transform.position = _startPosition;
	}

}
