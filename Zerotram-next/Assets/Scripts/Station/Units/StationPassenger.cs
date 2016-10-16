using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StationPassenger : MonoBehaviour
{
	private Vector3 _target;
	private int _velocity;
	private const float MinDistance = 0.1f;
	private static TramAnimator _tram;
	private bool _moveStarted;
	private PassengerInfo _info;

	void Start ()
	{
		if (_tram == null) {
			_tram = GameObject.Find ("tram").GetComponent<TramAnimator> ();
		}
		_tram.onDoorsOpened += StartMove;
		if (_tram.IsDoorsOpened ()) {
			StartMove ();
		}
	}

	private void StartMove ()
	{
		int rightDoor = Randomizer.GetInRange (0, 2);
		if (rightDoor == 0) {
			_target = GameObject.Find ("door1").transform.position;
		} else {
			_target = GameObject.Find ("door2").transform.position;
		}
		_info = new PassengerInfo (name);
		_velocity = _info.Velocity;
		_moveStarted = true;
	}

	void Update ()
	{
		if (!_moveStarted)
			return;
		float step = _velocity * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, _target, step);
		if (Vector3.Distance (transform.position, _target) < MinDistance) {
			transform.parent.GetComponent<Spawner> ().TryDisplayClickMessage (_tram);
			PassengerStorage.GetStorage ().AddPassenger (_info.PrefabName);
			Destroy (gameObject);
		}
	}
}
