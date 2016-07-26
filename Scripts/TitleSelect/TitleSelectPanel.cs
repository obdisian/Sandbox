using UnityEngine;
using System.Collections;

public class TitleSelectPanel : MonoBehaviour {

	public enum Panel { Title, Select_1, Select_2, Select_3,/* Select_4, Select_5,*/ End, }
	Panel panel;

	void Start () {

		//	とりあえず、初期設定
		switch (gameObject.name) {
		case "TitlePanel": panel = Panel.Title; transform.position = Mover.UBPosition (Mover.UiBasePos.Middle); break;
		case "SelectPanel_1": panel = Panel.Select_1; transform.position = Mover.UBPosition (Mover.UiBasePos.Bottom); break;
		case "SelectPanel_2": panel = Panel.Select_2; transform.position = Mover.UBPosition (Mover.UiBasePos.Bottom); break;
		case "SelectPanel_3": panel = Panel.Select_3; transform.position = Mover.UBPosition (Mover.UiBasePos.Bottom); break;
		}

		if (panel == TitleSelectProgression.nowPanel) {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Middle);
		} else if (panel < TitleSelectProgression.nowPanel) {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Top);
		} else if (panel > TitleSelectProgression.nowPanel) {
			transform.position = Mover.UBPosition (Mover.UiBasePos.Bottom);
		}
	}
	
	void Update () {

		//	初回起動時のスクロールの動き用
		if (TitleSelectProgression.isModeStart && TitleSelectProgression.nowPanel == Panel.Select_3) {
			if (panel == Panel.Select_3) {
				transform.position = Mover.UBPosition (Mover.UiBasePos.Middle);
			} else if (panel == Panel.Title) {
				transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Top), 0.1f);
			} else {
				transform.position = Mover.UBPosition (Mover.UiBasePos.Top);
			}
		}


		if (panel == Panel.Title) {
			return;
		}

		if (panel == TitleSelectProgression.nowPanel) {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.1f);
		} else if (panel < TitleSelectProgression.nowPanel) {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Top), 0.1f);
		} else if (panel > TitleSelectProgression.nowPanel) {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Bottom), 0.1f);
		}
	}
}
