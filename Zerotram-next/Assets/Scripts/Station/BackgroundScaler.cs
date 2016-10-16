using UnityEngine;
using System.Collections;

public class BackgroundScaler : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		if (renderer == null)
			return;
		string backgroundName = CurrentStation.GetCurrentStationInfo ().Background;
		renderer.sprite = Resources.Load<Sprite> (backgroundName) as Sprite;
		transform.localScale = new Vector3 (1, 1, 1);
		float width = renderer.sprite.bounds.size.x;
		float height = renderer.sprite.bounds.size.y;
		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3 ((float)(worldScreenWidth / width), (float)(worldScreenHeight / height), 1);
	}

}
