using UnityEngine;
using System.Collections;

public class KeyPanel : MonoBehaviour {

	void Start () {
		transform.position = Mover.UBPosition (Mover.UiBasePos.Right);
	}
	
	void Update () {

		transform.position = Mover.UBPosition (Mover.UiBasePos.Right);

		if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_2) {
			if (TitleSelectProgression.lock_Stage2) {
				transform.position = Mover.UBPosition (Mover.UiBasePos.Middle);
			}
		}
		else if (TitleSelectProgression.nowPanel == TitleSelectPanel.Panel.Select_3) {
			if (TitleSelectProgression.lock_Stage3) {
				transform.position = Mover.UBPosition (Mover.UiBasePos.Middle);
			}
		}
	}
}
