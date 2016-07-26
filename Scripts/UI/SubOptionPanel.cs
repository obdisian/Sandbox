using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubOptionPanel : MonoBehaviour {

	public static bool openSubOptionPanel;
	public static bool openFirstClearPanel;

	Text childText;

	void Start () {

		openSubOptionPanel = false;
		openFirstClearPanel = false;
		transform.position = Mover.UBPosition (Mover.UiBasePos.Bottom);

		childText = gameObject.transform.GetChild (2).gameObject.GetComponent<Text> ();
	}
	
	void Update () {
		if (SubOptionPanel.openFirstClearPanel) {
			childText.text = Info_StringText.firstPlayCountText [Info_StringText.textLanguage];
		} else {
			childText.text = Info_StringText.playCountText [Info_StringText.textLanguage];
		}

		if (openSubOptionPanel) {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.1f);
		} else {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Bottom), 0.1f);
		}
	}
}
