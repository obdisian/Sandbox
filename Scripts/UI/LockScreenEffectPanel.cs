using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LockScreenEffectPanel : MonoBehaviour {

	public static bool isOpen_LockScreenEffectPanel;

	GameObject childPanel;

	void Start () {
		isOpen_LockScreenEffectPanel = false;
		transform.position = Mover.UBPosition (Mover.UiBasePos.Right);

		childPanel = GameObject.Find ("LockScreenEffectPanel");
		childPanel.transform.GetChild (0).GetChild (1).GetComponent<Text> ().text = Info_StringText.allStageClearText [Info_StringText.textLanguage];

		transform.position = Mover.UBPosition (Mover.UiBasePos.Right);
		childPanel.transform.position = Mover.UBPosition (Mover.UiBasePos.Right);
	}

	void Update () {
		if (isOpen_LockScreenEffectPanel) {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Middle);
			childPanel.transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.1f);
		} else {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Right);
			childPanel.transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Right), 0.1f);
		}
	}

	public void CloseButton () {
		isOpen_LockScreenEffectPanel = false;
	}
}
