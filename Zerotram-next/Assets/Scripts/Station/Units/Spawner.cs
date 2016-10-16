using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;

public class Spawner : MonoBehaviour
{
	[SerializeField]
	List<GameObject> _unitPrefabs;
	[SerializeField]
	SpriteRenderer _renderer;

	private int _remainingCount;
	private List<string> _passengerNames;
	private float _timeSincePreviousSpawn = 0;
	private const float SpawnPeriod = 3;
	private int _currentPassengersCount;
	private int _passengersInsideTramCount = 0;

	void Start ()
	{
		_passengerNames = CurrentStation.GetCurrentStationInfo ().PassengerNames;
		_currentPassengersCount = _remainingCount = Randomizer.GetInRange (1, CurrentStation.GetCurrentStationInfo ().MaxCharacters);

	}

	public void TryDisplayClickMessage (TramAnimator tram)
	{
		_passengersInsideTramCount++;
		if (_passengersInsideTramCount == _currentPassengersCount) {
			tram.DisplayClickCaption ();
		}
	}

	void Update ()
	{
		if (_remainingCount == 0)
			return;
		_timeSincePreviousSpawn += Time.deltaTime;
		if (_timeSincePreviousSpawn > SpawnPeriod) {
			_remainingCount--;
			int randomIndex = Randomizer.GetInRange (0, _passengerNames.Count);
			string selectedName = _passengerNames [randomIndex];
			foreach (GameObject go in _unitPrefabs) {
				if (go.name == selectedName) {
					GameObject instantiated = Instantiate (go, transform) as GameObject;
					float x = Random.Range (transform.position.x - (_renderer.bounds.size.x * 0.5f), transform.position.x + (_renderer.bounds.size.x * 0.5f));
					float y = Random.Range (transform.position.y - (_renderer.bounds.size.y * 0.5f), transform.position.y + (_renderer.bounds.size.y * 0.5f));
					Vector3 position = new Vector3 (x, y, -10);
					instantiated.transform.position = position;
				}
			}
			_timeSincePreviousSpawn = 0;
		}
	}
}
