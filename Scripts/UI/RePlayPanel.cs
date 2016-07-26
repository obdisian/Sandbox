using UnityEngine;
using System.Collections;

public class RePlayPanel : MonoBehaviour {

	Vector2 size;
	public static bool isOpenReplayPanel;

	void Start () {
		isOpenReplayPanel = false;

		if (gameObject.name == "RePlayPanel") {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Right);
		}
		else if (gameObject.name == "RePlayBackPanel") {
			size = transform.localScale;
			transform.localScale = Vector2.zero;
		}
	}
	
	void Update () {
		if (gameObject.name == "RePlayPanel") {
			if (isOpenReplayPanel) {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.1f);
			} else {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Right), 0.1f);
			}
		}
		else if (gameObject.name == "RePlayBackPanel") {
			if (isOpenReplayPanel) {
				transform.localScale = size;
			} else {
				transform.localScale = Vector2.zero;
			}
		}
	}

	public void Replay_ () {
	}
	public void Replay_Ads () {
	}
	public void Replay_BackPanel () {
		isOpenReplayPanel = false;
	}
}
