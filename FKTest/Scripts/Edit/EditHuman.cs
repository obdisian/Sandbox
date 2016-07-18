using UnityEngine;
using System.Collections;

public class EditHuman : Human {

	public bool isPrevPlay;

	public EditManager editManager;


	new void Start () {
		base.Start ();

		motionList [0] = editManager.ToMotionData ();
	}
	
	new void Update () {

		if (editManager.isPlay != isPrevPlay) {
			isPrevPlay = editManager.isPlay;

			Data.Save ("テスト", editManager.ToMotionData ());

			//	編集中のデータをモーションリストに登録
			motionList [0] = Data.Load ("テスト");

			Info.MotionDataLog (motionList [0]);
		}

		if (editManager.isPlay) {
			base.Update ();
		}
		else {
			for (int i = 0; i < humanJoints.Length; i++) {
				if (editManager.joint == humanJoints [i]) {
					editManager.nowJointNum = i;
					break;
				}
//				human.EditUpdateFK ();
			}
		}
	}
}
