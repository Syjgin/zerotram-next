using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageHider : MonoBehaviour
{
	[SerializeField]
	Text _messageField;
	private float _messageTTL;
	private bool _timerActivated;
	private const float MaxTTL = 2;

	void Update ()
	{
		if (!_timerActivated) {
			if (_messageField.text != "") {
				_timerActivated = true;
				_messageTTL = MaxTTL;
			}
		} else {
			_messageTTL -= Time.deltaTime;
			if (_messageTTL < 0) {
				_timerActivated = false;
				_messageField.text = "";
			}
		}
	}
}
