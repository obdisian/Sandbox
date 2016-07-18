using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditManager : MonoBehaviour {

	public EditAxis posAxis, rotAxis;

	public HumanFK joint, prevJoint;




	public bool isPlay;

	//	現在編集中キーの番号
	public int nowEditNum = 0, prevnowEditNum;

	//	現在選択中の関節
	public int nowJointNum = 0;

	//++++++++++++++++++++++++++++++++++++++++
	//	編集中データ
	//++++++++++++++++++++++++++++++++++++++++
	public bool edit_IsLoop;
	public int edit_MaxFrame;
	public List<int> edit_KeyFrame = new List<int> ();
	public List<float> edit_SlerpT = new List<float> ();
	public List<Vector3> edit_Pos = new List<Vector3> ();
	public List<Vector3[]> edit_Angles = new List<Vector3[]> ();




	//================================================================================
	//	初期化
	//================================================================================
	void Start () {

		posAxis.gameObject.SetActive(false);
		rotAxis.gameObject.SetActive(false);
	}
	

	//================================================================================
	//	毎フレーム更新
	//================================================================================
	void LateUpdate () {

		if (Input.GetMouseButtonDown (0)) {

			GameObject obj = InputController.MouseHitObject;

			//	nullの場合
			if (!InputController.MouseHitObject) {
				return;
			}
			//	キャラクターじゃない場合
			if (!obj.GetComponent<PartsFK> ()) {
				//	地面の場合はアクシスを消す
				if (obj.name == "Ground") {
					rotAxis.gameObject.SetActive (false);
					SelectColor (prevJoint, Color.white);
				}
				return;
			}

			joint = obj.GetComponent<HumanFK> ();

			//	もし関節なら子パーツの色も変える
			if (joint) {
				SelectColor (joint, Color.cyan);
				RotAxisEnable ();
			}
			//	もしパーツならその親の関節を指定して色を変える
			else {
				joint = obj.GetComponent<FK> ().Owner.GetComponent<HumanFK> ();
				SelectColor (joint, Color.cyan);
				RotAxisEnable ();
			}

			//	前回と違う関節の場合
			if (joint != prevJoint) {
				if (prevJoint) {
					SelectColor (prevJoint, Color.white);
				}

				prevJoint = joint;
			}
		}


		if (isPlay) {
			SelectColor (prevJoint, Color.white);
			rotAxis.gameObject.SetActive (false);
		}
		else if (rotAxis.gameObject.activeSelf) {

			joint.Angle = new Vector3 (rotAxis.AxisAngleX, rotAxis.AxisAngleY, rotAxis.AxisAngleZ);

			edit_Angles [nowEditNum][nowJointNum] = joint.Angle;
		}
	}


	//================================================================================
	//	選択時の色変え
	//================================================================================
	void SelectColor (HumanFK hFK, Color color) {
		if (hFK) {
			hFK.GetComponent<Renderer> ().material.color = color;
			hFK.Parts.GetComponent<Renderer> ().material.color = color;
		}
	}


	//================================================================================
	//	回転軸出す
	//================================================================================
	void RotAxisEnable () {
		
		rotAxis.gameObject.SetActive (true);

		rotAxis.transform.position = joint.transform.position;
//		rotAxis.transform.rotation = joint.transform.rotation;

		rotAxis.AxisAngleX = joint.BaseAngle.x;
		rotAxis.AxisAngleY = joint.BaseAngle.y;
		rotAxis.AxisAngleZ = joint.BaseAngle.z;
	}


	//================================================================================
	//	編集中のデータをモーションデータに変換
	//================================================================================
	public MotionData ToMotionData () {

		MotionData motData = new MotionData ();

		motData.IsLoop = edit_IsLoop;
		motData.MaxFrame = edit_MaxFrame;
		motData.KeyFrame = edit_KeyFrame.ToArray ();
		motData.SlerpT = edit_SlerpT.ToArray ();
		motData.Pos = edit_Pos.ToArray ();
		motData.Angles = edit_Angles.ToArray ();

		return motData;
	}

}
