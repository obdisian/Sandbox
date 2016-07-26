using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {

	int nowBGM = 0;

	void Start () {

		for (int i = 0; i < 3; i++) {
			Audio.LoadBgm ("BGM_" + i, "BGM/SND_BGM_Stage" + (i + 1));
		}

		for (int i = 0; i < 10; i++) {
			Audio.LoadSe ("SE_" + i, "SE/SND_SE0" + i);
		}
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			Info.ShotSE (Info.SEName.Jump);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			Info.ShotSE (Info.SEName.Crush);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			Info.ShotSE (Info.SEName.Goal);
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			Info.ShotSE (Info.SEName.Rejump);
		}


		if (Input.GetKeyDown (KeyCode.Space)) {
			nowBGM++;
			if (nowBGM >= 3) {
				nowBGM = 0;
			}

			if (nowBGM == 0) {
				Info.PlayBGM (Info.BGMName.B1);
			}
			if (nowBGM == 1) {
				Info.PlayBGM (Info.BGMName.B2);
			}
			if (nowBGM == 2) {
				Info.PlayBGM (Info.BGMName.B3);
			}
		}
	}
}
