using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TramAnimator : MonoBehaviour
{

	[SerializeField]
	private GameObject _leftDoor;
	[SerializeField]
	private GameObject _rightDoor;
	[SerializeField]
	private GameObject _clickCaption;

	private const float _speed = 5;
	private bool _moveToStationActivated;
	private Vector3 CenterPoint = new Vector3 (0, 2.5f, 0);
	private const float MinDistance = 0.1f;
	private const float DoorDistance = 1.7f;
	private bool _isDoorsOpened;
	private bool _allPassengersInside;

	public delegate void DoorsOpened ();

	public event DoorsOpened onDoorsOpened;

	void Update ()
	{
		if (!_moveToStationActivated)
			return;
		float step = _speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, CenterPoint, step);
		if (Vector3.Distance (transform.position, CenterPoint) < MinDistance) {
			_moveToStationActivated = false;
			OpenDoors ();
		}
	}

	// Use this for initialization
	void Start ()
	{
		_moveToStationActivated = true;
	}

	public bool IsDoorsOpened ()
	{
		return _isDoorsOpened;
	}

	private void OpenDoors ()
	{
		_leftDoor.transform.localPosition = new Vector3 (-DoorDistance, _leftDoor.transform.localPosition.y, 0);
		_rightDoor.transform.localPosition = new Vector3 (DoorDistance, _rightDoor.transform.localPosition.y, 0);
		onDoorsOpened ();
		_isDoorsOpened = true;
	}

	public void DisplayClickCaption ()
	{
		_clickCaption.SetActive (true);
		_allPassengersInside = true;
	}

	public void OnMouseDown ()
	{
		if (_allPassengersInside) {
			SceneManager.LoadSceneAsync ("insidetram");
		}
	}
}
